using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace NjhLib.Utils
{
    public class ImageUtil
    {
        public static ImageFormat GetImgType(byte[] bytesImg)
        {
            using (var ms = new MemoryStream(bytesImg))
            {
                var img = Image.FromStream(ms);

                Console.WriteLine("");
                Console.WriteLine("{0}x{1}", img.Width, img.Height);

                if (img.RawFormat.Equals(ImageFormat.Jpeg))
                    return ImageFormat.Jpeg;
                if (img.RawFormat.Equals(ImageFormat.Bmp))
                    return ImageFormat.Bmp;
                if (img.RawFormat.Equals(ImageFormat.Png))
                    return ImageFormat.Png;
                if (img.RawFormat.Equals(ImageFormat.Gif))
                    return ImageFormat.Gif;
            }

            // Unknown
            return new ImageFormat(new Guid());
        }

        public static ImageFormat GetImageFormat(byte[] bytes)
        {
            // see http://www.mikekunz.com/image_file_header.html  

            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] { 137, 80, 78, 71 }; // PNG
            var tiff = new byte[] { 73, 73, 42 }; // TIFF
            var tiff2 = new byte[] { 77, 77, 42 }; // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.Bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.Gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.Png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.Tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.Tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.Jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.Jpeg;

            // Unknown
            return new ImageFormat(new Guid());
        }
        /// <summary>
        /// 是否是图片文件
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static bool IsImgFile(System.Web.HttpPostedFile f)
        {
            bool flag = false;
            string type = f.ContentType.ToString();
            type = type.Substring(0, 5);
            if (type == "image") { flag = true; }
            return flag;
        }
        /// <summary>
        /// 判断是否是允许的上传文件格式
        /// </summary>
        /// <param name="mytype"></param>
        /// <param name="rules"></param>
        /// <returns>true表示 允许上传 false 禁止上传</returns>
        public static bool IsAllowedImgExtension(string mytype, string[] rules)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(mytype))
            {
                foreach (string t in rules)
                {
                    if (t.ToLower() == mytype.ToLower()) { flag = true; break; }
                }
            }
            return flag;
        }

        #region get thumbnail
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="pathImageFrom">图片来源</param>
        /// <param name="pathImageTo">保存的地址</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        public static void GetThumbnail(string pathImageFrom, string pathImageTo, int width, int height)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(pathImageFrom);
            }
            catch
            {
                if (image != null)
                {
                    int num = image.Width;
                    int num2 = image.Height;
                    int num3 = width;
                    int num4 = height;
                    int x = 0;
                    int y = 0;
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
                    Bitmap bitmap = new Bitmap(width, height);
                    Graphics graphics = Graphics.FromImage(bitmap);
                    graphics.Clear(Color.White);
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(image, new Rectangle(x, y, num3, num4), new Rectangle(0, 0, num, num2), GraphicsUnit.Pixel);
                    try
                    {
                        bitmap.Save(pathImageTo, ImageFormat.Jpeg);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        image.Dispose();
                        bitmap.Dispose();
                        graphics.Dispose();
                    }
                }
            }
        }



        public static void OutputImg(string pathImageFrom, int width, int height)
        {
            string pathImageTo = "";

            string originalImagePath = pathImageFrom;
            string thumbnailPath = pathImageTo;


            Image originalImage = null;
            if (!pathImageFrom.ToLower().StartsWith("http"))
                originalImage = Image.FromFile(originalImagePath);
            else
            {
                originalImage = null;
                WebRequest wreq = WebRequest.Create(pathImageFrom);
                wreq.Timeout = 10000;//超时时间
                HttpWebResponse wresp = (HttpWebResponse)wreq.GetResponse();
                using (Stream s = wresp.GetResponseStream())
                {
                    originalImage = System.Drawing.Image.FromStream(s);
                }
            }

            int w1 = originalImage.Width, h1 = originalImage.Height;
            if (w1 > width) height = width * h1 / w1;
            else if (h1 > height) width = w1 * height / h1;
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            string mode = "Cut";
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）　　　　　　　　
                    break;
                case "W"://指定宽，高按比例　　　　　　　　　　
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）　　　　　　　　
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
              new Rectangle(x, y, ow, oh),
              GraphicsUnit.Pixel);



            try
            {
                //以jpg格式保存缩略图
                //bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "image/jpeg";

                bitmap.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="pathImageFrom">图片来源</param>
        /// <param name="pathImageTo">保存的地址</param>
        /// <param name="width">缩略图宽</param>
        /// <param name="height">缩略图高</param>
        public static void MakeThumbnail(string pathImageFrom, string pathImageTo, int width, int height)
        {
            //System.Drawing.Image image, newimage;
            //System.Drawing.Image.GetThumbnailImageAbort callb = null;
            //image = System.Drawing.Image.FromFile(pathImageFrom);
            //int w1 = image.Width;
            //int h1 = image.Height;
            //if (w1 > width)
            //{
            //    height = width * h1 / w1;
            //}
            //else if (h1 > height)
            //{
            //    width = w1 * height / h1;
            //}

            ////保存缩略图  
            //newimage = image.GetThumbnailImage(width, height, callb, new IntPtr());
            //newimage.Save(pathImageTo);
            //newimage.Dispose();



            string originalImagePath = pathImageFrom;
            string thumbnailPath = pathImageTo;


            Image originalImage = Image.FromFile(originalImagePath);
            int w1 = originalImage.Width, h1 = originalImage.Height;
            if (w1 > width) height = width * h1 / w1;
            else if (h1 > height) width = w1 * height / h1;
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = originalImage.Width;
            int oh = originalImage.Height;
            string mode = "Cut";
            switch (mode)
            {
                case "HW"://指定高宽缩放（可能变形）　　　　　　　　
                    break;
                case "W"://指定宽，高按比例　　　　　　　　　　
                    toheight = originalImage.Height * width / originalImage.Width;
                    break;
                case "H"://指定高，宽按比例
                    towidth = originalImage.Width * height / originalImage.Height;
                    break;
                case "Cut"://指定高宽裁减（不变形）　　　　　　　　
                    if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                    {
                        oh = originalImage.Height;
                        ow = originalImage.Height * towidth / toheight;
                        y = 0;
                        x = (originalImage.Width - ow) / 2;
                    }
                    else
                    {
                        ow = originalImage.Width;
                        oh = originalImage.Width * height / towidth;
                        x = 0;
                        y = (originalImage.Height - oh) / 2;
                    }
                    break;
                default:
                    break;
            }
            //新建一个bmp图片
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
              new Rectangle(x, y, ow, oh),
              GraphicsUnit.Pixel);

            try
            {
                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        #endregion
        /// <summary>
        /// 替换图片地址，转换为绝对路径
        /// </summary>
        /// <param name="imghtml">html字符串代码</param>
        /// <param name="httpurl">要在地址前添加的地址</param>
        /// <returns></returns>
        public static string ChangeImgSrcFromHtmlContent(string imghtml, string httpurl)
        {
            return Regex.Replace(imghtml, "(?i)(?<=<img[^>]*?src=\")(?!http://)", httpurl);
        }
        public static List<string> GetImgSrcFromHtmlContent(string imghtml)
        {
            List<string> list = new List<string>();
            string pattern = "(?is)<img.*?src=(['\"]?)(?<url>[^'\" ]+)(?=\\1)[^>]*>";
            MatchCollection mc = Regex.Matches(imghtml, pattern, RegexOptions.IgnoreCase);
            string imgpath = string.Empty;
            foreach (Match m in mc)
            {
                imgpath = m.Groups["url"].Value;//提取出图片地址
                list.Add(imgpath);
            }
            return list;
        }

        public static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            double nPercent = ((double)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            const int destX = -1;
            const int destY = -1;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(destX, destY, destWidth + 2, destHeight + 2),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        public static Image FixedSize(Image imgPhoto, int width, int height, Color color)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            int destX = 0;
            int destY = 0;
            decimal nPercent;
            decimal nPercentW = (width / (decimal)sourceWidth);
            decimal nPercentH = (height / (decimal)sourceHeight);

            //if we have to pad the height pad both the top and the bottom
            //with the difference between the scaled height and the desired height
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = (int)Math.Round((width - (sourceWidth * nPercent)) / 2, 0);
            }
            else
            {
                nPercent = nPercentW;
                destY = (int)Math.Round((height - (sourceHeight * nPercent)) / 2, 0);
            }

            var destWidth = (int)Math.Round(sourceWidth * nPercent);
            var destHeight = (int)Math.Round(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(color);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(destX, destY, destWidth, destHeight),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);
            grPhoto.Dispose();

            return bmPhoto;
        }
        public enum Dimensions
        {
            Width,
            Height
        }
        public enum AnchorPosition
        {
            Top,
            Center,
            Bottom,
            Left,
            Right
        }
        public static Image ConstrainProportions(Image imgPhoto, int size, Dimensions dimension)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            const int destX = -1;
            const int destY = -1;
            double nPercent;

            switch (dimension)
            {
                case Dimensions.Width:
                    nPercent = (size / (double)sourceWidth);
                    break;
                default:
                    nPercent = (size / (double)sourceHeight);
                    break;
            }

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(destX, destY, destWidth + 2, destHeight + 2),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        public static Image Crop(Image imgPhoto, int width, int height, AnchorPosition anchor)
        {
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            int destX = -1;
            int destY = -1;
            double nPercent;
            double nPercentW = (width / (double)sourceWidth);
            double nPercentH = (height / (double)sourceHeight);

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentW;
                switch (anchor)
                {
                    case AnchorPosition.Top:
                        destY = 0;
                        break;
                    case AnchorPosition.Bottom:
                        destY = (int)(height - (sourceHeight * nPercent));
                        break;
                    default:
                        destY = (int)((height - (sourceHeight * nPercent)) / 2);
                        break;
                }
            }
            else
            {
                nPercent = nPercentH;
                switch (anchor)
                {
                    case AnchorPosition.Left:
                        destX = 0;
                        break;
                    case AnchorPosition.Right:
                        destX = (int)(width - (sourceWidth * nPercent));
                        break;
                    default:
                        destX = (int)((width - (sourceWidth * nPercent)) / 2);
                        break;
                }
            }

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                              new Rectangle(destX, destY, destWidth + 2, destHeight + 2),
                              new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                              GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        #region# from haili tieba
        public static byte[] GenerateVerificationCode(string verificationCode)
        {
            System.Drawing.Bitmap bitmap = null;
            System.Drawing.Graphics graphics = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                bitmap = new System.Drawing.Bitmap((int)System.Math.Ceiling((verificationCode.Length * 12.5)), 22);
                graphics = System.Drawing.Graphics.FromImage(bitmap);
                memoryStream = new System.IO.MemoryStream();

                System.Random random = new System.Random();
                //清空图片背景色
                graphics.Clear(System.Drawing.Color.White);
                //画图片的背景噪音线
                /*for (int i = 0; i < 25; ++i)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }*/
                System.Drawing.Font font = new System.Drawing.Font("宋体", 14, (System.Drawing.FontStyle.Bold));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Color.Blue, System.Drawing.Color.DarkRed, 1.2f, true);
                graphics.DrawString(verificationCode, font, brush, 2, 2);
                //画图片的前景噪音点
                /*for (int i = 0; i < 100; ++i)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }*/
                //画图片的边框线
                //g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Gif);
                return memoryStream.ToArray();
            }
            finally
            {
                if (bitmap != null)
                {
                    bitmap.Dispose();
                }
                if (graphics != null)
                {
                    graphics.Dispose();
                }
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }
        }

        public static System.Drawing.Bitmap ChangeSize(System.Drawing.Image image, int width, int height)
        {
            double bl = 1d;
            if ((double)image.Height / (double)height >= (double)image.Width / (double)width)
            {
                bl = System.Convert.ToDouble(image.Height) / System.Convert.ToDouble(height);
            }
            else
            {
                bl = System.Convert.ToDouble(image.Width) / System.Convert.ToDouble(width);
            }
            return new System.Drawing.Bitmap(image, System.Convert.ToInt32(image.Width / bl), System.Convert.ToInt32(image.Height / bl));
        }

        public static System.Drawing.Bitmap ChangeSize(System.IO.Stream imageStream, int width, int height)
        {
            System.Drawing.Image image = null;
            try
            {
                image = System.Drawing.Image.FromStream(imageStream);
                return ChangeSize(image, width, height);
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        public static System.Drawing.Bitmap DownSize(System.Drawing.Image image, int width, int height)
        {
            if (width >= image.Width && height >= image.Height)
            {
                return new System.Drawing.Bitmap(image);
            }
            else
            {
                return ChangeSize(image, width, height);
            }
        }

        public static System.Drawing.Bitmap DownSize(System.IO.Stream imageStream, int width, int height)
        {
            System.Drawing.Image image = null;
            try
            {
                image = System.Drawing.Image.FromStream(imageStream);
                if (width >= image.Width && height >= image.Height)
                {
                    return new System.Drawing.Bitmap(image);
                }
                else
                {
                    return ChangeSize(image, width, height);
                }
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        public static void Save(string physicalPath, System.Drawing.Image image)
        {
            try
            {
                image.Save(physicalPath);
            }
            finally
            {
                image.Dispose();
            }
        }
        #endregion

        public static bool AddWatermark(string strText, string orgImgPath, string outImgPath)
        {
            bool flag;
            try
            {
                Image image = Image.FromFile(orgImgPath);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                Font font = new Font("Verdana", 10f);
                Brush brush = new SolidBrush(Color.LightSlateGray);
                graphics.DrawString(strText, font, brush, (float)(image.Width - 100), (float)(image.Height - 20));
                graphics.Dispose();
                image.Save(outImgPath);
                image.Dispose();
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }
        public static bool AddWatermark(string strText, string font, int size, string orgImgPath, string outImgPath)
        {
            bool flag;
            try
            {
                Image image = Image.FromFile(orgImgPath);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(image, 0, 0, image.Width, image.Height);
                Font font2 = new Font(font, (float)size);
                Brush brush = new SolidBrush(Color.LightSlateGray);
                graphics.DrawString(strText, font2, brush, (float)(image.Width - 100), (float)(image.Height - 20));
                graphics.Dispose();
                image.Save(outImgPath);
                image.Dispose();
                flag = true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return flag;
        }
        private static byte[] LoadPictMemory(string filePath)
        {
            byte[] buffer = null;
            FileInfo info = new FileInfo(filePath);
            if (info.Exists)
            {
                buffer = new byte[info.Length];
                FileStream input = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                new BinaryReader(input).Read(buffer, 0, Convert.ToInt32(info.Length));
                input.Dispose();
                return buffer;
            }
            HttpContext.Current.Response.Write("<script language='javascript'>alert('没有找到你所指定的图片')</script>");
            return buffer;
        }
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
        {
            Image image = Image.FromFile(originalImagePath);
            int num = width;
            int num2 = height;
            int x = 0;
            int y = 0;
            int num5 = image.Width;
            int num6 = image.Height;
            string str = mode;
            if ((str != null) && (str != "HW"))
            {
                if (!(str == "W"))
                {
                    if (str == "H")
                    {
                        num = (image.Width * height) / image.Height;
                    }
                    else if (str == "Cut")
                    {
                        if ((((double)image.Width) / ((double)image.Height)) > (((double)num) / ((double)num2)))
                        {
                            num6 = image.Height;
                            num5 = (image.Height * num) / num2;
                            y = 0;
                            x = (image.Width - num5) / 2;
                        }
                        else
                        {
                            num5 = image.Width;
                            num6 = (image.Width * height) / num;
                            x = 0;
                            y = (image.Height - num6) / 2;
                        }
                    }
                }
                else
                {
                    num2 = (image.Height * width) / image.Width;
                }
            }
            Image image2 = new Bitmap(num, num2);
            Graphics graphics = Graphics.FromImage(image2);
            graphics.InterpolationMode = InterpolationMode.High;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.Clear(Color.Transparent);
            graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num5, num6), GraphicsUnit.Pixel);
            try
            {
                image2.Save(thumbnailPath, ImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            finally
            {
                image.Dispose();
                image2.Dispose();
                graphics.Dispose();
            }
        }



    }
}
