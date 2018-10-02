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
    public partial class Others : System.Web.UI.Page
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
            if (txtItem.Text == "")
            {
                showMessageModal(eMessage.eError, "Data still empty", "Item is empty.");
                return false;
            }
            if (txtDescription.Text == "")
            {
                showMessageModal(eMessage.eError, "Data still empty", "Description is empty.");
                return false;
            }
            if (txtAmount.Text == "")
            {
                showMessageModal(eMessage.eError, "Data still empty", "Amount is empty.");
                return false;
            }

            return true;
        }

        private void clearData()
        {
            bEdit = false;
            
            txtItem.Text = "";
            txtDescription.Text = "";
            txtAmount.Text = "";
            
            Session["Edit"] = false;

            BindGrid(txtCompanyID.Text);
        }

        private void BindGrid(string iCFSID)
        {
            string iSql = "execute spGetOTH_Query " + iCFSID + " ";
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

        private void executeAdd(string iCFSID, string iSuiteNo, string iFloor, string iItem, string iDescription, string iAmount)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertInvoiceOTH", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@SuiteNo", iSuiteNo);
                cmd.Parameters.AddWithValue("@Floor", iFloor);
                cmd.Parameters.AddWithValue("@Item", iItem);
                cmd.Parameters.AddWithValue("@Description", iDescription);
                cmd.Parameters.AddWithValue("@Amount", iAmount);
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

        private void executeEdit(string iID, string iCFSID, string iSuiteNo, string iFloor, string iItem, string iDescription, string iAmount)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateInvoiceOTH", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@CFSID", iCFSID);
                cmd.Parameters.AddWithValue("@SuiteNo", iSuiteNo);
                cmd.Parameters.AddWithValue("@Floor", iFloor);
                cmd.Parameters.AddWithValue("@Item", iItem);
                cmd.Parameters.AddWithValue("@Description", iDescription);
                cmd.Parameters.AddWithValue("@Amount", iAmount);
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

                SqlCommand cmd = new SqlCommand("spCreateInvoiceOTHNo ", conn);
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

            if (Session["Edit"].ToString()=="True")
            {
                executeEdit(lblID.Text, txtCompanyID.Text, txtSuiteNo.Text, txtFloor.Text, txtItem.Text,txtDescription.Text,txtAmount.Text);
            }
            else
            {
                executeAdd(txtCompanyID.Text, txtSuiteNo.Text, txtFloor.Text, txtItem.Text, txtDescription.Text, txtAmount.Text);
            }
            if (bRes)
            {
                clearData();                            

                BindGrid(txtCompanyID.Text);
            }
        }

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            if (!checkData("0")) { return; }

            //executeCreateInvoice(String.Format("{0:mm}", iStartDate.Month.ToString()), iStartDate.Year.ToString());

            if (bRes)
            {
                clearData();
                lblID.Text = "";
                txtCFSID.Text = "";
                txtCompanyID.Text = "";
                txtCompanyName.Text = "";
                txtFloor.Text = "";
                txtSuiteNo.Text = "";
                
                BindGrid("0");
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
                //return;
            }
            else
            {
                executeDelete("tInvoiceOTH", lblID.Text,
                    txtReasonToDelete.Text.Trim(), HttpContext.Current.Session["userid"].ToString());

                BindGrid(txtCompanyID.Text);
            }
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            BindGrid(txtCompanyID.Text);
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

                txtItem.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text);
                txtDescription.Text = HttpUtility.HtmlDecode(gvrow.Cells[5].Text);
                txtAmount.Text = HttpUtility.HtmlDecode(gvrow.Cells[6].Text);
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
                    BindGrid(txtCompanyID.Text);
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

                BindGrid(kode);
            }
        }
    }
}