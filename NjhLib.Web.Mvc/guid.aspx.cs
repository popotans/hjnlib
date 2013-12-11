using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc
{
    public partial class guid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Guid.NewGuid());


            //string _className = "ss";
            //if (_className.Length > 1)
            //{
            //    int lastindexof = _className.LastIndexOf('s');
            //    if (lastindexof > 0)
            //    {
            //        _className = _className.Substring(0, lastindexof);
            //    }
            //}
            //Response.Write("<br/>" + _className);


            string code = "我爱博客园们";

            string result = NjhLib.Utils.ChineseToPhonetic.ConvertFirst(code);
            Response.Write(result);


        }
    }
}