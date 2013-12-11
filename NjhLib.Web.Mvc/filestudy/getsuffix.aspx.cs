using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace NjhLib.Web.Mvc.filestudy
{
    public partial class getsuffix : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = GetImgSuffix("c:/05/05/06/02/03/04/05/09//asdjh.jpeg");
            Response.Write(s);
            string s1 = "/service/answer";
            string s2 = "5689.jpg";
            Response.Write(s2.Substring(1));
        }
        private string GetImgSuffix(string fileName)
        {
            int lastIndex = fileName.LastIndexOf('.');
            string result = fileName.Substring(lastIndex + 1);
            return result;
        }
    }
}