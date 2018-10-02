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
    public partial class UnitPrice : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;
        Boolean isEdit;
        string iIDDat;

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
        }

        private void executeAdd(string iUserID, string iMonth, string iYear, string iKWhRate, 
                                string iPPJ, string iAdmin, string iMaterai)
        {
            string connString = Db.GetConnectionString();

            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertUnitPrice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@KWhRate", iKWhRate);
                cmd.Parameters.AddWithValue("@PPJ", iPPJ);
                cmd.Parameters.AddWithValue("@Admin", iAdmin);
                cmd.Parameters.AddWithValue("@Materai", iMaterai);
                

                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "executeAdd", me.Message);
            }
        }

        private void executeEdit(string iID, string iUserID, string iMonth, string iYear, string iKWhRate,
                                string iPPJ, string iAdmin, string iMaterai)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spUpdateUnitPrice", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", iID);
                cmd.Parameters.AddWithValue("@UserID", iUserID);
                cmd.Parameters.AddWithValue("@Month", iMonth);
                cmd.Parameters.AddWithValue("@Year", iYear);
                cmd.Parameters.AddWithValue("@KWhRate", iKWhRate);
                cmd.Parameters.AddWithValue("@PPJ", iPPJ);
                cmd.Parameters.AddWithValue("@Admin", iAdmin);
                cmd.Parameters.AddWithValue("@Materai", iMaterai);


                cmd.ExecuteNonQuery();
                conn.Close();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "executeEdit", me.Message);
            }
        }

        private void loadData()
        {
            try
            {
                hideMessageBox();

                string iMonth = ddlMonthPeriod.SelectedValue.ToString();
                string iYear = txtYearPeriod.Text.ToString();

                if (iMonth == "")
                {
                    iMonth = DateTime.Now.Month.ToString();
                }
                if (iYear =="")
                {                    
                    iYear = DateTime.Now.Year.ToString();
                    
                }

                ds = Db.get_list("execute spGetUnitPrice " + iMonth + ", " + iYear) ;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtID.Text= ds.Tables[0].Rows[0]["ID"].ToString();
                    txtKWhRate.Text = ds.Tables[0].Rows[0]["KWhRate"].ToString();
                    txtPPJ.Text = ds.Tables[0].Rows[0]["PPJ"].ToString();
                    txtMaterai.Text = ds.Tables[0].Rows[0]["Materai"].ToString();
                    txtAdmin.Text = ds.Tables[0].Rows[0]["Admin"].ToString();

                    isEdit = true;
                }
                else
                {
                    txtID.Text = "";
                    txtKWhRate.Text = "";
                    txtPPJ.Text = "";
                    txtMaterai.Text = "";
                    txtAdmin.Text = "";

                    isEdit = false;
                }
            }
            catch (Exception ex)
            {
                showMessage(eMessage.eError, "loadData", ex.Message);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Vd.isValidLogin();

            if (!IsPostBack)
            {
                ddlMonthPeriod.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
                txtYearPeriod.Text = DateTime.Now.Year.ToString();
                hideMessageBox();
                isEdit = false;
                loadData();
            }
        }

        protected void ddlMonthPeriod_Change(object sender, EventArgs e)
        {
            loadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string iMonth = ddlMonthPeriod.SelectedValue.ToString();
            string iYear = txtYearPeriod.Text.ToString();

            if (iMonth == "")
            {
                iMonth = DateTime.Now.Month.ToString();
            }
            if (iYear == "")
            {
                iYear = DateTime.Now.Year.ToString();

            }

            ds = Db.get_list("execute spGetUnitPrice " + iMonth + ", " + iYear);
            if (ds.Tables[0].Rows.Count > 0)
            { isEdit = true; }else { isEdit = false; }

                if (isEdit)
            {
                executeEdit(txtID.Text.Trim(), HttpContext.Current.Session["userid"].ToString(), ddlMonthPeriod.SelectedValue.ToString(),
                            txtYearPeriod.Text.Trim(), txtKWhRate.Text.Trim(), txtPPJ.Text.Trim(), txtAdmin.Text.Trim(), txtMaterai.Text.Trim());
            }
            else
            {
                executeAdd(HttpContext.Current.Session["userid"].ToString(), ddlMonthPeriod.SelectedValue.ToString(),
                            txtYearPeriod.Text.Trim(), txtKWhRate.Text.Trim(), txtPPJ.Text.Trim(), txtAdmin.Text.Trim(), txtMaterai.Text.Trim());
            }

            if (bRes)
            {
                bRes = false;
                showMessage(eMessage.eSuccess, "Save data", "Save data success.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            hideMessageBox();
            clearAll(this.Form.Controls);
            isEdit = false;

        }
    }
}