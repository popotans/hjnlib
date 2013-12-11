using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using NjhLib.Utils;
using System.Drawing.Imaging;

namespace NjhLib.Web.Mvc.app
{
    public partial class SessionCode2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Code();
        }
        private void Code()
        {
            Response.Buffer = true;   //设置页面立即过期
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            this.makeCheckCode();
            Random ran = new Random((int)DateTime.Now.Ticks);
            Font f = new Font("Arial", 28, FontStyle.Regular);
            Bitmap bmp = new Bitmap(195, 48);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            for (int i = 0; i < 35; i++)
            {
                int x = ran.Next(200);
                int y = ran.Next(50);
                Color c = Color.FromArgb(ran.Next(255), ran.Next(255), ran.Next(255));
                g.DrawRectangle(new Pen(c), x, y, 1, 5);
            }
            string ranstr = Session["checkcode"].ToString();
            int ypx = 0;
            for (int i = 0; i < ranstr.Length; i++)
            {
                int xpos = i * 35 + 10;
                int ypos = 1 + ypx * 8;
                ypx = ypx == 0 ? 1 : 0;
                Color c = Color.DarkRed;
                SolidBrush sb = new SolidBrush(c);
                g.DrawString(ranstr.Substring(i, 1), f, sb, xpos, ypos);
            }
            Response.ClearHeaders();
            Response.Clear();
            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Gif);
            Response.AddHeader("Content-Type", "image/Gif");
            Response.BinaryWrite(ms.ToArray());

            g.Dispose();
            bmp.Dispose();
            ms.Close();
        }
        private void makeCheckCode()
        {
            string ranstr = StringUtil.GetRandomStr(5).ToUpper();
            Session["checkcode"] = ranstr;
        }
    }
}
