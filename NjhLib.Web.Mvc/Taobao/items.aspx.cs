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
    public partial class items : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<TaobaokeItem> list = NjhLib.Utils.TaobaoLib.GetItemListByCid("16", "", "1", "40");
            Response.Write(list.Count);
        }
    }
}