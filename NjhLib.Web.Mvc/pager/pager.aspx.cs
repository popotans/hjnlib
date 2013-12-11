using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.pager
{
    public partial class pager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int current = int.Parse(Request["page"]);
            int total = 100; ;
            string s = NjhLib.Utils.PageModel.GetPager(total, current);

            s = NjhLib.Utils.PageModel.GetHtmlPager(total, current, "page.aspx", "/-list-" + current + 1);
            Response.Write(s);
        }
    }
}