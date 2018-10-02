using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Globalization;

using KMO.Class;

namespace KMO.Report
{
    public partial class SumCFS : System.Web.UI.Page
    {
        DataTable dt;
        DataSet ds;
        Boolean bRes;
        Boolean bAwal;
        Boolean bNoData;
        DateTime thisDay = DateTime.Today;

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

        private void executeDelete(string iTableName,string id, string iReason, string userID)
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

        private void CreateDynamicGrid()
        {
            try
            {
                string iQuery;

                ds = Db.get_list("exec spGetCFS");

                bool hasRows = ds.Tables.Cast<DataTable>()
                                               .Any(table => table.Rows.Count != 0);

                //execute the select statement
                DataView dvProducts = ds.Tables[0].DefaultView;
                DataTable dtProducts = dvProducts.Table;

                BoundField boundField = null;

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

                        //if (iNoKolom > 6)
                        //{
                        //    boundField.ItemStyle.HorizontalAlign = HorizontalAlign.Right;
                        //    boundField.DataFormatString = "{0:N0}";
                        //}
                        //else
                        //{

                        //}



                        //Add the field to the GridView columns.
                        GridView1.Columns.Add(boundField);

                    }
                    //bind the gridview the DataSource
                }

                GridView1.DataSource = dtProducts;

                GridView1.DataBind();

            }
            catch (Exception ex)
            {
                if (ex.Message == "Cannot find table 0.")
                {
                    bNoData = true;
                    CreateDynamicGrid();
                }
                else
                {
                    showMessage(eMessage.eError, "CreateDynamicGrid", ex.Message);    
                }
            }

        }

        private void BindGrid()
        {
            try
            {
                GridView1.DataSource = Db.get_list("execute spGetCFS");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                System.Console.Error.Write(ex.Message);
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["EditCFS"] = null;
            Session["DetailCFS"] = null;

            Vd.isValidLogin();

            if (!IsPostBack)
            {
                bAwal = true;
                bNoData = false;

                hideMessageBox();
                //CreateDynamicGrid();
                BindGrid();
            }
            else
            {

            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {            
            HttpContext.Current.Response.Redirect("./CFS.aspx"); 
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtReasonToDelete.Text.Trim() == "")
            {
                showMessage(eMessage.eWarning, "Reason is empty.", "Please add your reason to delete data.");
                txtReasonToDelete.Focus();
            }
            else
            {
                executeDelete("mCFSSuite", lblIDSuite.Text, txtReasonToDelete.Text.Trim(), HttpContext.Current.Session["userid"].ToString());
                if (bRes ){
                    Session["iIDData"] = "";
                    BindGrid();
                    hideMessageBox();
                }
                txtReasonToDelete.Text = "";
            }
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

            if (isNumber)
            {
                index = Convert.ToInt32(e.CommandArgument);
            }

            if (e.CommandName.Equals("detail"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                string kode = GridView1.DataKeys[index].Value.ToString();

                Session["EditCFS"] = true;
                Session["DetailCFS"] = true;
                Session["EditIDCFS"] = kode;
                Session["EditLoo"] = gvrow.Cells[5].Text;
                Session["SuiteNo"] = gvrow.Cells[11].Text;
                HttpContext.Current.Response.Redirect("./CFS.aspx");

            }
            else if (e.CommandName.Equals("editRecord"))
            {
                GridViewRow gvrow = GridView1.Rows[index];
                string kode = GridView1.DataKeys[index].Value.ToString();
                
                Session["EditCFS"] = true;
                Session["EditIDCFS"] = kode;
                Session["EditLoo"] = gvrow.Cells[5].Text;
                Session["SuiteNo"] = gvrow.Cells[11].Text;
                HttpContext.Current.Response.Redirect("./CFS.aspx");
                
            }
            else if (e.CommandName.Equals("deleteRecord"))
            {
                //string kode = GridView1.DataKeys[index].Value.ToString();
                //executeDelete("mCFS", kode, "None", Session["userid"].ToString());
                //BindGrid();

                GridViewRow gvrow = GridView1.Rows[index];
                string kode = GridView1.DataKeys[index].Value.ToString();
                Session["iIDData"] = kode;

                lblIDSuite.Text = "";
                txtReasonToDelete.Text = "";

                string iSql = "select * from mCFSSuite where status = 1 and CFSID = " + kode + " and SuiteNo = " + gvrow.Cells[11].Text;
                ds = Db.get_list(iSql);
                
                lblIDSuite.Text= ds.Tables[0].Rows[0]["ID"].ToString();

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#deleteModal').modal('show');");
                sb.Append(@"</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteModalScript", sb.ToString(), false);

                //UpdatePanel3.Update();
            }
        }
    }
}