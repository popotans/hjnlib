using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.xxml
{
    public partial class xmlx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Server.MapPath("meuser.config");

            Dictionary<string, string> dic = XMLUtils.GetDictionary(path, "/configuration/users/user", "keyq", "valuea");
            //dic = XMLUtils.GetDictionary(path, "/configuration/users/user");
            foreach (KeyValuePair<string, string> item in dic)
            {
                Response.Write("<br/>");
                Response.Write(item.Key);
                Response.Write("==");
                Response.Write(item.Value);
            }

        }
    }
}