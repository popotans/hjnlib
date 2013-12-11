using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.Configs
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Config cfg = new Config(Server.MapPath("this.config"));
            Response.Write(cfg.sitename + "<br/>");
            Response.Write(cfg.absolutepath + "<br/>");
            Response.Write(cfg.dbstr + "<br/>");
        }
    }
}