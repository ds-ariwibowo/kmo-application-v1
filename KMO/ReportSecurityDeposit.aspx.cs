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
    public partial class ReportSecurityDeposit : System.Web.UI.Page
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
                    iTitle = "SUCCESS. " + iTitle;
                    break;
                case eMessage.eWarning:
                    iTitle = "WARNING!!!. " + iTitle;
                    break;
                case eMessage.eError:
                    iTitle = "ERROR!!!. " + iTitle;
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

        private Boolean checkInvoice()
        {
            Boolean iRes = true;

            if (txtInvPrintedDate.Text == "" || txtInvDueDate.Text == "")
            {
                showMessageModal(eMessage.eError, "Date still empty", "Print date or due date is empty.");
                iRes = false;
            }

            if (txtPeriodFrom.Text == "" || txtPeriodTo.Text == "")
            {
                showMessageModal(eMessage.eError, "Date still empty", "Invoice period date is empty.");
                iRes = false;
            }



            try
            {
                DateTime iPD = DateTime.ParseExact(txtInvPrintedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime iDD = DateTime.ParseExact(txtInvDueDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (iDD.Date < iPD.Date)
                {
                    showMessageModal(eMessage.eError, "Format date", "Due date is lower than print date.");
                    iRes = false;
                }
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eWarning, "Format date", "Please input your format date to dd-MM-yyyy.");
                iRes = false;
            }


            try
            {
                DateTime iPD = DateTime.ParseExact(txtPeriodFrom.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime iDD = DateTime.ParseExact(txtPeriodTo.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (iDD.Date < iPD.Date)
                {
                    showMessageModal(eMessage.eError, "Format date", "Period from date is lower than to date.");
                    iRes = false;
                }
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eWarning, "Format date", "Please input your format date to dd-MM-yyyy.");
                iRes = false;
            }

            return iRes;
        }

        private int checkMaterai(long iTotal)
        {
            int iRes = 6000;

            if (iTotal <= 250000) { iRes = 0; }
            if (iTotal > 250000 && iTotal < 1000000) { iRes = 3000; }
            if (iTotal > 1000000) { iRes = 6000; }

            return iRes;
        }

        private void calculate(string kode, int awal)
        {
            //get print date and due date
            string iSql = "select * from vwInvoiceSource where ID = " + kode;
            ds = Db.get_list(iSql);

            decimal iRentalArea;
            decimal iRentalPeriod;
            long iRentalCharge;
            long iRentalAmount;

            decimal iServiceArea;
            decimal iServicePeriod;
            long iServiceCharge;
            long iServiceAmount;

            decimal iOtherArea;
            decimal iOtherPeriod;
            long iOtherCharge;
            long iOtherAmount;

            long iPhoneLine;
            long iPhoneLineCharge;
            long iPhoneLineAmount;

            long iSubTotal;
            long iPPn;
            long iMaterai;

            long iGrandTotal;

            if (awal == 1)
            {
                txtInvPrintedDate.Text = ds.Tables[0].Rows[0]["PrintDate"].ToString();
                txtInvDueDate.Text = ds.Tables[0].Rows[0]["DueDate"].ToString();

                txtPeriodFrom.Text = ds.Tables[0].Rows[0]["FromPeriod"].ToString();
                txtPeriodTo.Text = ds.Tables[0].Rows[0]["ToPeriod"].ToString();

                iRentalArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositRentalArea"].ToString());
                iRentalPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositRentalPeriod"].ToString());
                iRentalCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["DepositRentalCharge"].ToString());
                
                iServiceArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositServiceArea"].ToString());
                iServicePeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositServicePeriod"].ToString());
                iServiceCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["DepositServiceCharge"].ToString());
                
                iOtherArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositOtherArea"].ToString());
                iOtherPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositOtherPeriod"].ToString());
                iOtherCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["DepositOtherCharge"].ToString());

                iPhoneLine = Convert.ToInt64(ds.Tables[0].Rows[0]["ProsTelpLine"].ToString());
                iPhoneLineCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["ProsTelpLineCharge"].ToString());                
            }
            else
            {
                decimal dVal;
                int iVal;

                if (!Decimal.TryParse(txtRentalPeriod.Text, out dVal)) { showMessageModal(eMessage.eError, "Format period", "Rental period format is not numeric/decimal."); return; }
                if (!Decimal.TryParse(txtServicePeriod.Text, out dVal)) { showMessageModal(eMessage.eError, "Format period", "Service period format is not numeric/decimal."); return; }
                if (!Decimal.TryParse(txtOtherPeriod.Text, out dVal)) { showMessageModal(eMessage.eError, "Format period", "Other period format is not numeric/decimal."); return; }
                if (!Decimal.TryParse(txtOtherArea.Text, out dVal)) { showMessageModal(eMessage.eError, "Format period", "Other area is not numeric/decimal."); return; }
                if (!int.TryParse(txtOtherCharge.Text, out iVal)) { showMessageModal(eMessage.eError, "Format period", "Other charge is not numeric/decimal."); return; }


                iRentalArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositRentalArea"].ToString());
                iRentalPeriod = Convert.ToDecimal(txtRentalPeriod.Text);
                iRentalCharge = Convert.ToInt64(Convert.ToInt64(ds.Tables[0].Rows[0]["DepositRentalCharge"].ToString()));

                iServiceArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositServiceArea"].ToString());
                iServicePeriod = Convert.ToDecimal(txtServicePeriod.Text);
                iServiceCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["DepositServiceCharge"].ToString());

                iOtherArea = Convert.ToDecimal(txtOtherArea.Text);
                iOtherPeriod = Convert.ToDecimal(txtOtherPeriod.Text);
                iOtherCharge = Convert.ToInt64(txtOtherCharge.Text);

                iPhoneLine= Convert.ToInt64(txtPhoneLine.Text);
                iPhoneLineCharge= Convert.ToInt64(txtPhoneLineCharge.Text);
            }

            iRentalAmount = Convert.ToInt64(String.Format("{0:0}", iRentalArea * iRentalPeriod * iRentalCharge));
            iServiceAmount = Convert.ToInt64(String.Format("{0:0}", iServiceArea * iServicePeriod * iServiceCharge));
            iOtherAmount = Convert.ToInt64(String.Format("{0:0}", iOtherArea * iOtherPeriod * iOtherCharge));
            iPhoneLineAmount = Convert.ToInt64(String.Format("{0:0}", iPhoneLine * iPhoneLineCharge));

            iSubTotal = iRentalAmount + iServiceAmount + iOtherAmount + iPhoneLineAmount;
            iPPn = 0;
            iMaterai = checkMaterai(iSubTotal);

            iGrandTotal = iSubTotal + iPPn + iMaterai;

            lblRentalArea.Text = iRentalArea.ToString();
            txtRentalPeriod.Text = iRentalPeriod.ToString();
            lblRentalPeriodMY.Text = ds.Tables[0].Rows[0]["DepositRentalPeriodMY"].ToString();
            lblRentalCharge.Text = string.Format("{0:N0}", iRentalCharge);
            lblRentalAmount.Text = string.Format("{0:N0}", iRentalAmount);

            lblServiceArea.Text = iServiceArea.ToString();
            txtServicePeriod.Text = iServicePeriod.ToString();
            lblServicePeriodMY.Text = ds.Tables[0].Rows[0]["DepositServicePeriodMY"].ToString();
            lblServiceCharge.Text = string.Format("{0:N0}", iServiceCharge);
            lblServiceAmount.Text = string.Format("{0:N0}", iServiceAmount);

            txtOtherArea.Text = iOtherArea.ToString();
            txtOtherPeriod.Text = iOtherPeriod.ToString();
            lblOtherPeriodMY.Text = ds.Tables[0].Rows[0]["DepositRentalPeriodMY"].ToString();
            txtOtherCharge.Text = iOtherCharge.ToString();
            lblOtherAmount.Text = string.Format("{0:N0}", iOtherAmount);

            txtPhoneLine.Text = iPhoneLine.ToString();
            txtPhoneLineCharge.Text = iPhoneLineCharge.ToString();
            lblPhoneLineAmount.Text = string.Format("{0:N0}", iPhoneLineAmount);

            lblSubTotal.Text = string.Format("{0:N0}", iSubTotal);
            lblPPN.Text = string.Format("{0:N0}", iPPn);
            lblMaterai.Text = string.Format("{0:N0}", iMaterai);
            lblGrandTotal.Text = string.Format("{0:N0}", iGrandTotal);
        }

        private void BindGridSort(string iOrderBy)
        {
            string iSql = "Select Distinct * from vwInvoiceSource_All Where InvSource= 'SEC'  and Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
            //string iSql2 = "Select * from [vwCopPln] Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
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

        private void CreateDynamicGrid(string iOrderBy)
        {
            try
            {
                string iQuery;
                iQuery = "Select Distinct * from vwInvList_SEC_All Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;

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

        private void printInvoice(string iID)
        {
            //loadRPT();            

            try
            {
                String SQL = "select Distinct * from [dbo].[vwInvoiceSource] Where ID = " + iID;

                if (!checkInvoice()) { return; }

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

                                //if (ds.Tables[0].Rows[0]["DueDate"].ToString() == "")
                                //{
                                //update print and due date based on text
                                DateTime iInvPrintDate = DateTime.ParseExact(txtInvPrintedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                DateTime iInvDueDate = DateTime.ParseExact(txtInvDueDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                DateTime iInvPeriodFrom = DateTime.ParseExact(txtPeriodFrom.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                                DateTime iInvPeriodTo = DateTime.ParseExact(txtPeriodTo.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                                String SQL1 = "Update tInvoiceSource " +
                                                "Set InvPrintDate = '" + iInvPrintDate.ToString() + "', InvDueDate = '" + iInvDueDate.ToString() + "', " +
                                                "PeriodFrom = '" + iInvPeriodFrom.ToString() + "', PeriodTo = '" + iInvPeriodTo.ToString() + "', " +
                                                "DepositRentalPeriod = " + txtRentalPeriod.Text + ", DepositServicePeriod = " + txtServicePeriod.Text + ", " +
                                                "DepositOtherArea = " + txtOtherArea.Text + ", DepositOtherPeriod = " + txtOtherPeriod.Text + ", DepositOtherCharge = " + txtOtherCharge.Text + " " +                                                
                                                "Where ID = " + iID;

                                string SQL2 = "Update mCFS Set ProsTelpLine = " + txtPhoneLine.Text + ", ProsTelpLineCharge = " + txtPhoneLineCharge.Text + " " +
                                                "Where ID = " + ds.Tables[0].Rows[0]["CFSID"].ToString();

                                try
                                {
                                    SqlConnection conn1 = new SqlConnection(Db.GetConnectionString());
                                    conn1.Open();

                                    SqlCommand cmd = new SqlCommand(SQL1, conn1);
                                    cmd.CommandType = CommandType.Text;
                                    cmd.ExecuteNonQuery();

                                    SqlCommand cmd2 = new SqlCommand(SQL2, conn1);
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.ExecuteNonQuery();

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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptSEC"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, iID);

                    //check file exist or not
                    string fDate = "INV-SEC-" + ds.Tables[0].Rows[0]["InvNo"].ToString();

                    if (!string.IsNullOrEmpty(fDate))
                    {
                        //if (!File.Exists(HttpContext.Current.Server.MapPath("~\\RptTemp\\" + fDate + ".pdf")))
                        //{
                        _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                        //}
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

        protected void ddlMonthPeriod_Change(object sender, EventArgs e)
        {
            BindGridSort(ddlSort.SelectedValue.ToString());
        }

        protected void ddlSort_Change(object sender, EventArgs e)
        {
            BindGridSort(ddlSort.SelectedValue.ToString());
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
        protected void btnInvPrint_Click(object sender, EventArgs e)
        {
            printInvoice(lblInvID.Text);
            BindGridSort(ddlSort.SelectedValue.ToString());
            upContract.Update();
        }

        protected void txtCalculate_TextChanged(object sender, EventArgs e)
        {
            calculate(lblInvID.Text, 0);
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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptSEC"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());

                    //show crystal report viewer
                    //CrystalReportViewer1.ReportSource = _rdReportViewer;

                    bRes = true;

                    //check file exist or not
                    string fDate = "INV-SEC-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim();

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
                    //previewInvoice(ddlMonthPeriod.SelectedValue.ToString(), txtYearPeriod.Text.Trim(), HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString(), kode);
                    //BindGrid();

                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    Label1.Text = "Print SEC Invoice No INV:SEC-" + gvrow.Cells[2].Text + " | " + gvrow.Cells[10].Text;
                    lblInvID.Text = kode;
                    calculate(kode, 1);

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
                showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                bRes = false;
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
                    if (drv["InvStatus"].ToString().Equals("Void"))
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