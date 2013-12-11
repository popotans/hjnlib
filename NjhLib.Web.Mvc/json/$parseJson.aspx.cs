using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.json
{
    public partial class _parseJson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  Response.Write(GetStr());
        }

        public string GetStr()
        {
            string s = string.Empty;
            Leaders l1 = new Leaders { LeaderId = "111", LeaderName = "hktw" };
            Leaders l2 = new Leaders { LeaderId = "2222", LeaderName = "UsJapan" };
            List<Leaders> list = new List<Leaders>();
            list.Add(l1); list.Add(l2);
            Person p = new Person { Age = 25, Jobs = "C#Asp>net", Name = "JJHnie", list = list };
            s = SerializeUtil.ObjectToJson20<Person>(p);
            return s;
        }
    }



   

}