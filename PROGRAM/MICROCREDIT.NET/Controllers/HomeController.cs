using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace MICROCREDIT.NET.Controllers
{
    public class HomeController : Controller
    {
        // общее подключение к БД
        MICROCREDITEntities db = new MICROCREDITEntities();
        DATAEntities dbData = new DATAEntities();

        // был поиск, устанавливается,
        // если после открытия формы
        // был произведён поиск
        bool bBilPoisk;
        bool bObnovlReestr;
        // true, если запуск в режиме "Поиск по совпадению"
        bool bPoiskPoSovp;

        // Для режима "Поиск по совпадению" аттрибуты адреса
        // для поиска (передается из анкеты)
        // идентификатор человека, по которому ищем совпадение,
        // для исключения его из поиска
        int iPoiskPoSovpCounter0Fio;
        int iPoiskPoSovpLpuinFio;
        // жительства
        int iPoiskPoSovpStreetR;
        int iPoiskPoSovpHouseR;
        int iPoiskPoSovpFlatR;
        // регистрации
        int iPoiskPoSovpStreetJ;
        int iPoiskPoSovpHouseJ;
        int iPoiskPoSovpFlatJ;
        // телефон по месту регистрации
        string sPoiskPoSovpMedicalPhoneHomeR;
        // телефон по месту жительства
        string sPoiskPoSovpMedicalPhoneHomeJ;
        // телефон мобильный
        string sPoiskPoSovpMedicalPhoneMobile;
        // контактные лица
        // массив домашних телефонов контактных лиц
        string[] arsRelativePhoneHome = new string[0];
        // массив мобильных телефонов контактных лиц
        string[] arsRelativePhoneMobile = new string[0];
        // массив ФИО контактных лиц
        string[] arsRelativeFIO = new string[0];
        // телефон ОК/секретаря по месту работы
        string sPoiskPoSovpWorkPhoneSecretary;
        // личный рабочий телефон
        string sPoiskPoSovpWorkPhoneWork;

        // заглушка на время
        TErrorListForm ErrorListForm = new TErrorListForm();
        TReestrKlientovForm ReestrKlientovForm = new TReestrKlientovForm();
        TFormWaiting formWaiting = new TFormWaiting();
        TEdit edFio = new TEdit();
        TEdit edPhone = new TEdit();
        int iUSERRole = MyConst.iRoleMenedjer;
        TPanel pnlInfo = new TPanel();
        TDBGridEh gridFio = new TDBGridEh();
        int FCounter0Fio = 131707;
        int FLpuinFio = 103408001;
        int FCounterFio = 131707;



        // работать будет так, если есть строка
        //  "какой-то текст /*<tagName>*/эта_строка_заменится/*</tagName>*/ ещё какой-то текст"
        // и в качестве параметра замены будет передано "новая_строка", то будет
        //  "какой-то текст /*<tagName>*/новая_строка/*</tagName>*/ ещё какой-то текст"
        // WithQuotes - вставить кавычки в значение, т.е. ""значение"", а если false, то "значение"
        // RemoveTag - удалить теги /*<tagName>*//*</tagName>*/, после замены, если true
        public string ReplaceTaggedParameter(string stringToReplace, string tagName
          ,string replaceString, bool withQuotes = false, bool removeTag = false)
        {

          // ?s - чтобы по нескольким строкам искать, а не только на одной строке
          Regex r = new Regex("(?is)\\/\\*<" + tagName + ">\\*\\/(.*?)\\/\\*</" + tagName + ">\\*\\/");
          string s;

          if (withQuotes) {
            s = "'" + replaceString + "'";
          } else {
            s = replaceString;
          }

          if (!removeTag) {
            s = "/*<" + tagName + ">*/" + s + "/*</" + tagName + ">*/";
          }

          return r.Replace(stringToReplace, s);

        }

        // получить тело хранимой процедуры
        // sDBName - имя БД, в которой создана процедура
        // sSPName - имя хранимой процедуры,
        // тело которой необходимо получить
        // bOnlyBody - True, если необходимо получить только
        // тело процедуры, False, если необходимо получить с
        // заголовочной частью (create, alter..)
        public string GetSPBody(string sDBName, string sSPName, bool bOnlyBody = true) {
          const string TAG_NAME_BEGIN_PROC = "BEGIN_PROC";
          const string TAG_NAME_END_PROC = "END_PROC";

          string s;
          s = db.Database.SqlQuery<string>(String.Format(
                "select                                         "
              + "	object_definition(o.[object_id]) [DATA_SQL]   "
              + "from                                           "
              + "	sys.objects as o                              "
              + "where                                          "
              + "	o.name = '{0}'   "
            ,sSPName
          )).FirstOrDefault<string>();

          if (String.IsNullOrEmpty(s)) {
            throw new Exception(String.Format("Процедура {0} не найдена в БД {1}!", sSPName, sDBName));
          }

          if (bOnlyBody) {
            // проверяю, что в процедуре есть теги,
            // отмечающие её начало и окончание
            if (
                 (s.IndexOf(TAG_NAME_BEGIN_PROC) == -1)
              || (s.IndexOf(TAG_NAME_END_PROC) == -1))
            {
              throw new Exception(String.Format("В хранимой процедуре {0} не найдены "
                + "теги отмечающие её начало и конец (BEGIN_PROC, END_PROC)!", sSPName));
            }

            // удаляю заголовок процедуры
            s = ReplaceTaggedParameter(
                s
              ,TAG_NAME_BEGIN_PROC
              ,""
            );

            // удаляю end процедуры
            s = ReplaceTaggedParameter(
                s
              ,TAG_NAME_END_PROC
              ,""
            );
          
          } // if bOnlyBody

          return s;

        }




        public ActionResult RequeryFio(int iCounterFio = -1) {
          edFio.Text = "АБРАМОВ";
          const string TAG_NAME_STAT = "STAT";
          const string TAG_NAME_FIO = "FIO";
          const string TAG_NAME_KOD_OTDELENIYA = "KOD_OTDELENIYA";
          const string TAG_NAME_OPERATOR_START = "OPERATOR_START";
          const string TAG_NAME_TEK_DEN = "TEK_DEN";
          const string TAG_NAME_TOP = "TOP";
          const string TAG_NAME_COUNTER_FIO = "COUNTER_FIO";
          const string TAG_NAME_NE_ZAKR = "NE_ZAKR";
          const string TAG_NAME_POSL = "POSL";
          const string TAG_NAME_EST_ZAYM = "EST_ZAYM";
          const string TAG_NAME_PHONE_MOBILE = "PHONE_MOBILE";
          const string TAG_NAME_PHONE_RELATIVE = "PHONE_RELATIVE";
          const string TAG_NAME_POISK_PO_SOVPADENIYU_COUNTER0_FIO = "POISK_PO_SOVPADENIYU_COUNTER0_FIO";
          const string TAG_NAME_POISK_PO_SOVPADENIYU_LPUIN_FIO = "POISK_PO_SOVPADENIYU_LPUIN_FIO";
          const string TAG_NAME_POISK_PO_SOVPADENIYU_ADDR1 = "POISK_PO_SOVPADENIYU_ADDR1";
          const string TAG_NAME_POISK_PO_SOVPADENIYU_ADDR2 = "POISK_PO_SOVPADENIYU_ADDR2";
          const string TAG_NAME_POISK_PO_SOVPADENIYU_PHONE = "POISK_PO_SOVPADENIYU_PHONE";

          const string TAG_NAME_POISK_FIO ="POISK_FIO";
          const string TAG_NAME_POISK_PHONE ="POISK_PHONE";
          const string TAG_NAME_POISK_PUST ="POISK_PUST";
          const string TAG_NAME_POISK_COUNTER_FIO ="POISK_COUNTER_FIO";
          const string TAG_NAME_PHONE ="PHONE";
          const string TAG_NAME_POISK_PO_SOVPADENIYU ="POISK_PO_SOVPADENIYU";

            // МИНИМАЛЬНОЕ КОЛИЧЕСТВО СИМВОЛОВ ДЛЯ ПОИСКА ПО ФИО
          const int MIN_KOLVO_SIM_FIO = 3;
            // МИНИМАЛЬНОЕ КОЛИЧЕСТВО СИМВОЛОВ ДЛЯ ПОИСКА ПО ТЕЛЕФОНУ
          const int MIN_KOLVO_SIM_PHONE = 5;

            //   алгоритм поиска
            // по телефону регистрации
          const int ALG_PHONE_HOME_R = 3;
            // по телефону жительства
          const int ALG_PHONE_HOME_J = 4;
            // по мобильному телефону
          const int ALG_PHONE_MOBILE = 5;
            // по домашнему телефону контактного лица
          const int ALG_RELATIVE_PHONE_HOME = 6;
            // по мобильному телефону контактного лица
          const int ALG_RELATIVE_PHONE_MOBILE = 7;
            // по телефону ОК/секретаря
          const int ALG_WORK_PHONE_SECRETARY = 8;
            // по личному рабочему телефону
          const int ALG_WORK_PHONE_WORK = 9;
        
          string s;
          string s1;
          int i;

          string sStat;

          // удаляю лишние пробелы
          edFio.Text = edFio.Text.Trim();
          edFio.Text.Replace("  ", " ");
          edFio.Text.Replace("  ", " ");
          edFio.Text.Replace("  ", " ");
          edFio.Text.Replace("  ", " ");
          edFio.Text.Replace("  ", " ");
          edFio.Text.Replace("  ", " ");
          
          //edFio.SelStart := Length(edFIO.Text);
  

          // если поиск по ФИО и меньше MIN_KOLVO_SIM_FIO,
          // то сообщение и выход
          if (
                (!String.IsNullOrEmpty(edFio.Text) )
            && (edFio.Text.Length < MIN_KOLVO_SIM_FIO))
          {
            ErrorListForm.ClearErrors();
            ErrorListForm.AddError(edFio, String.Format(
               "Минимальное количество символов для поиска: {0}!"
              ,MIN_KOLVO_SIM_FIO
              )
            );
            ReestrKlientovForm.BringToFront();
            return Json(new {error = "error"}, JsonRequestBehavior.AllowGet);

          } else {
            ErrorListForm.RemoveError(edFio, String.Format(
               "Минимальное количество символов для поиска: {0}!"
              ,MIN_KOLVO_SIM_FIO
              )
            );
          }


          // если поиск по телефону и меньше MIN_KOLVO_SIM_PHONE,
          // то сообщение и выход
          if (
                (!String.IsNullOrEmpty(edPhone.Text))
            && (edPhone.Text.Length < MIN_KOLVO_SIM_PHONE))
          {
            ErrorListForm.ClearErrors();
            ErrorListForm.AddError(edPhone, String.Format(
               "Минимальное количество символов для поиска: {0}!"
              ,MIN_KOLVO_SIM_PHONE
             )
            );
            ReestrKlientovForm.BringToFront();
            return Json(new {error = "error"}, JsonRequestBehavior.AllowGet);

          } else {
            ErrorListForm.RemoveError(edPhone, String.Format(
               "Минимальное количество символов для поиска: {0}!"
              ,MIN_KOLVO_SIM_PHONE
             )
            );
          }



          // устанавливаю признак того,
          // что был поиск
          bBilPoisk = true;

          //Screen.Cursor := crHourGlass;
          formWaiting.Show();
          try {


          // получаю запрос из БД
          s = GetSPBody("MICROCREDIT", "MC_BROWSE_FIO");

          // статусы, доступные для менеджера
          if (iUSERRole == MyConst.iRoleMenedjer) {
            sStat = String.Format(
               "{0}, {1}, {2}"
              ,MyConst.CREDIT_STATUS_OTKAZ, MyConst.CREDIT_STATUS_ODOBREN, MyConst.CREDIT_STATUS_NA_RASSM
            );


          // для других ролей пока доступны все статусы
          } else {
            sStat = String.Format(
               " {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11} "
              ,MyConst.CREDIT_STATUS_OTKR_NET_PRO
              ,MyConst.CREDIT_STATUS_OTKR_BILI_PRO
              ,MyConst.CREDIT_STATUS_OTKR_TEK_PRO
              ,MyConst.CREDIT_STATUS_OTKR_NA_VZISK
              ,MyConst.CREDIT_STATUS_ZAKR_NE_BILO_PRO
              ,MyConst.CREDIT_STATUS_ZAKR_BILI_PRO
              ,MyConst.CREDIT_STATUS_ZAKR_NA_VZISK
              ,MyConst.CREDIT_STATUS_ZAKR_ANNUL
              ,MyConst.CREDIT_STATUS_OTKAZ
              ,MyConst.CREDIT_STATUS_NA_RASSM
              ,MyConst.CREDIT_STATUS_ODOBREN
              ,MyConst.CREDIT_STATUS_NET_AK_ZAYMA
            );
          }



          // если запуск в режиме "Поиск по совпадению"
          if (bPoiskPoSovp) {

            // убираю набранный в ФИО и в телефоне текст
            edFio.Clear();
            edPhone.Clear();

            // удаляю поиск по ФИО
            s = ReplaceTaggedParameter(s, TAG_NAME_POISK_FIO, "");

            // удаляю поиск по телефону
            s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PHONE, "");

            // убираю поиск без условий
            s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PUST, "");

            // убираю поиск по COUNTER_FIO
            s = ReplaceTaggedParameter(s, TAG_NAME_POISK_COUNTER_FIO, "");

            // отображаю панель, в которой отображается информация
            // об алгоритме, по которому произошло совпадение в
            // режиме "Поиск по совпадению"
            if (!pnlInfo.Visible) {
              pnlInfo.Visible = true;
              gridFio.Height = gridFio.Height - pnlInfo.Height - 5;
            }

          } else {

            // удаляю поиск по совпадению
            s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU, "");

            // скрываю панель, в которой отображается информация
            // об алгоритме, по которому произошло совпадение в
            // режиме "Поиск по совпадению"
            if (pnlInfo.Visible) {
              pnlInfo.Visible = false;
              gridFio.Height = gridFio.Height + pnlInfo.Height + 5;
            }
          }


          // поиск по ФИО
          if (!String.IsNullOrEmpty(edFio.Text)) {

            // анализирую ФИО
            // по частичному совпадению
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_FIO
              ,String.Format(
                "and d.SURNAME + ' ' + d.NAME + ' ' + d.SECNAME like '%{0}%'"
               ,edFio.Text.Replace(" ", "%")
              )
            );

            // убираю поиск без условий
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_PUST
              ,""
            );



          // убираю поиск по ФИО
          } else {
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_FIO
              ,""
            );
          }






          // поиск по телефону
          if (!String.IsNullOrEmpty(edPhone.Text)) {

            // анализирую телефон
            // по частичному совпадению
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_PHONE
              ,String.Format(
                "{0}"
               ,edPhone.Text
              )
              ,false
              ,true
            );


            // убираю поиск без условий
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_PUST
              ,""
            );




          // убираю поиск по телефону
          } else {
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_PHONE
              ,""
            );
          }



          // поиск по Counter_Fio
          if (iCounterFio != -1) {

            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_COUNTER_FIO
              ,String.Format("f.[COUNTER] = {0}", iCounterFio)
            );



            // убираю поиск без условий
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_PUST
              ,""
            );


          // убираю поиск по COUNTER_FIO
          } else {

            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_POISK_COUNTER_FIO
              ,""
            );

          }




          // поиск по доступным роли статусам
          // уже может на этот момент быть
          // удален из тела запроса
          s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_STAT
              ,String.Format("and isnull(c.STAT, @CREDIT_STATUS_NET_AK_ZAYMA) in ({0})", sStat)
            );

          // менеджер может видеть только текущий день
          if (iUSERRole == MyConst.iRoleMenedjer) {
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_TEK_DEN
              ,"and cast(c.D_START as date) = cast(getdate() as date)"
            );


          // для всех остальных дата не учитывается
          } else {
            s = ReplaceTaggedParameter(
               s
              ,TAG_NAME_TEK_DEN
              ,"and 1=1"
            );
          };


          // код отделения
          s = ReplaceTaggedParameter(
             s
            ,TAG_NAME_KOD_OTDELENIYA
            ,"and 1=1"
          );


          // менеджер может видеть только созданные
          // им займы
          if (iUSERRole == MyConst.iRoleMenedjer) {
            s = ReplaceTaggedParameter(s, TAG_NAME_OPERATOR_START,
              "and c.OPERATOR_START = " + MyFunc.QuotedStr(MyFunc.GetUserName()));
          } else {
            s = ReplaceTaggedParameter(s, TAG_NAME_OPERATOR_START,"");
          }


          // поиск по совпадению
          // исключаю анкету человека, по параметрам
          // которого идет поиск
          s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU_COUNTER0_FIO,
            iPoiskPoSovpCounter0Fio.ToString());
          s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU_LPUIN_FIO,
            iPoiskPoSovpLpuinFio.ToString());

          // заполняю адрес
          s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU_ADDR1,
            String.Format("{0}, {1}, {2}", iPoiskPoSovpStreetR, iPoiskPoSovpHouseR, iPoiskPoSovpFlatR));
          s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU_ADDR2,
            String.Format("{0}, {1}, {2}", iPoiskPoSovpStreetJ, iPoiskPoSovpHouseJ, iPoiskPoSovpFlatJ));

          // телефон
          s1 = String.Format(
                "({0}, {1}, ''), ({2}, {3}, ''), " +
                "({4}, {5}, ''), ({6}, {7}, ''), ({8}, {9}, '')"
            ,MyFunc.QuotedStr(sPoiskPoSovpMedicalPhoneHomeR), ALG_PHONE_HOME_R
            ,MyFunc.QuotedStr(sPoiskPoSovpMedicalPhoneHomeJ), ALG_PHONE_HOME_J
            ,MyFunc.QuotedStr(sPoiskPoSovpMedicalPhoneMobile), ALG_PHONE_MOBILE
            ,MyFunc.QuotedStr(sPoiskPoSovpWorkPhoneSecretary), ALG_WORK_PHONE_SECRETARY
            ,MyFunc.QuotedStr(sPoiskPoSovpWorkPhoneWork), ALG_WORK_PHONE_WORK
          );

          // домашний телефон  и
          // мобильный телефон контактных лиц
          // на всякий случай проверяю, что длина
          // массивов, содержащих домашний телефон,
          // мобильный телефон и ФИО контактных лиц
          // имеют одинаковое количество элементов
          if    ((arsRelativePhoneHome.Length == arsRelativePhoneMobile.Length)
            && (arsRelativePhoneMobile.Length == arsRelativeFIO.Length)
            && (arsRelativePhoneMobile.Length > 0))
          {
            for(int i1 = 0; i1 < arsRelativePhoneHome.Length; i1++) {
              // %s, %d, %s - телефон, алгоритм, ФИО контактного лица
              s1 = s1 + String.Format(", ({0}, {1}, {2})",
                 MyFunc.QuotedStr(arsRelativePhoneHome[i1])
                ,ALG_RELATIVE_PHONE_HOME
                ,MyFunc.QuotedStr(arsRelativeFIO[i1])
              );
              s1 = s1 + String.Format(", ({0}, {1}, {2})",
                 MyFunc.QuotedStr(arsRelativePhoneMobile[i1])
                ,ALG_RELATIVE_PHONE_MOBILE
                ,MyFunc.QuotedStr(arsRelativeFIO[i1])
              );
            }
          }

          s = ReplaceTaggedParameter(s, TAG_NAME_POISK_PO_SOVPADENIYU_PHONE, s1);



          // выполнение сформированного запроса
          // рву соединение после выполнения
          var r1 = db.Database.SqlQuery<MC_BROWSE_FIO_Result>(s);
          return Json(new {total = 1, page = 1, record = r1.Count(), rows = r1}, JsonRequestBehavior.AllowGet);

          } finally {
            // Screen.Cursor := crDefault;
            formWaiting.Close();

            // сбрасываю признак поиска по совпадению
            bPoiskPoSovp = false;
          }

        }

        public ActionResult Index() {

          var r = db.Database.SqlQuery<POLD_FIO>("select top 10 * from dbo.POLD_FIO");
          ViewBag.spBody = GetSPBody("MICROCREDIT", "MC_BROWSE_FIO");
          edFio.Text = "АБРАМОВ";
        //  return RequeryFio();
          //string s = "";
          //s = ReplaceTaggedParameter(s, "POISK_PHONE", "");
          //ViewBag.s = s;
          return View();

        }

        public ActionResult GetPhoto() {
          var r = dbData.MC_BROWSE_CLIENT_PHOTO(FCounter0Fio, FLpuinFio).ToList();
          if (r.Count() > 0) {
            return ViewBag.imgURL = File(r.FirstOrDefault().DATA_FILE, "image/jpg");
          }

          return Json(new {error = "error"}, JsonRequestBehavior.AllowGet);
        }

        public MC_BROWSE_ACTIVE_DOCUMENT_Result RequeryDocumentDeystvuyuschiy() {

          var r = db.MC_BROWSE_ACTIVE_DOCUMENT(FCounter0Fio, FLpuinFio).ToList();
          if (r.Count() > 0) {
            return r[0];
          }

          return new MC_BROWSE_ACTIVE_DOCUMENT_Result();
        
        }

        public ActionResult Form() {

          return View();

        }

        public ActionResult _Doc() {

          return View();

        }

        public ActionResult Start() {

          return View();

        }

        public ActionResult GetDoc(int COUNTER0_FIO, int LPUIN_FIO){

          IEnumerable<MC_BROWSE_ACTIVE_DOCUMENT_Result> r = db.MC_BROWSE_ACTIVE_DOCUMENT(COUNTER0_FIO, LPUIN_FIO).ToList();

          if (r.Count() != 1) {
            throw new Exception("Ошибка при выполнениии хранимой процедуры MC_BROWSE_ACTIVE_DOCUMENT");
          }

          MC_BROWSE_ACTIVE_DOCUMENT_Result d = r.First();

          JsonSerializerSettings ss = new JsonSerializerSettings{DateFormatString = "yyyy-MM-dd"};

          string jsonResult = JsonConvert.SerializeObject(d, ss);

          return Content(jsonResult, "application/json");


        }

        public ActionResult GetSexOptions() {

          var r = db.Database.SqlQuery<ByteStringDictionary>(
            "select CODE, NAME from AKTPAK.dbo.AKPC_SEX_V005 where D_FIN is null").ToArray();

          return Json(r, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Login() {

          return View();

        }


        public ActionResult Main() {

          return View();

        }

    }
}
