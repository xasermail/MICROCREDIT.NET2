using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MICROCREDIT.NET.Controllers
{

    public class TControl {
      public bool Visible {get; set;}
      public int Height {get; set;}
    }

    public class TEdit : TControl {
      public string Text {get; set;}
      public void Clear() {
        return;
      }
    }

    public class TPanel : TControl {
    }

    public class TDBGridEh : TControl {
    }

    public class TErrorListForm {

      public void ClearErrors() {
        return;
      }

      public void AddError(TControl c, string error) {
        return;
      }

      public void RemoveError(TControl c, string error) {
        return;
      }

    }

    public class TFormWaiting {

      public void Show() {
      }

      public void Close() {
      }

    }

    public class TReestrKlientovForm {

      public void BringToFront() {
      }

    }

    public static class MyFunc {

      public static string QuotedStr(string s) {
        if (!String.IsNullOrEmpty(s)) {
          s = s.Replace("'", "''");
          s = "'" + s + "'";
        } else {
         s = "''";
        }
        return s;
      }

      public static string GetUserName() {
        return "some dummy user";
      }

    }

    public class MyFuncController : Controller
    {
        

        public ActionResult Index()
        {
            return View();
        }

    }

    public class ByteStringDictionary 
    {

      public byte code {get; set;}
      public string name {get; set;}
      
    }
}
