using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlServerCe;
using System.Data;
using System.IO;
namespace NjhLib.Web.Mvc.sqlcesss
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string str = @"DATA SoURCE=C:\Users\njh\Desktop\MyDatabase#1.sdf";
            //SqlCeEngine sce = new SqlCeEngine(str);
            //sce.Upgrade();
            //SqlCeConnection scc = new SqlCeConnection(str);
            //scc.Open();
            //SqlCeCommand cmd = new SqlCeCommand("select * from test");
            //cmd.Connection = scc;
            //SqlCeDataAdapter adap = new SqlCeDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adap.Fill(dt);
            //scc.Close();

            NjhLib.Utils.DB.SqlceHelper h = new Utils.DB.SqlceHelper(str);
            DataTable dt = new DataTable();
            dt = NjhLib.Utils.DB.SqlceHelper.ExecuteDatatable("select * from test");


            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCEHelper db = new SqlCEHelper(Server.MapPath("test.sdf"));
            db.CreateDatabase();
            Response.Write("数据库创建成功！");
        }
    }

    public class SqlCEHelper
    {
        private string Path = string.Empty;

        public SqlCEHelper(string path)
        {
            this.Path = path;
        }

        public void CreateDatabase()
        {
            if (!File.Exists(Path))
            {
                SqlCeEngine engine = new SqlCeEngine("DATA SoURCE=" + Path);
            }
            else
            {
                throw new FileNotFoundException("该数据库已经存在啦！");
            }
        }
    }
}