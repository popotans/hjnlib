using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using System.Data.SQLite;
using System.Data;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.sqllitesss
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            long ticks = DateTime.Now.Ticks;
           SQLiteDBHelper db = new SQLiteDBHelper(Server.MapPath("dtcms.db"));
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);

            string _pno = Request["pno"];
            if (_pno == null) _pno = "1";
            int pno = int.Parse(_pno);
            int pagecount = 0, recordcount = 0;

            DataTable dt = db.SqlLitePaging("*", " from article ", " order by id desc ", pno, 15, out pagecount, out recordcount);

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
            Response.Write(s + "<br>" + (ticks2 - ticks2));


        }

        void Button2_Click(object sender, EventArgs e)
        {
            string _pno = Request["pno"];
            int pno = int.Parse(_pno);


        }

        void Button1_Click(object sender, EventArgs e)
        {
            SQLiteDBHelper db = new SQLiteDBHelper(Server.MapPath("dtcms.db"));
            System.Data.Common.DbTransaction trans = db.OpenDbTransaction();
            string s = NjhLib.Utils.FileUtil.GetHtmlStringByFilePath(Server.MapPath("1.htm"));
            s = Server.HtmlEncode(s);

            for (int i = 0; i < 100000; i++)
            {
                string sql = " insert into article(title ,author,form,keyword,zhaiyao,classid,imgurl,daodu,content,click,ismsg,istop,isred,ishot,isslide,islock,addtime) ";
                sql += "values('项目背景说明及系统开发要求" + i.ToString() + "','author','form','keyword.keyword','zhaiyaozhaiyaozhaiyaozhaiyaozhaiyaozhaiyao',1,'','daodudaodudaodudddddddddddddddddddddddddddddddddd','" + s + "',1,1,1,1,1,1,0,'" + DateTime.Now.ToString() + "')";
                db.ExecuteNonQueryWithTrans(sql, null, trans);
            }
            db.CommitDbTransaction(trans);
            Response.Write("<script>alert('insert suc')</script>");
        }
    }
}