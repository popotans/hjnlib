using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using NjhLib.Utils;
namespace NjhLib.Web.Mvc.Test
{
    public partial class DownloadFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Button1.Click += new EventHandler(Button1_Click);
            this.Button2Encode.Click += new EventHandler(Button2Encode_Click);
            this.Button2Decode.Click += new EventHandler(Button2Decode_Click);
        }

        void Button2Decode_Click(object sender, EventArgs e)
        {
            string s = "RebFxRdUxf5KClq6fMYm0g== ";
            s = StringUtil.DESDecode(s, "njhandzd");
            Response.Write(s);
        }

        void Button2Encode_Click(object sender, EventArgs e)
        {
            string s = "n23456789012345678901234567890123456789";
            s = StringUtil.AESEncrypt(s, StringUtil.MD5("njhandzd5555"));
            Response.Write(s + "<br/>");
            Response.Write(s.Length);
        }

        void Button1_Click(object sender, EventArgs e)
        {
            string filename = Server.MapPath("files/软通测试.html");
            DownloadFileByWriteFile(filename);
        }
        private void DownloadFileByTransmit(string serverfilename)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            FileInfo f = new FileInfo(serverfilename);
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlPathEncode(f.Name));
            Response.TransmitFile(serverfilename);
            Response.End();
        }
        private void DownloadFileByStream(string serverfilename)
        {
            string fileName = serverfilename;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            if (fileInfo.Exists)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力 51.      
                byte[] buffer = new byte[ChunkSize];
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                System.IO.FileStream iStream = System.IO.File.OpenRead(fileName);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小 56.          
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileInfo.Name));
                while (dataLengthToRead > 0 && Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小 61.            
                    Response.OutputStream.Write(buffer, 0, lengthRead);
                    Response.Flush();
                    dataLengthToRead -= lengthRead;
                }
                Response.Close();
            }
        }
        private void DownloadFileByWriteFile(string serverfilename)
        {
            string fileName = serverfilename;//客户端保存的文件名 24     
            FileInfo fileInfo = new FileInfo(fileName);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlPathEncode(fileInfo.Name));
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }
    }
}