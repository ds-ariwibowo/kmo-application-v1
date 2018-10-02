using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMO.Class;

namespace KMO.Controls
{
    public partial class C_TopMenu : System.Web.UI.UserControl
    {
        protected void FillMenu()
        {
            //lit_nama_user.Text = (Session["userid"] != null) ? Session["userid"].ToString() : "-";
            //lit_no_polis_aktif.Text = (Session["no_polis_aktif"] != null) ? Session["no_polis_aktif"].ToString() : "-";

            if (Cf.GetUrl("FileUrl").ToLower() == ("Default" + ".aspx")) { li_home.Attributes.Add("class", "active"); }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("CFS" + ".aspx")) { li_setup.Attributes.Add("class", "active"); }
            else if (Cf.GetUrl("FileUrl").ToLower() == ("SumCFS" + ".aspx")) { li_setup.Attributes.Add("class", "active"); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //divMenu.Visible = false ;

            if (!IsPostBack)
            {
                //Vd.isValidLogin();
                //FillMenu();
            }
            else
            {
                //FillMenu();
            }

            
        }     
    }
}