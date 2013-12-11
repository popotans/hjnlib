using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = "key";
            string s = "niejunhua";
            string ss = StringUtil.DESEncrypt2(s, key);


            Response.Write(ss + "<BR/>");
            string JIMI = "KIRJ4eLssohWf83gPJk5og== ";
            JIMI = StringUtil.DesDecrypt2(JIMI, key);
            Response.Write(JIMI);
        }
    }
}