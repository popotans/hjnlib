using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.DatetimeUSe
{
    public partial class DateTimeCompare : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DateTime dt1 = new DateTime(2011, 11, 13);
            DateTime dt2 = DateTime.Now;

            Response.Write(dt1.Ticks+"<br>");
            Response.Write(dt2.Ticks + "<br>");

            Response.Write(Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")).ToString("MM/dd/yyyy HH:mm:ss") + "<br>");
            Response.Write(Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")) < dt1);

           ;
            //DateTime dtNow = new DateTime();
            //DateTime.TryParse(datetimestr, out dtNow);
            //Response.Write(dtNow.ToString());

        }
    }
}