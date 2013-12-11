using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NjhLib.Web.Mvc.usingHttpHandler
{
    public class UrlHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            
        }
    }
}