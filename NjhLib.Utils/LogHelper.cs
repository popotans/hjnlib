using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NjhLib.Utils
{
    public static class LogHelper
    {
        public static log4net.ILog Logger = null;
        static LogHelper()
        {
            if (Logger == null)
            {
                object o = ConfigurationManager.GetSection("log4net");
                log4net.Config.XmlConfigurator.Configure(o as System.Xml.XmlElement);
                Logger = log4net.LogManager.GetLogger("AppLogger");
            }
        }
        /*
         使用log4net记录日志的步骤：
         * 1. 编写此Helper,并在Assembly.cs 中指定log4net的配置文件：
         * 2.在使用的程序中写入：log4net.config 文件；
         * 3. 在程序的Application_Error中调用log4net;
         */
    }
}
