using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMO;
using System.Globalization;

namespace KMO.Class
{
    public class Vd
    {
        
        /// Class Function : To Validate anything

        public static bool isValidUserPass(string user, string pass)
        {
            WebService_KMO.Service ws = new WebService_KMO.Service();
            bool x = ws.validate_userpass(user, pass);
            return x;
        }

        public static bool isRequired(string x)
        {
            if (x.Trim() != "") { return true; } else { return false; }
        }

        public static void isValidLogin()
        {
            if (HttpContext.Current.Session["userid"] == null) { HttpContext.Current.Response.Redirect("./LogIn.aspx"); }
        }

        public static void isValidLogin2()
        {
            if (HttpContext.Current.Session["userid"] == null || HttpContext.Current.Session["no_polis_aktif"] == null) { HttpContext.Current.Response.Redirect("Default.aspx"); }
        }

        public static bool isValidDate_ID(string x)
        {
            bool status = false; DateTime dateTime; 
            string format = "dd-MM-yyyy"; // 16-02-2014 : valid
            if (DateTime.TryParseExact(x, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) { status = true; }
            return status;
        }

        public static bool isValidDate(string x)
        {
            bool status = false; DateTime dateTime;
            string format = "yyyy-MM-dd"; // 2014-02-16 : valid
            if (DateTime.TryParseExact(x, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime)) { status = true; }
            return status;
        }

        public static bool isValidDecimal(string x)
        {
            bool status = false; decimal d;
            status = Decimal.TryParse(x, out d);
            return status;
        }

        public static bool isValidInteger(string x)
        {
            bool status = false; int n;
            status = int.TryParse(x, out n);
            return status;
        }

        public static bool isValidTextOnly(string x)
        {
            bool status = false;
            return status;
        }

        public static bool isValidNumberOnly(string x)
        {
            bool status = false;
            return status;
        }

        public static bool isValidAlphaNumeric(string x)
        {
            bool status = false;
            return status;
        }

        public static bool isValidEmail(string x)
        {
            bool status = false;
            return status;
        }


    }
}