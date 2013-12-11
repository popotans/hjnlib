using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
namespace NjhLib.Web.Mvc.app
{
    public partial class img2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Img();
        }

        private void Img()
        {
            MemoryStream ms = new MemoryStream();
            if (string.IsNullOrEmpty(Request.QueryString["path"]))
            {
                this.GetInitImage(ms);
            }
            string path = Request.QueryString["path"];
            int num = 99;
            int num2 = 63;
            int width = num;
            int height = num2;
            try
            {
                if (Request.QueryString["width"] != null)
                {
                    width = Convert.ToInt16(Request.QueryString["width"]);
                }
            }
            catch
            {
                width = num;
            }
            try
            {
                if (Request.QueryString["height"] != null)
                {
                    height = Convert.ToInt16(Request.QueryString["height"]);
                }
            }
            catch
            {
                height = num2;
            }
            string mode = "";
            if (Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"];
            }
            this.GetThumbnailImage(ms, path, width, height, mode);

        }
        #region 获取初始图片
        /// <summary>
        /// 获取初始图片
        /// </summary>
        /// <param name="ms">MemoryStream对象</param>
        /// <param name="path">初始图片路径</param>
        private void GetInitImage(MemoryStream ms)
        {
            string strPath = "/images/noimg.jpg";
            // Bitmap bitmap = new Bitmap(HttpContext.Current.Server.MapPath(strPath));
            Bitmap bitmap = new Bitmap(System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(strPath)));
            bitmap.Save(ms, ImageFormat.Jpeg);
            bitmap.Dispose();
            HttpContext.Current.Response.ContentType = "image/Jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());

            ms.Close();
        }

        #endregion
        #region 获取缩略图
        /// <summary>
        /// 获取缩略图
        /// </summary>
        /// <param name="ms">MemoryStream对象</param>
        /// <param name="path">原图路径</param>
        /// <param name="width">画布长度</param>
        /// <param name="height">画布长度</param>
        /// <param name="mode">模式</param>
        private void GetThumbnailImage(MemoryStream ms, string path, int width, int height, string mode)
        {
            System.Drawing.Image image = null;
            try
            {
                int num;
                if (path.StartsWith("http"))
                {
                    using (Stream stream = WebRequest.Create(path).GetResponse().GetResponseStream())
                    {
                        image = System.Drawing.Image.FromStream(stream);
                        goto Label_0041;
                    }
                }
                image = System.Drawing.Image.FromFile(Server.MapPath(path));
            Label_0041:
                num = image.Width;
                int num2 = image.Height;
                int num3 = width;
                int num4 = height;
                int x = 0;
                int y = 0;
                if (mode == "scale")
                {
                    if ((num4 * num) > (num3 * num2))
                    {
                        num4 = (num2 * width) / num;
                        y = (height - num4) / 2;
                    }
                    else
                    {
                        num3 = (num * height) / num2;
                        x = (width - num3) / 2;
                    }
                }
                Bitmap bitmap = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.White);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, new Rectangle(x, y, num3, num4), new Rectangle(0, 0, num, num2), GraphicsUnit.Pixel);
                image.Dispose();
                bitmap.Save(ms, ImageFormat.Jpeg);
                HttpContext.Current.Response.ContentType = "image/Jpeg";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                bitmap.Dispose();
                graphics.Dispose();
                ms.Close();
            }
            catch
            {
                this.GetInitImage(ms);
            }
        }
        #endregion

    }
}
