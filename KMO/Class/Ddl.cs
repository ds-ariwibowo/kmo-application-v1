using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KMO;

namespace KMO.Class
{
    public class Ddl
    {
        /// Class Function : Dropdownlist
        /// 

        public static void JenisKelamin(DropDownList x, string id)
        {
            x.Items.Clear();
            x.Items.Insert(0, new ListItem("Laki-laki","M"));
            x.Items.Insert(1, new ListItem("Perempuan", "F"));

            try { x.Items.FindByValue(id).Selected = true; }
            catch (Exception ex) { }
        }

        public static void Bank(DropDownList x, string id)
        {
            WebService_KMO.Service ws = new WebService_KMO.Service();
            DataSet ds = new DataSet();

            ds = ws.get_ddl_list_bank("");
            x.DataSource = ds;
            x.DataTextField = "DDL_TXT";
            x.DataValueField = "DDL_VAL";
            x.DataBind();

            try { x.Items.FindByValue(id).Selected = true; }
            catch (Exception ex) { }
        }

        public static void CaraBayarPremi(DropDownList x, string id)
        {
            WebService_KMO.Service ws = new WebService_KMO.Service();
            DataSet ds = new DataSet();

            ds = ws.get_ddl_list_premium_type("");
            x.DataSource = ds;
            x.DataTextField = "DDL_TXT";
            x.DataValueField = "DDL_VAL";
            x.DataBind();

            try { x.Items.FindByValue(id).Selected = true; }
            catch (Exception ex) { }
        }

        public static void JenisDokumenPengajuan(DropDownList x, string id)
        {
            WebService_KMO.Service ws = new WebService_KMO.Service();
            DataSet ds = new DataSet();

            ds = ws.get_ddl_list_jenis_dokumen_pengajuan();
            x.DataSource = ds;
            x.DataTextField = "DDL_TXT";
            x.DataValueField = "DDL_VAL";
            x.DataBind();

            try { x.Items.FindByValue(id).Selected = true; }
            catch (Exception ex) { }
        }

        public static void PilihPolisAktif(DropDownList x, string id, string username)
        {
            WebService_KMO.Service ws = new WebService_KMO.Service();
            DataSet ds = new DataSet();

            ds = ws.get_ddl_polis_login_userid(username);
            x.DataSource = ds;
            x.DataTextField = "DDL_TXT";
            x.DataValueField = "DDL_VAL";
            x.DataBind();

            try { x.Items.FindByValue(id).Selected = true; }
            catch (Exception ex) { }
        }

    }
}