using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    public partial class pinyin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "皇上万岁万岁万万岁！";

            s = ChineseToPhonetic.Convert(s, 200);
            Response.Write(s);
        }
    }
}