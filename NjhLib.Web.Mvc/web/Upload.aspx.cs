using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;

namespace NjhLib.Web.Mvc.web
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Button1.Click += new EventHandler(Button1_Click);


        }

        void Button1_Click(object sender, EventArgs e)
        {
            string[] allow = new string[] { 
            "jpg",
            "jpeg",
            "pdf",
            "gif",
            "zip",
            "rar",
            "doc",
            "xls",
            "ppt",
            "mp3",
            "wma",
            "wmv",
            "asf",
            "avi",
            "bmp"
            };

            string filepath = "";

            WebUtil.Upload(FileUpload1.PostedFile, allow, 555555555, "./", ref filepath);
            Response.Write(filepath + "suc");
        }
    }
}