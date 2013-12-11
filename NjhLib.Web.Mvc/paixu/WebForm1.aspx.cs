using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.paixu
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<int> list = new List<int>();

            list.Add(5);
            list.Add(1);
            list.Add(25);
            list.Add(50);
            list.Add(8);
            list.Add(9);
            list.Add(7);
            list.Add(7);
            list.Add(7);
            list.Add(50);

            list = UniqueList(list);
            list = SortList(list, "desc");

            foreach (int i in list)
            {
                Response.Write(i + ",");
            }
        }

        

        private List<int> UniqueList(List<int> list)
        {
            return list.Distinct<Int32>().ToList<Int32>();
        }

        private List<int> SortList(List<int> list, string order)
        {
    
            if (order == "asc")
            {
                list.Sort(delegate(int a, int b)
                {
                    return a.CompareTo(b);
                });
            }
            else if (order == "desc")
            {
                list.Sort(delegate(int a, int b)
                {
                    return b.CompareTo(a);
                });
            }
            else
            {
                SortList(list, "asc");
            }

            return list;
        }
    }
}