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
    public partial class copSEC : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;
        Boolean bEdit;
        Boolean bAwal;

        enum eMessage : byte { eSuccess = 1, eWarning = 2, eError = 3 };

        private void hideMessageBox()
        {
            mySuccess.Visible = false;
            lblMySuccess.Text = "";
            myWarning.Visible = false;
            lblMyWarning.Text = "";
            myError.Visible = false;
            lblMyError.Text = "";

            lblMessageModal_Title.Text = "";
            lblMessageModal_Body.Text = "";
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

        private void clearText()
        {
            rblPaymentMethod.SelectedIndex = 0;
            txtReceivedDate.Text = "";
            txtPaymentNo.Text = "";
            txtBankName.Text = "";
            txtPaymentNominal.Text = "";
            txtVariance.Text = "";
            txtPPhFnl.Text = "";
            txtBankCharge.Text = "";
            txtOther.Text = "";
            txtTotal.Text = "";
            txtNote.Text = "";

            lblOkNg.Text = "";
            lblTotalOkNg.Text = "";

            btnSave.Enabled = false;
            txtPaymentNo.Enabled = false;
            txtBankName.Enabled = false;

            Session["EditCopSEC"] = false;
        }

        private Boolean checkData()
        {
            Boolean iRes = true;

            try
            {
                if (txtReceivedDate.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Received date is empty."); }
                if (txtPaymentNominal.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Payment nominal is empty."); }
                DateTime iDate;
                try
                {
                    iDate = DateTime.ParseExact(txtReceivedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception me)
                {
                        iRes = false; showMessageModal(eMessage.eWarning, "Format date", "Please input your format date to dd-MM-yyyy.");
                }


            }
            catch (Exception me)
            {
                iRes = false;
                //showMessage(eMessage.eError, "checkData", me.Message);
                showMessageModal(eMessage.eError, "checkData", me.Message);
            }
            return iRes;
        }

        private void exportReport(CrystalDecisions.CrystalReports.Engine.ReportClass selectedReport, CrystalDecisions.Shared.ExportFormatType eft)
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

        private void executeAdd(string iUserID, string iInvID, string iPaymentMethod, string iNoPayment, string iBankName,
                                string iReceivedDate, string iPaymentNominal, string iPPh, string iBankCharge, string iOther, string iNote)
        {
            try
            {                
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertCOP_SEC ", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //iNoPayment = "122-539-9998";
                //iBankName = "BNI - Cabang Senayan a/n PT Primanusa Graha";

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@InvID", iInvID);
                cmd.Parameters.AddWithValue("@PaymentMethod", iPaymentMethod);
                cmd.Parameters.AddWithValue("@NoPayment", iNoPayment);
                cmd.Parameters.AddWithValue("@BankName", iBankName);
                cmd.Parameters.AddWithValue("@ReceivedDate", iReceivedDate);
                cmd.Parameters.AddWithValue("@PaymentNominal", iPaymentNominal);
                cmd.Parameters.AddWithValue("@PPh", iPPh);
                cmd.Parameters.AddWithValue("@BankCharge", iBankCharge);
                cmd.Parameters.AddWithValue("@Other", iOther);
                cmd.Parameters.AddWithValue("@Note", iNote);

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

        private void executeEdit(string iUserID, string iID, string iInvID, string iPaymentMethod, string iNoPayment, string iBankName,
                                string iReceivedDate, string iPaymentNominal, string iPPh, string iBankCharge, string iOther, string iNote)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCOP_SEC ", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //iNoPayment = "122-539-9998";
                //iBankName = "BNI - Cabang Senayan a/n PT Primanusa Graha";

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@InvID", iInvID);
                cmd.Parameters.AddWithValue("@PaymentMethod", iPaymentMethod);
                cmd.Parameters.AddWithValue("@NoPayment", iNoPayment);
                cmd.Parameters.AddWithValue("@BankName", iBankName);
                cmd.Parameters.AddWithValue("@ReceivedDate", iReceivedDate);
                cmd.Parameters.AddWithValue("@PaymentNominal", iPaymentNominal);
                cmd.Parameters.AddWithValue("@PPh", iPPh);
                cmd.Parameters.AddWithValue("@BankCharge", iBankCharge);
                cmd.Parameters.AddWithValue("@Other", iOther);
                cmd.Parameters.AddWithValue("@Note", iNote);

                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "executeEdit", me.Message);
            }
        }

        private void previewInvoice(string iInvNo)
        {
            //loadRPT();            

            try
            {
                String SQL = "select * from [dbo].[vwInvoiceSource] Where InvNo = '" + iInvNo + "' And InvSource = 'SEC' ";


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
                            if (dt.Rows.Count > 0) { ds = Db.get_list(SQL); bRes = true; }
                        }
                    }
                }

                if (bRes)
                {
                    ReportDocument _rdReportViewer = new ReportDocument();
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptSEC"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ds.Tables[0].Rows[0]["ID"].ToString());

                    //check file exist or not
                    string fDate = "INV-SEC-" + ds.Tables[0].Rows[0]["InvNo"].ToString();

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
                showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                bRes = false;
            }
        }

        private void BindGridSort(string iOrderBy)
        {
            string iSql = "Select * From vwCopList_SEC_All Where InvSource = 'SEC' And Month = " + ddlMonthPeriod.Text + " And Year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
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
                iQuery= "SELECT [ID],[InvNo],[Month],[Year],[CID],[Name],[Address],[City],[ZIP],[PIC1Name],[ContractNo] " +
                        ",[MemorandumNo],[Note],[Floor],[SuiteNo],[KVA] " +
                        ",[RentalArea],[RentalPeriod],[RentalPeriodMY],[RentalCharge],[RentalAmount] " +
                        ",[ServiceArea],[ServicePeriod],[ServicePeriodMY],[ServiceCharge],[ServiceAmount] " +
                        ",[OtherArea],[OtherPeriod],[OtherPeriodMY],[OtherCharge],[OtherAmount] " +
                        ",[SecTotalAmount],[SecMaterai],[SecGrandTotal] " +
                        ",[PeriodFrom],[PeriodTo],[InvSource],[InvPrintDate],[InvDueDate],[PaymentMethod] " +
                        ",[NoPayment],[BankName],[ReceivedDate],[PaymentNominal],[PPh],[BankCharge] " +
                        ",[Other],[Notes],[Status] " +
                        "FROM [vwCOPList_SEC_All_Export] " +
                        "Where InvSource = 'SEC' And Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;

                ds = Db.get_list(iQuery);

                bool hasRows = ds.Tables.Cast<DataTable>()
                                               .Any(table => table.Rows.Count != 0);

                //execute the select statement
                DataView dvProducts = ds.Tables[0].DefaultView;
                DataTable dtProducts = dvProducts.Table;

                BoundField boundField = null;
                bAwal = true;
                if (bAwal)
                {
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
                }

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

        private Boolean checkDB(string iID)
        {
            Boolean iRes = false;
            try
            {
                String SQL = "Exec spGetCOP_SEC_Query " + iID;


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
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "getData", me.Message);
            }
            return iRes;
        }

        private int checkMaterai(int iTotal)
        {
            int iRes = 6000;

            if (iTotal<=250000){ iRes= 0; }
            if (iTotal > 250000 && iTotal < 1000000) { iRes = 3000; }
            if (iTotal > 1000000) { iRes = 6000; }

            return iRes;
        }

        private void getData(string iID)
        {
            try
            {
                ds = Db.get_list("execute spGetInvoiceSourceCOP_Query " + iID);
                

                decimal iRentalArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositRentalArea"].ToString());
                decimal iRentalPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositRentalPeriod"].ToString());
                int iRentalCharge= Convert.ToInt32(ds.Tables[0].Rows[0]["DepositRentalCharge"].ToString());
                int iRentalAmount = Convert.ToInt32(String.Format("{0:0}", iRentalArea * iRentalPeriod * iRentalCharge));

                decimal iServiceArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositServiceArea"].ToString());
                decimal iServicePeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositServicePeriod"].ToString());
                int iServiceCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["DepositServiceCharge"].ToString());
                int iServiceAmount = Convert.ToInt32(String.Format("{0:0}", iServiceArea * iServicePeriod * iServiceCharge));

                decimal iOtherArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositOtherArea"].ToString());
                decimal iOtherPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["DepositOtherPeriod"].ToString());
                int iOtherCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["DepositOtherCharge"].ToString());
                int iOtherAmount = Convert.ToInt32(String.Format("{0:0}", iOtherArea * iOtherPeriod * iOtherCharge));

                int iPhoneLine = Convert.ToInt32(ds.Tables[0].Rows[0]["ProsTelpLine"].ToString());
                int iPhoneLineCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["ProsTelpLineCharge"].ToString());
                int iPhoneLineAmount = Convert.ToInt32(ds.Tables[0].Rows[0]["ProsTelpLineAmount"].ToString());

                int iSubTotal = iRentalAmount + iServiceAmount + iOtherAmount + iPhoneLineAmount;
                int iPPn = 0;//Convert.ToInt32(String.Format("{0:0}", (iSubTotal) * (0.1)));
                int iMaterai = checkMaterai(iSubTotal);

                int iGrandTotal = iSubTotal + iPPn + iMaterai;

                lblMeterID.Text = iID;
                lblCopNo.Text = ds.Tables[0].Rows[0]["InvNo"].ToString();
                lblCopPeriod.Text = ds.Tables[0].Rows[0]["Month"].ToString() + "-" + ds.Tables[0].Rows[0]["Year"].ToString();
                lblCID.Text = ds.Tables[0].Rows[0]["CID"].ToString();
                lblSuiteNo.Text = ds.Tables[0].Rows[0]["SuiteNo"].ToString();
                lblCompanyName.Text = ds.Tables[0].Rows[0]["Name"].ToString();

                lblRentalArea.Text = iRentalArea.ToString();
                lblRentalPeriod.Text = iRentalPeriod.ToString();
                lblRentalPeriodMY.Text=ds.Tables[0].Rows[0]["RentalPeriodMY"].ToString();
                lblRentalCharge.Text= string.Format("{0:N0}", ds.Tables[0].Rows[0]["RentalCharge"]);
                lblRentalAmount.Text= string.Format("{0:N0}", iRentalAmount);

                lblServiceArea.Text = iServiceArea.ToString();
                lblServicePeriod.Text = iServicePeriod.ToString();
                lblServicePeriodMY.Text = ds.Tables[0].Rows[0]["ServicePeriodMY"].ToString();
                lblServiceCharge.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["ServiceCharge"]);
                lblServiceAmount.Text = string.Format("{0:N0}", iServiceAmount);

                lblOtherArea.Text = iOtherArea.ToString();
                lblOtherPeriod.Text = iOtherPeriod.ToString();
                lblOtherPeriodMY.Text = ds.Tables[0].Rows[0]["OtherPeriodMY"].ToString();
                lblOtherCharge.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["OtherCharge"]);
                lblOtherAmount.Text = string.Format("{0:N0}", iOtherAmount);
                
                lblPhoneLine.Text= string.Format("{0:N0}", iPhoneLine);
                lblPhoneLineCharge.Text = string.Format("{0:N0}", iPhoneLineCharge);
                lblPhoneLineAmount.Text = string.Format("{0:N0}", iPhoneLineAmount);

                lblSubTotal.Text= string.Format("{0:N0}", iSubTotal);
                lblPPN.Text = string.Format("{0:N0}", iPPn);
                lblMaterai.Text = string.Format("{0:N0}", iMaterai);
                lblGrandTotal.Text = string.Format("{0:N0}", iGrandTotal);
                lblGT.Text = iGrandTotal.ToString();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "getData", me.Message);
            }
        }

        private void getEditData(string iID)
        {
            try
            {
                ds = Db.get_list("exec [spGetCOP_SEC_Query] " + iID);
                bRes = false;

                if (ds.Tables[0].Rows.Count != 0)
                {

                    int iPaymentNominal = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentNominal"].ToString());
                    //int iVariance = Convert.ToInt32(lblGT.Text) - iPaymentNominal;
                    int iPPh = Convert.ToInt32(ds.Tables[0].Rows[0]["PPh"].ToString());
                    int iBankCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["BankCharge"].ToString());
                    int iOther = Convert.ToInt32(ds.Tables[0].Rows[0]["Other"].ToString());
                    int iTotal = iPPh + iBankCharge + iOther;

                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Direct Transfer") { rblPaymentMethod.SelectedIndex = 0; txtPaymentNo.Enabled = false; txtBankName.Enabled = false; }
                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Check") { rblPaymentMethod.SelectedIndex = 1; txtPaymentNo.Enabled = true; txtBankName.Enabled = true; }
                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Giro") { rblPaymentMethod.SelectedIndex = 2; txtPaymentNo.Enabled = true; txtBankName.Enabled = true; }

                    lblID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblMeterID.Text = ds.Tables[0].Rows[0]["InvID"].ToString();

                    txtReceivedDate.Text = ds.Tables[0].Rows[0]["RDate"].ToString();
                    txtPaymentNo.Text = ds.Tables[0].Rows[0]["NoPayment"].ToString();
                    txtBankName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                    txtPaymentNominal.Text = iPaymentNominal.ToString();
                    //txtVariance.Text = iVariance.ToString();
                    txtPPhFnl.Text = iPPh.ToString();
                    txtBankCharge.Text = iBankCharge.ToString();
                    txtOther.Text = iOther.ToString();
                    txtTotal.Text = iTotal.ToString();
                    txtNote.Text = ds.Tables[0].Rows[0]["Note"].ToString(); ;


                    bRes = true;
                }
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "getData", me.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bRes = false;
            txtPaymentNo.Enabled = false;
            txtBankName.Enabled = false;

            Vd.isValidLogin();

            if (!IsPostBack)
            {
                bAwal = true;

                hideMessageBox();
                clearText();

                ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
                txtYearPeriod.Text = DateTime.Now.Year.ToString();

                BindGridSort(ddlSort.SelectedValue.ToString());
            }
            else
            {

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {


            try
            {
                string iSuiteNo = Session["iSuiteNo"].ToString();

                String SQL = "select * from[dbo].[vwInvoiceSource] Where InvSource = 'SEC' And Month = " + ddlMonthPeriod.SelectedValue.ToString() + " And Year = " + txtYearPeriod.Text.Trim() + " And SuiteNo = " + iSuiteNo;


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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPSEC"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());
                    _rdReportViewer.SetParameterValue(2, iSuiteNo);

                    //show crystal report viewer
                    //CrystalReportViewer1.ReportSource = _rdReportViewer;                

                    //check file exist or not
                    string fDate = "COP-SEC-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim() + "-" + iSuiteNo;

                    if (!string.IsNullOrEmpty(fDate))
                    {
                        _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                    }

                    string url = "./PDFViewer.aspx?FN=" + fDate + ".pdf";
                    string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                    hideMessageBox();
                    Session["iSuiteNo"] = null;
                }
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eError, "btnUpdate_Click", me.Message);
                bRes = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                

                DateTime iDate = DateTime.ParseExact(txtReceivedDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                if (Session["EditCopSEC"].ToString() =="True")
                {
                    executeEdit(HttpContext.Current.Session["userid"].ToString(), lblID.Text, lblMeterID.Text, rblPaymentMethod.SelectedValue.ToString(),
                            txtPaymentNo.Text, txtBankName.Text, iDate.ToString(), txtPaymentNominal.Text, txtPPhFnl.Text, txtBankCharge.Text, txtOther.Text, txtNote.Text);
                }
                else
                {
                    executeAdd(HttpContext.Current.Session["userid"].ToString(), lblMeterID.Text, rblPaymentMethod.SelectedValue.ToString(),
                            txtPaymentNo.Text, txtBankName.Text, iDate.ToString(), txtPaymentNominal.Text, txtPPhFnl.Text, txtBankCharge.Text, txtOther.Text, txtNote.Text);
                }
                


                //create pdf file
                //btnUpdate_Click(sender, e);
                try
                {
                    string iSuiteNo = Session["iSuiteNo"].ToString();
                    string iInvSEC = Session["iINVSEC"].ToString();

                    String SQL = "SELECT * FROM  dbo.vwCOPSEC WHERE  ID = " + lblMeterID.Text;

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
                        
                        string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPSEC"].ToString());
                        _rdReportViewer.Load(reportPath);

                        _rdReportViewer.SetDataSource(dt);

                        _rdReportViewer.SetDatabaseLogon(ConfigurationManager.AppSettings["rptUser"].ToString(), ConfigurationManager.AppSettings["rptPassword"].ToString());

                        _rdReportViewer.SetParameterValue(0, txtYearPeriod.Text.Trim());
                        _rdReportViewer.SetParameterValue(1, ddlMonthPeriod.SelectedValue.ToString());
                        _rdReportViewer.SetParameterValue(2, iSuiteNo);

                        string iTgl = "";
                        
                        string fDate = "COP-SEC-" + iInvSEC + iTgl;

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

                        BindGridSort(ddlSort.SelectedValue.ToString());
                        upContract.Update();

                        hideMessageBox();
                        Session["iSuiteNo"] = null;
                        Session["EditCopSEC"] = null;
                    }                    

                }
                catch (Exception me)
                {
                    showMessageModal(eMessage.eError, "btnUpdate_Click", me.Message);
                    bRes = false;
                }
            }
        }

        protected void rblPaymentMethod_Change(object sender, EventArgs e)
        {
            txtPaymentNo.Text = "";
            txtBankName.Text = "";

            if (rblPaymentMethod.SelectedValue== "Direct Transfer")
            {
                txtPaymentNo.Text = "122-539-9998";
                txtBankName.Text = "BNI cabang Senayan";

                txtPaymentNo.Enabled = false;
                txtBankName.Enabled = false;
            }
            else
            {
                txtPaymentNo.Enabled = true;
                txtBankName.Enabled = true;
            }
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try {
                if (txtPaymentNominal.Text == "") { txtPaymentNominal.Text = "0"; }
                if (txtPPhFnl.Text == "") { txtPPhFnl.Text = "0"; }
                if (txtBankCharge.Text == "") { txtBankCharge.Text = "0"; }
                if (txtOther.Text == "") { txtOther.Text = "0"; }

                int iNilai = Convert.ToInt32(lblGT.Text) - Convert.ToInt32(txtPaymentNominal.Text);
                int iBeda = Convert.ToInt32(txtPPhFnl.Text) + Convert.ToInt32(txtBankCharge.Text) + Convert.ToInt32(txtOther.Text);
                int iTotal = iNilai - iBeda;

                //cek variance
                if (iNilai > 0) { lblOkNg.Text = "NG"; } else { lblOkNg.Text = "OK"; }

                //cek pph+bank charge, other
                if (iNilai != iBeda) { lblTotalOkNg.Text = "NG"; btnSave.Enabled = false; } else { lblTotalOkNg.Text = "OK"; btnSave.Enabled = true; }

                txtVariance.Text = iNilai.ToString();
                txtTotal.Text = iTotal.ToString();
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eError, "btnCalculate_Click", me.Message);
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
                //BindGridSort(ddlSort.SelectedValue.ToString());
                CreateDynamicGrid(ddlSort.SelectedValue.ToString());

                //GridView2.HeaderRow.BackColor = Color.White;
                //foreach (TableCell cell in GridView2.HeaderRow.Cells)
                //{
                //    cell.BackColor = GridView2.HeaderStyle.BackColor;
                //}
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
            //BindGrid();
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

                clearText();

                GridViewRow gvrow = GridView1.Rows[index];
                string kode = GridView1.DataKeys[index].Value.ToString();
                Session["iSuiteNo"] = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();
                Session["iINVSEC"] = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

                if (e.CommandName.Equals("voidRecord"))
                {

                }
                else if (e.CommandName.Equals("previewRecord"))
                {
                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    previewInvoice(HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString());
                }
                else if (e.CommandName.Equals("printRecord"))
                {
                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    try
                    {
                        Session["EditCopSEC"] = false;

                        string iSuiteNo = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();

                        String SQL = "SELECT * FROM  dbo.vwCOPSEC WHERE  ID = " + kode;


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

                            string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPSEC"].ToString());
                            _rdReportViewer.Load(reportPath);

                            _rdReportViewer.SetDataSource(dt);

                            _rdReportViewer.SetDatabaseLogon(ConfigurationManager.AppSettings["rptUser"].ToString(), ConfigurationManager.AppSettings["rptPassword"].ToString());

                            _rdReportViewer.SetParameterValue(0, txtYearPeriod.Text.Trim());
                            _rdReportViewer.SetParameterValue(1, ddlMonthPeriod.SelectedValue.ToString());
                            _rdReportViewer.SetParameterValue(2, iSuiteNo);

                            _rdReportViewer.RefreshReport += new EventHandler((object o, EventArgs e2) =>
                            {
                                _rdReportViewer.SetParameterValue(0, txtYearPeriod.Text.Trim());
                                _rdReportViewer.SetParameterValue(1, ddlMonthPeriod.SelectedValue.ToString());
                                _rdReportViewer.SetParameterValue(2, iSuiteNo);
                            });

                            _rdReportViewer.Refresh();

                            //check file exist or not
                            string fDate = "COP-SEC-" + HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

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

                            hideMessageBox();
                            Session["iSuiteNo"] = null;
                        }
                        else
                        {
                            showMessageModal(eMessage.eError, "COP:SEC Not Found", "COP:SEC document for Suite No : " + iSuiteNo + " not found or has not made the payment.");

                        }
                    }
                    catch (Exception me)
                    {
                        showMessageModal(eMessage.eError, "GridView1_RowCommand", me.Message);
                        bRes = false;
                    }
                }
                else if (e.CommandName.Equals("editRecord"))
                {
                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    clearText();
                    getData(kode);
                    getEditData(kode);


                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#infoModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InfoModalScript", sb.ToString(), false);

                    UpdatePanel1.Update();
                }
                else if (e.CommandName.Equals("paymentRecord"))
                {
                    if (HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString() == "Void")
                    {
                        showMessageModal(eMessage.eWarning, "Invoice has been void", "Invoice has been canceled can not be processed further for payment.");
                        return;
                    }

                    checkDB(kode);
                    getData(kode);
                    getEditData(kode);
                    if (bRes)
                    {
                        Session["EditCopSEC"] = true;
                    }
                    else
                    {

                    }                

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script type='text/javascript'>");
                    sb.Append("$('#infoModal').modal('show');");
                    sb.Append(@"</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InfoModalScript", sb.ToString(), false);

                    UpdatePanel1.Update();
                }
            }
            catch (Exception me)
            {
            }            
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //&& e.Row.RowState == DataControlRowState.Alternate
                if (e.Row.RowType == DataControlRowType.DataRow )
                {
                    DataRowView drv = e.Row.DataItem as DataRowView;
                    if (drv["Status"].ToString().Equals("Void"))
                    {
                        e.Row.BackColor = System.Drawing.Color.Red;
                    }
                    else if (drv["Status"].ToString().Equals("Paid"))
                    {
                        e.Row.BackColor = System.Drawing.Color.GreenYellow;
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