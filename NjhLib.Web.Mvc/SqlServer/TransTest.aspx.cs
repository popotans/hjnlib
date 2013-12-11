using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using System.Data;
using System.Data.Common;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.SqlServer
{
    public partial class TransTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = " update transtest set [name]='jhhhh2' where [id]=1 ";
            string sql2 = " update transtest set [name]='love2' where [id]=2 ";
            using (Trans t = new Trans())
            {
                DbHelper dh = new DbHelper();
                try
                {
                    DbCommand cmd = dh.GetSqlStringCommond(sql);
                    dh.ExecuteNonQuery(cmd, t);

                    cmd = dh.GetSqlStringCommond(sql2);
                    dh.ExecuteNonQuery(cmd, t);
                    throw new Exception("hhhhh");
                    t.Commit();
                }
                catch (Exception ee)
                {
                     t.RollBack();
                    Response.Write(ee.Message);
                }
            }
        }
    }
}