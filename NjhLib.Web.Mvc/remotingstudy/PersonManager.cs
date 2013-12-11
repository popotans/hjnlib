using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace NjhLib.Web.Mvc.remotingstudy
{
    /// <summary>
    /// 然后建立一个可远程调用的对象，注意远程对象必须继承MarshalByRefObject
    /// </summary>
    public class PersonManager : MarshalByRefObject
    {
        public List<Person> GetList()
        {
            List<Person> personList = new List<Person>();
            Person p1 = new Person { Age = 25, ID = 10, Name = "niejunhua" };
            BinaryFormatter format = new BinaryFormatter();
           
            /////在服务器文件里面获取虚拟数据
            FileStream stream = new FileStream("datasource.sour", FileMode.Open, FileAccess.Read);
      
            /////对虚拟数据进行反序列化获取集合
            personList = format.Deserialize(stream) as List<Person>;
            return personList;
        }
    }
}