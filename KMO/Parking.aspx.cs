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
    public partial class Parking : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;
        Boolean bEdit;

        enum eMessage : byte { eSuccess = 1, eWarning = 2, eError = 3 };
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

        private Boolean checkData(string iPilih)
        {
            if (txtFloor.Text == "") {
                showMessageModal(eMessage.eError, "Data still empty", "Tenant data is empty.");
                return false;
            }
            if (txtParkingStartPeriod.Text == "")
            {
                showMessageModal(eMessage.eError, "Data still empty", "Start period is empty.");
                return false;
            }

            try
            {
                DateTime iStartDate = DateTime.ParseExact(txtParkingStartPeriod.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
            }
            catch (Exception me)
            {
                showMessageModal(eMessage.eWarning, "Format date", "Please input your parking format date to dd-MM-yyyy.");
                return false;
            }

            if (iPilih == "1")
            {
                if (ddlParkingType.Text == "")
                {
                    showMessageModal(eMessage.eError, "Data still empty", "Parking type is empty.");
                    return false;
                }
                if (txtParkingLot.Text == "")
                {
                    showMessageModal(eMessage.eError, "Data still empty", "Lot data is empty.");
                    return false;
                }
                if (txtParkingPeriod.Text == "")
                {
                    showMessageModal(eMessage.eError, "Data still empty", "Period is empty.");
                    return false;
                }
                if (txtParkingPrice.Text == "")
                {
                    showMessageModal(eMessage.eError, "Data still empty", "Price data is empty.");
                    return false;
                }
                if (txtParkingLicenseNo.Text == "")
                {
                    showMessageModal(eMessage.eError, "Data still empty", "License data is empty.");
                    return false;
                }
            }

            return true;
        }

        private void clearData()
        {
            bEdit = false;
            ddlParkingType.Text = "";
            txtParkingLot.Text = "";
            txtParkingPeriod.Text = "";
            txtParkingPrice.Text = "";
            txtParkingLicenseNo.Text = "";

            Session["Edit"] = false;

            BindGridParking(txtCompanyID.Text);
        }

        private void BindGridParking(string iCFSID)
        {
            string iSql = "execute spGetCFSParking_Query " + iCFSID + " ";
            try
            {
                GridView2.DataSource = Db.get_list(iSql);
                GridView2.DataBind();
            }
            catch (SqlException ex)
            {
                showMessageModal(eMessage.eError, "BindGridParking", ex.Message);
            }
            catch (Exception ex)
            {
                showMessageModal(eMessage.eError, "BindGridParking", ex.Message);
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
                showMessageModal(eMessage.eError, "BindGrid2- SqlExeption", ex.Message);
            }
            catch (Exception ex)
            {
                showMessageModal(eMessage.eError, "BindGrid2", ex.Message);
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
                showMessageModal(eMessage.eError, "executeDelete", me.Message);
            }

        }

        private void executeAdd(string iMonth, string iYear, string iCFSID, string iStartDate, string iParkingType,
                                string iLot, string iLotNo, string iLicenseNo, string iPeriod, string iPeriodMY, string iCharge)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertInvoicePAK", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@StartDate", iStartDate);
                cmd.Parameters.AddWithValue("@Type", iParkingType);
                cmd.Parameters.AddWithValue("@Lot", iLot);
                cmd.Parameters.AddWithValue("@LotNo", iLotNo);
                cmd.Parameters.AddWithValue("@LicenseNo", iLicenseNo);
                cmd.Parameters.AddWithValue("@Period", iPeriod);
                cmd.Parameters.AddWithValue("@PeriodMY", iPeriodMY);
                cmd.Parameters.AddWithValue("@Charge", iCharge);
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

        private void executeEdit(string iID, string iCFSID, string iStartDate, string iParkingType,
                                string iLot, string iLotNo, string iLicenseNo, string iPeriod, string iPeriodMY, string iCharge)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateInvoicePAK", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@StartDate", iStartDate);
                cmd.Parameters.AddWithValue("@Type", iParkingType);
                cmd.Parameters.AddWithValue("@Lot", iLot);
                cmd.Parameters.AddWithValue("@LotNo", iLotNo);
                cmd.Parameters.AddWithValue("@LicanseNo", iLicenseNo);
                cmd.Parameters.AddWithValue("@Period", iPeriod);
                cmd.Parameters.AddWithValue("@PeriodMY", iPeriodMY);
                cmd.Parameters.AddWithValue("@Charge", iCharge);
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

        private void executeCreateInvoice(string iMonth, string iYear)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spCreateInvoicePAKNo ", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                
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

        private void executeAddParking(string iCFSID, string iParkingType, string iLot, string iPeriod,
                            string iPrice, string iLicenseNo, string iStartDate)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertCFSParking", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@ParkingType", iParkingType);
                cmd.Parameters.AddWithValue("@Lot", iLot);
                cmd.Parameters.AddWithValue("@Period", iPeriod);
                cmd.Parameters.AddWithValue("@PeriodMY", "Month(s)");
                cmd.Parameters.AddWithValue("@Price", iPrice);
                cmd.Parameters.AddWithValue("@LicenseNo", iLicenseNo);
                cmd.Parameters.AddWithValue("@StartDate", iStartDate);
                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "executeAddParking", me.Message);
            }
        }        

        private void executeEditParking(string iID, string iCFSID, string iParkingType, string iLot, string iPeriod,
                                    string iPrice, string iLicenseNo, string iStartDate)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateCFSParking", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@ParkingType", iParkingType);
                cmd.Parameters.AddWithValue("@Lot", iLot);
                cmd.Parameters.AddWithValue("@Period", iPeriod);
                cmd.Parameters.AddWithValue("@PeriodMY", "Month(s)");
                cmd.Parameters.AddWithValue("@Price", iPrice);
                cmd.Parameters.AddWithValue("@LicenseNo", iLicenseNo);
                cmd.Parameters.AddWithValue("@StartDate", iStartDate);
                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "executeEditParking", me.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            bRes = false;

            Vd.isValidLogin();

            if (!IsPostBack)
            {
                Session["Edit"] = false;
            }
            else
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindGrid2();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#selectTenantModal').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SelectTenantModalScript", sb.ToString(), false);

            UpdatePanel1.Update();
        }

        protected void btnAddParking_Click(object sender, EventArgs e)
        {
            if (!checkData("1")) { return; }

            DateTime iStartDate = DateTime.ParseExact(txtParkingStartPeriod.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            if (Session["Edit"].ToString()=="True")
            {
                //executeEditParking(lblID.Text, txtCompanyID.Text, ddlParkingType.SelectedValue.ToString(),
                //    txtParkingLot.Text, txtParkingPeriod.Text, txtParkingPrice.Text,
                //    txtParkingLicenseNo.Text,iStartDate.ToString());

                executeEdit(lblID.Text, txtCompanyID.Text, iStartDate.ToString(), ddlParkingType.ToString(), "1", txtParkingLot.Text,
                    txtParkingLicenseNo.Text, txtParkingPeriod.Text, "Month(s)", txtParkingPrice.Text);
            }
            else
            {
                //executeAddParking(txtCompanyID.Text, ddlParkingType.SelectedValue.ToString(),
                //    txtParkingLot.Text, txtParkingPeriod.Text, txtParkingPrice.Text,
                //    txtParkingLicenseNo.Text, iStartDate.ToString());
                executeAdd(String.Format("{0:mm}",iStartDate.Month.ToString()),iStartDate.Year.ToString(), txtCompanyID.Text,
                    iStartDate.ToString(),ddlParkingType.SelectedItem.Text,"1",txtParkingLot.Text,txtParkingLicenseNo.Text,
                    txtParkingPeriod.Text, "Month(s)",txtParkingPrice.Text);
            }
            if (bRes)
            {
                clearData();                            

                BindGridParking(txtCompanyID.Text);
            }
        }

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            if (!checkData("0")) { return; }

            DateTime iStartDate = DateTime.ParseExact(txtParkingStartPeriod.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

            executeCreateInvoice(String.Format("{0:mm}", iStartDate.Month.ToString()), iStartDate.Year.ToString());

            if (bRes)
            {
                clearData();
                lblID.Text = "";
                txtCFSID.Text = "";
                txtCompanyID.Text = "";
                txtCompanyName.Text = "";
                txtFloor.Text = "";
                txtSuiteNo.Text = "";
                
                BindGridParking("0");
            }
        }

        protected void btnCancelParking_Click(object sender, EventArgs e)
        {
            clearData();
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtReasonToDelete.Text.Trim() == "")
            {
                showMessageModal(eMessage.eWarning, "Reason is empty.", "Please add your reason to delete data.");
                txtReasonToDelete.Focus();
            }
            else
            {
                executeDelete("tInvoicePAK", lblID.Text,
                    txtReasonToDelete.Text.Trim(), HttpContext.Current.Session["userid"].ToString());

                BindGridParking(txtCompanyID.Text);
            }
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGridParking(txtCompanyID.Text);
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (Session["DetailCFS"] != null) { showMessage(eMessage.eWarning, "Cannot Edit Data", "You cannot edit this data in detailed mode."); return; }

            int index = 0;
            double num;
            string myString = e.CommandArgument.ToString();
            bool isNumber = double.TryParse(myString, out num);
            Session["Edit"] = false;

            if (isNumber)
            {
                index = Convert.ToInt32(e.CommandArgument);
            }

            if (e.CommandName.Equals("editRecord"))
            {
                GridViewRow gvrow = GridView2.Rows[index];
                string kode = GridView2.DataKeys[index].Value.ToString();

                ////getEdit(kode);

                Session["Edit"] = true;

                lblID.Text = kode;

                ddlParkingType.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text);
                txtParkingLot.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text);
                txtParkingStartPeriod.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text);
                txtParkingPeriod.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text);
                txtParkingLicenseNo.Text = HttpUtility.HtmlDecode(gvrow.Cells[8].Text);
                txtParkingPrice.Text = HttpUtility.HtmlDecode(gvrow.Cells[9].Text);
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                string kode = GridView2.DataKeys[index].Value.ToString();

                GridViewRow gvrow = GridView2.Rows[index];

                Session["iIDDataParking"] = kode;

                txtReasonToDelete.Text = "";

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);                
                
                if (bRes)
                {                    
                    BindGridParking(txtCompanyID.Text);
                }
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
                txtCompanyID.Text = kode;
                txtCFSID.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text).ToString();
                txtCompanyName.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text).ToString();


                clearData();
                txtParkingStartPeriod.Text = "";

                BindGridParking(kode);              
            }
        }
    }
}