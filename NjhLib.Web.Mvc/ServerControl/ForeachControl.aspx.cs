using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.ServerControl
{
    public partial class ForeachControl : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "";
            foreach (System.Web.UI.Control c in this.Controls)
            {
              //  foreach (System.Web.UI.Control cc in c.Controls)
                {
                    s += "<script>alert('" + c.ClientID + "');</script>";
                }
            }
            Response.Write(s);
        }
    }
}