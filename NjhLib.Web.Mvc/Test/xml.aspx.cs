using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    public partial class xml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            h1();
        }
        private void h1()
        {
            string path = "http://soso.china9986.com/commonfile/xml/webkeywords.xml";
            Dictionary<string, string> d = XMLUtils.GetDictionary(path, "WebInfos/WebInfo");
            Response.Write(d["ExpoHomeDescription"]);
            foreach (KeyValuePair<string, string> k in d)
            {
                Response.Write(d[k.Key]);
                Response.Write("<br>--------------------");
            }

        }
    }
}
