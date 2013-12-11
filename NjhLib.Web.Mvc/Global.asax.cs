using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NjhLib.Web.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
           // AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            //sql2005 sqlcache dependency
          //  System.Data.SqlClient.SqlDependency.Start(System.Configuration.ConfigurationManager.ConnectionStrings["ibatisdb"].ConnectionString);
        }

        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();
           // Server.ClearError();//log4net 注释掉本行数据
            //bool Is404Err = (ex is HttpException) && (ex as HttpException).GetHttpCode() == 404;
            //if (!Is404Err)
            //{
            //    string ip = HttpContext.Current.Request.UserHostAddress;
            //    string url = HttpContext.Current.Request.Url.ToString();
            //    NjhLib.Utils.LogHelper.Logger.Error(string.Format("IP:{0}\r\nUrl:{1}\r\n", ip, url), ex);
            //}
        }
    }
}