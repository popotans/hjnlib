using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.keywordshee
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("百度呢", "http://www.baidune.com");
            dic.Add("百度", "http://www.baidu.com");

            dic.Add("贴吧", "http://tieba.baidu.com");
            dic.Add("deep", "http://www.deep.com");
            string s = "大家红啊，今天给大家及诶少侠百度，百度呢事业个很大deep网站，至今为止没人可以超过百度，作为老发，百度大力发展了贴吧系统。";
            Response.Write(NjhLib.Utils.KeyWordsHelper.AddKwdsLink(dic, s));
            //
            Response.Write("<br/><br/>"+Environment.NewLine);
            s = "大家红啊，今天给大家及诶少侠<a href='http://www.baidu.com'>百度</a>，<a href='http://www.baidune.com'>百度呢</a>事业个很大<a href='http://www.deep.com'>deep</a>网站，至今为止没人可以超过百度，作为老发，百度大力发展了<a href='http://tieba.baidu.com'>贴吧</a>系统。";

            Response.Write(NjhLib.Utils.KeyWordsHelper.RemoveALink(s));
        }
    }
}