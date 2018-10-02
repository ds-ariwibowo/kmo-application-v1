using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using clsSecurity.Encryption;

using KMO.Class;

namespace KMO
{
    public partial class LogIn : System.Web.UI.Page
    {
        DataSet ds;
        DataTable dt;
        Boolean bRes;
        enum eMessage :byte {eSuccess=1, eWarning=2, eError=3};

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

        private void getDept(int iUserID)
        {
            try
            {
                ds = Db.get_list("execute spGetUserDept_Query " + iUserID);

                Session["userdept"] = ds.Tables[0].Rows[0]["DepartmentID"].ToString();

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "getDept", me.Message);
            }
        }

        private void checkUserPassword(string iUser, string iPass)
        {
            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spCheckLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter parm = new SqlParameter("@Result", SqlDbType.Int);
                parm.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(parm);
                cmd.Parameters.Add(new SqlParameter("@UserName", iUser));
                cmd.Parameters.Add(new SqlParameter("@Password", iPass));

                SqlParameter returnParameter = cmd.Parameters.Add("@Result", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                cmd.ExecuteNonQuery();
                conn.Close();

                int id = Convert.ToInt32(parm.Value);
                if (id> 0) {
                    Session["userid"] = id;
                    getDept(id);
                    bRes = true;
                }
                else
                {
                    showMessageModal(eMessage.eWarning, "checkUserPassword", "Invalid user name or password.");                 
                }
                
            }
            catch (Exception me)
            {
                bRes = false;
                showMessageModal(eMessage.eError, "checkUserPassword", me.Message);                   
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = SyahDesignCryptograhy.Encrypt(txtUserName.Text.Trim());
            string userPassword = SyahDesignCryptograhy.Encrypt(txtPassword.Text.Trim()); 

            //checkLogin(userName, userPassword);
            checkUserPassword(userName, userPassword);

            if (bRes)
            {
                HttpContext.Current.Response.Redirect("Default.aspx");
            }
            else
            {

            }

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            hideMessageBox();
            txtUserName.Focus();
        }
    }
}