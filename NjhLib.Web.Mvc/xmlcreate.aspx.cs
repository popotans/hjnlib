using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using System.Xml;
namespace NjhLib.Web.Mvc
{
    public partial class xmlcreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            XMLCreater create = new XMLCreater("c:\\xml.xml", "root");

            XmlNode node1 = create.RootElement.AddNode(create, "users");
            node1.AddAttibute(create, "target", "orgval");
            XmlNode n = node1.AddNode(create, "user").AddAttibute(create, "asd", "as");
          
            create.Save();
        }
    }
}