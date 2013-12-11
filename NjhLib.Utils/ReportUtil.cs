using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSExcel = Microsoft.Office.Interop.Excel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace NjhLib.Utils
{
    public class ReportUtil
    {
        public static void OutPutExcelByGridView(GridView grvExcel, string excelName, string encodingName, System.Globalization.CultureInfo ci)
        {
            //定义文档类型、字符编码
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = encodingName;
            //下面这行很重要， attachment 参数表示作为附件下载，您可以改成 online在线打开
            //filename=FileFlow.xls 指定输出文件的名称，注意其扩展名和指定文件类型相符，可以为：.doc 　　 .xls 　　 .txt 　　.htm
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(excelName));
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encodingName);
            //Response.ContentType指定文件类型 可以为application/ms-excel、application/ms-word、application/ms-txt、application/ms-html 或其他浏览器可直接支持文档
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            grvExcel.EnableViewState = false;
            if (ci == null) ci = new System.Globalization.CultureInfo("zh-CN", true);
            System.Globalization.CultureInfo myCultureInfo = ci;

            System.IO.StringWriter oStringWriter = new System.IO.StringWriter(myCultureInfo);
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);

            oHtmlTextWriter.Write("<html><head><meta http-equiv=\"content-type\" content=\"text/html;charset=gb2312\"></head>");

            grvExcel.Visible = true;
            grvExcel.RenderControl(oHtmlTextWriter);
            //this 表示输出本页，你也可以绑定datagrid,或其他支持obj.RenderControl()属性的控件
            HttpContext.Current.Response.Write(oStringWriter.ToString());
            HttpContext.Current.Response.End();
        }
        private void ReportToExcel(Control ctl, string FileName)//导出数据excel（控件中的内容）
        {
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "" + FileName);
            ctl.EnableViewState = false;
            System.IO.StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            ctl.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();
        }

        private void ReportToExcel(string strContent, string fileName, string encodinganme)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = encodinganme;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding(encodinganme);
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
            HttpContext.Current.Response.Write(strContent);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }
}
