using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.Test
{
    public partial class TreeExample : System.Web.UI.Page
    {
        IList<MM> list = new List<MM>();
        IList<MM> root = null;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int level = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            MM m = new MM();



            MM mm1 = new MM { ID = 1, Name = "kind1", Parent = 0 };
            MM mm2 = new MM { ID = 2, Name = "kind2", Parent = 0 };
            MM mm3 = new MM { ID = 3, Name = "kind3", Parent = 0 };
            MM mm4 = new MM { ID = 4, Name = "kind4", Parent = 0 };
            MM mm5 = new MM { ID = 5, Name = "kind5", Parent = 1 };
            MM mm6 = new MM { ID = 6, Name = "kind6", Parent = 1 };
            MM mm7 = new MM { ID = 7, Name = "kind7", Parent = 1 };
            MM mm8 = new MM { ID = 8, Name = "kind8", Parent = 2 };
            MM mm9 = new MM { ID = 9, Name = "kind9", Parent = 7 };
            MM mm10 = new MM { ID = 10, Name = "kind10", Parent = 7 };
            MM mm11 = new MM { ID = 11, Name = "kind12", Parent = 10 };
            MM mm12 = new MM { ID = 12, Name = "kind13", Parent = 11 };
            MM mm13 = new MM { ID = 13, Name = "kind14", Parent = 12 };
            MM mm14 = new MM { ID = 14, Name = "kind15", Parent = 13 };
            list.Add(mm1);
            list.Add(mm2);
            list.Add(mm3);
            list.Add(mm4);
            list.Add(mm5);
            list.Add(mm6);
            list.Add(mm7);
            list.Add(mm8);
            list.Add(mm9);
            list.Add(mm10);
            list.Add(mm11);
            list.Add(mm12);
            list.Add(mm13);
            list.Add(mm14);

            root = this.GetSub(0);
            this.DoTree();
        }

        void DoTree()
        {
            DoData(this.root);
            w(sb.ToString());
        }

        void DoData(IList<MM> rt)
        {
            foreach (MM mm in rt)
            {
                getLevel(mm);
                for (int j = 0; j <= level; j++)
                {
                    sb.Append("--");
                }
                sb.Append(mm.Name + "--" + level + "<br/>");
                level = 0;
                IList<MM> sub = this.GetSub(mm.ID);
                this.DoData(sub);
            }
        }

        void getLevel(MM mm)
        {
            if (mm.Parent == 0) level += 1;
            else
            {
                //获取子分类是mm的所有父类类
                foreach (MM m in list)
                {
                    if (m.ID == mm.Parent)
                    {
                        level += 1;
                        getLevel(m);
                    }
                }
            }
        }

        IList<MM> GetSub(int parent)
        {

            IList<MM> list2 = new List<MM>();
            foreach (MM mm in list)
            {
                if (mm.Parent == parent) list2.Add(mm);
            }
            return list2;
        }

        void w(string s)
        {
            Response.Write(s + "<br/>");
        }
    }
    public class MM
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public int Parent { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}