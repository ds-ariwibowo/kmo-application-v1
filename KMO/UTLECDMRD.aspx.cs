using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;
using System.Configuration;
using System.Xml.Linq;

using KMO.Class;

namespace KMO.Transaction
{
    public partial class UTLECDMRD : System.Web.UI.Page
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
        
        private void getAllCtl(ControlCollection ctls)
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
                    getAllCtl(c.Controls);

                }
            }
        }

        private void addDataPerLantai(int iCFSID, string iNoSuite, int iReadingMeterAC, string iInitialOfficerAC, 
                                        int iReadingMeterNonAC, string iInitialOfficerNonAC, 
                                        int iReadingMeterACOutdoor, string iInitialOfficerACOutdoor)
        {
            string connString = Db.GetConnectionString();


            try
            {
                SqlConnection conn = new SqlConnection(Db.GetConnectionString());
                conn.Open();

                SqlCommand cmd = new SqlCommand("spInsertCFS", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", (HttpContext.Current.Session["userid"].ToString()));
                cmd.Parameters.AddWithValue("@KWhRate", "");
                cmd.Parameters.AddWithValue("@Month", "");
                cmd.Parameters.AddWithValue("@Year", "");
                cmd.Parameters.AddWithValue("@CFSID", "");
                cmd.Parameters.AddWithValue("@Floor", "");
                cmd.Parameters.AddWithValue("@SuiteNo", "");
                cmd.Parameters.AddWithValue("@ReadingAC", "");
                cmd.Parameters.AddWithValue("@InitialOfficerAC", "");
                cmd.Parameters.AddWithValue("@ReadingNonAC", "");
                cmd.Parameters.AddWithValue("@InitialOfficerNonAC", "");
                cmd.Parameters.AddWithValue("@ReadingACOutdoor", "");
                cmd.Parameters.AddWithValue("@InitialACOutdoor", "");
                
                cmd.ExecuteNonQuery();
                conn.Close();                

                bRes = true;
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "addDataPerLantai", me.Message);
            }
        }

        private void getValue(ControlCollection ctls)
        {
            try
            {
                int i = 901;
                int j = 9;

                DateTime iDate = DateTime.ParseExact(txtUTLRecordDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);

                string iY = iDate.Year.ToString();
                string iYear = Str.Right(iY, 2);


                string rMon = iDate.Month.ToString();
                string iMon = "";
                if (rMon.Length == 1)
                {
                    iMon = "0" + rMon;
                }
                else { iMon = rMon; }

                string iSql = "spInsertCFS " + HttpContext.Current.Session["userid"].ToString() + ", " + txtKWhRate.Text.Trim() + ", " + iMon + ", " + iYear + ", " + "1 ";
                string iRes = "";

                foreach (Control c in ctls)
                {
                    iRes = "";
                    if (c is System.Web.UI.WebControls.TextBox)
                    {

                        i = 901;

                        while (i < 916)
                        {
                            //AC
                            if (c.ID == "txtACReading" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }
                            if (c.ID == "txtACInitial" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }

                            //Non AC
                            if (c.ID == "txtNonACReading" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }
                            if (c.ID == "txtNonACInitial" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }

                            //AC Outdoor
                            if (c.ID == "txtACOutdoorReading" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }
                            if (c.ID == "txtNonACOutdoorInitial" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }

                            //Lobby
                            if (c.ID == "txtACReadingLoby" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }
                            if (c.ID == "txtACInitialLoby" + i)
                            {
                                if (((TextBox)c).Text != "") { iRes = iRes + ", " + ((TextBox)c).Text; }
                            }

                            i++;
                        }                        
                    }

                    if (c.HasControls())
                    {
                        getValue(c.Controls);

                    }                    
                }

                iRes = iRes + "";
                if (iRes.Trim() != "")
                {
                    iSql = iSql + iRes + "      ";

                    bRes = true;
                }
                
            }
            catch (Exception me)
            {
                bRes = false;
                showMessage(eMessage.eError, "getValue", me.Message);
            }
            
            
        }

        private void excuteAddData(){

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Vd.isValidLogin();
            bRes = false;

            if (!IsPostBack)
            {
                hideMessageBox();
            }
            else
            {

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            getValue(this.Form.Controls);
            if (bRes)
            {
                getAllCtl(this.Form.Controls);
                hideMessageBox();

                bRes = false;
            }
            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            getAllCtl(this.Form.Controls);
            hideMessageBox();
        }

        
    }
}