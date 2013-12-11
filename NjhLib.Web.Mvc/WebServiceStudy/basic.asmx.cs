using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
namespace NjhLib.Web.Mvc.WebServiceStudy
{
    /// <summary>
    /// Summary description for basic
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class basic : System.Web.Services.WebService
    {
        public
          MyHeader header = new MyHeader() { };
        [WebMethod]
        public string HelloWorld(string u, string p)
        {
            if (!string.IsNullOrEmpty(u) && u == p)
            {
                return "Hello World";
            }
            return "null";

        }

        [WebMethod(EnableSession = true)]
        [SoapHeader("header")]
        public string HelloWorld2(string content)
        {
            string msg = string.Empty;
            if (header.IsValid()) { msg = "您哟全访问！"; } else { msg = "您无权访问啊！"; }
            return msg;
        }

        [WebMethod]
        public Student GetStudent()
        {
            Student h = new Student
            {
                Name = "jehnie",
                Pwd = "niejuasdasdas"
            };
            return h;
        }

        [WebMethod]
        public int UploadFile(byte[] filebyte, string fileextension)
        {
            if (File.Exists("d:\\example." + fileextension))
            {
                File.Delete("d:\\example." + fileextension);
            }
            FileStream fs = new FileStream("d:\\example." + fileextension, FileMode.CreateNew);
            fs.Write(filebyte, 0, filebyte.Length);
            fs.Close();
            return filebyte.Length;
        }
    }

    public class MyHeader : System.Web.Services.Protocols.SoapHeader
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public MyHeader() { }
        public MyHeader(string u, string p) { UserId = u; Password = p; }
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(UserId) && this.UserId == this.Password;
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Pwd { get; set; }
    }
}
