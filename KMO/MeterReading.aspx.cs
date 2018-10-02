using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

using KMO.Class;

namespace KMO
{
    public partial class MeterReading : System.Web.UI.Page
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
                    clearAll(c.Controls);

                }
            }

            lblEdit.Text = "";

            string rMon = DateTime.Now.Month.ToString();
            string iMon = "";
            if (rMon.Length == 1)
            {
                iMon = "0" + rMon;
            }
            else { iMon = rMon; }

            txtACInitialOfficer.Enabled = true;
            txtNonACInitialOfficer.Enabled = true;
            txtACOutdoor.Enabled = true;
            txtACOutdoorInitialOfficer.Enabled = true;
            txtACReadingLobby.Enabled = true;
            txtACInitialOfficerLobby.Enabled = true;

            isEdit = false;
            Session["iEditData"] = false;

            txtYearPeriod.Text = DateTime.Now.Year.ToString();
            ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;

            getKWhRate();
        }

        private Boolean checkData()
        {
            Boolean iRes = true;

            try
            {
                if (txtYearPeriod.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Year is empty."); }
                if (txtCompanyID.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "CID is empty."); }

                if (txtInputDate.Text.Trim()=="") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Input date is empty."); }

                if (txtKWhRate.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "KWh rate is empty."); }

                if (txtACReading.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "AC reading is empty."); }
                if (txtACInitialOfficer.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Initial officer is empty."); }
                if (txtACOutdoor.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "AC outdoor is empty."); }
                if (txtACOutdoorInitialOfficer.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Initial officer is empty."); }
                if (txtNonACReading.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Non AC reading is empty."); }
                if (txtNonACInitialOfficer.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Initial officer is empty."); }
                if (txtACReadingLobby.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "AC lobby is empty."); }
                if (txtACInitialOfficerLobby.Text.Trim() == "") { iRes = false; showMessageModal(eMessage.eWarning, "Data empty", "Initial officer is empty."); }

                DateTime iDate;
                try
                {
                    iDate = DateTime.ParseExact(txtInputDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception me)
                {
                    iRes = false; showMessageModal(eMessage.eWarning, "Format date", "Please input your format date to dd-MM-yyyy.");
                }
            }
            catch (Exception me)
            {
                iRes = false;
                showMessageModal(eMessage.eError, "checkData", me.Message);
            }
            return iRes;
        }

        private Boolean checkDataRecord(string iMon, string iYear, string iSuite, string iInvID)
        {
            Boolean iRes = true;

            //cek datanya sdh ada belum dibulan berikutnya
            ds = Db.get_list("Select * From tUTLECDMRD Where Status = 1 And Month = " + iMon + " And Year = " + iYear + " And SuiteNo = " + iSuite);
            if (ds.Tables[0].Rows.Count > 0)
            {                
                showMessageModal(eMessage.eError, "Data cannot be changed", "the data can not be changed because there are other data related to these data");
                return false;
            }

            ds = Db.get_list("Select * From tCOP_PLN_Payment Where Status = 1 And MeterID = " + iInvID );
            if (ds.Tables[0].Rows.Count > 0)
            {
                showMessageModal(eMessage.eError, "Data cannot be changed", "the data can not be changed because there are other data related to these data");
                return false;
            }


            return iRes;
        }

        private void executeAdd(string iUserID, string iKWhRate, string iMonth, string iYear,
                                string iCFSID, string iFloor, string iSuiteNo,
                                string iReadingAC, string iInitialOfficerAC,
                                string iReadingNonAC, string iInitialOfficerNonAC,
                                string iReadingACOutdoor, string iInitialOfficerACOutdoor,
                                string iReadingACLobby, string iInitialOfficerACLobby,
                                string iInputDate)
        {
           // string connString = Db.GetConnectionString();

            try
            {
                DateTime date = DateTime.ParseExact(iInputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertUTLECDMRD", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@KWhRate", iKWhRate);
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@Floor", iFloor);
                cmd.Parameters.AddWithValue("@SuiteNo", iSuiteNo);
                cmd.Parameters.AddWithValue("@ReadingAC", iReadingAC);
                cmd.Parameters.AddWithValue("@InitialOfficerAC", iInitialOfficerAC);
                cmd.Parameters.AddWithValue("@ReadingNonAC", iReadingNonAC);
                cmd.Parameters.AddWithValue("@InitialOfficerNonAC", iInitialOfficerNonAC);
                cmd.Parameters.AddWithValue("@ReadingACOutdoor", iReadingACOutdoor);
                cmd.Parameters.AddWithValue("@InitialOfficerACOutdoor", iInitialOfficerACOutdoor);
                cmd.Parameters.AddWithValue("@ReadingACLobby", iReadingACLobby);
                cmd.Parameters.AddWithValue("@InitialOfficerACLobby", iInitialOfficerACLobby);
                cmd.Parameters.AddWithValue("@InputDate", date);

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

        private void executeEdit(string iUserID, string iID, string iKWhRate, string iMonth, string iYear,
                                string iCFSID, string iFloor, string iSuiteNo,
                                string iReadingAC, string iInitialOfficerAC,
                                string iReadingNonAC, string iInitialOfficerNonAC,
                                string iReadingACOutdoor, string iInitialOfficerACOutdoor,
                                string iReadingACLobby, string iInitialOfficerACLobby,
                                string iInputDate)
        {
            //string connString = Db.GetConnectionString();

            try
            {
                DateTime date = DateTime.ParseExact(iInputDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUTLECDMRD", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@KWhRate", iKWhRate);
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@Floor", iFloor);
                cmd.Parameters.AddWithValue("@SuiteNo", iSuiteNo);
                cmd.Parameters.AddWithValue("@ReadingAC", iReadingAC);
                cmd.Parameters.AddWithValue("@InitialOfficerAC", iInitialOfficerAC);
                cmd.Parameters.AddWithValue("@ReadingNonAC", iReadingNonAC);
                cmd.Parameters.AddWithValue("@InitialOfficerNonAC", iInitialOfficerNonAC);
                cmd.Parameters.AddWithValue("@ReadingACOutdoor", iReadingACOutdoor);
                cmd.Parameters.AddWithValue("@InitialOfficerACOutdoor", iInitialOfficerACOutdoor);
                cmd.Parameters.AddWithValue("@ReadingACLobby", iReadingACLobby);
                cmd.Parameters.AddWithValue("@InitialOfficerACLobby", iInitialOfficerACLobby);
                cmd.Parameters.AddWithValue("@InputDate", date);

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

                    ddlFloor.Text= ds.Tables[0].Rows[0]["Floor"].ToString();

                    txtCompanyID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    txtSuiteNo.Text= txtCompanyID.Text = ds.Tables[0].Rows[0]["SuiteNo"].ToString();

                    txtInputDate.Text = ds.Tables[0].Rows[0]["CheckDate"].ToString();

                    txtACReading.Text = ds.Tables[0].Rows[0]["ReadingAC"].ToString();
                    txtACInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerAC"].ToString();

                    txtNonACReading.Text = ds.Tables[0].Rows[0]["ReadingNonAC"].ToString();
                    txtNonACInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerNonAC"].ToString();

                    txtACOutdoor.Text = ds.Tables[0].Rows[0]["ReadingACOutdoor"].ToString();
                    txtACOutdoorInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerACOutdoor"].ToString();

                    txtACReadingLobby.Text = ds.Tables[0].Rows[0]["ReadingACLobby"].ToString();
                    txtACInitialOfficerLobby.Text = ds.Tables[0].Rows[0]["InitialOfficerACLobby"].ToString();
                    
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

        private void getKWhRate()
        {
            try
            {
                
                int iSelItemBefore = Convert.ToInt32(ddlMonthPeriod.SelectedItem.Value.ToString())-1;
                string iSelItemBeforeText = ddlMonthPeriod.Items[iSelItemBefore].ToString();

                string iSelItem = ddlMonthPeriod.SelectedItem.ToString();


                //ds = Db.get_list("execute spGetKWhRate " + ddlMonthPeriod.SelectedValue.ToString() + " , " + txtYearPeriod.Text);
                ds = Db.get_list("execute spGetKWhRate " + iSelItemBefore + " , " + txtYearPeriod.Text);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    txtKWhRate.Text = ds.Tables[0].Rows[0]["KWhRate"].ToString();
                    txtUsageMonth.Text = iSelItemBeforeText;
                    txtUsageYear.Text = txtYearPeriod.Text;
                }else
                {
                    showMessageModal(eMessage.eError, "PLN Rate", "PLN rate is empty.");
                    txtKWhRate.Text = "";
                    txtUsageMonth.Text = "";
                    txtUsageYear.Text = "";
                }
            }
            catch (Exception ex)
            {
                showMessage(eMessage.eError, "getCompanyInfo", ex.Message);
            }           
        }

        private void getCompanyInfo()
        {
            try
            {
                ds = Db.get_list("execute spGetCFSSuiteFloorSuiteNo_Query " + ddlFloor.SelectedValue.ToString() + ", " + txtSuiteNo.Text.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtCompanyID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    txtCFSID.Text = ds.Tables[0].Rows[0]["CID"].ToString();
                    txtCompanyName.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                }
                else
                {
                    showMessage(eMessage.eWarning, "Data not found", "Client data not found. Please check again floor and suite no.");
                }
            }
            catch (Exception me)
            {
                showMessage(eMessage.eError, "getCompanyInfo", me.Message);
            }       
        }

        private void getACLobbyACOutdoorInfo()
        {

            try
            {
                ds = Db.get_list("execute spGetCFSSuiteMonthYearFloor_Query " + ddlMonthPeriod.SelectedValue.ToString() + ", " + txtYearPeriod.Text.ToString() + ", " + ddlFloor.SelectedValue.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtACInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerAC"].ToString(); ;                    
                    txtNonACInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerNonAC"].ToString(); ;
                    txtACOutdoor.Text= ds.Tables[0].Rows[0]["ReadingACOutdoor"].ToString(); ;
                    txtACOutdoorInitialOfficer.Text = ds.Tables[0].Rows[0]["InitialOfficerACOutdoor"].ToString(); ;
                    txtACReadingLobby.Text = ds.Tables[0].Rows[0]["ReadingACLobby"].ToString(); ;
                    txtACInitialOfficerLobby.Text = ds.Tables[0].Rows[0]["InitialOfficerACLobby"].ToString(); ;

                    txtACInitialOfficer.Enabled = false;
                    txtNonACInitialOfficer.Enabled = false;
                    txtACOutdoor.Enabled = false;
                    txtACOutdoorInitialOfficer.Enabled = false;
                    txtACReadingLobby.Enabled = false;
                    txtACInitialOfficerLobby.Enabled = false;
                }
                else
                {
                    //showMessage(eMessage.eWarning, "Data not found", "Client data not found. Please check again floor and suite no.");
                }
            }
            catch (Exception me)
            {
                showMessage(eMessage.eError, "getCompanyInfo", me.Message);
            }            
        }

        private void BindGrid()
        {
            string iSql = "execute spGetUTLECDMRD_Query " + ddlMonthPeriod.SelectedValue.ToString() + ", " + txtYearPeriod.Text.Trim() + "";
            try
            {
                GridView1.DataSource = Db.get_list(iSql);
                GridView1.DataBind();
            }
            catch (SqlException ex)
            {
                showMessage(eMessage.eError, "getCompanyInfo- SqlExeption", ex.Message);
            }
            catch (Exception ex)
            {
                showMessage(eMessage.eError, "getCompanyInfo", ex.Message);
            }            
        }

        private void BindGrid2()
        {
            //string iSql = "select id, CID+'-'+CodeLob+'-'+CodePropertyName+'-'+CodeSeqNo+'-'+CodeYear+CodeMonth as CID, name, address, city, phone, PIC1Name from mCFS where status = 1 order by name";
            string iSql = "exec[spGetCFS]";

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
                //ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;

                hideMessageBox();
                clearAll(this.Form.Controls);
                
                txtYearPeriod.Text = DateTime.Now.Year.ToString();
                ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
                
                getKWhRate();                
                BindGrid();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            hideMessageBox();
            //getCompanyInfo();
            //getACLobbyACOutdoorInfo();

            //if (txtInputDate.Text.ToString() == "")
            //{
            //    txtInputDate.Focus();
            //}else
            //{
            //    txtACReading.Focus();
            //}

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
            //dMonth =ddlMonthPeriod.SelectedValue.ToString();
            //dYear=txtYearPeriod.Text.Trim();

            
            if (!checkData()) { return; }

            dMonth = (Convert.ToInt32(ddlMonthPeriod.SelectedItem.Value.ToString()) - 1).ToString();
            dYear = txtUsageYear.Text.Trim();
            dFloor =ddlFloor.SelectedValue.ToString();
            dDate=txtInputDate.Text.Trim();

            string iACOutdoorUnit = txtACOutdoor.Text;
            string iACOutdoorOfficer = txtACOutdoorInitialOfficer.Text;
            string iLobbyUnit = txtACReadingLobby.Text;
            string iLobbyOfficer = txtACInitialOfficerLobby.Text;
            string iACOfficer = txtACInitialOfficer.Text;
            string iNonACOfficer = txtNonACInitialOfficer.Text;

            isEdit = Convert.ToBoolean(Session["iEditData"]);

            if (isEdit)
            {
                executeEdit((HttpContext.Current.Session["userid"].ToString()), lblEdit.Text, txtKWhRate.Text.Trim(), ddlMonthPeriod.SelectedValue.ToString(),
                    txtYearPeriod.Text.Trim(), txtCompanyID.Text.Trim(), ddlFloor.SelectedValue.ToString(), txtSuiteNo.Text.Trim(),
                    txtACReading.Text.Trim(), txtACInitialOfficer.Text.Trim(), txtNonACReading.Text.Trim(), txtNonACInitialOfficer.Text.Trim(),
                    txtACOutdoor.Text.Trim(), txtACOutdoorInitialOfficer.Text.Trim(), txtACReadingLobby.Text.Trim(),
                    txtACInitialOfficerLobby.Text.Trim(),txtInputDate.Text.Trim());
            }
            else
            {
                executeAdd((HttpContext.Current.Session["userid"].ToString()), txtKWhRate.Text.Trim(), ddlMonthPeriod.SelectedValue.ToString(),
                    txtYearPeriod.Text.Trim(), txtCompanyID.Text.Trim(), ddlFloor.SelectedValue.ToString(), txtSuiteNo.Text.Trim(),
                    txtACReading.Text.Trim(), txtACInitialOfficer.Text.Trim(), txtNonACReading.Text.Trim(), txtNonACInitialOfficer.Text.Trim(),
                    txtACOutdoor.Text.Trim(), txtACOutdoorInitialOfficer.Text.Trim(), txtACReadingLobby.Text.Trim(),
                    txtACInitialOfficerLobby.Text.Trim(), txtInputDate.Text.Trim());
            }
            
            if (bRes)
            {
                bRes = false;
                showMessageModal(eMessage.eSuccess, "Save data", "Your data has been saved.");
                BindGrid();

                hideMessageBox();
                clearAll(this.Form.Controls);

                if (!isEdit)
                {
                    //ddlMonthPeriod.Items.FindByValue(dMonth).Selected = true;
                    //txtYearPeriod.Text = dYear;
                    //ddlFloor.Items.FindByValue(dFloor).Selected = true;
                    txtInputDate.Text = dDate;

                    //txtSuiteNo.Focus();
                    //txtACInitialOfficer.Text = iACOfficer.ToString();
                    //txtNonACInitialOfficer.Text = iNonACOfficer.ToString();
                    txtACOutdoor.Text = iACOutdoorUnit.ToString();
                    txtACOutdoorInitialOfficer.Text = iACOutdoorOfficer.ToString();
                    txtACReadingLobby.Text = iLobbyUnit.ToString();
                    txtACInitialOfficerLobby.Text = iLobbyOfficer.ToString();
                }
            }
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
                executeDelete("tUTLECDMRD", HttpContext.Current.Session["iIDData"].ToString(), txtReasonToDelete.Text.Trim(), HttpContext.Current.Session["userid"].ToString());
                Session["iIDData"] = "";
                BindGrid();
            }
        }

        protected void ddlMonthPeriod_Change(object sender, EventArgs e)
        {
            DateTime origDT = Convert.ToDateTime(txtYearPeriod.Text + "-" + ddlMonthPeriod.SelectedValue.ToString() + "-01" );
            DateTime lastDate = new DateTime(origDT.Year, origDT.Month, 1).AddMonths(1).AddDays(-1);

            //txtStartPeriod.Text = origDT.ToString("dd/MM/yyyy");
            //txtEndPeriod.Text = lastDate.ToString("dd/MM/yyyy");
            getKWhRate();
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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

            GridViewRow gvrow = GridView1.Rows[index];
            string kode = GridView1.DataKeys[index].Value.ToString();
            iIDDat = kode;
            lblEdit.Text = kode;
            Session["iEditData"] = true;
            Session["iIDData"] = kode;

            string iMon =Convert.ToString( Convert.ToInt32( HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString())+1);
            string iYear = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();
            string iFloor = HttpUtility.HtmlDecode(gvrow.Cells[7].Text).ToString();
            string iSuite = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();

            if (iMon == "12")
            {
                iMon = "1";
                iYear = Convert.ToString(Convert.ToInt32(iYear) + 1);
            }

            if (e.CommandName.Equals("editRecord"))
            {
                if (!checkDataRecord(iMon, iYear, iSuite, kode)) { return; };
                //getEdit(kode);

                ddlMonthPeriod.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();
                ddlFloor.Text = HttpUtility.HtmlDecode(gvrow.Cells[7].Text).ToString();
                txtSuiteNo.Text = HttpUtility.HtmlDecode(gvrow.Cells[8].Text).ToString();

                ds = Db.get_list("Select CFSID From tUTLECDMRD Where ID = " + kode);
                txtCompanyID.Text = ds.Tables[0].Rows[0]["CFSID"].ToString();
                
                txtCFSID.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString();
                txtCompanyName.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text).ToString();

                //txtInputDate.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();

                txtACReading.Text = HttpUtility.HtmlDecode(gvrow.Cells[9].Text).ToString();
                txtACInitialOfficer.Text = HttpUtility.HtmlDecode(gvrow.Cells[10].Text).ToString();

                txtNonACReading.Text = HttpUtility.HtmlDecode(gvrow.Cells[11].Text).ToString();
                txtNonACInitialOfficer.Text = HttpUtility.HtmlDecode(gvrow.Cells[12].Text).ToString();

                txtACOutdoor.Text = HttpUtility.HtmlDecode(gvrow.Cells[13].Text).ToString();
                txtACOutdoorInitialOfficer.Text = HttpUtility.HtmlDecode(gvrow.Cells[14].Text).ToString();

                txtACReadingLobby.Text = HttpUtility.HtmlDecode(gvrow.Cells[15].Text).ToString();
                txtACInitialOfficerLobby.Text = HttpUtility.HtmlDecode(gvrow.Cells[16].Text).ToString();

                txtInputDate.Text = HttpUtility.HtmlDecode(gvrow.Cells[17].Text).ToString();

            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                if (!checkDataRecord(iMon, iYear, iSuite, kode)) { return; };

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
                iQuery = "Exec spGetCFSSuite_Query " + kode;

                //ds = Db.get_list(iQuery);
                
                ddlFloor.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text).ToString();
                txtSuiteNo.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text).ToString();
                txtCompanyID.Text = kode;
                txtCFSID.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString(); 
                txtCompanyName.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString(); 
            }
        }
    }
}