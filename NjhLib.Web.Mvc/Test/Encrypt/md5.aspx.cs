using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;

namespace NjhLib.Web.Mvc.Test.Encrypt
{
    public partial class md5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string admin = "admintest";
            Response.Write(StringUtil.MD5(admin));
            Guid id = Guid.NewGuid();
            Response.Write("<br/>" + id.ToString());
        }
    }
}