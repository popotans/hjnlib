﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace NjhLib.Utils
{
    /// <summary>
    /// 采用ClientScript.RegisterStartupScript(string msg)的方式输出，不会改变xhtml的结构,
    /// 不会影响执行效果。
    /// </summary>
    public class JscriptUtil
    {
        #region 新版本
        /// <summary>
        /// 弹出JavaScript小窗口
        /// </summary>
        /// <param name="js">窗口信息</param>
        public static void Alert(string message, Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    alert('" + message + "');</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "alert"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "alert", js);
            }
            #endregion
        }
        /// <summary>
        /// 弹出消息框并且转向到新的URL
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <param name="toURL">连接地址</param>
        public static void AlertAndRedirect(string message, string toURL, Page page)
        {
            #region
            string js = "<script type='text/javascript'>alert('{0}');window.location.replace('{1}')</script>";
            //HttpContext.Current.Response.Write(string.Format(js, message, toURL));
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "AlertAndRedirect"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "AlertAndRedirect", string.Format(js, message, toURL));
            }
            #endregion
        }
        /// <summary>
        /// 转向新的url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="page"></param>
        public static void Redirect(string url, Page page)
        {
            string js = @"<script type='text/javascript'>window.location.href='{0}';</script>";
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "AlertAndRedirect"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "Redirect", string.Format(js, url));
            }
        }

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1</param>
        public static void GoHistory(int value, Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>
              history.go({0});
                  </Script>";
            //HttpContext.Current.Response.Write(string.Format(js, value));
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "GoHistory1"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "GoHistory1", string.Format(js, value));
            }
            #endregion
        }

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent(string url, Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.opener.location.href='" + url + "';window.close();</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshParent"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshParent", js);
            }
            #endregion
        }

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener(Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    opener.location.reload();
                  </Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "RefreshOpener"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "RefreshOpener", js);
            }
            #endregion
        }

        /// <summary>
        /// 打开指定大小的新窗体
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="width">宽</param>
        /// <param name="heigth">高</param>
        /// <param name="top">头位置</param>
        /// <param name="left">左位置</param>
        public static void OpenNewWindowSize(string url, int width, int heigth, int top, int left, Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>window.open('" + url + @"','','height=" + heigth + ",width=" + width + ",top=" + top + ",left=" + left + ",location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');</Script>";
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "OpenWebFormSize"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebFormSize", js);
            }
            #endregion
        }
        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">连接地址</param>
        public static void JstLocationHref(string url, Page page)
        {
            #region
            string js = @"<Script type='text/javascript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            //HttpContext.Current.Response.Write(js);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "JavaScriptLocationHref"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "JavaScriptLocationHref", js);
            }
            #endregion
        }
        /// <summary>
        /// 弹出模态窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        /// <returns></returns>
        public static string ShowModalDialogJavascript(string webFormUrl, string features)
        {
            #region
            string js = @"<script type='text/javascript'>                            
                            showModalDialog('" + webFormUrl + "','','" + features + "');</script>";
            return js;
            #endregion
        }
        /// <summary>
        /// 弹出模态窗口
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <param name="features"></param>
        public static void ShowModalDialogWindow(string webFormUrl, string features, Page page)
        {
            string js = ShowModalDialogJavascript(webFormUrl, features);
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), "ShowModalDialogWindow"))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "ShowModalDialogWindow", js);
            }
        }

        /// <summary>
        /// 打开指定大小位置的模式对话框
        /// </summary>
        /// <param name="webFormUrl">连接地址</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="top">距离上位置</param>
        /// <param name="left">距离左位置</param>
        public static void ShowModalDialogWindow(string webFormUrl, int width, int height, int top, int left, Page page)
        {
            #region
            string features = "dialogWidth:" + width.ToString() + "px"
                + ";dialogHeight:" + height.ToString() + "px"
                + ";dialogLeft:" + left.ToString() + "px"
                + ";dialogTop:" + top.ToString() + "px"
                + ";center:yes;help=no;resizable:no;status:no;scroll=yes";
            ShowModalDialogWindow(webFormUrl, features, page);
            #endregion
        }

        #endregion

        #region from myblog
        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(System.Web.UI.WebControls.WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并回到前一页面
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void AlertandBack(string msg, System.Web.UI.Page page)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script type='text/javascript' defer>alert('" + msg.ToString() + "');window.history.go(-1);</script>");

        }
        /// <summary>
        /// 打开新窗口
        /// </summary>
        /// <param name="page"></param>
        /// <param name="url"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public static void OpenNewWindow(string url, string w, string h, System.Web.UI.Page page)
        {
            StringBuilder Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("form = window.open('" + url + "','','toolbar=no,menubar=no,titlebar=no,directories=no,resizable=no,status=no,fullscreen=no,center=yes,width=" + w + ",height=" + h + "');window.opener=null;");
            //form.resizeTo(screen.availWidth,screen.availHeight);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(System.Web.UI.Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>" + script + "</script>");
        }
        #endregion
    }
}
