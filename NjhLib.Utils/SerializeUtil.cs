using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Xml;
using System.Xml.Serialization;

using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Script.Serialization;

namespace NjhLib.Utils
{
    public class SerializeUtil
    {
        /// <summary>
        /// 3.5
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToJsonByJavaScriptSerializer<T>(T t)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();

            return jser.Serialize(t);

        }
        /// <summary>
        /// 3.5
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObjectByJavaScriptSerializer<T>(string json)
        {
            JavaScriptSerializer jser = new JavaScriptSerializer();
            return jser.Deserialize<T>(json);
        }
        /// <summary>
        /// 3.5
        /// 将一段json 字符串转换为 dictionary<string，object>
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, object> JsonToDictionary(string jsonstr)
        {
            return new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(jsonstr);
        }


        /// <summary>
        /// 将对象序列化为json格式 .net2.0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson20<T>(T obj)
        {
            string rtn = "";
            rtn = JavaScriptConvert.SerializeObject(obj);
            return rtn;
        }
        /// <summary>
        /// .net 2.0
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject20<T>(string json)
        {
            T obj = default(T);
            obj = (T)JavaScriptConvert.DeserializeObject(json, typeof(T));
            return obj;
        }

        /// <summary>
        /// .net 3.5 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson35<T>(T obj)
        {
            string rt = "";
            System.Runtime.Serialization.Json.DataContractJsonSerializer ds = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                ds.WriteObject(ms, obj);
                rt = Encoding.UTF8.GetString(ms.ToArray());
            }
            return rt;
        }

        /// <summary>
        /// .net 3.5 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject35<T>(string json)
        {
            T obj = default(T);
            DataContractJsonSerializer ds = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                obj = (T)ds.ReadObject(ms);
            }
            return obj;
        }

        public static string ObjectToXml<T>(T obj)
        {
            string rtn = "";
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.Serialize(ms, obj);
                rtn = Encoding.UTF8.GetString(ms.ToArray());
            }
            return rtn; ;
        }
        public static T XmlToObject<T>(string xml)
        {
            T t = default(T);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (Stream xmlstream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                using (XmlReader erader = XmlReader.Create(xmlstream))
                {
                    t = (T)serializer.Deserialize(erader);
                }
            }
            return t;
        }

        public static string GetXML(object obj)
        {
            if (obj == null) return string.Empty;
            XmlSerializer formmafter = new XmlSerializer(obj.GetType());
            using (TextWriter writer = new StringWriter())
            {
                formmafter.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        /// <summary>
        /// 序列化为文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public static void ObjectToXmlFile(string path, object obj)
        {
            FileStream stream = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                serializer.Serialize(stream, obj);
            }
            catch (Exception e) { throw new Exception(e.Message); }
            finally
            {
                if (stream != null) stream.Close();
            }
        }

        /// <summary>
        /// 从文件序列化为对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object XmlFileToObject(string path, Type type)
        {
            FileStream stream = null;
            object obj = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                obj = serializer.Deserialize(stream);
            }
            catch (Exception e) { throw new Exception(e.Message); }
            finally { if (stream != null)stream.Close(); }
            return obj;
        }


        /// <summary>
        /// 把对象序列化为字节数组
        /// </summary>
        public static byte[] ObjectToBytes(object obj)
        {
            if (obj == null)
                return null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }
        public static string ObjectToBinaryString(object obj)
        {
            return Encoding.UTF8.GetString(ObjectToBytes(obj));
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public static object BytesToObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null)
                return obj;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }
        public static object BinaryStringToObject(string binarystring)
        {
            return BytesToObject(Encoding.UTF8.GetBytes(binarystring));
        }
    }
}
