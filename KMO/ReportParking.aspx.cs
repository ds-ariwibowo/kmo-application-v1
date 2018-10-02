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
using System.IO;

using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Reporting;

using KMO.Class;

namespace KMO
{
    public partial class ReportParking : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;

        enum eMessage : byte { eSuccess = 1, eWarning = 2, eError = 3 };

        private void hideMessageBox()
        {
            mySuccess.Visible = false;
            lblMySuccess.Text = "";
            myWarning.Visible = false;
            lblMyWarning.Text = "";
            myError.Visible = false;
            lblMyError.Text = "";
        }

        private void showMessage(eMessage iPilih, string iTitle, string iMessage)
        {
            hideMessageBox();

            switch (iPilih)
            {
                case eMessage.eSuccess:
                    mySuccess.Visible = true;
                    lblTitleMySuccess.Text = iTitle;
                    lblMySuccess.Text = iMessage;
                    break;
                case eMessage.eWarning:
                    myWarning.Visible = true;
                    lblTitleMyWarning.Text = iTitle;
                    lblMyWarning.Text = iMessage;
                    break;
                case eMessage.eError:
                    myError.Visible = true;
                    lblTitleMyError.Text = iTitle;
                    lblMyError.Text = iMessage;
                    break;
            }
        }

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
                hideMessageBox();
            }
            else
            {

            }             
        }

        protected void btnOpenPDF_Click(object sender, EventArgs e)
        {
            //loadRPT();            

            try
            {
                String SQL = "select* from[dbo].[vwInvoiceSource] Where Month = " + ddlMonthPeriod.SelectedValue.ToString() + " And Year = " + txtYearPeriod.Text.Trim();


                string sConstr = Db.GetConnectionString();
                using (SqlConnection conn = new SqlConnection(sConstr))
                {
                    using (SqlCommand comm = new SqlCommand(SQL, conn))
                    {
                        conn.Open();
                        using (SqlDataAdapter da = new SqlDataAdapter(comm))
                        {
                            dt = new DataTable("tbl");
                            da.Fill(dt);
                            if (dt.Rows.Count > 0) { bRes = true; }
                        }
                    }
                }

                if (bRes)
                {
                    ReportDocument _rdReportViewer = new ReportDocument();
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptPAK"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());

                    //show crystal report viewer
                    //CrystalReportViewer1.ReportSource = _rdReportViewer;

                    bRes = true;

                    //check file exist or not
                    string fDate = "INV-PAK-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim();

                    if (!string.IsNullOrEmpty(fDate))
                    {
                        if (!File.Exists(HttpContext.Current.Server.MapPath("~\\RptTemp\\" + fDate + ".pdf")))
                        {
                            // deletevprevious image
                            //File.Delete(HttpContext.Current.Server.MapPath(deletePath));
                            _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                        }
                    }

                    string url = string.Format("./PDFViewer.aspx?FN=" + fDate + ".pdf", (sender as Button).CommandArgument);
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    hideMessageBox();
                }
                else
                {
                    showMessage(eMessage.eWarning, "Data not found", "Invoice for " + ddlMonthPeriod.SelectedItem.ToString() + " - " + txtYearPeriod.Text.Trim() + " still empty.");
                }
            }
            catch (Exception me)
            {
                showMessage(eMessage.eError, "btnOpenPDF_Click", me.Message);
                bRes = false;
            }                
        }
    }
}