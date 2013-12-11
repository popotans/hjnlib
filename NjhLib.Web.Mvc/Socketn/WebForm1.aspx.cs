using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text;
namespace NjhLib.Web.Mvc.Socketn
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] buf = new byte[3819200];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.baidu.com");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream resStream = response.GetResponseStream();
            int count = resStream.Read(buf, 0, buf.Length);
            string s = Encoding.Default.GetString(buf, 0, count);
            Response.Write(s);
            resStream.Close();
        }
    }
}