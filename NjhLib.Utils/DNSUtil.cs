using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NjhLib.Utils
{
    [Obsolete("此类不再更新，不建议在使用，请用WebUtil代替")]
    public class DNSUtil
    {
        public static string GetIP()
        {
            string ip = string.Empty;
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            switch (ip)
            {
                case null:
                case "":
                    ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    break;
            }
            if ((ip == null) || (ip == string.Empty))
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            if (!string.IsNullOrEmpty(ip) && StringUtil.IsIP(ip))
            {
                return ip;
            }
            return "";
        }
        public static string GetPageName()
        {
            string[] strArray = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[] { '/' });
            return strArray[strArray.Length - 1].ToLower();
        }
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
    }
}
