using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMO.Class
{
    public class Cf
    {
        public static string GetUrl(string mode)
        {
            string Result = "";
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            /// http://localhost:1302/TESTERS/Default6.aspx?p1=x&p2=y

            string path = HttpContext.Current.Request.Url.AbsolutePath;
            /// /TESTERS/Default6.aspx

            String[] path2 = path.Split('/');
            Int32 path2_length = path2.Length;
            String path2_final = path2[(path2_length - 1)];
            String[] path3 = path2_final.Split('?');
            String FileUrl = path3[0];
            String PathUrl = url.Replace(path2_final, "");

            String host = HttpContext.Current.Request.Url.Host;
            /// localhost

            if (mode == "FileUrl") { Result = FileUrl; } // return Default6.aspx
            else if (mode == "PathUrl") { Result = PathUrl; } // return Default6.aspx?p1=x&p2=y

            return Result;
        }
    }
}