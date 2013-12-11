using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NjhLib.Utils.DB;
using System.IO;
using Helper.Database;
using System.Data.SqlServerCe;
namespace NjhLib.Web.Mvc.sqlcesss
{
    public partial class import : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            njh.sys.DAL.MySqlHelper db = new njh.sys.DAL.MySqlHelper();
            //DataTable dt = db.ExecDataTable("select * from companyinfo");
            //Response.Write(dt.Rows.Count);

            //string s = Helper.Serialize.BinaryHelper<DataTable>.ToString(dt);
            //StreamWriter sw = new StreamWriter("d:\\dt.bin");
            //sw.Write(s);
            //sw.Close();

            StreamReader sr = new StreamReader("d:\\dt.bin");
            string s = sr.ReadToEnd();
            DataTable dt = Helper.Serialize.BinaryHelper<DataTable>.ToObject(s);
            Response.Write(dt.Rows.Count);
            string ss = "", sql = "";
            MSAccessHelper dbs = new MSAccessHelper("d:\\company.mdb");
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string guid = dr["guid"].ToString();
                    ss = guid;
                    int classid = 0;
                    string tag = "";
                    string companyname = dr["companyname"].ToString();
                    string tel = dr["tel"].ToString();
                    string contact = dr["contact"].ToString();
                    string qq = dr["qq"].ToString();
                    string email = dr["email"].ToString();
                    string url = dr["url"].ToString();
                    string jyfw = dr["jyfw"].ToString();
                    string jianjie = "";
                    string province = dr["province"].ToString();
                    string city = dr["city"].ToString();
                    int click = 0;
                    string sourceurl = dr["sourceurl"].ToString();
                    int isinfo = 0;
                    int issend = 0;
                    string clsaanem1 = dr["classname1"].ToString();
                    string clasnanme2 = dr["classname2"].ToString();
                    string address = dr["address"].ToString();
                    sql = "insert into companyinfo([guid],classid,tag,companyname,tel,contact,qq,email,url,jyfw,province,city,click,sourceurl,isinfo,issend,classname1,classname2,address)";
                    sql += "values('" + guid + "'," + classid + ",'" + tag + "','" + companyname + "','" + tel + "','" + contact + "','" + qq + "','" + email + "','" + url + "','" + jyfw + "','" + province + "','" + city + "',0,'" + sourceurl + "',0,0,'" + clsaanem1 + "','" + clasnanme2 + "','" + address + "')";
                    dbs.ExecuteNonQuery(sql);
                }

            }
            catch (Exception ee)
            {

                StreamWriter sw = new StreamWriter("d:\\0.txt");
                sw.Write(sql);
                sw.Close();
                throw new Exception(ss + sql + "<br>" + ee.Message);
            }

        }
    }
}