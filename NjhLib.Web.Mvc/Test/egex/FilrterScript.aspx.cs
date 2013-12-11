using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;

namespace NjhLib.Web.Mvc.Test.egex
{
    public partial class FilrterScript : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);
            Button3.Click += new EventHandler(Button3_Click);
        }

        void Button3_Click(object sender, EventArgs e)
        {
            string s = "我[Br]要[br/]换[br /]行[ br]啦[ BR/ ]阿[br / ]斯达";
            string pattern = @"\[(\s*)br\s*\/?\s*\]";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnoreCase);
            s = rex.Replace(s, "<br/>");
            Response.Write(s);
        }

        void Button2_Click(object sender, EventArgs e)
        {
            string s = "[quote]<b>以下是引用<i>admin在2011-05-25 17:00:47</i>的发言：[br/]</b>\r\n开贴疑问？。 [/quote]";
            s = System.Text.RegularExpressions.Regex.Replace(s, "[\n|\r]", "<br/>", RegexOptions.IgnoreCase);
            string pattern = @"\[(quote)\](.*)\[\/\1\]";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            s = rex.Replace(s, "<BLOCKQUOTE><FONT size=1>quote:</FONT><HR>$2<HR></BLOCKQUOTE>");
            Response.Write(s);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            string s = "asd<sc ript><sc ript src='25.js'>ds</script>alert('nonono.oh.no');</sc ript>asdasd";
            string pattern = @"<(\s*)s\1c\1r\1i\1p\1t\1\s+[^>]+>.*<\1\/\1s\1c\1r\1i\1p\1t\1>";
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.IgnoreCase);
            s = rex.Replace(s, "厉害吧 你已经被过滤了。");
            Response.Write("result after filter:" + s);

        }
    }
}