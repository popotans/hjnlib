using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.stringstudy
{
    public partial class indexof : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "27735";
            Response.Write(",27735,".IndexOf("," + s + ","));

        }
    }
}