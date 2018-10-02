using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KMO.Class;

namespace KMO
{
    public partial class SecurityDeposit : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;
        Boolean isEdit;
        string iIDDat;
        string dMonth;
        string dYear;
        string dFloor;
        string dDate;
        string dCompanyID;

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

        private void clearAll(ControlCollection ctls)
        {
            foreach (Control c in ctls)
            {
                if (c is System.Web.UI.WebControls.TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                if (c is System.Web.UI.WebControls.Label)
                {
                    ((Label)c).Text = "xxxxxx";// string.Empty;
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
                    clearAll(c.Controls);

                }

                GridView1.DataSource = null;
                GridView1.DataBind();
            }

            string rMon = DateTime.Now.Month.ToString();
            string iMon = "";
            if (rMon.Length == 1)
            {
                iMon = "0" + rMon;
            }
            else { iMon = rMon; }

            isEdit = false;
            Session["iEditData"] = false;
        }

        private Boolean checkDataRecord(string iMon, string iYear, string iSuite, string iInvID)
        {
            Boolean iRes = true;

            //cek datanya sdh ada belum dibulan berikutnya
            ds = Db.get_list("Select * From tCOP_SEC_Payment Where Status = 1 And InvID = " + iInvID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                showMessageModal(eMessage.eError, "Data cannot be changed", "the data can not be changed because there are other data related to these data");
                return false;
            }


            return iRes;
        }

        protected void executeAdd(string iCFSID, string iFloor, string iSuiteNo, string iArea, string iKVA,
                                    string iRentServicePeriod, string iRentServicePeriodMY, string iRentCharge, string iServiceCharge, string iOtherCharge,
                                    string iDepositPeriod, string iDepositPeriodMY, string iDepositRentCharge, string iDepositServiceCharge, string iDepositOtherCharge,
                                    string iRpcArea, string iRpcPeriod, string iRpcMY, string iRpcCharge,
                                    string iUnRpcArea, string iUnRpcPeriod, string iUnRpcMY, string iUnRpcCharge,
                                    string iRpmArea, string iRpmPeriod, string iRpmMY, string iRpmCharge,
                                    string iRpoArea, string iRpoPeriod, string iRpoMY, string iRpoCharge,
                                    string iPeriodFrom, string iPeriodTo, string iInvSource)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertInvoiceSource", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@Month", DateTime.Now.Month);
                cmd.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@Floor", iFloor);
                cmd.Parameters.AddWithValue("@SuiteNo", iSuiteNo);
                cmd.Parameters.AddWithValue("@Area", iArea);
                cmd.Parameters.AddWithValue("@KVA", iKVA);

                //add rental & service data
                cmd.Parameters.AddWithValue("@RentalArea", iArea);
                cmd.Parameters.AddWithValue("@RentalPeriod", iRentServicePeriod);
                cmd.Parameters.AddWithValue("@RentalPeriodMY", iRentServicePeriodMY);
                cmd.Parameters.AddWithValue("@RentalCharge", iRentCharge);
                cmd.Parameters.AddWithValue("@ServiceArea", iArea);
                cmd.Parameters.AddWithValue("@ServicePeriod", iRentServicePeriod);
                cmd.Parameters.AddWithValue("@ServicePeriodMY", iRentServicePeriodMY);
                cmd.Parameters.AddWithValue("@ServiceCharge", iServiceCharge);
                cmd.Parameters.AddWithValue("@OtherArea", iArea);
                cmd.Parameters.AddWithValue("@OtherPeriod", iRentServicePeriod);
                cmd.Parameters.AddWithValue("@OtherPeriodMY", iRentServicePeriodMY);
                cmd.Parameters.AddWithValue("@OtherCharge", iOtherCharge);

                //add deposit data
                cmd.Parameters.AddWithValue("@DepositRentalArea", iArea);
                cmd.Parameters.AddWithValue("@DepositRentalPeriod", iDepositPeriod);
                cmd.Parameters.AddWithValue("@DepositRentalPeriodMY", iDepositPeriodMY);
                cmd.Parameters.AddWithValue("@DepositRentalCharge", iDepositRentCharge);
                cmd.Parameters.AddWithValue("@DepositServiceArea", iArea);
                cmd.Parameters.AddWithValue("@DepositServicePeriod", iDepositPeriod);
                cmd.Parameters.AddWithValue("@DepositServicePeriodMY", iDepositPeriodMY);
                cmd.Parameters.AddWithValue("@DepositServiceCharge", iDepositServiceCharge);
                cmd.Parameters.AddWithValue("@DepositOtherArea", iArea);
                cmd.Parameters.AddWithValue("@DepositOtherPeriod", iDepositPeriod);
                cmd.Parameters.AddWithValue("@DepositOtherPeriodMY", iDepositPeriodMY);
                cmd.Parameters.AddWithValue("@DepositOtherCharge", iDepositOtherCharge);

                // add parking data
                cmd.Parameters.AddWithValue("@ParkingReservedArea", iRpcArea);
                cmd.Parameters.AddWithValue("@ParkingReservedPeriod", iRpcPeriod);
                cmd.Parameters.AddWithValue("@ParkingReservedMY", iRpcMY);
                cmd.Parameters.AddWithValue("@ParkingReservedCharge", iRpcCharge);
                cmd.Parameters.AddWithValue("@ParkingUnReservedArea", iUnRpcArea);
                cmd.Parameters.AddWithValue("@ParkingUnReservedPeriod", iUnRpcPeriod);
                cmd.Parameters.AddWithValue("@ParkingUnReservedMY", iUnRpcMY);
                cmd.Parameters.AddWithValue("@ParkingUnReservedCharge", iUnRpcCharge);
                cmd.Parameters.AddWithValue("@ParkingMotorcyleArea", iRpmArea);
                cmd.Parameters.AddWithValue("@ParkingMotorcylePeriod", iRpmPeriod);
                cmd.Parameters.AddWithValue("@ParkingMotorcyleMY", iRpmMY);
                cmd.Parameters.AddWithValue("@ParkingMotorcyleCharge", iRpmCharge);
                cmd.Parameters.AddWithValue("@ParkingOtherArea", iRpoArea);
                cmd.Parameters.AddWithValue("@ParkingOtherPeriod", iRpoPeriod);
                cmd.Parameters.AddWithValue("@ParkingOtherMY", iRpoMY);
                cmd.Parameters.AddWithValue("@ParkingOtherCharge", iRpoCharge);

                //AddedControl period and inv source
                cmd.Parameters.AddWithValue("@PeriodFrom", iPeriodFrom);
                cmd.Parameters.AddWithValue("@PeriodTo", iPeriodTo);
                cmd.Parameters.AddWithValue("@InvSource", "SEC");

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

        private void executeDelete(string iTableName, string id, string iReason, string userID)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();
                SqlCommand deleteCmd = new SqlCommand("spDeleteTable", conn);
                deleteCmd.CommandType = CommandType.StoredProcedure;

                deleteCmd.Parameters.AddWithValue("@TableName", iTableName);
                deleteCmd.Parameters.AddWithValue("@ID", id);
                deleteCmd.Parameters.AddWithValue("@Reason", iReason);
                deleteCmd.Parameters.AddWithValue("@UserID", userID);

                deleteCmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "executeDelete", me.Message);
            }

        }

        private void getEdit(string iID)
        {
            isEdit = false;

            try
            {
                ds = Db.get_list("execute spGetUTLECDMRDID_Query " + iID);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    txtFloor.Text= ds.Tables[0].Rows[0]["Floor"].ToString();

                    txtCompanyID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    txtSuiteNo.Text= txtCompanyID.Text = ds.Tables[0].Rows[0]["SuiteNo"].ToString();

                    isEdit = true;
                }
                else
                {
                    
                }
            }
            catch (Exception me)
            {
                showMessage(eMessage.eError, "executeEdit", me.Message);
            }
        }

        private int checkMaterai(long iTotal)
        {
            int iRes = 6000;

            if (iTotal <= 250000) { iRes = 0; }
            if (iTotal > 250000 && iTotal < 1000000) { iRes = 3000; }
            if (iTotal > 1000000) { iRes = 6000; }

            return iRes;
        }

        private void getData(string iID, string iSuiteNo)
        {
            try
            {
                ds = Db.get_list("select * from mCFS, mCFSSuite " +
                                 "Where mCFS.ID = mCFSSuite.CFSID And mCFS.Status = 1 And mCFSSuite.Status = 1 and CFSid = " + iID + " And SuiteNo = " + iSuiteNo );


                decimal iRentalArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["RentalArea"].ToString());
                decimal iRentalPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["RentalPeriod"].ToString());
                long iRentalCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["RentalCharge"].ToString());
                long iRentalAmount = Convert.ToInt64(String.Format("{0:0}", iRentalArea * iRentalPeriod * iRentalCharge));

                decimal iServiceArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["ServiceArea"].ToString());
                decimal iServicePeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["ServicePeriod"].ToString());
                long iServiceCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["ServiceCharge"].ToString());
                long iServiceAmount = Convert.ToInt64(String.Format("{0:0}", iServiceArea * iServicePeriod * iServiceCharge));

                decimal iOtherArea = Convert.ToDecimal(ds.Tables[0].Rows[0]["OtherArea"].ToString());
                decimal iOtherPeriod = Convert.ToDecimal(ds.Tables[0].Rows[0]["OtherPeriod"].ToString());
                long iOtherCharge = Convert.ToInt64(ds.Tables[0].Rows[0]["OtherCharge"].ToString());
                long iOtherAmount = Convert.ToInt64(String.Format("{0:0}", iOtherArea * iOtherPeriod * iOtherCharge));

                int iTL= Convert.ToInt32(ds.Tables[0].Rows[0]["ProsTelpLine"].ToString());
                int iTLCharge = Convert.ToInt32(ds.Tables[0].Rows[0]["ProsTelpLineCharge"].ToString());
                int iTLAmount = iTL * iTLCharge;

                long iSubTotal = iRentalAmount + iServiceAmount + iOtherAmount + iTLAmount;
                long iPPn = 0;// Convert.ToInt64(String.Format("{0:0}", (iSubTotal) * (0.1)));
                long iMaterai = checkMaterai(iSubTotal);

                long iGrandTotal = iSubTotal + iPPn + iMaterai ;

                lblRentalArea.Text = iRentalArea.ToString();
                lblRentalPeriod.Text = iRentalPeriod.ToString();
                lblRentalPeriodMY.Text = ds.Tables[0].Rows[0]["RentalPeriodMY"].ToString();
                lblRentalCharge.Text = string.Format("{0:N0}", ds.Tables[0].Rows[0]["RentalCharge"]);
                lblRentalAmount.Text = string.Format("{0:N0}", iRentalAmount);

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

                lblProsTelpLine.Text = string.Format("{0:N0}", iTL); ;
                lblProsTelpLineCharge.Text = string.Format("{0:N0}", iTLCharge);
                lblProsTelpLineAmount.Text= string.Format("{0:N0}", iTLAmount);

                lblSubTotal.Text = string.Format("{0:N0}", iSubTotal);
                lblPPN.Text = string.Format("{0:N0}", iPPn);
                lblMaterai.Text = string.Format("{0:N0}", iMaterai);
                lblGrandTotal.Text = string.Format("{0:N0}", iGrandTotal);

                //insert into textbox
                txtKVA.Text = ds.Tables[0].Rows[0]["KVA"].ToString();

                txtRentalArea.Text = ds.Tables[0].Rows[0]["RentalArea"].ToString();
                txtRentalPeriod.Text = ds.Tables[0].Rows[0]["RentalPeriod"].ToString();
                txtRentalPeriodMY.Text = ds.Tables[0].Rows[0]["RentalPeriodMY"].ToString();
                txtRentalCharge.Text = ds.Tables[0].Rows[0]["RentalCharge"].ToString();
                txtServiceArea.Text = ds.Tables[0].Rows[0]["ServiceArea"].ToString();
                txtServicePeriod.Text = ds.Tables[0].Rows[0]["ServicePeriod"].ToString();
                txtServicePeriodMY.Text = ds.Tables[0].Rows[0]["ServicePeriodMY"].ToString();
                txtServiceCharge.Text = ds.Tables[0].Rows[0]["ServiceCharge"].ToString();
                txtOtherArea.Text = ds.Tables[0].Rows[0]["OtherArea"].ToString();
                txtOtherPeriod.Text = ds.Tables[0].Rows[0]["OtherPeriod"].ToString();
                txtOtherPeriodMY.Text = ds.Tables[0].Rows[0]["OtherPeriodMY"].ToString();
                txtOtherCharge.Text = ds.Tables[0].Rows[0]["OtherCharge"].ToString();

                txtDepositRentalArea.Text = ds.Tables[0].Rows[0]["DepositRentalArea"].ToString();
                txtDepositRentalPeriod.Text = ds.Tables[0].Rows[0]["DepositRentalPeriod"].ToString();
                txtDepositRentalPeriodMY.Text = ds.Tables[0].Rows[0]["DepositRentalPeriodMY"].ToString();
                txtDepositRentalCharge.Text = ds.Tables[0].Rows[0]["DepositRentalCharge"].ToString();
                txtDepositServiceArea.Text = ds.Tables[0].Rows[0]["DepositServiceArea"].ToString();
                txtDepositServicePeriod.Text = ds.Tables[0].Rows[0]["DepositServicePeriod"].ToString();
                txtDepositServicePeriodMY.Text = ds.Tables[0].Rows[0]["DepositServicePeriodMY"].ToString();
                txtDepositServiceCharge.Text = ds.Tables[0].Rows[0]["DepositServiceCharge"].ToString();
                txtDepositOtherArea.Text = ds.Tables[0].Rows[0]["DepositOtherArea"].ToString();
                txtDepositOtherPeriod.Text = ds.Tables[0].Rows[0]["DepositOtherPeriod"].ToString();
                txtDepositOtherPeriodMY.Text = ds.Tables[0].Rows[0]["DepositOtherPeriodMY"].ToString();
                txtDepositOtherCharge.Text = ds.Tables[0].Rows[0]["DepositOtherCharge"].ToString();

                txtParkingReservedArea.Text = ds.Tables[0].Rows[0]["ParkingReservedArea"].ToString();
                txtParkingReservedPeriod.Text = ds.Tables[0].Rows[0]["ParkingReservedPeriod"].ToString();
                txtParkingReservedPeriodMY.Text = ds.Tables[0].Rows[0]["ParkingReservedMY"].ToString();
                txtParkingReservedCharge.Text = ds.Tables[0].Rows[0]["ParkingReserverCharge"].ToString();
                txtParkingUnreservedArea.Text = ds.Tables[0].Rows[0]["ParkingUnReservedArea"].ToString();
                txtParkingUnreservedPeriod.Text = ds.Tables[0].Rows[0]["ParkingUnReservedPeriod"].ToString();
                txtParkingUnreservedPeriodMY.Text = ds.Tables[0].Rows[0]["ParkingUnReservedMY"].ToString();
                txtParkingUnreservedCharge.Text = ds.Tables[0].Rows[0]["ParkingUnReserverCharge"].ToString();
                txtParkingMotorcycleArea.Text = ds.Tables[0].Rows[0]["ParkingMotorcyleArea"].ToString();
                txtParkingMotorcyclePeriod.Text = ds.Tables[0].Rows[0]["ParkingMotorcylePeriod"].ToString();
                txtParkingMotorcyclePeriodMY.Text = ds.Tables[0].Rows[0]["ParkingMotorcyleMY"].ToString();
                txtParkingMotorcycleCharge.Text = ds.Tables[0].Rows[0]["ParkingMotorcyleCharge"].ToString();
                txtParkingOtherArea.Text = ds.Tables[0].Rows[0]["ParkingOtherArea"].ToString();
                txtParkingOtherPeriod.Text = ds.Tables[0].Rows[0]["ParkingOtherPeriod"].ToString();
                txtParkingOtherPeriodMY.Text = ds.Tables[0].Rows[0]["ParkingOtherMY"].ToString();
                txtParkingOtherCharge.Text = ds.Tables[0].Rows[0]["ParkingOtherCharge"].ToString();

                //get period value
                string iSql = "select Top 1 *, " +
                          "format(DATEADD(DAY, 1, PeriodTo), 'dd-MM-yyyy') as NextPeriodFrom, " +
                          //"format(DATEADD(MONTH, SuiteRentalPeriod, PeriodTo), 'dd-MM-yyyy') as NextPeriodTo " +
                          "Case DatePart(dd,DATEADD(DAY, 1, PeriodTo)) " +
                          "  When 1 Then format(DATEADD(s, -1, DATEADD(mm, DATEDIFF(m, 0, DATEADD(MONTH, SuiteRentalPeriod, PeriodTo)) + 1, 0)), 'dd-MM-yyyy') " +
                          "  Else format(DATEADD(MONTH, SuiteRentalPeriod, PeriodTo), 'dd-MM-yyyy') " +
                          "End as NextPeriodTo " +
                          "from vwInvoiceSource " +
                          "where InvSource = 'SEC' And Status = 1 And CFSid = " + iID + " And SuiteNo = " + iSuiteNo + " " +
                          "Order By InvNo Desc";
                ds = Db.get_list(iSql);

                if(ds.Tables[0].Rows.Count > 0)
                {

                } else
                {
                    iSql = "select Top 1 *, " +
                            "format(DATEADD(DAY, 0, ServiceCommDate), 'dd-MM-yyyy') as NextPeriodFrom, " +
                            //"format(DATEADD(MONTH, RentalPeriod, ServiceCommDate), 'dd-MM-yyyy') as NextPeriodTo " +
                            "Case DatePart(dd,DATEADD(DAY, 0, ServiceCommDate)) " +
                            "  When 1 Then format(DATEADD(s, -1, DATEADD(mm, DATEDIFF(m, 0, DATEADD(MONTH, RentalPeriod-1, ServiceCommDate)) + 1, 0)), 'dd-MM-yyyy') " +
                            "  Else format(DATEADD(MONTH, RentalPeriod, ServiceCommDate), 'dd-MM-yyyy') " +
                            "End as NextPeriodTo " +
                            "from mCFS, mCFSSuite where mCFS.ID = mCFSSuite.CFSID And mCFSSuite.Status = 1 And mCFS.Status = 1 " + 
                            "and mCFS.ID = " + iID + " And mCFSSuite.SuiteNo =  " + iSuiteNo + " " +
                            "order by mCFS.ID Desc";

                    ds = Db.get_list(iSql);                    
                }

                txtPeriodFrom.Text = ds.Tables[0].Rows[0]["NextPeriodFrom"].ToString();
                txtPeriodTo.Text = ds.Tables[0].Rows[0]["NextPeriodTo"].ToString();

                bRes = true;
            }

            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "getData", me.Message);
            }
        }

        private void BindGrid(string iCFSID, string iSuiteNo)
        {
            string iSql = "Select Distinct ID, Month, Year, InvNo, PrintDate, " +
                          "FromPeriod, ToPeriod " +
                          "from vwInvoiceSource_All " +
                          "where InvSource = 'SEC' And Status = 1 And CFSid = " + iCFSID + " And SuiteNo = " + iSuiteNo + " " + 
                          "Order By InvNo Desc";
            try
            {
                ds = Db.get_list(iSql);

                if (ds.Tables[0].Rows.Count > 0)
                { btnSave.Enabled = false;}
                else { btnSave.Enabled = true; }

                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (SqlException ex)
            {
                showMessage(eMessage.eError, "BindGrid- SqlExeption", ex.Message);
            }
            catch (Exception ex)
            {
                showMessage(eMessage.eError, "BindGrid", ex.Message);
            }            
        }

        private void BindGrid2()
        {
            string iSql = "exec [spGetCFSSuite] ";

            try
            {
                GridViewSelectTenant.DataSource = Db.get_list(iSql);
                GridViewSelectTenant.DataBind();
            }
            catch (SqlException ex)
            {
                showMessage(eMessage.eError, "BindGrid2- SqlExeption", ex.Message);
            }
            catch (Exception ex)
            {
                showMessage(eMessage.eError, "BindGrid2", ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Vd.isValidLogin();           

            if (!IsPostBack)
            {
                hideMessageBox();
                clearAll(this.Form.Controls);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hideMessageBox();

            BindGrid2();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#selectTenantModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SelectTenantModalScript", sb.ToString(), false);

            UpdatePanel1.Update();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            dFloor= txtFloor.Text.ToString();
            dCompanyID = txtCompanyID.Text;

            isEdit = Convert.ToBoolean(Session["iEditData"]);

            if (isEdit)
            {
                //executeEdit((HttpContext.Current.Session["userid"].ToString()), lblEdit.Text, txtKWhRate.Text.Trim(), ddlMonthPeriod.SelectedValue.ToString(),
                //    txtYearPeriod.Text.Trim(), txtCompanyID.Text.Trim(), txtFloor.Text.ToString(), txtSuiteNo.Text.Trim(),
                //    txtACReading.Text.Trim(), txtACInitialOfficer.Text.Trim(), txtNonACReading.Text.Trim(), txtNonACInitialOfficer.Text.Trim(),
                //    txtACOutdoor.Text.Trim(), txtACOutdoorInitialOfficer.Text.Trim(), txtACReadingLobby.Text.Trim(),
                //    txtACInitialOfficerLobby.Text.Trim());
            }
            else
            {
                DateTime iPeriodFromDate = DateTime.ParseExact(txtPeriodFrom.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                DateTime iPeriodToDate = DateTime.ParseExact(txtPeriodTo.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                executeAdd(txtCompanyID.Text, txtFloor.Text, txtSuiteNo.Text, txtRentArea.Text, txtKVA.Text,
                            txtRentalPeriod.Text, txtRentalPeriodMY.Text, txtRentalCharge.Text, txtServiceCharge.Text, txtOtherCharge.Text,
                            txtDepositRentalPeriod.Text, txtDepositRentalPeriodMY.Text, txtDepositRentalCharge.Text, txtDepositServiceCharge.Text,
                            txtDepositOtherCharge.Text, txtParkingReservedArea.Text, txtParkingReservedPeriod.Text, txtParkingReservedPeriodMY.Text,
                            txtParkingReservedCharge.Text, txtParkingUnreservedArea.Text, txtParkingUnreservedPeriod.Text, txtParkingUnreservedPeriodMY.Text,
                            txtParkingUnreservedCharge.Text, txtParkingMotorcycleArea.Text, txtParkingMotorcyclePeriod.Text,
                            txtParkingMotorcyclePeriodMY.Text, txtParkingMotorcycleCharge.Text, txtParkingOtherArea.Text,
                            txtParkingOtherPeriod.Text, txtParkingOtherPeriodMY.Text, txtParkingOtherCharge.Text, iPeriodFromDate.ToString(), iPeriodToDate.ToString(), "SEC");
            }

            showMessageModal(eMessage.eSuccess, "Save data", "Your data has been saved.");

            hideMessageBox();
            //clearAll(this.Form.Controls);

            BindGrid(dCompanyID, txtSuiteNo.Text);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hideMessageBox();
            clearAll(this.Form.Controls);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtReasonToDelete.Text.Trim() == "")
            {
                showMessage(eMessage.eWarning, "Reason is empty.", "Please add your reasin to delete data.");
                txtReasonToDelete.Focus();
            }
            else
            {
                executeDelete("tInvoiceSource", HttpContext.Current.Session["iIDData"].ToString(), txtReasonToDelete.Text.Trim(), HttpContext.Current.Session["userid"].ToString());
                Session["iIDData"] = "";

                hideMessageBox();
                clearAll(this.Form.Controls);
                //BindGrid(txtCompanyID.Text, txtSuiteNo.Text);
            }
        }

        protected void ddlMonthPeriod_Change(object sender, EventArgs e)
        {
            //DateTime origDT = Convert.ToDateTime(txtYearPeriod.Text + "-" + ddlMonthPeriod.SelectedValue.ToString() + "-01" );
            //DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);

            ////txtStartPeriod.Text = origDT.ToString("dd/MM/yyyy");
            ////txtEndPeriod.Text = lastDate.ToString("dd/MM/yyyy");
            //getKWhRate();
            //BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid(txtCompanyID.Text, txtSuiteNo.Text);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            double num;
            string myString = e.CommandArgument.ToString();
            bool isNumber = double.TryParse(myString, out num);
            isEdit = false;

            GridViewRow gvrow = GridView1.Rows[index];
            string kode = GridView1.DataKeys[index].Value.ToString();
            iIDDat = kode;

            string iMon = Convert.ToString(Convert.ToInt32(HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString()) + 1);
            string iYear = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
            string iFloor = txtFloor.Text.ToString();
            string iSuite = txtSuiteNo.Text.ToString();

            if (isNumber)
            {
                index = Convert.ToInt32(e.CommandArgument);
            }

            if (e.CommandName.Equals("editRecord"))
            {
                //lblEdit.Text = kode;
                Session["iEditData"] = true;

                //getEdit(kode);

                txtFloor.Text = HttpUtility.HtmlDecode(gvrow.Cells[7].Text).ToString();
                txtSuiteNo.Text = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();

                ds = Db.get_list("Select CFSID From tUTLECDMRD Where ID = " + kode);
                txtCompanyID.Text = ds.Tables[0].Rows[0]["CFSID"].ToString();
                
                txtCFSID.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString();
                txtCompanyName.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text).ToString();
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                if (!checkDataRecord(iMon, iYear, iSuite, kode)) { return; };

                Session["iIDData"] = kode;

                txtReasonToDelete.Text = "";
               
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);
            }
        }

        protected void GridViewSelectTenant_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSelectTenant.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void GridViewSelectTenant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = 0;
            double num;
            string myString = e.CommandArgument.ToString();
            bool isNumber = double.TryParse(myString, out num);
            isEdit = false;

            if (isNumber)
            {
                index = Convert.ToInt32(e.CommandArgument);
            }

            if (e.CommandName.Equals("selectRecord"))
            {
                GridViewRow gvrow = GridViewSelectTenant.Rows[index];
                string kode = GridViewSelectTenant.DataKeys[index].Value.ToString();

                string iQuery;
                iQuery = "Select * from mCFSSuite Where Status = 1 And CFSID = " + kode + " And SuiteNo = " + HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();

                ds = Db.get_list(iQuery);

                
                txtFloor.Text = ds.Tables[0].Rows[0]["Floor"].ToString();
                txtSuiteNo.Text = ds.Tables[0].Rows[0]["SuiteNo"].ToString();
                txtRentPeriod.Text = ds.Tables[0].Rows[0]["RentalPeriod"].ToString();
                txtRentArea.Text = ds.Tables[0].Rows[0]["RentalArea"].ToString();

                txtCompanyID.Text = kode;
                txtCFSID.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString();
                txtCompanyName.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();

                getData(kode, HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString());
                BindGrid(kode, HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString());                
            }
        }
    }
}