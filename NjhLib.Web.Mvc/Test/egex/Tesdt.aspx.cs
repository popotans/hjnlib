using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace NjhLib.Web.Mvc.Test.Regex
{
    public partial class Tesdt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string p = "window(?:1|2|3)";
            string b = System.Text.RegularExpressions.Regex.Match("window222", p).Value;
            write(b.ToString());
        }
        void write(string s)
        {
            Response.Write("<script>alert('" + s + "');</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string p = "window(?=1|2|3)";
            string b = System.Text.RegularExpressions.Regex.Match("window159", p).Value;
            write(b.ToString());
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string p = "window(?!1|2|3)";
            string b = System.Text.RegularExpressions.Regex.Match("windows59", p).Value;
            write(b.ToString());
        }
    }
}