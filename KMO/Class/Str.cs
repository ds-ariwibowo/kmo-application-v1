using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace KMO.Class
{
    public class Str
    {
        
        public void getAllCtl(ControlCollection ctls)
        {

            foreach (Control c in ctls)
            {
                if (c is System.Web.UI.WebControls.TextBox)
                {
                    //TextBox tt = c as TextBox;
                    ////to do something by using textBox tt.
                    ((TextBox)c).Text = string.Empty;
                }
                if (c is System.Web.UI.WebControls.CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                if (c is System.Web.UI.WebControls.DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = -1;
                }
                if (c.HasControls())
                {
                    getAllCtl(c.Controls);

                }
            }

        }

        // Number Thousand Separator
        public static string NumTS(string a, string sep)
        {
            string b = Regex.Replace(a, @"/[^\d]/g", "");
            string c = "";
            int nlength = b.Length;
            int j = 0;
            for (int i = nlength; i > 0; i--) {
                j = j + 1;
                if (((j % 3) == 1) && (j != 1)) {
                    c = b.Substring(i - 1, 1) + sep + c;
                }
                else {
                    c = b.Substring(i - 1, 1) + c;
                }
            }
            return c; 
        }

        // Date Format Indo dd-MM-yyyy
        public static string dateID(DateTime x)
        {
            return String.Format("{0:dd-MM-yyyy}", x);
        }

        // Date Format Indo dd-MM-yyyy
        public static string fulldateID(DateTime x)
        {
            return String.Format("{0:dd-MM-yyyy HH:mm:ss}", x);
        }

        // Fungsi untuk menghilangkan separator seribu sebelum diinsert ke Store Procedure
        public static string delcom(string arg, string repl, string replier)
        {
            do
            {
                arg = arg.Replace(repl, replier);
            } while (arg.IndexOf(repl) > 0);
            return arg;
        }

        public static String Right(String Str, byte Len)
        {
            String R = Str.Substring((Str.Length - Len), Len);
            return R;
        }
        public static String Left(String Str, byte Len)
        {
            String L = Str.Substring(0, Len);
            return L;
        }
    }
}