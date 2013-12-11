using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace NjhLib.Web.Mvc.app
{
    public partial class SessionCodeFromWebCast : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "";

            Response.BinaryWrite(GeneRateImage(25, ref s));
        }


        public byte[] GeneRateImage(int nLen, ref string strKey)
        {
            int nBmpWidth = 13 * nLen + 5;
            int nBmpHeight = 25;
            Bitmap bmp = new Bitmap(nBmpWidth, nBmpHeight);

            int nRed, nGreen, Nblue;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            nRed = rnd.Next(255) % 128 + 128;
            nGreen = rnd.Next(255) % 128 + 128;
            Nblue = rnd.Next(255) % 128 + 128;

            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(bmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(nRed, nGreen, Nblue)),
                0,
                0,
                nBmpWidth,
                nBmpHeight);


            int nlines = 10;
            System.Drawing.Pen pen = new Pen(System.Drawing.Color.FromArgb(nRed - 17, nGreen - 17, Nblue - 17), 2);
            for (int a = 0; a < nlines; a++)
            {
                int x1 = rnd.Next() % nBmpWidth;
                int y1 = rnd.Next() % nBmpHeight;
                int x2 = rnd.Next() % nBmpWidth;
                int y2 = rnd.Next() % nBmpHeight;
                graph.DrawLine(pen, x1, y1, x2, y2);

            }

            string strCode = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            string strResult = "";
            for (int i = 0; i < nLen; i++)
            {
                int x = i * 13 + rnd.Next(3);
                int y = rnd.Next(4) + 1;


                System.Drawing.Font font = new Font("arial", 12 + rnd.Next() % 4, System.Drawing.FontStyle.Bold);

                char c = strCode[rnd.Next(strCode.Length)];
                strResult += c.ToString();

                graph.DrawString(c.ToString(), font,
                    new SolidBrush(System.Drawing.Color.FromArgb(nRed - 60 + y * 3, nGreen - 60 + y * 3, Nblue - 60 + y * 3)), x, y);
            }

            for (int i = 0; i < 10; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                bmp.SetPixel(x, y, Color.FromArgb(rnd.Next()));
            }

            graph.DrawRectangle(new Pen(Color.Silver), 0, 0, bmp.Width - 1, bmp.Height - 1);

            System.IO.MemoryStream bstream = new System.IO.MemoryStream();
            bmp.Save(bstream, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Dispose();
            graph.Dispose();
            strKey = strResult;

            byte[] returns = bstream.ToArray();

            bstream.Close();
            return returns;
        }

    }
}