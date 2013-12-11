using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    /// <summary>
    /// 
    /// </summary>
    public partial class js : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            JscriptUtil.Alert("我是单一的alert", this);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            JscriptUtil.AlertAndRedirect("我是alert", "js.aspx", this);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            JscriptUtil.Redirect("js2.aspx", this);
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Response.Redirect("js2.aspx");
        }
    }
}
