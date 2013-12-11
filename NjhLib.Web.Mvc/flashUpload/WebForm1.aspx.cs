using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace NjhLib.Web.Mvc.flashUpload
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.Request.Files.Count > 0)
            {
                //	RgaWeb.DAL.RGA.Header header;
                string tempId = Request.Params["TempId"];
                string tempFile = this.Page.Request.PhysicalApplicationPath;
                string filePath = string.Format("{0}{1}\\{2}\\{3}\\", tempFile, "RGA", tempId.Substring(0, 2), DateTime.Now.Year.ToString());

                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                for (int j = 0; j < this.Page.Request.Files.Count; j++)
                {
                    HttpPostedFile uploadFile = this.Page.Request.Files[j];
                    if (uploadFile.ContentLength > 0)
                    {
                      //  header = new RgaWeb.DAL.RGA.Header();
                        string extName = uploadFile.FileName.Substring(uploadFile.FileName.LastIndexOf("."), uploadFile.FileName.Length - uploadFile.FileName.LastIndexOf("."));
                        int maxNum = 1;// header.GetMaxFileNum(tempId);//Get File Count
                        string fileName = tempId + maxNum + extName.ToLower();
                        uploadFile.SaveAs(string.Format("{0}{1}", filePath, fileName));
                        //header.AddTempFile(tempId, fileName); Save TO DataBase
                    }
                }
            }
        }


        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion

    }
}