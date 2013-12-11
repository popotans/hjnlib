using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.IO;
namespace NjhLib.Web.Mvc.Configs
{
    public class Config
    {
        public string absolutepath { get; set; }
        public string relativepath { get; set; }
        public string sitename { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }
        public string dbtype { get; set; }
        public string dbstr { get; set; }

        private static string _filePath = string.Empty;
        private static XmlDocument _document;
        public Config(string filepath)
        {
            _filePath = filepath;
            _document = new XmlDocument();
            _document.LoadXml(File.ReadAllText(_filePath));

            this.BuildPropertie();
        }

        private void BuildPropertie()
        {
            this.absolutepath = GetText("/configuration/absolutepath");
            this.relativepath = GetText("/configuration/relativepath");
            this.sitename = GetText("/configuration/sitename");
            this.keywords = GetText("/configuration/keywords");
            this.description = GetText("/configuration/description");
            this.dbstr = GetText("/configuration/db/dbstr");
            this.dbtype = GetText("/configuration/db/dbtype");
        }

        public static string GetText(string node)
        {
            string rs = string.Empty;
            rs = _document.SelectSingleNode(node).InnerText;
            return rs;
        }

        public static void SetText(string node, string val)
        {
            _document.SelectSingleNode(node).InnerText = val;
            _document.Save(_filePath);
        }





    }
}