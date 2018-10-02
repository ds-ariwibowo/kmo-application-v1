using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMO.Class
{
    public class Db
    {
        //Define all variable
        DataTable dt;

        // Get Connection String From Web.Config
        public static string GetConnectionString()
        {
            if (ConfigurationManager.AppSettings["isDevelopment"].ToString() == "yes")
            {
                if (ConfigurationManager.AppSettings["isLocal"].ToString() == "yes")
                {
                    return ConfigurationManager.ConnectionStrings["ConnStrLocal"].ConnectionString; /// development at local pc
                }else
                {
                    return ConfigurationManager.ConnectionStrings["ConnStrDEV"].ConnectionString; /// development at dev server
                }

            }
            else
            {
                return ConfigurationManager.ConnectionStrings["ConnStrPRO"].ConnectionString; /// production
            }
        }

        #region pemanggilan database
            public static DataSet get_list(string iSql)
            {
               try
               {
                   string strConn = GetConnectionString();
                   string select = iSql;

                   SqlDataAdapter da = new SqlDataAdapter(select, strConn);

                   da.GetFillParameters();
                   DataSet ds = new DataSet();
                   da.Fill(ds, "get_list");
                   return (ds);
               }
               catch (Exception ex)
               {
                   //System.Diagnostics.EventLog.WriteEntry("New Agency", "ERROR at get_list with message : " + ex.Message);
                   return null;
               }                
            }            
        #endregion

        #region insert database
            public static Boolean ins_RateRemuneration(string kdProduk, string topUp, int thnPolisKe, 
                int masaPertanggungan, int masaPremi, decimal nilaiProsentase, int nilaiFix, string keterangan, string userID)
            {
                try
                {
                    

                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.EventLog.WriteEntry("New Agency", "ERROR at ins_RateRemuneration with message : " + ex.Message);
                    return false;
                }
            }         
        #endregion

            internal static void ins_RateRemuneration(char p1, char p2, int p3, int p4, int p5, int p6, int p7, char p8, char p9)
            {
                throw new NotImplementedException();
            }
    }
}