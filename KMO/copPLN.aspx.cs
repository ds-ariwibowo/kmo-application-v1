﻿using System;
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
    public partial class copPLN : System.Web.UI.Page
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

            Session["EditCopPln"] = false;
        }

        private Boolean checkData()
        {
            Boolean iRes = true;

            try
            {
                if (txtReceivedDate.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Received date is empty."); }
                //if (txtPaymentNo.Text.Trim() == "") { iRes = false; showMessage(eMessage.eWarning, "Data empty", "No payment is empty."); }
                //if (txtBankName.Text.Trim() == "") { iRes = false; showMessage(eMessage.eWarning, "Data empty", "Bank name is empty."); }
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

        private void executeAdd(string iUserID, string iMeterID, string iInvNo, string iPaymentMethod, string iNoPayment, string iBankName,
                                string iReceivedDate, string iPaymentNominal, string iPPh, string iBankCharge, string iOther, string iNote)
        {
            try
            {                
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertCOP_PLN ", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //iNoPayment = "122-539-9998";
                //iBankName = "BNI - Cabang Senayan a/n PT Primanusa Graha";

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@MeterID", iMeterID);
                cmd.Parameters.AddWithValue("@InvNo", iInvNo);
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

        private void executeEdit(string iUserID, string iID, string iMeterID, string iInvNo, string iPaymentMethod, string iNoPayment, string iBankName,
                                string iReceivedDate, string iPaymentNominal, string iPPh, string iBankCharge, string iOther, string iNote)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCOP_PLN ", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                //iNoPayment = "122-539-9998";
                //iBankName = "BNI - Cabang Senayan a/n PT Primanusa Graha";

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@MeterID", iMeterID);
                cmd.Parameters.AddWithValue("@InvNo", iInvNo);
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

        private void previewInvoice(string iMonth, string iYear, string iSuiteNo)
        {
            //loadRPT();            

            try
            {
                String SQL = "select * from[dbo].[vwUTLECDMRD] Where Month = " + iMonth + 
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
                            if (dt.Rows.Count > 0) { ds = Db.get_list(SQL); bRes = true; }
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
                    showMessage(eMessage.eWarning, "Data not found", "Invoice for " + ddlMonthPeriod.SelectedItem.ToString() + " - " + txtYearPeriod.Text.Trim() + " still empty.");
                }

            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                bRes = false;
            }
        }

        private void previewInvoicePLN(string iID)
        {
            //loadRPT();            

            try
            {
                String SQL = "select * , format(InvDueDate,'dd-MM-yyyy') as DueDate, format(InvPrintDate,'dd-MM-yyyy') as PrintDate " +
                            "From [dbo].[vwUTLECDMRD] Where InvNo = '" + iID + "'";


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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptPLN"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ds.Tables[0].Rows[0]["ID"].ToString());

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
                    showMessage(eMessage.eWarning, "Data not found", "Invoice for " + ddlMonthPeriod.SelectedItem.ToString() + " - " + txtYearPeriod.Text.Trim() + " still empty.");
                }

            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eError, "previewInvoice", me.Message);
                bRes = false;
            }
        }

        private void BindGrid()
        {
            string iSql = "execute spGetUTLECDMRD_Query " + ddlMonthPeriod.Text + ", " + txtYearPeriod.Text + " ";
            string iSql2 = "Select * from [vwCopPln] Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " ";
            try
            {
                GridView1.DataSource = Db.get_list(iSql);
                GridView1.DataBind();

                GridView2.DataSource = Db.get_list(iSql2);
                GridView2.DataBind();
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
            string iSql = "Select * From vwCopList_Pln_All Where Month = " + ddlMonthPeriod.Text + " And Year = " + txtYearPeriod.Text + " Order By " + iOrderBy;
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
                iQuery= "Select * from vwCopList_Pln_All_Export Where Month = " + ddlMonthPeriod.Text + " and year = " + txtYearPeriod.Text + " Order By " + iOrderBy;

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
                String SQL = "Exec spGetCOP_PLN_Query " + iID;


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
                ds = Db.get_list("execute spGetUTLECDMRDIDCOP_Query " + iID);
                

                int iBiayaPerKWh = Convert.ToInt32(ds.Tables[0].Rows[0]["KWhRate"].ToString());
                    int iNonACTotalRp = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalReadingNonACMeter"].ToString()) * iBiayaPerKWh;

                    int iACIndoorKWh = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalReadingACMeter"].ToString());
                    int iTotalACOutDoorPerSuiteP = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalACOutdoorPerSuiteP"].ToString());
                    int iTotalACKWh = iACIndoorKWh + iTotalACOutDoorPerSuiteP;
                    int iTotalACRp = iTotalACKWh * iBiayaPerKWh;

                    int iSubTotal = iNonACTotalRp + iTotalACRp;
                    //int iPPj = Convert.ToInt32((Convert.ToDecimal(ds.Tables[0].Rows[0]["CostPPJ"].ToString()) / 100) * iSubTotal);
                    int iPPj = Convert.ToInt32(String.Format("{0:0}", (Convert.ToDecimal(ds.Tables[0].Rows[0]["CostPPJ"].ToString()) / 100) * iSubTotal, 0));
                    int iBiayaAdmin = Convert.ToInt32(String.Format("{0:0}", (Convert.ToDecimal(ds.Tables[0].Rows[0]["CostAdmin"].ToString()) / 100) * (iSubTotal + iPPj), 0));

                    int iPPn = Convert.ToInt32(String.Format("{0:0}", (iSubTotal + iPPj + iBiayaAdmin) * (0.1)));
                    //int iMaterai=Convert.ToInt32(ds.Tables[0].Rows[0]["CostMaterai"].ToString());
                    int iMaterai = checkMaterai(iSubTotal);

                    int iGrandTotal = iSubTotal + iPPj + iBiayaAdmin + iPPn + iMaterai;

                    lblMeterID.Text = iID;
                    lblCopPlnNo.Text = ds.Tables[0].Rows[0]["InvNo"].ToString();
                    lblCopPeriod.Text = ds.Tables[0].Rows[0]["Bulan"].ToString() + "-" + ds.Tables[0].Rows[0]["Year"].ToString();
                    lblCID.Text = ds.Tables[0].Rows[0]["CID"].ToString();
                    lblSuiteNo.Text = ds.Tables[0].Rows[0]["SuiteNo"].ToString();
                    lblCompanyName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    lblBiayaKwh.Text = string.Format("{0:N0}", iBiayaPerKWh);

                    lblNonACAkhir.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["ReadingNonAC"]);
                    lblNonACAwal.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["ReadingNonACLast"]);
                    lblNonACKwh.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["TotalReadingNonACMeter"]);
                    lblNonACTotal.Text = string.Format("{0:N0}", iNonACTotalRp);

                    lblACAkhir.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["ReadingAC"]);
                    lblACAwal.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["ReadingACLast"]);
                    lblACIndoorKwh.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["TotalReadingACMeter"]);
                    lblACOutdoorKwh.Text = string.Format("{0:N0}", iTotalACOutDoorPerSuiteP);
                    lblACKwh.Text = string.Format("{0:N0}", iTotalACKWh);
                    lblACTotal.Text = string.Format("{0:N0}", iTotalACRp);

                    lblSubTotal.Text = string.Format("{0:N0}", iSubTotal);
                    lblPpj.Text = string.Format("{0:N0}", iPPj);
                    lblBiayaAdmin.Text = string.Format("{0:N0}", iBiayaAdmin);

                    lblPPn.Text = string.Format("{0:N0}", iPPn);
                    //lblMaterai.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["CostMaterai"]);
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

        private void getEditData(string iMeterID)
        {
            try
            {
                ds = Db.get_list("exec [spGetCopPln_Meter_Query] " + iMeterID);
                bRes = false;

                if (ds.Tables[0].Rows.Count != 0)
                {

                    int iPaymentNominal = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentNominal"].ToString());
                    int iVariance = Convert.ToInt32(lblGT.Text) - iPaymentNominal;
                    int iPPh = Convert.ToInt32(ds.Tables[0].Rows[0]["PPh"].ToString());
                    int iBankCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["BankCharge"].ToString());
                    int iOther = Convert.ToInt32(ds.Tables[0].Rows[0]["Other"].ToString());
                    int iTotal = iPPh + iBankCharge + iOther;

                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Direct Transfer") { rblPaymentMethod.SelectedIndex = 0; txtPaymentNo.Enabled = false; txtBankName.Enabled = false; }
                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Check") { rblPaymentMethod.SelectedIndex = 1; txtPaymentNo.Enabled = true; txtBankName.Enabled = true; }
                    if (ds.Tables[0].Rows[0]["PaymentMethod"].ToString() == "Giro") { rblPaymentMethod.SelectedIndex = 2; txtPaymentNo.Enabled = true; txtBankName.Enabled = true; }

                    lblID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    lblMeterID.Text = ds.Tables[0].Rows[0]["MeterID"].ToString();

                    txtReceivedDate.Text = ds.Tables[0].Rows[0]["RDate"].ToString();
                    txtPaymentNo.Text = ds.Tables[0].Rows[0]["NoPayment"].ToString();
                    txtBankName.Text = ds.Tables[0].Rows[0]["BankName"].ToString();
                    txtPaymentNominal.Text = iPaymentNominal.ToString();
                    txtVariance.Text = iVariance.ToString();
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

                //BindGrid();
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

                String SQL = "select * from[dbo].[vwUTLECDMRD] Where Month = " + ddlMonthPeriod.SelectedValue.ToString() + " And Year = " + txtYearPeriod.Text.Trim() + " And SuiteNo = " + iSuiteNo;


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
                    string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPPLN"].ToString());
                    _rdReportViewer.Load(reportPath);

                    _rdReportViewer.SetDataSource(dt);

                    _rdReportViewer.SetParameterValue(0, ddlMonthPeriod.SelectedValue.ToString());
                    _rdReportViewer.SetParameterValue(1, txtYearPeriod.Text.Trim());
                    _rdReportViewer.SetParameterValue(2, iSuiteNo);

                    //show crystal report viewer
                    //CrystalReportViewer1.ReportSource = _rdReportViewer;                

                    //check file exist or not
                    string fDate = "COP-PLN-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim() + "-" + iSuiteNo;

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

                if (Session["EditCopPln"].ToString() =="True")
                {
                    executeEdit(HttpContext.Current.Session["userid"].ToString(), lblID.Text, lblMeterID.Text, lblCopPlnNo.Text, rblPaymentMethod.SelectedValue.ToString(),
                            txtPaymentNo.Text, txtBankName.Text, iDate.ToString(), txtPaymentNominal.Text, txtPPhFnl.Text, txtBankCharge.Text, txtOther.Text, txtNote.Text);
                }
                else
                {
                    executeAdd(HttpContext.Current.Session["userid"].ToString(), lblMeterID.Text, lblCopPlnNo.Text, rblPaymentMethod.SelectedValue.ToString(),
                            txtPaymentNo.Text, txtBankName.Text, iDate.ToString(), txtPaymentNominal.Text, txtPPhFnl.Text, txtBankCharge.Text, txtOther.Text, txtNote.Text);
                }
                


                //create pdf file
                //btnUpdate_Click(sender, e);
                try
                {
                    string iSuiteNo = Session["iSuiteNo"].ToString();
                    string iInvPln = Session["iINVPLN"].ToString();

                    String SQL = "SELECT vwCopPln.InvNo, vwCopPln.ReadingACLast, vwCopPln.ReadingAC, vwCopPln.ReadingNonACLast, vwCopPln.ReadingNonAC, " +
                                     "vwCopPln.TotalACOutdoorPerSuiteP, vwCopPln.KWhRate, vwCopPln.CostAdmin, vwCopPln.CostPPJ, vwCopPln.Name, vwCopPln.Address, " +
                                     "vwCopPln.KVA, vwCopPln.CheckDate, vwCopPln.CheckDateLast, vwCopPln.Month, vwCopPln.Year, vwCopPln.SuiteNo, " +
                                     "vwCopPln.Floor, vwCopPln.CID, vwCopPln.PaymentNominal, vwCopPln.PPh, vwCopPln.BankCharge, vwCopPln.Other, " +
                                     "vwCopPln.Note, vwCopPln.PaymentMethod, vwCopPln.NoPayment, vwCopPln.BankName, vwCopPln.PIC1Name, vwCopPln.InvPrintDate, vwCopPln.InvDueDate, vwCopPln.ReceivedDate " +
                                     "FROM   PNG.dbo.vwCopPln vwCopPln " +
                                     "WHERE  vwCopPln.Month= " + ddlMonthPeriod.SelectedValue.ToString() + " AND vwCopPln.Year= " + txtYearPeriod.Text.Trim() + " AND vwCopPln.SuiteNo = " + iSuiteNo + " " +
                                     "ORDER BY vwCopPln.Year, vwCopPln.Month, vwCopPln.Floor, vwCopPln.SuiteNo";


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
                        
                        string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPPLN"].ToString());
                        _rdReportViewer.Load(reportPath);

                        _rdReportViewer.SetDataSource(dt);

                        _rdReportViewer.SetDatabaseLogon(ConfigurationManager.AppSettings["rptUser"].ToString(), ConfigurationManager.AppSettings["rptPassword"].ToString());

                        _rdReportViewer.SetParameterValue(0, txtYearPeriod.Text.Trim());
                        _rdReportViewer.SetParameterValue(1, ddlMonthPeriod.SelectedValue.ToString());
                        _rdReportViewer.SetParameterValue(2, iSuiteNo);

                        //_rdReportViewer.RefreshReport += new EventHandler((object o, EventArgs e2) =>
                        //{
                        //    _rdReportViewer.SetParameterValue(0, txtYearPeriod.Text.Trim());
                        //    _rdReportViewer.SetParameterValue(1, ddlMonthPeriod.SelectedValue.ToString());
                        //    _rdReportViewer.SetParameterValue(2, iSuiteNo);
                        //});

                        //_rdReportViewer.Refresh();

                        //check file exist or not
                        //string fDate = "COP-PLN-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim() + "-" + iSuiteNo + "-" + DateTime.Now.ToString("HHmmss");
                        string iTgl = "";
                        
                        //if (Session["EditCopPln"].ToString() == "True")
                        //{
                        //    iTgl+= "-" + DateTime.Now.ToString("HHmmss");
                        //}
                            string fDate = "COP-PLN-" + iInvPln + iTgl;

                        if (!string.IsNullOrEmpty(fDate))
                        {
                            //if (!File.Exists(HttpContext.Current.Server.MapPath("~\\RptTemp\\" + fDate + ".pdf")))
                            //{
                                // deletevprevious image
                                //File.Delete(HttpContext.Current.Server.MapPath(deletePath));                                
                                _rdReportViewer.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath("~\\RptTemp\\" + fDate + ".pdf"));
                            //}
                        }

                        string url = "./PDFViewer.aspx?FN=" + fDate + ".pdf";
                        //string script = "<script type='text/javascript'>window.open('" + url + "')</script>";
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append(@"<script type='text/javascript'>");
                        sb.Append("window.open('" + url + "')");
                        sb.Append(@"</script>");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InfoModalScript", sb.ToString(), false);

                        BindGridSort(ddlSort.SelectedValue.ToString());
                        upContract.Update();

                        hideMessageBox();
                        Session["iSuiteNo"] = null;
                        Session["EditCopPln"] = null;
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
                Session["iINVPLN"] = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

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

                    //previewInvoice(ddlMonthPeriod.SelectedValue.ToString(), txtYearPeriod.Text.Trim(), HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString());
                    previewInvoicePLN(HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString());
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
                        Session["EditCopPln"] = false;

                        string iSuiteNo = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();

                        String SQL = "SELECT vwCopPln.InvNo, vwCopPln.ReadingACLast, vwCopPln.ReadingAC, vwCopPln.ReadingNonACLast, vwCopPln.ReadingNonAC, " +
                                     "vwCopPln.TotalACOutdoorPerSuiteP, vwCopPln.KWhRate, vwCopPln.CostAdmin, vwCopPln.CostPPJ, vwCopPln.Name, vwCopPln.Address, " +
                                     "vwCopPln.KVA, vwCopPln.CheckDate, vwCopPln.CheckDateLast, vwCopPln.Month, vwCopPln.Year, vwCopPln.SuiteNo, " +
                                     "vwCopPln.Floor, vwCopPln.CID, vwCopPln.PaymentNominal, vwCopPln.PPh, vwCopPln.BankCharge, vwCopPln.Other, " +
                                     "vwCopPln.Note, vwCopPln.PaymentMethod, vwCopPln.NoPayment, vwCopPln.BankName, vwCopPln.PIC1Name, vwCopPln.InvPrintDate, vwCopPln.InvDueDate, vwCopPln.ReceivedDate " +
                                     "FROM   PNG.dbo.vwCopPln vwCopPln " +
                                     "WHERE  vwCopPln.Month= " + ddlMonthPeriod.SelectedValue.ToString() + " AND vwCopPln.Year= " + txtYearPeriod.Text.Trim() + " AND vwCopPln.SuiteNo = " + iSuiteNo + " " +
                                     "ORDER BY vwCopPln.Year, vwCopPln.Month, vwCopPln.Floor, vwCopPln.SuiteNo";


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

                            string reportPath = Server.MapPath("~\\Rpt\\" + ConfigurationManager.AppSettings["rptCOPPLN"].ToString());
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
                            //string fDate = "COP-PLN-" + ddlMonthPeriod.SelectedValue.ToString() + "-" + txtYearPeriod.Text.Trim() + "-" + iSuiteNo + "-" + DateTime.Now.ToString("HHmmss");
                            string fDate = "COP-PLN-" + HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

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
                            showMessageModal(eMessage.eError, "COP:PLN Not Found", "COP:PLN document for Suite No : " + iSuiteNo + " not found or has not made the payment.");

                        }
                    }
                    catch (Exception me)
                    {
                        showMessage(eMessage.eError, "GridView1_RowCommand", me.Message);
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
                        Session["EditCopPln"] = true;
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