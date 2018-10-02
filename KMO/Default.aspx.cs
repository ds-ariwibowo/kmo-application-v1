using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KMO.Class;

namespace KMO
{
    public partial class _Default : Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            Vd.isValidLogin();

            if (!IsPostBack)
            {
                string v = Request.QueryString["LogOut"];
                if (v != null)
                {
                    Session["userid"] = null;
                    HttpContext.Current.Response.Redirect("./Default.aspx");
                }
            } 
        }
    }
}
