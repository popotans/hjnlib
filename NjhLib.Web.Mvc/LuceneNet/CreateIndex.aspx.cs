using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System.Data;
using NjhLib.Utils.DB;


namespace NjhLib.Web.Mvc.LuceneNet
{
    public partial class CreateIndex : System.Web.UI.Page
    {
        Lucene.Net.Analysis.Standard.StandardAnalyzer anay = new StandardAnalyzer();
        Directory directory = new RAMDirectory();
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
            Button2.Click += new EventHandler(Button2_Click);
            if (!IsPostBack)
            {
                directory = new RAMDirectory();
            }

        }

        void Button2_Click(object sender, EventArgs e)
        {
            this.Search();
        }


        private void Search()
        {
            IndexSearcher ish = new IndexSearcher("d:\\lucene");
            
            //sort
           

            string strSearch = TextBox1.Text;
            Query q = MultiFieldQueryParser.Parse("" + strSearch + "*", new string[] { "content", "title", "content", "nick", "bname" }, anay);
            Hits his = ish.Search(q);
            string s = string.Format("符合条件记录：{0}，索引总记录：{1}", his.Length(), "" + ish.MaxDoc());
            for (int i = 0; i < his.Length(); i++)
            {
                Document doc = his.Doc(i);
                string content = doc.GetField("content").StringValue();
                string title = doc.GetField("title").StringValue();
                string nick = doc.GetField("nick").StringValue();
                string bname = doc.GetField("bname").StringValue();
                string bid = doc.GetField("bid").StringValue();
                string aid = doc.GetField("aid").StringValue();
                s += "<br/><div style='width:100%;background:#ccc'><hr>" + (i + 1) + "title:" + aid + "--" + title + "<hr>content:" + content + "<hr>bname:" + bid + bname + "</div>";
            }
            Literal1.Text = s;
        }

        void Button1_Click(object sender, EventArgs e)
        {
            CreateIndex2();
        }
        private void CreateIndex2()
        {

            IndexWriter iw = null;
            iw = new IndexWriter("D:\\lucene", anay, true);

            DataTable dt = SqlHelper2.QueryTable("select  a_id, b_name,u_nickname,a_title,a_content,b_id from v_article");

            foreach (DataRow dr in dt.Rows)
            {
                Document doc = new Document();
                string title = dr["a_title"].ToString();
                string content = dr["a_content"].ToString();
                string nickname = dr["u_nickname"].ToString();
                string bname = dr["b_name"].ToString();
                string bid = dr["b_id"].ToString();
                string aid = dr["a_id"].ToString();
                if (aid == "5938")
                {
                    doc.SetBoost(100);
                }
                doc.Add(Field.Keyword("title", title));
                doc.Add(Field.Keyword("content", content));
                doc.Add(Field.Keyword("nick", nickname));
                doc.Add(Field.Text("bname", bname));
                doc.Add(Field.Keyword("bid", bid));
                doc.Add(Field.Keyword("aid", aid));
               
                iw.AddDocument(doc);
            }
            iw.Optimize();
            iw.Close();
            Response.Write("<script>alert('建立索引完成！');</script>");

        }
    }
}