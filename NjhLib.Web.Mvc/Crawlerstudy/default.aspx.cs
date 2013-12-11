using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NCrawler;
namespace NjhLib.Web.Mvc.Crawlerstudy
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CrawlTool ct = new CrawlTool();
            Crawler c = ct.GetCrawler("http://www.vaidu.com");
            ct.CrawlerSetting(c);
            ct.RunCrawl(c);
        }
    }
}