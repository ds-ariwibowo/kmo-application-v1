using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

using KMO.Class;

namespace KMO
{
    public partial class PDFViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Vd.isValidLogin();

            if (!IsPostBack)
            {
                //string FilePath = Server.MapPath("~\\RptTemp\\1.pdf");
                //WebClient User = new WebClient();
                //Byte[] FileBuffer = User.DownloadData(FilePath);
                //if (FileBuffer != null)
                //{
                //    Response.ContentType = "application/pdf";
                //    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                //    Response.BinaryWrite(FileBuffer);
                //}
                string filePath = Server.MapPath("~\\RptTemp\\") + Request.QueryString["FN"];
                this.Response.ContentType = "application/pdf";
                this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + Request.QueryString["FN"]);
                this.Response.WriteFile(filePath);
                this.Response.End();
            }            
        }
    }
}