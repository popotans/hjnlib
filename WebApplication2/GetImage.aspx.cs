using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils;
namespace WebApplication2
{
    public partial class GetImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "http://localhost:14977/attach/month_1308/130804113241118500.jpg";
            url = "http://static.googleadsserving.cn/pagead/imgad?id=CICAgIDQp-7xjAEQrAIY-gEyCMBPcjdgV-QB";
            ImageUtil.OutputImg(url, 1555, 222);

                
             

        }
    }
}