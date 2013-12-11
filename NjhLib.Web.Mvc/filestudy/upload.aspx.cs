using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
namespace NjhLib.Web.Mvc.filestudy
{
    public partial class upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fielname = FileUpload1.FileName;
            Stream fs = FileUpload1.PostedFile.InputStream;
            byte[] arr = new byte[fs.Length];
            fs.Read(arr, 0, arr.Length);
            Attachment atc = new Attachment
            {
                FileName = fielname,
                ImgArr = arr,
                Title = "alt"
            };

            AtService service = new AtService(atc, 0, 0);
            service.Save();
            Response.Write("uplaod suc.");


        }
    }
    public class AtService
    {
        private Attachment _attachment;
        private int _questionId;
        private int _answerId;

        public AtService(Attachment attachment, int questionId, int answerId)
        {
            this._attachment = attachment;
            this._questionId = questionId;
            this._answerId = answerId;
        }


        /// <summary>
        /// 验证附件大小
        /// </summary>
        /// <returns></returns>
        public void ValidateSize()
        {
            bool result = false;
            byte[] arr = this._attachment.ImgArr;
            result = arr.Length / 1024 <= 1024;
            if (!result)
                throw new Exception("上传附件太大。");
        }

        /// <summary>
        /// 验证后缀
        /// </summary>
        /// <returns></returns>
        public void ValidateSuffix()
        {
            bool result = false;
            byte[] arr = this._attachment.ImgArr;
            ImageFormat format = this.GetImgType(arr);
            result = format == ImageFormat.Jpeg
                    || format == ImageFormat.Gif
                    || format == ImageFormat.Png
                    || format == ImageFormat.Bmp;
            if (!result)
                throw new Exception("该类型不允许上传。");
        }

        public void Save()
        {
            this.ValidateSize();
            this.ValidateSuffix();
            string filename = this._attachment.FileName;
            byte[] arrFile = this._attachment.ImgArr;
            string title = this._attachment.Title;
            string suffix = this.GetImgSuffix();
            string pathRelative = this.SaveFile(suffix, arrFile);
            this.SaveToDb(pathRelative);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="suffix">文件后缀名</param>
        /// <param name="arrFile">附件字节数组</param>
        /// <returns>上传相对路径</returns>
        private string SaveFile(string suffix, byte[] arrFile)
        {
            string name = DateTime.Now.Ticks.ToString();
            string pathRelative = "filestudy/" + name + "." + suffix;
            string pathBase = AppDomain.CurrentDomain.BaseDirectory;
            string pathAbsolute = pathBase + pathRelative;
            MemoryStream stream = new MemoryStream(arrFile);
            System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
            img.Save(pathAbsolute);
            img.Dispose();

            return pathRelative;
        }

        /// <summary>
        /// 保存附件信息到数据库，并与question相关联
        /// </summary>
        /// <param name="uploadPath">上传相对路径</param>
        private void SaveToDb(string uploadPath)
        {
            //TODO isnert file info to db

        }


        /// <summary>
        /// 获取附件后缀名，不含“.”
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetImgSuffix()
        {
            ImageFormat format = this.GetImageFormat(this._attachment.ImgArr);

            return format.ToString();
        }
        #region tools
        private ImageFormat GetImageFormat(byte[] bytes)
        {
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
        ImageFormat GetImgType(byte[] bytesImg)
        {
            using (var ms = new MemoryStream(bytesImg))
            {
                var img = System.Drawing.Image.FromStream(ms);

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
        #endregion
    }
    public class Attachment
    {
        /// <summary>
        /// 图片的标题
        /// </summary>

        public string Title { get; set; }

        /// <summary>
        /// 图片字节数组
        /// </summary>

        public byte[] ImgArr { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>

        public string FileName { get; set; }
    }
}