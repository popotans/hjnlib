using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils.MyCache;
namespace NjhLib.Web.Mvc.memcacheddmy
{
    public partial class m2 : System.Web.UI.Page
    {
        MemcacheClient2 client = new MemcacheClient2("");
        protected void Page_Load(object sender, EventArgs e)
        {


            btnread.Click += new EventHandler(btnread_Click);
            btnWrite.Click += new EventHandler(btnWrite_Click);

        }

        void btnWrite_Click(object sender, EventArgs e)
        {
            client.Set("ky", DateTime.Now, 555);
            client.Set("ky2", DateTime.Now, 555);

            for (int i = 0; i < 500; i++)
            {
                client.Set("k" + i, i.ToString() + DateTime.Now, 75);
            }
        }

        void btnread_Click(object sender, EventArgs e)
        {
            string ss = "";
            object obj = client.Get("ky"); string s = "";
            string hsot = "";
            if (obj == null) { }
            else
            {
                s = obj.ToString();
                s += "<br>" + client.Get("ky2");
                hsot = client.GetSocketHost("ky");
                hsot += "<br/>" + client.GetSocketHost("ky2");
            }
            Response.Write(s);
            w("---------------------------------------");
            Response.Write("<br>" + hsot);
            w("---------------------------------------");
            for (int i = 0; i < 500; i++)
            {
              //  w(client.GetSocketHost("k" + i).ToString());
            }

            w("---------------------------------------");

            w("servers able:");
            foreach (string item in client.GetConnectedSocketHost())
            {
                w(item);
            }
         
            w("---------------------------------------");
            w("<b>stast:</b>");
            ArrayList arrl = client.GetStats();
            foreach (string item in arrl)
            {
                w(item);
            }

            client.GetStats();
        }


        void w(string s)
        {
            Response.Write("<br/>" + s);
        }
    }
}