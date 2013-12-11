using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.Accesssss
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
  
        }

        void Button1_Click(object sender, EventArgs e)
        {
            MSAccessHelper db = new MSAccessHelper(Server.MapPath("dtcms.mdb"));

            string s = NjhLib.Utils.FileUtil.GetHtmlStringByFilePath(Server.MapPath("1.htm"));
            s = Server.HtmlEncode(s);
           // Response.Write("开始时间：" + DateTime.Now.ToString());
            for (int i = 0; i < 100000; i++)
            {
                TextBox1.Text = "insert begin:" + DateTime.Now + "\r" + TextBox1.Text;
                TextBox1.Text = "now :" + i.ToString() + "\r" + TextBox1.Text;
                string sql = " insert into article(title ,author,form,keyword,zhaiyao,classid,imgurl,daodu,content,click,ismsg,istop,isred,ishot,isslide,islock,addtime) ";
                sql += "values('项目背景说明及系统开发要求" + i.ToString() + "','author','form','keyword.keyword','zhaiyaozhaiyaozhaiyaozhaiyaozhaiyaozhaiyao',1,'','daodudaodudaodudddddddddddddddddddddddddddddddddd','" + s + "',1,1,1,1,1,1,0,'" + DateTime.Now.ToString() + "')";
                db.ExecuteNonQuery(sql);

            }
            TextBox1.Text = "insert end:" + DateTime.Now.ToString() + "\r" + TextBox1.Text;
            //Response.Write("结束时间：" + DateTime.Now.ToString());
            Response.Write("<script>alert('insert suc')</script>");

        }
    }
}