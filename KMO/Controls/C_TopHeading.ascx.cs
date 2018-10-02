using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMO.Class;

namespace KMO.Controls
{
    public partial class C_TopHeading : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillHeading();
            }
            else
            {
                FillHeading();
            }

            
        }

        protected void FillHeading()
        {
            if (Cf.GetUrl("FileUrl").ToLower() == ("home" + ".aspx")) { lit_heading.Text = "Home"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengajuan" + ".aspx")) { lit_heading.Text = "List Pengajuan"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengajuaninput" + ".aspx")) { lit_heading.Text = "Input Pengajuan"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengajuandetail" + ".aspx")) { lit_heading.Text = "Detail Pengajuan"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengajuanupload" + ".aspx")) { lit_heading.Text = "Upload Pengajuan"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengajuanview" + ".aspx")) { lit_heading.Text = "View Pengajuan"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("verifikasidata" + ".aspx")) { lit_heading.Text = "Verifikasi Data"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("verifikasidataunderwriting" + ".aspx")) { lit_heading.Text = "Verifikasi Data Underwriting"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("dokumen" + ".aspx")) { lit_heading.Text = "Dokumen"; }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("pengaturan" + ".aspx")) { lit_heading.Text = "Pengaturan"; }
        }
    }
}