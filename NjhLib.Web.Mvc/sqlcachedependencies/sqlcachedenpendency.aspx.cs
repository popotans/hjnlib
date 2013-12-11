using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NjhLib.Utils;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.sqlcachedependencies
{
    public partial class sqlcachedenpendency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = Cache["dt"] as DataTable;

            if (dt == null)
            {
               
                SqlHelper2.connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ibatisdb"].ConnectionString;
                SqlConnection con = new SqlConnection(SqlHelper2.connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select id, name,age,birth from dbo.student order by id";
                SqlDataAdapter sad = new SqlDataAdapter(cmd);
                System.Web.Caching.SqlCacheDependency adp = new System.Web.Caching.SqlCacheDependency(cmd);
                dt = new DataTable();
                sad.Fill(dt);
                con.Close();

               
                Cache.Insert("dt", dt,adp);
               
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
    }
}