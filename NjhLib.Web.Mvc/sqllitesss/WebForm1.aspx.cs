using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
using System.Data.SQLite;
using NjhLib.Utils.DB;
namespace NjhLib.Web.Mvc.sqllitesss
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = Server.MapPath("test.db");
            SQLiteDBHelper db = new SQLiteDBHelper(path);

            //chuangjiantable
            string sql = "CREATE TABLE Test3(id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,Name char(3),TypeName varchar(50),addDate datetime,UpdateTime Date,Time time,Comments blob)";
            //db.ExecuteNonQuery(sql, null); 


//            string sql2 = "INSERT INTO Test3(Name,TypeName,addDate,UpdateTime,Time,Comments)values(@Name,@TypeName,@addDate,@UpdateTime,@Time,@Comments)";
//            System.Data.Common.DbTransaction trans = db.OpenDbTransaction();
//            for (char c = 'A'; c <= 'Z'; c++)
//            {
//                for (int i = 0; i < 100; i++)
//                {
//                    SQLiteParameter[] parameters = new SQLiteParameter[]{ 
//new SQLiteParameter("@Name",c+i.ToString()+"s"), 
//new SQLiteParameter("@TypeName",c.ToString()), 
//new SQLiteParameter("@addDate",DateTime.Now), 
//new SQLiteParameter("@UpdateTime",DateTime.Now.Date), 
//new SQLiteParameter("@Time",DateTime.Now.ToShortTimeString()), 
//new SQLiteParameter("@Comments","Just a Test"+i) 
//};
//                    db.ExecuteNonQueryWithTrans(sql2, parameters, trans);
//                }
//            }
//            db.CommitDbTransaction(trans);



            //show 
            System.Data.DataTable dt = db.ExecuteDataTable("select * from test3", null);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Response.Write(dr[0] + "=" + dr[1] + "=" + dr[2] + "=" + dr[3] + "=" + dr[4]+"<br>");
            }
        }
    }
}