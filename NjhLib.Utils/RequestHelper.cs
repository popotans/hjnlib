using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    public class RequestHelper
    {
        public static T Get<T>(string paramName)
        {
            string value = System.Web.HttpContext.Current.Request[paramName];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }
        public static T GetQuery<T>(string paramName)
        {
            string value = System.Web.HttpContext.Current.Request.QueryString[paramName];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }

        public static T GetForm<T>(string paramName)
        {
            string value = System.Web.HttpContext.Current.Request.Form[paramName];
            Type type = typeof(T);
            object result;
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch
            {
                result = default(T);
            }
            return (T)result;
        }
    }
}
