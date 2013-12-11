using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using reg = System.Text.RegularExpressions;
namespace NjhLib.Web.Mvc.Regex
{
    public partial class font : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<br>");
            string s = " 您好我只是这里的一个<font color=red>大家好我是font</font> ";
            string pattern = "<.+?>";

            Response.Write(s.IndexOf("njh"));

            Response.Write("<br>");
            Response.Write("<br>");
            Response.Write("<br>");
        }
    }
}