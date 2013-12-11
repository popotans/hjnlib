using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Text;
namespace NjhLib.Web.Mvc
{
    public partial class e : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  Exception eee = Server.GetLastError();
            // Response.Write(((Exception)eee).Message);

            string s = "1,2,3,bbsadmin,5,ad,";
          //  Response.Write(FilterStr(s));

            Response.Write(Math.Ceiling(1.1));

        }


        private string FilterStr(string source)
        {
            string[] banzhu = source.Split(new char[] { ',' });
            string s = "";
            foreach (string item in banzhu)
            {
                if (IsInt(item)) { s += item + ","; }
            }
            if (s.Length > 0)
            {
                if (s.EndsWith(","))
                {
                    s = s.Substring(0, s.Length - 1);
                }
            }
            source = s;
            return source;

        }
        public bool IsInt(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            string pattern = @"^-?\d+$";
            return System.Text.RegularExpressions.Regex.IsMatch(s, pattern);
        }
    }
}