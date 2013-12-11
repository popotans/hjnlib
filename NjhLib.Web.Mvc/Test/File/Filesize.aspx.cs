using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace NjhLib.Web.Mvc.Test.File
{
    public partial class Filesize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = @"D:\works\0411\download\resttioted softwane\受限软件\TP7.zip";
            FileInfo fi = new FileInfo(str);

            Response.Write(fi.Length);
        }
    }
}