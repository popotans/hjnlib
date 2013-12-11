using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NjhLib.Web.Mvc.Test
{
    public partial class mytreeviewexample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            for (int i = 0; i <= 10; i++)
            {
                TreeNode tn = new TreeNode();
                tn.Text = i + "";
                tn.Value = i * i + "";
                
                
                trv1.Nodes.Add(tn);
            }
            trv1.TreeNodePopulate += new TreeNodeEventHandler(trv1_TreeNodePopulate);
        }

        void trv1_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}