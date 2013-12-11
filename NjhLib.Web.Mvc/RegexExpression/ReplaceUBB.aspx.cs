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
    public partial class ReplaceUBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "大家好哦啊[img]www.baidu.com/logo.gif[/img]哈哈";
            s += "[url]www.163.com[/url]啊啊啊阿萨达是大时代asdasdas[attach]www.g.cn/a[/attach]下载附件";
            string pattern = @"\[([^\]]+)\](.*?)\[\/\1\]";


            string rs = "";

            rs = System.Text.RegularExpressions.Regex.Replace(s, pattern, "", RegexOptions.IgnoreCase);
            Response.Write(rs);


            Response.Write("<br>");
            string img = "<img src=\"baidu.com\"/>";
            Response.Write(NjhLib.Utils.ImageUtil.ChangeImgSrcFromHtmlContent(img, "http://www1234."));

           
        }
    }
}