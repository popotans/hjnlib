using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils.MyCache;
namespace NjhLib.Web.Mvc.memcacheddmy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // WebCache.Add("key1", "我是key1缓存，来自web1" + DateTime.Now.ToString(), 20);
            WebCache.Add("key1", new student { Name = "niejunhua", Age = 23 }, 20);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (WebCache.Retrieve("key1") == null)
            {
                Response.Write("缓存到期啦");
            }
            else
            {
                string s = WebCache.Retrieve("key1").ToString();

                Response.Write(s);
            }
        }

    }
    [Serializable]
    public class student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public override string ToString()
        {
            return "Name=" + Name + ",Age=" + Age;
        }
    }
}