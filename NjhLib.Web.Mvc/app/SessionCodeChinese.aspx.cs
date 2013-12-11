using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;

namespace NjhLib.Web.Mvc.app
{
    public partial class SessionCodeChinese : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GraphicsImage(4, HttpContext.Current);
        }

        private object[] CreateString(int strlength)
        {
            //定义一个数组存储汉字编码的组成元素 
            string[] str = new string[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", 
 "e", "f" };

            Random ran = new Random(); //定义一个随机数对象 
            object[] bytes = new object[strlength];
            for (int i = 0; i < strlength; i++)
            {
                //获取区位码第一位 
                int ran1 = ran.Next(11, 14);
                string str1 = str[ran1].Trim();

                //获取区位码第二位并防止数据重复 
                ran = new Random(ran1 * unchecked((int)DateTime.Now.Ticks) + i);
                int ran2;
                if (ran1 == 13)
                {
                    ran2 = ran.Next(0, 7);
                }
                else
                {
                    ran2 = ran.Next(0, 16);
                }
                string str2 = str[ran2].Trim();

                //获取区位码第三位 
                ran = new Random(ran2 * unchecked((int)DateTime.Now.Ticks) + i);
                int ran3 = ran.Next(10, 16);
                string str3 = str[ran3].Trim();

                //获取区位码第四位 
                ran = new Random(ran3 * unchecked((int)DateTime.Now.Ticks) + i);
                int ran4;
                if (ran3 == 10)
                {
                    ran4 = ran.Next(1, 16);
                }
                else if (ran3 == 15)
                {
                    ran4 = ran.Next(0, 15);
                }
                else
                {
                    ran4 = ran.Next(0, 16);
                }
                string str4 = str[ran4].Trim();

                //定义字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str1 + str2, 16);
                byte byte2 = Convert.ToByte(str3 + str4, 16);

                byte[] stradd = new byte[] { byte1, byte2 };
                //将产生的汉字字节放入数组 
                bytes.SetValue(stradd, i);
            }
            return bytes;

        }
        private string GetString(int length, HttpContext hc)
        {
            Encoding gb = Encoding.GetEncoding("gb2312");
            object[] bytes = CreateString(length);

            //根据汉字字节解码出中文汉字 
            string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
            string str2 = gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
            string str3 = gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));
            string str4 = gb.GetString((byte[])Convert.ChangeType(bytes[3], typeof(byte[])));

            string str = str1 + str2 + str3 + str4;
            hc.Response.Cookies.Add(new HttpCookie("CheckCode", str));
            return str;
        }
        private void GraphicsImage(int length, HttpContext hc)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((GetString(length, hc).Length *
            22.5)), 22);
            Graphics g = Graphics.FromImage(image); //创建画布 

            try
            {
                //生成随机生成器 
                Random random = new Random();

                //清空图片背景色 
                g.Clear(Color.White);

                //画图片的背景噪音线 
                for (int i = 0; i < 1; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Couriew New", 12, System.Drawing.FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new
                System.Drawing.Drawing2D.LinearGradientBrush
                (new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(GetString(length, hc), font, brush, 2, 2);

                //画图片的前景噪音点 
                for (int i = 0; i < 20; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                hc.Response.ClearContent();
                hc.Response.ContentType = "image/Gif";
                hc.Response.BinaryWrite(ms.ToArray());
            }
            catch (Exception ms)
            {
                hc.Response.Write(ms.Message);
            }
        }
    }
}
