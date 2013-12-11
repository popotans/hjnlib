using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NjhLib.Web.Mvc.remotingstudy
{
    /// <summary>
    /// 首先建立一个Model.dll，注意因为对象要进行序列化转化，必须对其加上Serializable特性！
    /// </summary>
    [Serializable]
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}