using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NjhLib.Utils
{
    public class XMLUtils
    {
        /// <summary>
        /// 读取远程 xml文件 中记录 网站二级栏目的描叙
        /// </summary>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, string> GetWebInfo(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList nodeList = doc.SelectSingleNode("WebInfos").ChildNodes;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (XmlNode node in nodeList)
            {
                if (node is XmlComment)
                {
                    //如果是注释，就跳过！
                    continue;
                }
                else
                {
                    XmlElement element = (XmlElement)(node as XmlNode);
                    string key = element.GetAttribute("key");
                    string value = element.GetAttribute("value");
                    dic.Add(key, value);
                }
            }
            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <returns></returns>
        public static XmlDocument GetDocument(string xmlpath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlpath);
            return doc;
        }
        /// <summary>
        /// 取得xmlNodeList
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static XmlNodeList GetNodeList(string xmlpath, string xpath)
        {
            XmlNodeList list = GetDocument(xmlpath).SelectNodes(xpath);
            return list;
        }


        /// <summary>
        /// 取值 一个xml节点 的字典xml节点列表
        /// </summary>
        /// <param name="fielpath"></param>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string fielpath, string xpath)
        {
            Dictionary<string, string> dct = new Dictionary<string, string>();
            // fielpath = System.Web.HttpContext.Current.Server.MapPath(fielpath);
            XmlDocument doc = new XmlDocument();
            doc.Load(fielpath);
            XmlNodeList xns = doc.SelectNodes(xpath);
            string key = "", value = "";
            foreach (XmlNode xn in xns)
            {
                key = xn.Attributes["key"].InnerText;
                //if (!dct.ContainsKey(key))
                //{
                //    value = xn.Attributes["value"].InnerText;
                //    dct.Add(key, value);
                //}
                //上边注释经过优化的代码, 如下
                if (!dct.TryGetValue(key, out value))
                {
                    value = xn.Attributes["value"].InnerText;
                    dct.Add(key, value);
                }
            }
            return dct;
        }
        /// <summary>
        /// 取值 一个xml节点 的字典xml节点列表
        /// </summary>
        /// <param name="fielpath">xml文件路径</param>
        /// <param name="xpath"> xmlpath 节点选择器</param>
        /// <param name="keytofind">查找的key值</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string fielpath, string xpath, string keytofind, string valuetofind)
        {
            Dictionary<string, string> dct = new Dictionary<string, string>();
            // fielpath = System.Web.HttpContext.Current.Server.MapPath(fielpath);
            XmlDocument doc = new XmlDocument();
            doc.Load(fielpath);
            XmlNodeList xns = doc.SelectNodes(xpath);
            string key = "", value = "";
            try
            {
                foreach (XmlNode xn in xns)
                {
                    key = xn.Attributes[keytofind].InnerText;
                    //if (!dct.ContainsKey(key))
                    //{
                    //    value = xn.Attributes["value"].InnerText;
                    //    dct.Add(key, value);
                    //}
                    //上边注释经过优化的代码, 如下
                    if (!dct.TryGetValue(key, out value))
                    {
                        value = xn.Attributes[valuetofind].InnerText;
                        dct.Add(key, value);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dct;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlpath">绝对路径</param>
        /// <param name="cssselector">节点选择器</param>
        /// <returns></returns>
        public static string GetTextFromXML(string xmlpath, string cssselector)
        {
            // xmlpath = HttpContext.Current.Server.MapPath(xmlpath);
            string s = "";
            XmlNodeList list = GetNodeList(xmlpath, cssselector);
            if (list != null)
            {
                if (list.Count != 0)
                {
                    s = list[0].InnerText;
                }
            }
            return s;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlpath"> 相对路径</param>
        /// <param name="cssselector">节点选择器</param>
        /// <param name="valproperty">属性名称</param>
        /// <returns></returns>
        public static string GetValFromXML(string xmlpath, string cssselector, string valproperty)
        {
            //  xmlpath = HttpContext.Current.Server.MapPath(xmlpath);
            string s = "";
            XmlNodeList list = GetNodeList(xmlpath, cssselector);
            if (list != null)
            {
                if (list.Count != 0)
                {
                    s = list[0].Attributes[valproperty].InnerText;
                }
            }
            return s;
        }

        // Methods
        public static bool CreateOrUpdateXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName, string value)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        if (attribute.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            attribute.Value = value;
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        XmlAttribute attribute2 = document.CreateAttribute(xmlAttributeName);
                        attribute2.Value = value;
                        node.Attributes.Append(attribute2);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateOrUpdateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        if (node2.Name.ToLower() == xmlNodeName.ToLower())
                        {
                            node2.InnerXml = innerText;
                            flag2 = true;
                            break;
                        }
                    }
                    if (!flag2)
                    {
                        XmlElement newChild = document.CreateElement(xmlNodeName);
                        newChild.InnerXml = innerText;
                        node.AppendChild(newChild);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlDocument(string xmlFileName, string rootNodeName, string encoding)
        {
            bool flag = false;
            try
            {
                XmlDocument document = new XmlDocument();
                XmlDeclaration newChild = document.CreateXmlDeclaration("1.0", encoding, null);
                XmlNode node = document.CreateElement(rootNodeName);
                document.AppendChild(newChild);
                document.AppendChild(node);
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool CreateXmlNodeByXPath(string xmlFileName, string xpath, string xmlNodeName, string innerText, string xmlAttributeName, string value)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    XmlElement newChild = document.CreateElement(xmlNodeName);
                    newChild.InnerXml = innerText;
                    if (!(string.IsNullOrEmpty(xmlAttributeName) || string.IsNullOrEmpty(value)))
                    {
                        XmlAttribute attribute = document.CreateAttribute(xmlAttributeName);
                        attribute.Value = value;
                        newChild.Attributes.Append(attribute);
                    }
                    node.AppendChild(newChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteAllXmlAttributeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node != null)
                {
                    node.Attributes.RemoveAll();
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlAttributeByXPath(string xmlFileName, string xpath, string xmlAttributeName)
        {
            bool flag = false;
            bool flag2 = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                XmlAttribute attribute = null;
                if (node != null)
                {
                    foreach (XmlAttribute attribute2 in node.Attributes)
                    {
                        if (attribute2.Name.ToLower() == xmlAttributeName.ToLower())
                        {
                            attribute = attribute2;
                            flag2 = true;
                            break;
                        }
                    }
                    if (flag2)
                    {
                        node.Attributes.Remove(attribute);
                    }
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static bool DeleteXmlNodeByXPath(string xmlFileName, string xpath)
        {
            bool flag = false;
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                XmlNode oldChild = document.SelectSingleNode(xpath);
                if (oldChild != null)
                {
                    oldChild.ParentNode.RemoveChild(oldChild);
                }
                document.Save(xmlFileName);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        public static XmlAttribute GetXmlAttribute(string xmlFileName, string xpath, string xmlAttributeName)
        {
            XmlDocument document = new XmlDocument();
            XmlAttribute attribute = null;
            try
            {
                document.Load(xmlFileName);
                XmlNode node = document.SelectSingleNode(xpath);
                if (node == null)
                {
                    return attribute;
                }
                if (node.Attributes.Count > 0)
                {
                    attribute = node.Attributes[xmlAttributeName];
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return attribute;
        }

        public static XmlNode GetXmlNodeByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectSingleNode(xpath);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static XmlNodeList GetXmlNodeListByXpath(string xmlFileName, string xpath)
        {
            XmlDocument document = new XmlDocument();
            try
            {
                document.Load(xmlFileName);
                return document.SelectNodes(xpath);
            }
            catch (Exception)
            {
                return null;
            }
        }


    }
}
