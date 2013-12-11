using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TopSpace.Bll;
using SpaceTime;
using SpaceTime.DataBase;
using SpaceTime.Page;

namespace NjhLib.Web.Mvc.Taobao
{
    public partial class cats : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<ItemCat> list = NjhLib.Utils.TaobaoLib.GetCatList("0");
            foreach (ItemCat ic in list)
            {
                Response.Write("<a href='?cid=" + ic.cid + "'>" + ic.name + "</a><br>");
            }
        }
    }
}