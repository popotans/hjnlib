using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NjhLib.Utils.Configs
{
    public class Config
    {

        #region properties
        public _Email Email { get; private set; }
        public class _Email
        {
            public bool Enable { get; set; }
            public string EmailFrom { get; set; }
            public string Account { get; set; }
            public string Password { get; set; }
            public bool RequireLogin { get; set; }
            public string SMTP { get; set; }
            public bool IsSSL { get; set; }
            public int Port { get; set; }
            public string MailSubject_Override { get; set; }
            public string MailBody_Override { get; set; }
            public string MailTo_Override { get; set; }
            public string MailCc_Override { get; set; }
            public string MailBcc_Override { get; set; }
            public string DisplayName { get; set; }
        }

        #endregion

        public Config()
        {
            string file = "sys.config";
            file = AppDomain.CurrentDomain.BaseDirectory + "\\" + file;
            //file = System.Web.HttpContext.Current.Server.MapPath(file);
            string xml = File.ReadAllText(file);
            LoadConfig(xml);
        }

        public Config(string configXmlStr)
        {
            LoadConfig(configXmlStr);
        }
        private void LoadConfig(string configXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(configXml);
            var root = doc.SelectSingleNode("/configuration");

            this.Email = new _Email
            {
                Account = ReadXmlNodeValue(root, "Email/Account"),
                EmailFrom = ReadXmlNodeValue(root, "Email/EmailFrom"),
                Enable = bool.Parse(ReadXmlNodeValue(root, "Email/Enable")),
                IsSSL = bool.Parse(ReadXmlNodeValue(root, "Email/IsSSL")),
                MailBcc_Override = ReadXmlNodeValue(root, "Email/MailBcc_Override"),
                MailBody_Override = ReadXmlNodeValue(root, "Email/MailBody_Override"),
                MailCc_Override = ReadXmlNodeValue(root, "Email/MailCc_Override"),
                MailSubject_Override = ReadXmlNodeValue(root, "Email/MailSubject_Override"),
                MailTo_Override = ReadXmlNodeValue(root, "Email/MailTo_Override"),
                Password = ReadXmlNodeValue(root, "Email/Password"),
                Port = int.Parse(ReadXmlNodeValue(root, "Email/Port")),
                RequireLogin = bool.Parse(ReadXmlNodeValue(root, "Email/RequireLogin")),
                SMTP = ReadXmlNodeValue(root, "Email/SMTP"),
                DisplayName = ReadXmlNodeValue(root, "Email/DisplayName"),

            };
        }
        private string ReadXmlNodeValue(XmlNode doc, string xpath)
        {
            XmlNode node = doc.SelectSingleNode(xpath);
            return node.InnerText;
        }

        private string ReadXmlNodeAttr(XmlNode node, string attr)
        {
            return node.Attributes[attr].Value;
        }

        private string ReadXmlNodeAttr(XmlNode doc, string xpathNode, string attr)
        {
            XmlNode node = doc.SelectSingleNode(xpathNode);
            return node.Attributes[attr].Value;
        }

        private XmlNodeList ReadXmlNodes(XmlNode doc, string xpath)
        {
            XmlNodeList nodes = doc.SelectNodes(xpath);
            return nodes;
        }
    }
}
