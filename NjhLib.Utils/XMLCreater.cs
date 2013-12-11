using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Runtime.CompilerServices;
namespace NjhLib.Utils
{

    ///
    ///作者：凌晨的搜索者
    ///网址：www.uu102.com
    ///

    public class XMLCreater
    {
        public string FileName { get; set; }
        public XmlDocument XmlDoc { get; set; }
        public XmlElement RootElement { get; set; }
        public XMLCreater()
        {
        }
        public XMLCreater(string fileName, string rootElement)
        {
            this.FileName = fileName;
            this.XmlDoc = new XmlDocument();//创建xml文档
            XmlDeclaration xmlDeclaration = this.XmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            this.XmlDoc.AppendChild(xmlDeclaration);//添加申明
            this.RootElement = this.XmlDoc.CreateElement(rootElement);//创建根元素
            this.XmlDoc.AppendChild(this.RootElement); //添加根元素
        }

        public static XmlDocument LoadFromFile(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileName);//加载已经存在的文件
            return xmlDoc;
        }
        public void Save(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
                this.FileName = fileName;
            if (this.XmlDoc == null) throw new Exception("没有创建xml文档，无法保存");
            this.XmlDoc.Save(this.FileName);
        }
        public void Save()
        {
            Save("");
        }
    }
    /// <summary>
    /// 这个类，用到扩展方法，用起来相当方便，有点像jquery的链式编程
    /// </summary>
    public static class Methods
    {

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="arr">
        ///  第一个参数：xml实例化类
        ///  第二个参数：节点名字 
        /// <returns></returns>
        public static XmlNode AddNode(this XmlElement _xmlElement, params object[] arr)
        {
            XMLCreater u_Xml = (XMLCreater)arr[0];
            XmlNode xmlNode = u_Xml.XmlDoc.CreateNode(XmlNodeType.Element, (string)arr[1], "");
            _xmlElement.AppendChild(xmlNode);//添加节点
            return xmlNode;
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="_xmlElement"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static XmlNode AddNode(this XmlElement _xmlElement, XMLCreater self, string nodeName)
        {

            XMLCreater u_Xml = self;
            XmlNode xmlNode = u_Xml.XmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            _xmlElement.AppendChild(xmlNode);//添加节点
            return xmlNode;
        }

        ///// <summary>
        ///// 添加节点
        ///// </summary>
        ///// <param name="arr">
        ///// 第一个参数：xml实例化类\n
        ///// 第二个参数：节点名字
        ///// </param>
        ///// <returns></returns>
        //public static XmlNode AddNode(this XmlNode _xmlElement, params object[] arr)
        //{
        //    XMLCreater u_Xml = (XMLCreater)arr[0];
        //    XmlNode xmlNode = u_Xml.XmlDoc.CreateNode(XmlNodeType.Element, (string)arr[1], "");
        //    _xmlElement.AppendChild(xmlNode);//添加节点
        //    return xmlNode;
        //}

        public static XmlNode AddNode(this XmlNode _xmlElement, XMLCreater self, string nodeName)
        {
            XMLCreater u_Xml = self;
            XmlNode xmlNode = u_Xml.XmlDoc.CreateNode(XmlNodeType.Element, nodeName, "");
            _xmlElement.AppendChild(xmlNode);//添加节点
            return xmlNode;
        }

        ///// <summary>
        ///// 添加属性
        ///// </summary>
        ///// <param name="arr">
        ///// 第一个参数：xml实例化类\n
        ///// 第二个参数：属性名称\n
        ///// 第三个参数：属性值
        ///// </param>
        ///// <returns></returns>
        //public static XmlAttribute AddAttibute(this XmlElement _xmlElement, params object[] arr)
        //{
        //    XMLCreater u_Xml = (XMLCreater)arr[0];
        //    XmlAttribute xmlAttribute = u_Xml.XmlDoc.CreateAttribute((string)(arr[1]));
        //    xmlAttribute.Value = (string)arr[2];
        //    _xmlElement.Attributes.Append(xmlAttribute);
        //    return xmlAttribute;
        //}

        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="_xmlElement"></param>
        /// <param name="self"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeVal"></param>
        /// <returns></returns>
        public static XmlAttribute AddAttibute(this XmlElement _xmlElement, XMLCreater self, string attributeName, string attributeVal)
        {
            XMLCreater u_Xml = self;
            XmlAttribute xmlAttribute = u_Xml.XmlDoc.CreateAttribute(attributeName);
            xmlAttribute.Value = attributeVal;
            _xmlElement.Attributes.Append(xmlAttribute);
            return xmlAttribute;
        }

        ///// <summary>
        ///// 添加属性
        ///// </summary>
        ///// <param name="arr">
        ///// 第一个参数：xml实例化类\n
        ///// 第二个参数：属性名称\n
        ///// 第三个参数：属性值
        ///// </param>
        ///// <returns></returns>
        //public static XmlAttribute AddAttibute(this XmlNode _xmlElement, params object[] arr)
        //{
        //    XMLCreater u_Xml = (XMLCreater)arr[0];
        //    XmlAttribute xmlAttribute = u_Xml.XmlDoc.CreateAttribute((string)(arr[1]));
        //    xmlAttribute.Value = (string)arr[2];
        //    _xmlElement.Attributes.Append(xmlAttribute);
        //    return xmlAttribute;
        //}
        /// <summary>
        /// 
        ///添加属性 
        /// </summary>
        /// <param name="_xmlElement"></param>
        /// <param name="self"></param>
        /// <param name="attributeName"></param>
        /// <param name="attrbuteVal"></param>
        /// <returns></returns>
        public static XmlAttribute AddAttibute(this XmlNode _xmlElement, XMLCreater self, string attributeName, string attrbuteVal)
        {
            XMLCreater u_Xml = self;
            XmlAttribute xmlAttribute = u_Xml.XmlDoc.CreateAttribute(attributeName);
            xmlAttribute.Value = attrbuteVal;
            _xmlElement.Attributes.Append(xmlAttribute);
            return xmlAttribute;
        }


        public static XmlNode ReName(this XmlElement _xmlElement, params object[] arr)
        {
            string value = (string)arr[2];

            XMLCreater u_Xml = (XMLCreater)arr[0];
            XmlNode xmlElement = u_Xml.XmlDoc.CreateNode("element", value, "");
            if (u_Xml.XmlDoc[(string)arr[1]] == null) throw new Exception("不存在该节点");
            xmlElement.InnerText = u_Xml.XmlDoc[(string)arr[1]].InnerText;
            u_Xml.XmlDoc[(string)arr[1]].ParentNode.ReplaceChild(xmlElement, u_Xml.XmlDoc[(string)arr[1]]);
            u_Xml.Save();
            return xmlElement;
        }
        public static XmlNode ReName(this XmlNode _xmlElement, params object[] arr)
        {
            string value = (string)arr[2];

            XMLCreater u_Xml = (XMLCreater)arr[0];
            XmlNode xmlElement = u_Xml.XmlDoc.CreateNode("element", value, "");
            xmlElement.InnerText = u_Xml.XmlDoc[(string)arr[1]].InnerText;
            u_Xml.XmlDoc[(string)arr[1]].ParentNode.ReplaceChild(xmlElement, u_Xml.XmlDoc[(string)arr[1]]);
            u_Xml.Save();
            return xmlElement;
        }
        public static void Delete(this XmlNode _xmlElement)
        {
            if (_xmlElement == null) throw new Exception("该节点不存在");
            if (_xmlElement.ParentNode.HasChildNodes) _xmlElement.ParentNode.RemoveChild(_xmlElement);
        }
    }

}
