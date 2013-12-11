using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.zips
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //IList<string> list = new List<string>();
            //list.Add("d:\\njh\\js\\163js\\select.js");
            //list.Add("d:\\njh\\js\\163js\\popup163.js");
            //ZipLib.CreateZipfromFiles(list, "c:\\163js.zip");
            //Response.Write("压缩文件成功");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //string to = "d:\\01";
            //string file = "c:\\163js.zip";
            //ZipLib.ExtractZipToDir(to, file);
            //Response.Write("jiya文件成功");
        }
    }
}