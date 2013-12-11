using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace NjhLib.Web.Mvc.Regex
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string pattern = "<b>(.*)</b>";
            //string s = "x哈哈<b><font color=blue>技术论坛原则（20100901版）</font></B>嘻嘻嘻i想";

            //s = System.Text.RegularExpressions.Regex.Replace(s, pattern, "$1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //Response.Write(s);
            //Response.Write("<br>");
            //test1();
            //Response.Write("<br>");
            //Response.Write(((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000 - 8 * 60 * 60).ToString());

            bool fal = false;
            //IsMobile("12210207931");
            //   fal = NjhLib.Utils.StringUtil.IsIP("1.256.254.3");
            //  fal = NjhLib.Utils.StringUtil.IsMac("00:0A:eB:Fi:17:21");
            // Response.Write(fal);

            Button1.Click += new EventHandler(Button1_Click);

          

        }

        void Button1_Click(object sender, EventArgs e)
        {
            string rs = NjhLib.Utils.StringUtil.ReplaceHtml2(tb1.Text);
            Response.Write(string.IsNullOrEmpty(rs));
            if (!string.IsNullOrEmpty(rs))
            {
                Response.Write("<br/>");
                Response.Write(HttpUtility.HtmlEncode(rs));
            }
        }

        void test1()
        {
            //input
            string str = "<em id=\"defen\" type=\"\" a=\"\" default=\"78.00\">78.00</em>";
            //pattern
            string pattern = "<em\\s+id=\"defen\"[^>]+>(?<value>.*)</em>";
            //result
            string rs = string.Empty;
            Match match = System.Text.RegularExpressions.Regex.Match(str, pattern, RegexOptions.IgnoreCase);
            if (match.Success) rs = match.Groups["value"].Value;
            Response.Write(rs);
        }

        public bool IsMobile(string str)
        {
            bool flag = false;
            string pattern = "^(13[0-9]|14[0-9]|15[0-9]|18[0-9])\\d{8}$";
            flag = System.Text.RegularExpressions.Regex.IsMatch(str, pattern);
            return flag;
        }


    }
}