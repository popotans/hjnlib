using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.Test
{
    public partial class ascii : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s1 = "563";

            List<Person> listp = new List<Person>()
            {

            };
            listp.Add(new Person { name = "niejunhua", city = "fugou" });
            listp.Add(new Person { name = "zhangdan", city = "pingdingshan" });
            listp.Add(new Person { name = "yanging", city = "suzhou" });

            //处理
            foreach (Person p in listp)
            {
                p.aihao = p.name + "爱好";
            }

            foreach (Person oo in listp)
            {
                Response.Write("<br>" + oo.aihao);
            }
            NjhLib.Utils.IPList ipl = new NjhLib.Utils.IPList();
            ipl.AddRange("192.168.1.1", "192.168.2.2");
            foreach (var s in ipl.usedList)
            {
                Response.Write(s+"<br/>");
            }


        }
    }
    public class Person
    {
        public string name { get; set; }
        public string city { get; set; }
        public string aihao { get; set; }
    }
}