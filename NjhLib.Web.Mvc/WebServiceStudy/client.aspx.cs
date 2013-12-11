using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
namespace NjhLib.Web.Mvc.WebServiceStudy
{
    public partial class client : System.Web.UI.Page
    {
        MyWebService.basicSoapClient client1 = new MyWebService.basicSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            MyWebService.MyHeader header = new MyWebService.MyHeader() { UserId = "01", Password = "001" };

            System.Net.NetworkCredential snt = new System.Net.NetworkCredential("jhnie", "njh@03.com");





            string s = client1.HelloWorld2(header, "00");
            Response.Write(s + "user:" + header.UserId + ";pwd:" + header.Password);
            //string ss = client.HelloWorld();
            //Response.Write("<br>" + ss);
            //client.ClientCredentials
            MyWebService.Student ss = client1.GetStudent();
            Response.Write(ss.Name + ss.Pwd);

        }

        protected void Unnamed2_Click(object sender, EventArgs e)
        {
            HttpPostedFile hps = Request.Files[0];
            Stream stream = hps.InputStream;
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            client1.UploadFile(bytes, "jpg");
            stream.Close();
        }

        public int Add(int a, int b) { return 0; }
        public double Add(int a, double b) { return 0.0d; }
    }
}