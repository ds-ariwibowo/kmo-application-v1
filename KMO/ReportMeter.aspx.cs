using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
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
    public partial class ReportMeter : System.Web.UI.Page
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
        private void showMessageModal(eMessage iPilih, string iTitle, string iMessage)
        {

            switch (iPilih)
            {
                case eMessage.eSuccess:
                    iTitle += "SUCCESS. " + iTitle;
                    break;
                case eMessage.eWarning:
                    iTitle += "WARNING!!!. " + iTitle;
                    break;
                case eMessage.eError:
                    iTitle += "ERROR!!!. " + iTitle;
                    break;
            }

            lblMessageModal_Title.Text = iTitle;
            lblMessageModal_Body.Text = iMessage;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#messageModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "messageModalScript", sb.ToString(), false);

            UpdatePanel2.Update();

            bRes = false;
        }

        private void createInvNo(string iMonth, string iYear, string iSuiteNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spCreateInvoicePLNNo ", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@SuiteNO", iSuiteNo);
                
                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "executeAdd", me.Message);
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

        private void BindGrid()
        {
            string iSql = "execute [spGetUTLECDMRD_Query] " + ddlMonthPeriod.Text + ", " + txtYearPeriod.Text + " ";            
            try
            {
                GridView1.DataSource = Db.get_list(iSql);
                GridView1.DataBind();
            }
            catch (SqlException ex)
            {
                showMessageModal(eMessage.eError, "BindGrid", ex.Message);
            }
            catch (Exception ex)
            {
                showMessageModal(eMessage.eError, "BindGrid", ex.Message);
            }
        }

        private void BindGridSort(string iOrderBy)
        {
            string iSql = "Select * from vwInvList_PLN_ALL Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
            //string iSql2 = "Select * from [vwCopPln] Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
            try
            {
                GridView1.DataSource = Db.get_list(iSql);
                GridView1.DataBind();

                //GridView2.DataSource = Db.get_list(iSql2);
                //GridView2.DataBind();
                //CreateDynamicGrid(iOrderBy);
            }
            catch (SqlException ex)
            {
                showMessageModal(eMessage.eError, "BindGrid", ex.Message);
            }
            catch (Exception ex)
            {
                showMessageModal(eMessage.eError, "BindGrid", ex.Message);
            }
        }

        private void CreateDynamicGrid(string iOrderBy)
        {
            try
            {
                string iQuery;
                iQuery = "Select * from vwInvList_PLN_ALL Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;

                ds = Db.get_list(iQuery);

                bool hasRows = ds.Tables.Cast<DataTable>()
                                               .Any(table => table.Rows.Count != 0);

                //execute the select statement
                DataView dvProducts = ds.Tables[0].DefaultView;
                DataTable dtProducts = dvProducts.Table;

                BoundField boundField = null;
                //iterate through the columns of the datatable and add them to the gridview
                int iNoKolom = 0;

                foreach (DataColumn col in dtProducts.Columns)
                {
                    iNoKolom += 1;

                    //initialize the bound field
                    boundField = new BoundField();

                    //set the DataField.
                    boundField.DataField = col.ColumnName;

                    //set the HeaderText
                    boundField.HeaderText = col.ColumnName;

                    //Add the field to the GridView columns.
                    GridView2.Columns.Add(boundField);

                }
                //bind the gridview the DataSource
                                
                GridView2.DataSource = dtProducts;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot find table 0.")
                {
                    CreateDynamicGrid(iOrderBy);
                }
            }

        }

        private void printInvoice(string iID)
        {
            try
            {
                String SQL = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                            "From [dbo].[vwUTLECDMRD] Where ID = " + iID;
                               
                if (txtInvPrintedDate.Text == "" || txtInvDueDate.Text == "")
                {
                    showMessageModal(eMessage.eError, "Date still empty", "Print date or due date is empty.");
                    return;
                }
                else
                {
                    try
                    {
                        DateTime iPD = DateTime.ParseExact(txtInvPrintedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        DateTime iDD = DateTime.ParseExact(txtInvDueDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                        if (iDD.Date < iPD.Date)
                        {
                            showMessageModal(eMessage.eError, "Format date", "Due date is lower than print date.");
                            return;
                        }
                    }
                    catch (Exception me)
                    {
                        showMessageModal(eMessage.eWarning, "Format date", "Please input your format date to dd-MM-yyyy.");
                        return;
                    }

                }

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
                            if (dt.Rows.Count > 0)
                            {
                                ds = Db.get_list(SQL);
                                
                                DateTime iInvPrintDate = DateTime.ParseExact(txtInvPrintedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                DateTime iInvDueDate = DateTime.ParseExact(txtInvDueDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                String SQL1 = "Update tUTLECDMRD " +
                                            "Set InvPrintDate = '" + iInvPrintDate.ToString() + "', InvDueDate = '" + iInvDueDate.ToString() + "' " +
                                            "Where ID = " + iID;
                                try
                                {
                                    SqlConnection conn1 = new SqlConnection(Db.GetConnectionString());
                                    conn1.Open();

                                    SqlCommand cmd = new SqlCommand(SQL1, conn1);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                    string sConstr3 = Db.GetConnectionString();
                                    using (SqlConnection conn3 = new SqlConnection(sConstr3))
                                    {
                                        using (SqlCommand comm3 = new SqlCommand(SQL, conn3))
                                        {
                                            conn3.Open();
                                            using (SqlDataAdapter da3 = new SqlDataAdapter(comm3))
                                            {
                                                dt = new DataTable("tbl");
                                                da3.Fill(dt);
                                                if (dt.Rows.Count > 0)
                                                {

                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception me)
                                {
                                    showMessageModal(eMessage.eError, "Update inv date & due date", me.Message);
                                    bRes = false;
                                }
                                //}
                                bRes = true;
                            }
                        }
                    }
                }

                if (bRes)
                {
                    ReportDocument _rdReportViewer = new ReportDocument();
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptPLN"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, iID);

                    //check file exist or not
                    string fDate = "INV-PLN-" + ds.Tables[0].Rows[0]["InvNo"].ToString();

                    if (!string.IsNullOrEmpty(fDate))
                    {
                        _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                    }

                    string url = "./PDFViewer.aspx?FN=" + fDate + ".pdf";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("window.open('" + url + "')");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InfoModalScript", sb.ToString(), false);
                }
                else
                {
                    showMessageModal(eMessage.eWarning, "Data not found", "Invoice for " + ddlMonthPeriod.SelectedItem.ToString() + " - " + txtYearPeriod.Text.Trim() + " still empty.");
                }

            }
            catch (Exception me)
            {
                if (me.Message == "String was not recognized as a valid DateTime.")
                {
                    showMessageModal(eMessage.eError, "Format date", "Please input your format date to dd-MM-yyyy.");
                }
                else
                {
                    showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                }

                bRes = false;
            }
        }

        private void previewInvoice(string iMonth, string iYear, string iSuiteNo)
        {
            //loadRPT();            

            try
            {
                String SQL = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                            "From [dbo].[vwUTLECDMRD] Where Month = " + ddlMonthPeriod.SelectedValue.ToString() +
                            " And Year = " + txtYearPeriod.Text.Trim() +
                            " And SuiteNo = " + iSuiteNo;


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
                            if (dt.Rows.Count > 0) {                               
                                ds = Db.get_list(SQL);

                                if (ds.Tables[0].Rows[0]["DueDate"].ToString() == "" )
                                {
                                    //update print and due date based on text
                                    DateTime iInvPrintDate = DateTime.ParseExact(txtPrintedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    DateTime iInvDueDate = DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                                    String SQL1= "Update tUTLECDMRD " +
                                                "Set InvPrintDate = '" + iInvPrintDate.ToString() + "', InvDueDate = '" + iInvDueDate.ToString() + "' " +
                                                "Where Month = " + ddlMonthPeriod.SelectedValue.ToString() +
                                                " And Year = " + txtYearPeriod.Text.Trim() +
                                                " And SuiteNo = " + iSuiteNo;
                                    try
                                    {
                                        SqlConnection conn1 = new SqlConnection(Db.GetConnectionString());
                                        conn1.Open();

                                        SqlCommand cmd = new SqlCommand(SQL1, conn1);
                                        cmd.CommandType = CommandType.Text;
                                        cmd.ExecuteNonQuery();


                                        String SQL3 = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                                                   "From [dbo].[vwUTLECDMRD] Where Month = " + ddlMonthPeriod.SelectedValue.ToString() +
                                                   " And Year = " + txtYearPeriod.Text.Trim() +
                                                   " And SuiteNo = " + iSuiteNo;

                                        string sConstr3 = Db.GetConnectionString();
                                        using (SqlConnection conn3 = new SqlConnection(sConstr3))
                                        {
                                            using (SqlCommand comm3 = new SqlCommand(SQL3, conn3))
                                            {
                                                conn3.Open();
                                                using (SqlDataAdapter da3 = new SqlDataAdapter(comm3))
                                                {
                                                    dt = new DataTable("tbl");
                                                    da3.Fill(dt);
                                                    if (dt.Rows.Count > 0)
                                                    {
                                                        
                                                    }
                                                }
                                            }
                                        }
                                    }                                                    
                                    catch (Exception me)
                                    {
                                        showMessageModal(eMessage.eError, "Update inv date & due date", me.Message);
                                        bRes = false;
                                    }
                                }
                                
                                bRes = true; }
                        }
                    }
                }

                if (bRes)
                {
                    ReportDocument _rdReportViewer = new ReportDocument();
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptPLN"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());

                    //check file exist or not
                    string fDate = "INV-PLN-" + ds.Tables[0].Rows[0]["InvNo"].ToString();

                    if (!string.IsNullOrEmpty(fDate))
                    {
                        if (!File.Exists(HttpContext.Current.Server.MapPath("~\\RptTemp\\" + fDate + ".pdf")))
                        {
                            _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                        }
                    }

                    string url = "./PDFViewer.aspx?FN=" + fDate + ".pdf";

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("window.open('" + url + "')");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InfoModalScript", sb.ToString(), false);

                    BindGrid();
                }
                else
                {
                    showMessage(eMessage.eWarning, "Data not found", "Invoice for " + ddlMonthPeriod.SelectedItem.ToString() + " - " + txtYearPeriod.Text.Trim() + " still empty.");
                }

            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                bRes = false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bRes = false;

            Vd.isValidLogin();           

            if (!IsPostBack)
            {
                hideMessageBox();

                ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
                txtYearPeriod.Text = DateTime.Now.Year.ToString();

                BindGridSort(ddlSort.SelectedValue.ToString());
            }
            else
            {

            }             
        }

        protected void btnInvPrint_Click(object sender, EventArgs e)
        {
            printInvoice(lblInvID.Text);
            BindGridSort(ddlSort.SelectedValue.ToString());
            upContract.Update();
        }

        protected void btnCreateInv_Click(object sender, EventArgs e)
        {
            createInvNo(lblMonth.Text,lblYear.Text,lblSuite.Text);

            //get print date and due date
            String iSql = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                    "From [dbo].[vwUTLECDMRD] Where ID = " + lblInvID.Text;

            ds = Db.get_list(iSql);

            if (ds.Tables[0].Rows[0]["InvNo"].ToString() != "")
            {
                lblInvNo.Text = ds.Tables[0].Rows[0]["InvNo"].ToString();
                btnCreateInv.Enabled = false;
            }
            else
            {
                lblInvNo.Text = "";
                btnCreateInv.Enabled = true;
            }

            BindGridSort(ddlSort.SelectedValue.ToString());
            upContract.Update();
        }

        protected void btnOpenPDF_Click(object sender, EventArgs e)
        {
            //loadRPT();            

            try
            {
                String SQL = "select* from[dbo].[vwUTLECDMRD] Where Month = " + ddlMonthPeriod.SelectedValue.ToString() + " And Year = " + txtYearPeriod.Text.Trim();


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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptPLN"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());

                    //show crystal report viewer
                    //CrystalReportViewer1.ReportSource = _rdReportViewer;                

                    //check file exist or not
                    string fDate = "INV-PLN-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim();

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
                }else
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

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;
                CreateDynamicGrid(ddlSort.SelectedValue.ToString());

                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                //Response.Output.Write(sw.ToString());
                Response.Write(sw.ToString());
                //Response.Flush();
                Response.End();
            }
        }

        public override void
        VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void ddlMonthPeriod_Change(object sender, EventArgs e)
        {
            BindGridSort(ddlSort.SelectedValue.ToString());
        }

        protected void ddlSort_Change(object sender, EventArgs e)
        {
            BindGridSort(ddlSort.SelectedValue.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridSort(ddlSort.SelectedValue.ToString());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                int index = 0;
                double num;
                string myString = e.CommandArgument.ToString();
                bool isNumber = double.TryParse(myString, out num);

                if (isNumber)
                {
                    index = Convert.ToInt32(e.CommandArgument);
                }
                // getData(kode);

                GridViewRow gvrow = GridView1.Rows[index];
                string kode = GridView1.DataKeys[index].Value.ToString();

                if (e.CommandName.Equals("printRecord"))
                {
                    //previewInvoice(ddlMonthPeriod.SelectedValue.ToString(), txtYearPeriod.Text.Trim(), HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString());
                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    lblInvID.Text = kode;

                    //get print date and due date
                    String iSql = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                            "From [dbo].[vwUTLECDMRD] Where ID = " + kode;

                    ds = Db.get_list(iSql);

                    if (ds.Tables[0].Rows[0]["InvNo"].ToString()!="") {
                        lblInvNo.Text = ds.Tables[0].Rows[0]["InvNo"].ToString();
                        btnCreateInv.Enabled = false;
                    }
                    else
                    {
                        lblInvNo.Text = "";
                        btnCreateInv.Enabled = true;
                    }

                    //Label1.Text = "Print PLN Invoice " + gvrow.Cells[10].Text;
                    lblCompanyName.Text= ds.Tables[0].Rows[0]["Name"].ToString();
                    lblSuite.Text= ds.Tables[0].Rows[0]["SuiteNo"].ToString();
                    lblMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                    lblYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();

                    txtInvPrintedDate.Text = ds.Tables[0].Rows[0]["PrintDate"].ToString();
                    txtInvDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#printInvModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "messageModalScript", sb.ToString(), false);

                    upPrintInv.Update();
                }
            }
            catch (Exception me)
            {
                //showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                //bRes = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //&& e.Row.RowState == DataControlRowState.Alternate
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    if (drv["Status"].ToString().Equals("Void"))
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }                    
                }
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eWarning, "GridView1_RowDataBound", me.Message);
            }
        }
    }
}