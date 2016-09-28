using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MICROCREDIT.NET.Controllers
{

    public static class MyConst {
      public const string PROG_VERSION = "8";

      // КОД ЗАДАЧИ В МОДУЛЕ РАЗГРАНИЧЕНИЯ ПРАВ
      public const int TASK_CODE = 1;

      // ПРИЧИНА УДАЛЕНИЯ ЗАПИСИ
      // ОБНОВЛЕНИЕ ЗАПИСИ
      public const int D_FIN_STATUS_OBNOVLENIE = 1;
      // УДАЛЕНИЕ ЗАПИСИ
      public const int D_FIN_STATUS_UDALENIE = 2;

      // КОДЫ  ПРАВ ДОСТУПА
      // [РЕЕСТР КЛИЕНТОВ] - ПРОСМОТР
      public const int ACTION_REESTR_KLIENTOV_PROSMOTR = 1;
      // [РЕЕСТР КЛИЕНТОВ] - РЕДАКТИРОВАНИЕ
      public const int ACTION_REESTR_KLIENTOV_REDAKTIROVANIE = 2;
      // [РЕЕСТР КЛИЕНТОВ] - УДАЛЕНИЕ
      public const int ACTION_REESTR_KLIENTOV_UDALENIE = 3;

      //// ИСТОРИЯ ЗАЙМОВ
      // НОВЫЙ
      public const int UR_IST_ZAYM_NOV = 4;
      // РЕДАКТИРОВАТЬ
      public const int UR_IST_ZAYM_RED = 5;
      // УДАЛИТЬ
      public const int UR_IST_ZAYM_UD = 6;
      // ЗАКРЫТЬ ЗАЙМ
      public const int UR_IST_ZAYM_ZAKR = 7;
      // ВЫДАТЬ
      public const int UR_IST_ZAYM_VID = 8;
      // ОДОБРИТЬ
      public const int UR_IST_ZAYM_ODOB = 9;
      // ОТКАЗ
      public const int UR_IST_ZAYM_OTKAZ = 10;
      // ПЕЧАТЬ
      public const int UR_IST_ZAYM_PRINT =11;


      // КАССА, ДОСТУП
      public const int UR_CASH_VIEW = 12;
      // КАССА, ВНЕСЕНИЕ ДЕНЕГ
      public const int UR_CASH_ADD_CASH = 13;
      // КАССА, ИЗЪЯТИЕ ДЕНЕГ
      public const int UR_CASH_OUT_CASH = 14;
      // КАССА, ВЫБОР ОТДЕЛЕНИЯ
      public const int UR_CASH_VIB_OTDEL = 15;

      // ОТЧЕТЫ, ДОСТУП
      public const int UR_REPORT_VIEW = 16;

      //ОТЧЕТЫ, ФОРМИРОВАНИЕ
      public const int UR_REPORT_GET = 17;

      // Платежи, редактирование
      public const int UR_PAYMENT_EDIT = 18;

      //Платежи, удаление
      public const int UR_PAYMENT_DELETE = 19;

      //Отчеты, печать кассовой книги
      public const int UR_REPORT_CASH_BOOK_PRINT = 20;

      // ОРГАНАЙЗЕР
      // [ОРГАНАЙЗЕР] - ДОБАВИТЬ
      public const int UR_ORG_DOBAVIT = 21;
      // [ОРГАНАЙЗЕР] - РЕДАКТИРОВАТЬ
      public const int UR_ORG_REDAKT = 22;
      // [ОРГАНАЙЗЕР] - УДАЛИТЬ
      public const int UR_ORG_UDALIT = 23;
      // [ОРГАНАЙЗЕР] - ОПРЕДЕЛИТЬ ПОЛЬЗОВАТЕЛЕЙ
      public const int UR_ORG_OPR_POL = 24;
      // [ОРГАНАЙЗЕР] - ВЫПОЛНИТЬ
      public const int UR_ORG_VIPOLNIT = 25;
      // [ОРГАНАЙЗЕР] - ОТМЕНИТЬ
      public const int UR_ORG_OTMENIT = 26;
      // [ОРГАНАЙЗЕР] - ПРОСМОТР
      public const int UR_ORG_PROSMOTR = 27;
      // [ОРГАНАЙЗЕР] - ОТЛОЖИТЬ
      public const int UR_ORG_OTLOJ = 28;

      // [КРАТКАЯ ИНФОРМАЦИЯ] - ПРОСМОТР
      public const int UR_KR_INF_PROSMOTR = 29;
      // [КРАТКАЯ ИНФОРМАЦИЯ] - РЕДАКТИРОВАНИЕ ИСТОРИИ
      public const int UR_KR_INF_REDAKT_ISTOR = 36;
      // [РАЗГРАНИЧЕНИЕ ПРАВ] - ДОСТУП
      public const int UR_USER_RIGHT_ACCESS = 30;
      // [АКТУАЛИЗАЦИЯ ЗАЙМОВ] - ДОСТУП
      public const int UR_AKT_SOST_ZAYM = 31;
      // [РЕЗЕРВНАЯ КОПИЯ БД] - ДОСТУП
      public const int UR_BACKUP_DATA = 32;
      // [СПРАВОЧНИКИ] - ДОСТУП
      public const int UR_SPRAVOCHNIKI = 34;
      // [СПРАВОЧНИКИ] - ДОСТУП К ШАБЛОНАМ ЗВОНКОВ/СМС
      public const int UR_TEMPLATES_SMS_ZVON = 35;


      // ВИД ПЛАТЕЖА (POLM_VID_PLATEJA)
      // ПЛАТЕЖ
      public const int VID_PLATEJA_PLATEJ = 1;

      // НАСТРОЙКИ - РЕЖИМ ОТОБРАЖЕНИЯ АРХИВНОГО ДОКУМЕНТА
      // USRM_OPTIONS_ARHIVNIY_DOKUMENT
      // ОТОБРАЖАТЬ
      public const string OPTIONS_ARHIVNIY_DOKUMENT_OTOBRAJAT = "1";
      // НЕ ОТОБРАЖАТЬ
      public const string OPTIONS_ARHIVNIY_DOKUMENT_NE_OTOBRAJAT = "2";
      // НЕ СОХРАНЯТЬ АНКЕТУ БЕЗ УКАЗАНИЯ
      public const string OPTIONS_ARHIVNIY_DOKUMENT_NE_SOHRANAT_ANKETU_BEZ_UKAZANIYA = "3";

      // НАСТРОЙКИ - НАИМЕНОВАНИЕ НАСТРОЙКИ
      // РЕЖИМ ОТОБРАЖЕНИЯ АРХИВНОГО ДОКУМЕНТА
      public const string OPTION_NAME_ARHIVNIY_DOKUMENT = "ARHIVNIY_DOKUMENT";
      // ТИП ДОКУМЕНТА ПО УМОЛЧАНИЮ ДЛЯ АКТИВНОГО ДОКУМЕНТА
      public const string OPTION_NAME_ACTIVE_DOCTYPE_PO_UMOLCHANIYU = "ACTIVE_DOCTYPE_PO_UMOLCHANIYU";
      // ТИП ДОКУМЕНТА ПО УМОЛЧАНИЮ ДЛЯ АРХИВНОГО ДОКУМЕНТА
      public const string OPTION_NAME_ARHIVE_DOCTYPE_PO_UMOLCHANIYU = "ARHIVE_DOCTYPE_PO_UMOLCHANIYU";
      // РЕГИОН ПО УМОЛЧАНИЮ, ПРИ ВВОДЕ АДРЕСОВ
      public const string OPTION_NAME_TER_PO_UMOLCHANIYU = "TER_PO_UMOLCHANIYU";
      // ПОДПИСЬ ДОВЕРЕННОГО ЛИЦА ДЛЯ ВЫХОДНЫХ ДОКУМЕНТОВ
      public const string OPTION_NAME_PODPIS_DOVERENNOGO_LITSA = "PODPIS_DOVERENNOGO_LITSA";
      // КОД ОТДЕЛЕНИЯ
      public const string OPTION_NAME_KOD_OTDELENIYA = "KOD_OTDELENIYA";
      // ФОТОКАМЕРА
      public const string OPTION_NAME_FOTOKAMERA = "FOTOKAMERA";
      // COM ПОРТ AT
      public const string OPTION_NAME_COM_PORT = "COM_PORT";
      // COM ПОРТ ДЛЯ ГОЛОСА
      public const string OPTION_NAME_COM_PORT_ZV = "COM_PORT_ZV";












      //// ШАГИ ВЫПОЛНЯЕМЫЕ ПРИ ДОЗВОНЕ
      // ПРОЗВОН НЕ АКТИВЕН
      public const int CALL_STEP_00_NONE = 0;
      // АКТИВАЦИЯ ПРОЗВОНА AT
      public const int CALL_STEP_01_INIT = 1;
      // АКТИВАЦИЯ НАБОР НОМЕРА
      public const int CALL_STEP_02_NABOR_NOMERA = 2;
      // ОЖИДАНИЕ СНЯТИЯ ТРУБКИ
      public const int CALL_STEP_03_OJID_SNYAT = 3;
      // ПЕРЕХОД В ЗВУКОВОЙ РЕЖИМ
      public const int CALL_STEP_04_PER_V_ZV_REJIM = 4;
      // ИГРАТЬ ЗВУК
      public const int CALL_STEP_05_IG_ZV = 5;
      // ПОЛОЖИТЬ ТРУБКУ
      public const int CALL_STEP_06_POL_TR = 6;






      //// РЕЗУЛЬТАТ ПРОИГРЫВАНИЯ ЗВУКА
      // ПОЛНОСТЬЮ ПРОСЛУШАН
      public const int REZ_POL_PR = 1;
      // ЧАСТИЧНО ПРОСЛУШАН
      public const int REZ_CHAST_PR = 2;
      // ОШИБКА
      public const int REZ_OSH = 3;
      // СБРОШЕН ВЫЗОВ
      public const int REZ_SBR_VIZ = 4;
      // НЕ ОТВЕЧАЮТ
      public const int REZ_NE_OTVECH = 5;
      // SMS отправлена
      public const int REZ_SMS_OTPR = 6;
      // ИСКЛЮЧЕН ИЗ ПРОЗВОНА
      public const int REZ_ISKL_IZ_PROZV = 7;
      // ОПЛАЧЕНО
      public const int REZ_OPLACHENO = 8;








      //// СТАТУС ЗАЙМА AKPC_CREDIT_STATUS
      // ОТКРЫТ, НЕТ ПРОСРОЧЕК
      public const int CREDIT_STATUS_OTKR_NET_PRO = 1;
      // ОТКРЫТ, БЫЛИ ПРОСРОЧКИ
      public const int CREDIT_STATUS_OTKR_BILI_PRO = 2;
      // ОТКРЫТ, ТЕКУЩАЯ ПРОСРОЧКА
      public const int CREDIT_STATUS_OTKR_TEK_PRO = 3;
      // ОТКРЫТ, НА ВЗЫСКАНИИ
      public const int CREDIT_STATUS_OTKR_NA_VZISK = 4;
      // ЗАКРЫТ, НЕ БЫЛО ПРОСРОЧЕК
      public const int CREDIT_STATUS_ZAKR_NE_BILO_PRO = 5;
      // ЗАКРЫТ, БЫЛИ ПРОСРОЧКИ
      public const int CREDIT_STATUS_ZAKR_BILI_PRO = 6;
      // ЗАКРЫТ НА ВЗЫСКАНИИ
      public const int CREDIT_STATUS_ZAKR_NA_VZISK = 7;
      // ЗАКРЫТ, АННУЛИРОВАН
      public const int CREDIT_STATUS_ZAKR_ANNUL = 8;
      // ОТКАЗ
      public const int CREDIT_STATUS_OTKAZ = 9;
      // НА РАССМОТРЕНИИ
      public const int CREDIT_STATUS_NA_RASSM = 10;
      // ОДОБРЕН
      public const int CREDIT_STATUS_ODOBREN = 11;
      // НЕТ АКТИВНОГО ЗАЙМА
      public const int CREDIT_STATUS_NET_AK_ZAYMA = -1;


      //// КОДЫ РОЛЕЙ
      // АДМИНИСТРАТОР
      public const int ROLE_CODE_AD = 1;
      // МЕНЕДЖЕР
      public const int ROLE_CODE_MEN = 2;



      //// СПОСОБ ОПОВЕЩЕНИЯ ПЛАНИРОВЩИКА
      // ОТПРАВКА СМС
      public const int SPOSOB_SMS= 1;
      // ЗВУКОВОЕ СООБЩЕНИЕ
      public const int SPOSOB_ZV= 2;

      ///ТИП СМС-СООБЩЕНИЯ
      /// При отправке через модем
      public const int SMS_TYPE_MODEM = 1;
      /// При отправке через веб-сервис
      public const int SMS_TYPE_WEB = 2;

      //// СТАТУС СОБЫТИЯ В ОРГАНАЙЗЕРЕ
      // Ожидается
      public const int STAT_ORG_OJID = 1;
      // Активно
      public const int STAT_ORG_AKTIV = 2;
      // Выполнено
      public const int STAT_ORG_VIPOLNENO = 3;
      // Просрочено
      public const int STAT_ORG_PROSROCH = 4;
      // Отсутствует
      public const int STAT_ORG_OTSUTSTV = 5;

      //// ПРИЗНАК ПОЛУЧЕНИЯ СОБЫТИЙ ОРГАНАЙЗЕРА
      // получать события
      public const int USRD_USER_ORG_EVENT_DA = 1;
      // не получать события
      public const int USRD_USER_ORG_EVENT_NET = 0;

      // ЧАСТОТА ПРОВЕРКИ ОРГАНАЙЗЕРА
      // НА НАЛИЧИЕ НОВЫХ СОБЫТИЙ (МИНУТ)
      public const double ORG_CHECK_TIME = 5.0;


      // ТИП ПОДОЗРИТЕЛЬНЫХ ТЕЛЕФОНОВ
      public const int TIP_PODOZR = 0; // ПОДОЗРИТЕЛЬНЫЕ
      public const int TIP_ISKL = 1; // ИСКЛЮЧЕННЫЙ
      public const int TIP_S_AVTOOTV = 2; // С АВТООТВЕТЧИКОМ

      public const string sInitLogin = "InitLogin"; // логин для чтения инициализационных параметров
      public const string sInitLoginPassword = "123ewqasdcxz"; // пароль для логина чтения инициализационных параметров

      // цифровой код (идентификатор) роли администратора
      public const int iRoleAdministrator = 1;
      // цифровой код (идентификатор) роли менеджера
      public const int iRoleMenedjer = 2;
      // главный администратор
      public const int iRoleGlAdmin = 5;
      // Сервис информирования клиентов (СИК)
      public const int iRoleSIK = 10;

    }

    public class ConstController : Controller
    {
        //
        // GET: /Const/

        public ActionResult Index()
        {
            return View();
        }

    }
}
