using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Threading;
using System.Net;
using System.Reflection;
using System.Text;
using System.Globalization;
using System.Configuration;

namespace KMOWEBSERVICE
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {
        /* -------------------------------------------------------- WS KONEKSI -----------------------------------------------------------------------------  */
        //[WebMethod]
        public string GetConnectionString()
        {
            if (ConfigurationManager.AppSettings["isDevelopment"].ToString() == "yes")
            {
                return ConfigurationManager.ConnectionStrings["ConnStrDEV"].ConnectionString; /// development
            }
            else
            {
                return ConfigurationManager.ConnectionStrings["ConnStrDBS"].ConnectionString; /// production
            }
        }

        public string GetConnectionStringSMSServer()
        {
            return ConfigurationManager.ConnectionStrings["ConnStrSMS"].ConnectionString; ///sms
        }

        /* -------------------------------------------------------- WS SMS -----------------------------------------------------------------------------  */

        /// get list pengajuan
        [WebMethod]
        public DataSet get_list_pengajuan(string NO_POLIS, string NO_PENGAJUAN, string NAMA, string PERIODE_MASUK_DARI, string PERIODE_MASUK_SAMPAI, string NO_BATCH)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_list_pengajuan "
                          + "'" + NO_POLIS + "'"
                          + "," + "'" + NO_PENGAJUAN + "'"
                          + "," + "'" + NAMA + "'"
                          + "," + "'" + PERIODE_MASUK_DARI + "'"
                          + "," + "'" + PERIODE_MASUK_SAMPAI + "'"
                          + "," + "'" + NO_BATCH + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_list_pengajuan");
            return (ds);
        }

        /// get list pengajuan supervisor
        [WebMethod]
        public DataSet get_list_pengajuan_supervisor(string NO_POLIS, string NO_PENGAJUAN, string NAMA, string PERIODE_MASUK_DARI, string PERIODE_MASUK_SAMPAI, string NO_BATCH)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_list_pengajuan_supervisor "
                          + "'" + NO_POLIS + "'"
                          + "," + "'" + NO_PENGAJUAN + "'"
                          + "," + "'" + NAMA + "'"
                          + "," + "'" + PERIODE_MASUK_DARI + "'"
                          + "," + "'" + PERIODE_MASUK_SAMPAI + "'"
                          + "," + "'" + NO_BATCH + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_list_pengajuan_supervisor");
            return (ds);
        }

        /// get list pengajuan underwriting
        [WebMethod]
        public DataSet get_list_pengajuan_underwriting(string NO_POLIS, string NO_PENGAJUAN, string NAMA, string PERIODE_MASUK_DARI, string PERIODE_MASUK_SAMPAI, string NO_BATCH)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_list_pengajuan_underwriting "
                          + "'" + NO_POLIS + "'"
                          + "," + "'" + NO_PENGAJUAN + "'"
                          + "," + "'" + NAMA + "'"
                          + "," + "'" + PERIODE_MASUK_DARI + "'"
                          + "," + "'" + PERIODE_MASUK_SAMPAI + "'"
                          + "," + "'" + NO_BATCH + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_list_pengajuan_underwriting");
            return (ds);
        }


        /// sp_get_list_dokumen_pengajuan
        [WebMethod]
        public DataSet get_list_dokumen_pengajuan(string NO_POLIS, string NO_PENGAJUAN)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_list_dokumen_pengajuan "
                          + "'" + NO_POLIS + "'"
                          + ", '" + NO_PENGAJUAN + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_list_dokumen_pengajuan");
            return (ds);
        }

        /// get data detail pengajuan
        [WebMethod]
        public DataSet get_data_detail_pengajuan(string NO_PENGAJUAN)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_data_detail_pengajuan "
                          + "'" + NO_PENGAJUAN + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_data_detail_pengajuan");
            return (ds);
        }

        /// get ddl bank
        [WebMethod]
        public DataSet get_ddl_list_bank(string NO_POLIS)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_ddl_list_bank "
                          + "'" + NO_POLIS + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_ddl_list_bank");
            return (ds);
        }

        /// get ddl jenis dokumen pengajuan
        [WebMethod]
        public DataSet get_ddl_list_jenis_dokumen_pengajuan()
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_ddl_list_jenis_dokumen_pengajuan "
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_ddl_list_jenis_dokumen_pengajuan");
            return (ds);
        }

        /// get ddl pilih polis aktif
        [WebMethod]
        public DataSet get_ddl_polis_login_userid(string username)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_ddl_polis_login_userid "
                          + "'" + username + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_ddl_polis_login_userid");
            return (ds);
        }

        /// get info user login
        [WebMethod]
        public DataSet get_userid_info(string username, string password)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_userid_info "
                          + "'" + username + "'"
                          + ", " + "'" + password + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_ddl_list_jenis_dokumen_pengajuan");
            return (ds);
        }

        /// get ddl cara bayar premi
        [WebMethod]
        public DataSet get_ddl_list_premium_type(string NO_POLIS)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_ddl_list_premium_type "
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_ddl_list_premium_type");
            return (ds);
        }

        /// get news by id
        [WebMethod]
        public DataSet get_news_type(int NEWS_TYPE)
        {
            string strConn = GetConnectionString();
            string select = " exec sp_get_news_type " + NEWS_TYPE
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);

            da.GetFillParameters();
            DataSet ds = new DataSet();
            da.Fill(ds, "get_news_type");
            return (ds);
        }

        [WebMethod]
        public bool validate_userpass(string user, string pass)
        {
            DataSet ds = new DataSet();

            string strConn = GetConnectionString();
            string select = " exec sp_validate_userpass "
                          + "'" + user + "'"
                          + ",'" + pass + "'"
                          ;
            SqlDataAdapter da = new SqlDataAdapter(select, strConn);
            da.GetFillParameters();
            da.Fill(ds, "validate_userpass");
            return Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
        }

        //sp_ins_dokumen_pengajuan
        [WebMethod]
        public String ins_dokumen_pengajuan(string GENERATE_ID
            , int DOC_TYPE_ID
            , string DOC_NAME_ORI
            , string DOC_NAME_MOD
            , string DOC_LOC
            , string ENTRY_USER)
        {
            try
            {
                DataSet ds = new DataSet();

                string strConn = GetConnectionString();
                string select = " exec sp_ins_dokumen_pengajuan "
                                + "'" + GENERATE_ID + "'"
                                + ", " + DOC_TYPE_ID
                                + ", " + "'" + DOC_NAME_ORI + "'"
                                + ", " + "'" + DOC_NAME_MOD + "'"
                                + ", " + "'" + DOC_LOC + "'"
                                + ", " + "'" + ENTRY_USER + "'"
                          ;
                SqlDataAdapter da = new SqlDataAdapter(select, strConn);
                da.GetFillParameters();
                da.Fill(ds, "ins_dokumen_pengajuan");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                return "Error : " + ex.ToString();
            }
        }

        //upd_dokumen_status
        [WebMethod]
        public String updt_status(string GENERATE_ID
            , int prev_STATUS_ID
            , int next_STATUS_ID
            , int MODE
            )
        {
            try
            {
                DataSet ds = new DataSet();

                string strConn = GetConnectionString();
                string select = " exec sp_updt_status "
                                + "'" + GENERATE_ID + "'"
                                + ", " + prev_STATUS_ID
                                + ", " + next_STATUS_ID
                                + ", " + MODE
                          ;
                SqlDataAdapter da = new SqlDataAdapter(select, strConn);
                da.GetFillParameters();
                da.Fill(ds, "updt_status");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                return "Error : " + ex.ToString();
            }
        }


        /// sp_del_dokumen_pengajuan
        [WebMethod]
        public string del_dokumen_pengajuan(string NO_PENGAJUAN, string NAMA_DOKUMEN)
        {
            try
            {
                DataSet ds = new DataSet();

                string strConn = GetConnectionString();
                string select = " exec sp_del_dokumen_pengajuan "
                                + "'" + NO_PENGAJUAN + "'"
                                + ", " + "'" + NAMA_DOKUMEN + "'"
                          ;
                SqlDataAdapter da = new SqlDataAdapter(select, strConn);
                da.GetFillParameters();
                da.Fill(ds, "del_dokumen_pengajuan");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                return "Error : " + ex.ToString();
            }
        }

        /// insert return string
        [WebMethod]
        public String ins_pengajuan(int BRANCH_ID
            , string BANK_ID
            , string NO_REK
            , string NAMA_PEMILIK_REK
            , decimal PENGHASILAN_PER_TAHUN
            , string SUMBER_PENDANAAN
            , string TUJUAN_PENGAJUAN_MENJADI_PESERTA
            , decimal UANG_PERTANGGUNGAN
            , int PREMI_SEKALIGUS
            , string TINGKAT_BUNGA_PER_TAHUN
            , string MULAI_PERTANGGUNGAN
            , string AKHIR_PERTANGGUNGAN
            , string NAMA_PENERIMA_MANFAAT_ASURANSI
            , string NO_POLIS_1
            , decimal JUMLAH_1
            , string NAMA_PERUSAHAAN_1
            , string NO_POLIS_2
            , decimal JUMLAH_2
            , string NAMA_PERUSAHAAN_2

            , string DEBITUR_NAMA
            , string DEBITUR_JENIS_KELAMIN
            , string DEBITUR_NIK
            , string DEBITUR_LAHIR_TEMPAT
            , string DEBITUR_LAHIR_TANGGAL
            , string DEBITUR_EMAIL
            , string DEBITUR_PEKERJAAN

            , string PESERTA_ALAMAT
            , string PESERTA_KOTA
            , string PESERTA_KODE_POS
            , string PESERTA_TELP
            , string PESERTA_FAX

            , string KORESPONDENSI_ALAMAT
            , string KORESPONDENSI_KOTA
            , string KORESPONDENSI_KODE_POS
            , string KORESPONDENSI_TELP
            , string KORESPONDENSI_FAX

            , int Q1_BADAN_BERAT
            , int Q1_BADAN_TINGGI

            , int Q2_PERAWATAN_OPSI
            , string Q2_PERAWATAN_DESC

            , int Q3_DIRAWAT_OPSI
            , string Q3_DIRAWAT_DESC

            , int Q4_MENDERITA_OPSI
            , string Q4_MENDERITA_DESC

            , string SES_NO_POLIS)
        {
            try
            {
                DataSet ds = new DataSet();

                string strConn = GetConnectionString();
                string select = " exec sp_ins_pengajuan " + BRANCH_ID
                                + ", " + "'" + BANK_ID + "'"
                                + ", " + "'" + NO_REK + "'"
                                + ", " + "'" + NAMA_PEMILIK_REK + "'"
                                + ", " + PENGHASILAN_PER_TAHUN
                                + ", " + "'" + SUMBER_PENDANAAN + "'"
                                + ", " + "'" + TUJUAN_PENGAJUAN_MENJADI_PESERTA + "'"
                                + ", " + UANG_PERTANGGUNGAN
                                + ", " + PREMI_SEKALIGUS
                                + ", " + "'" + TINGKAT_BUNGA_PER_TAHUN + "'"
                                + ", " + "'" + MULAI_PERTANGGUNGAN + "'"
                                + ", " + "'" + AKHIR_PERTANGGUNGAN + "'"
                                + ", " + "'" + NAMA_PENERIMA_MANFAAT_ASURANSI + "'"
                                + ", " + "'" + NO_POLIS_1 + "'"
                                + ", " + JUMLAH_1
                                + ", " + "'" + NAMA_PERUSAHAAN_1 + "'"
                                + ", " + "'" + NO_POLIS_2 + "'"
                                + ", " + JUMLAH_2
                                + ", " + "'" + NAMA_PERUSAHAAN_2 + "'"

                                + ", " + "'" + DEBITUR_NAMA + "'"
                                + ", " + "'" + DEBITUR_JENIS_KELAMIN + "'"
                                + ", " + "'" + DEBITUR_NIK + "'"
                                + ", " + "'" + DEBITUR_LAHIR_TEMPAT + "'"
                                + ", " + "'" + DEBITUR_LAHIR_TANGGAL + "'"
                                + ", " + "'" + DEBITUR_EMAIL + "'"
                                + ", " + "'" + DEBITUR_PEKERJAAN + "'"

                                + ", " + "'" + PESERTA_ALAMAT + "'"
                                + ", " + "'" + PESERTA_KOTA + "'"
                                + ", " + "'" + PESERTA_KODE_POS + "'"
                                + ", " + "'" + PESERTA_TELP + "'"
                                + ", " + "'" + PESERTA_FAX + "'"

                                + ", " + "'" + KORESPONDENSI_ALAMAT + "'"
                                + ", " + "'" + KORESPONDENSI_KOTA + "'"
                                + ", " + "'" + KORESPONDENSI_KODE_POS + "'"
                                + ", " + "'" + KORESPONDENSI_TELP + "'"
                                + ", " + "'" + KORESPONDENSI_FAX + "'"

                                + ", " + Q1_BADAN_BERAT
                                + ", " + Q1_BADAN_TINGGI

                                + ", " + Q2_PERAWATAN_OPSI
                                + ", " + "'" + Q2_PERAWATAN_DESC + "'"

                                + ", " + Q3_DIRAWAT_OPSI
                                + ", " + "'" + Q3_DIRAWAT_DESC + "'"

                                + ", " + Q4_MENDERITA_OPSI
                                + ", " + "'" + Q4_MENDERITA_DESC + "'"

                                + ", " + "'" + SES_NO_POLIS + "'"
                          ;
                SqlDataAdapter da = new SqlDataAdapter(select, strConn);
                da.GetFillParameters();
                da.Fill(ds, "ins_pengajuan");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                return "Error : " + ex.ToString();
            }
        }


        /// update return string
        [WebMethod]
        public String upd_pengajuan(int BRANCH_ID
            , string BANK_ID
            , string NO_REK
            , string NAMA_PEMILIK_REK
            , decimal PENGHASILAN_PER_TAHUN
            , string SUMBER_PENDANAAN
            , string TUJUAN_PENGAJUAN_MENJADI_PESERTA
            , decimal UANG_PERTANGGUNGAN
            , int PREMI_SEKALIGUS
            , string TINGKAT_BUNGA_PER_TAHUN
            , string MULAI_PERTANGGUNGAN
            , string AKHIR_PERTANGGUNGAN
            , string NAMA_PENERIMA_MANFAAT_ASURANSI
            , string NO_POLIS_1
            , decimal JUMLAH_1
            , string NAMA_PERUSAHAAN_1
            , string NO_POLIS_2
            , decimal JUMLAH_2
            , string NAMA_PERUSAHAAN_2

            , string DEBITUR_NAMA
            , string DEBITUR_JENIS_KELAMIN
            , string DEBITUR_NIK
            , string DEBITUR_LAHIR_TEMPAT
            , string DEBITUR_LAHIR_TANGGAL
            , string DEBITUR_EMAIL
            , string DEBITUR_PEKERJAAN

            , string PESERTA_ALAMAT
            , string PESERTA_KOTA
            , string PESERTA_KODE_POS
            , string PESERTA_TELP
            , string PESERTA_FAX

            , string KORESPONDENSI_ALAMAT
            , string KORESPONDENSI_KOTA
            , string KORESPONDENSI_KODE_POS
            , string KORESPONDENSI_TELP
            , string KORESPONDENSI_FAX

            , int Q1_BADAN_BERAT
            , int Q1_BADAN_TINGGI

            , int Q2_PERAWATAN_OPSI
            , string Q2_PERAWATAN_DESC

            , int Q3_DIRAWAT_OPSI
            , string Q3_DIRAWAT_DESC

            , int Q4_MENDERITA_OPSI
            , string Q4_MENDERITA_DESC

            , string SES_NO_POLIS
            , string NO_REGISTRATION)
        {
            try
            {
                DataSet ds = new DataSet();

                string strConn = GetConnectionString();
                string select = " exec sp_upd_pengajuan " + BRANCH_ID
                                + ", " + "'" + BANK_ID + "'"
                                + ", " + "'" + NO_REK + "'"
                                + ", " + "'" + NAMA_PEMILIK_REK + "'"
                                + ", " + PENGHASILAN_PER_TAHUN
                                + ", " + "'" + SUMBER_PENDANAAN + "'"
                                + ", " + "'" + TUJUAN_PENGAJUAN_MENJADI_PESERTA + "'"
                                + ", " + UANG_PERTANGGUNGAN
                                + ", " + PREMI_SEKALIGUS
                                + ", " + "'" + TINGKAT_BUNGA_PER_TAHUN + "'"
                                + ", " + "'" + MULAI_PERTANGGUNGAN + "'"
                                + ", " + "'" + AKHIR_PERTANGGUNGAN + "'"
                                + ", " + "'" + NAMA_PENERIMA_MANFAAT_ASURANSI + "'"
                                + ", " + "'" + NO_POLIS_1 + "'"
                                + ", " + JUMLAH_1
                                + ", " + "'" + NAMA_PERUSAHAAN_1 + "'"
                                + ", " + "'" + NO_POLIS_2 + "'"
                                + ", " + JUMLAH_2
                                + ", " + "'" + NAMA_PERUSAHAAN_2 + "'"

                                + ", " + "'" + DEBITUR_NAMA + "'"
                                + ", " + "'" + DEBITUR_JENIS_KELAMIN + "'"
                                + ", " + "'" + DEBITUR_NIK + "'"
                                + ", " + "'" + DEBITUR_LAHIR_TEMPAT + "'"
                                + ", " + "'" + DEBITUR_LAHIR_TANGGAL + "'"
                                + ", " + "'" + DEBITUR_EMAIL + "'"
                                + ", " + "'" + DEBITUR_PEKERJAAN + "'"

                                + ", " + "'" + PESERTA_ALAMAT + "'"
                                + ", " + "'" + PESERTA_KOTA + "'"
                                + ", " + "'" + PESERTA_KODE_POS + "'"
                                + ", " + "'" + PESERTA_TELP + "'"
                                + ", " + "'" + PESERTA_FAX + "'"

                                + ", " + "'" + KORESPONDENSI_ALAMAT + "'"
                                + ", " + "'" + KORESPONDENSI_KOTA + "'"
                                + ", " + "'" + KORESPONDENSI_KODE_POS + "'"
                                + ", " + "'" + KORESPONDENSI_TELP + "'"
                                + ", " + "'" + KORESPONDENSI_FAX + "'"

                                + ", " + Q1_BADAN_BERAT
                                + ", " + Q1_BADAN_TINGGI

                                + ", " + Q2_PERAWATAN_OPSI
                                + ", " + "'" + Q2_PERAWATAN_DESC + "'"

                                + ", " + Q3_DIRAWAT_OPSI
                                + ", " + "'" + Q3_DIRAWAT_DESC + "'"

                                + ", " + Q4_MENDERITA_OPSI
                                + ", " + "'" + Q4_MENDERITA_DESC + "'"

                                + ", " + "'" + SES_NO_POLIS + "'"
                                + ", " + "'" + NO_REGISTRATION + "'"
                          ;
                SqlDataAdapter da = new SqlDataAdapter(select, strConn);
                da.GetFillParameters();
                da.Fill(ds, "upd_pengajuan");
                return ds.Tables[0].Rows[0][0].ToString();
            }
            catch (SqlException ex)
            {
                return "Error : " + ex.ToString();
            }
        }

    }
}
