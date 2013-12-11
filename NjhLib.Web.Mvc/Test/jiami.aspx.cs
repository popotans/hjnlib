using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    public partial class jiami : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string s = StringUtil.AESEncryptHEX("呵呵woaizhangdan", StringUtil.MD5("niejunhua"));
            Response.Write(s);
            Response.Write("<br/>" + s.Length);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string s = StringUtil.AESDecryptHEX("47E9ABABBA5AFABEA1D0134C99CA63BEF9A265070A82D465C98904ADFD84825C", StringUtil.MD5("niejunhua"));
            Response.Write(s);
        }
    }
}
