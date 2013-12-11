using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Reflection;

namespace NjhLib.Utils
{
    //二级域名session共享
    public class CrossDomain : System.Web.IHttpModule
    {
        private string domain = System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString();
        private string DefaultDomain = System.Configuration.ConfigurationManager.AppSettings["DefaultDomain"].ToString();
        #region IHttpModule 成员

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Init(System.Web.HttpApplication context)
        {
            if (string.IsNullOrEmpty(this.domain)) this.domain = this.DefaultDomain;

            Type stateServerSessionProvider = typeof(HttpSessionState).Assembly.GetType("System.Web.SessionState.OutOfProcSessionStateStore");
            FieldInfo uriField = stateServerSessionProvider.GetField("s_uribase", BindingFlags.Static | BindingFlags.NonPublic);
            if (uriField == null)
                throw new ArgumentException("UriField was not found");

            uriField.SetValue(null, this.domain);
            context.EndRequest += new EventHandler(context_EndRequest);
            //China9986.Web.Util.CrossDomain 
        }

        void context_EndRequest(object sender, EventArgs e)
        {
            System.Web.HttpApplication app = sender as System.Web.HttpApplication;

            for (int i = 0; i < app.Context.Response.Cookies.Count; i++)
            {
                if (app.Context.Response.Cookies[i].Name == "ASP.NET_SessionId")
                {
                    app.Context.Response.Cookies[i].Domain = this.domain;
                }
            }
        }

        #endregion
    }
}
