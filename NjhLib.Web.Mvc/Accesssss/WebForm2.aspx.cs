using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using System.Data;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.Accesssss
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long ticks = DateTime.Now.Ticks;

            MSAccessHelper db = new MSAccessHelper(Server.MapPath("DtCms.mdb"));
            string _pno = Request["pno"];
            if (_pno == null) _pno = "1";
            int pno = int.Parse(_pno);
            int pagecount = 0, recordcount = 0;
            DataTable dt = db.AccessPaging(" * ", "article", "", " id desc ", 15, pno, out pagecount, out recordcount);

            string s = "";

            foreach (DataRow dr in dt.Rows)
            {
                //  Response.Write(dr["title"].ToString() + "<br>");
                s += dr["title"].ToString() + "<br>";
            }

            s += "<br>";
            s += "共" + pagecount + "页，" + recordcount + "条数据";
            s += "<br>";
            s += StringUtil.showPage(pno, pagecount, 10, null, "");
            s += "";
            long ticks2 = DateTime.Now.Ticks;
            Response.Write(s+"<br>"+(ticks2-ticks2));
        }
    }
}