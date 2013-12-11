using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Web.UI;

namespace NjhLib.Utils
{
    public class WebUtil
    {
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            return GetClientIP();
        }


        public static string GetClientIP()
        {
            HttpRequest request = HttpContext.Current.Request;
            string userHostAddress = string.Empty;
            userHostAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = "127.0.0.1";
            }
            return userHostAddress;

        }
        /// <summary>
        /// 获取页面名称
        /// </summary>
        /// <returns></returns>
        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }

        /// <summary>
        /// 得到前缀
        /// </summary>
        /// <returns></returns>
        public static string GetPrefixByDomain()
        {
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            string[] strArray = StringUtil.SplitString(serverName, ".");
            return serverName.Replace("." + strArray[strArray.Length - 2] + "." + strArray[strArray.Length - 1], "").ToLower();
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            return "";
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="hpf">上传对象</param>
        /// <param name="rules">上传过滤规则</param>
        /// <param name="ContentLength">文件大小</param>
        /// <param name="savePath">保存路径，当前路径下请“./”</param>
        /// <param name="fileFullPath">返回的文件名称路径</param>
        public static void Upload(HttpPostedFile hpf, string[] rules, long ContentLength, string savePath, ref string fileFullPath)
        {
            if (string.IsNullOrEmpty(savePath)) savePath = "./";
            if (!savePath.EndsWith("/")) savePath += "/";

            savePath += DateTime.Now.Year.ToString() + "/" + DateTime.Now.ToString("MMdd");

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
            }

            string name = hpf.FileName;
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("请选择上传文件！");
            }
            string suffix = name.Substring(name.LastIndexOf('.') + 1);
            suffix = suffix.ToLower();
            bool isallowed = false;
            foreach (string s in rules)
            {
                if (s == suffix || "." + s == suffix) { isallowed = true; break; }
            }

            try
            {
                if (!isallowed) { throw new Exception("文件格式不允许！"); }
                if (hpf.ContentLength > ContentLength) { throw new Exception("文件太大！"); }
                string fullpath = "";

                fullpath = savePath + "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "." + suffix;

                fileFullPath = fullpath;
                if (fileFullPath.StartsWith("./"))
                {
                    fileFullPath = fileFullPath.Substring(2);
                }

                hpf.SaveAs(HttpContext.Current.Server.MapPath(fullpath));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        #region loadForPage
        /// <summary>
        /// 加载js文件
        /// </summary>
        /// <param name="jsPath">js文件完整路径</param>
        /// <returns><script>代码段</returns>
        public static string LoadJs(string jsPath)
        {
            return string.Format("<script type=\"text/javascript\" src=\"{0}\"></script>", jsPath);
        }

        /// <summary>
        /// 加载样式表文件
        /// </summary>
        /// <param name="cssPath">样式表完整路径</param>
        /// <returns><link rel="">代码段</returns>
        public static string LoadCss(string cssPath)
        {
            return string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", cssPath);
        }
        #endregion


        #region#
        public static string GetQueryString(string key)
        {
            return System.Web.HttpContext.Current.Request.QueryString[key];
        }
        public static string GetForm(string key)
        {
            return System.Web.HttpContext.Current.Request.Form[key];
        }
        public static string GetRequest(string key)
        {
            return System.Web.HttpContext.Current.Request[key];
        }
        public static string GetCookie(string cookieName, string key)
        {
            if (System.Web.HttpContext.Current.Request.Cookies[cookieName] != null && System.Web.HttpContext.Current.Request.Cookies[cookieName][key] != null)
            {
                return System.Web.HttpContext.Current.Request.Cookies[cookieName][key];
            }
            else
            {
                return null;
            }
        }
        public static void RemoveCookie(string cookieName)
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove(cookieName);
        }
        public static void WriteCookie(string cookieName, string key, string keyValue, System.DateTime expires)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Expires = expires;
            cookie[key] = keyValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void WriteCookie(string cookieName, string key, string keyValue, System.DateTime expires, string path)
        {
            System.Web.HttpCookie cookie = new System.Web.HttpCookie(cookieName);
            cookie.Expires = expires;
            cookie.Path = path;
            cookie[key] = keyValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static string GetSessionString(string key)
        {
            object value = System.Web.HttpContext.Current.Session[key];
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return null;
            }
        }
        public static object GetSession(string key)
        {
            return System.Web.HttpContext.Current.Session[key];
        }
        public static void WriteSession(string key, object keyValue)
        {
            System.Web.HttpContext.Current.Session[key] = keyValue;
        }

        public static void RemoveSession(string key)
        {
            System.Web.HttpContext.Current.Session.Remove(key);
        }
        #endregion#

        public static void Show(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }
        public static void ShowAndRedirect(Page page, string msg, string url)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg + "');window.location=\"" + url + "\"</script>");
        }

        public static void Redirect301(string url)
        {
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.StatusCode = 301;
            HttpContext.Current.Response.Status = "301 Moved Permanently";
            HttpContext.Current.Response.AddHeader("Location", url);

        }



    }
}
