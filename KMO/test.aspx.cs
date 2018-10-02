using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Reporting;


using KMO.Class;

namespace KMO
{
    public partial class test : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;

        enum eMessage : byte { eSuccess = 1, eWarning = 2, eError = 3 };

        protected void exportReport(CrystalDecisions.CrystalReports.Engine.ReportClass selectedReport, CrystalDecisions.Shared.ExportFormatType eft)
        {
            selectedReport.ExportOptions.ExportFormatType = eft;

            string contentType = "";
            // Make sure asp.net has create and delete permissions in the directory
            string tempDir = System.Configuration.ConfigurationSettings.AppSettings["TempDir"];
            string tempFileName = Session.SessionID.ToString() + ".";
            switch (eft)
            {
                case CrystalDecisions.Shared.ExportFormatType.PortableDocFormat:
                    tempFileName += "pdf";
                    contentType = "application/pdf";
                    break;
                case CrystalDecisions.Shared.ExportFormatType.WordForWindows:
                    tempFileName += "doc";
                    contentType = "application/msword";
                    break;
                case CrystalDecisions.Shared.ExportFormatType.Excel:
                    tempFileName += "xls";
                    contentType = "application/vnd.ms-excel";
                    break;
                case CrystalDecisions.Shared.ExportFormatType.HTML32:
                case CrystalDecisions.Shared.ExportFormatType.HTML40:
                    tempFileName += "htm";
                    contentType = "text/html";
                    CrystalDecisions.Shared.HTMLFormatOptions hop = new CrystalDecisions.Shared.HTMLFormatOptions();
                    hop.HTMLBaseFolderName = tempDir;
                    hop.HTMLFileName = tempFileName;
                    selectedReport.ExportOptions.FormatOptions = hop;
                    break;
            }

            CrystalDecisions.Shared.DiskFileDestinationOptions dfo = new CrystalDecisions.Shared.DiskFileDestinationOptions();
            dfo.DiskFileName = tempDir + tempFileName;
            selectedReport.ExportOptions.DestinationOptions = dfo;
            selectedReport.ExportOptions.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile;

            selectedReport.Export();
            selectedReport.Close();

            string tempFileNameUsed;
            //if (eft == CrystalDecisions.Shared.ExportFormatType.HTML32 || eft == CrystalDecisions.Shared.ExportFormatType.HTML40)
            //{
            //    string[] fp = selectedReport.FilePath.Split("\\".ToCharArray());
            //    string leafDir = fp[fp.Length - 1];
            //    // strip .rpt extension
            //    leafDir = leafDir.Substring(0, leafDir.Length–4);
            //    tempFileNameUsed = string.Format("{0}{1}\\{2}", tempDir, leafDir, tempFileName);
            //}
            //else
            tempFileNameUsed = tempDir + tempFileName;

            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = contentType;

            Response.WriteFile(tempFileNameUsed);
            Response.Flush();
            Response.Close();

            System.IO.File.Delete(tempFileNameUsed);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bRes = false;

            Vd.isValidLogin();

            if (!IsPostBack)
            {
                try
                {
                    ReportDocument rprt = new ReportDocument();
                    ConnectionInfo connInfo = new ConnectionInfo();
                    connInfo.ServerName = "edo";
                    connInfo.DatabaseName = "png";
                    connInfo.UserID = "sa";
                    connInfo.Password = "server";

                    TableLogOnInfo tableLogOnInfo = new TableLogOnInfo();
                    tableLogOnInfo.ConnectionInfo = connInfo;

                    rprt.Load(Server.MapPath("~\\RptTemp\\rptPLNA4-4.rpt"));

                    foreach (CrystalDecisions.CrystalReports.Engine.Table table in rprt.Database.Tables)
                    {
                        table.ApplyLogOnInfo(tableLogOnInfo);
                        table.LogOnInfo.ConnectionInfo.ServerName = connInfo.ServerName;
                        table.LogOnInfo.ConnectionInfo.DatabaseName = connInfo.DatabaseName;
                        table.LogOnInfo.ConnectionInfo.UserID = connInfo.UserID;
                        table.LogOnInfo.ConnectionInfo.Password = connInfo.Password;

                        // Apply the schema name to the table's location
                        table.Location = "dbo." + table.Location;
                    }

                    SqlConnection con = new SqlConnection(Db.GetConnectionString());
                    SqlCommand cmd = new SqlCommand("select * from [dbo].[vwUTLECDMRD] Where Month = 4 And Year = 2016", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds, "Newtbl_data");

                    rprt.SetDataSource(ds);
                    rprt.VerifyDatabase();

                    Label1.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    CrystalReportViewer1.ReportSource = rprt;                    
                }
                catch (Exception me)
                {
                    bRes = false;
                }
            }
            else
            {

            }
        }
    }
}