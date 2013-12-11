using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NjhLib.Web.Mvc.json
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Jobs { get; set; }
        public List<Leaders> list { get; set; }
    }
    public class Leaders
    {
        public string LeaderId { get; set; }
        public string LeaderName { get; set; }
    }
}