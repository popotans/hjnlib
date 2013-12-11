using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.json
{
    public partial class SerializeDictionaryToJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("post", "1");
            dic.Add("del", "5,10,15");
            dic.Add("elite", "5,10,15,20");
            dic.Add("top", "1");
            dic.Add("hot", "10");

            string s = "";// NjhLib.Utils.SerializeUtil.ObjectToJson20<Dictionary<string, string>>(dic);
            s = SerializeUtil.ObjectToJsonByJavaScriptSerializer<Dictionary<string, string>>(dic);
            Response.Write(s);

            Response.Write("<br>=========<br>");
            dic = SerializeUtil.JsonToObjectByJavaScriptSerializer<Dictionary<string, string>>(s);
            Response.Write(dic["del"]);

        }
    }
}