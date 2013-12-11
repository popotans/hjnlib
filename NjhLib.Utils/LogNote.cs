using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Globalization;

namespace NjhLib.Utils
{
    public class LogNote
    {
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void WriteError(string errorMessage)
        {
            try
            {
                string filepath = "~/Error/";
                string path2 = HttpContext.Current.Server.MapPath(filepath);
                if (!Directory.Exists(path2))
                {
                    Directory.CreateDirectory(path2);
                }
                string path = filepath + DateTime.Today.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                }
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("rnLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() +
                           ". Error Message:" + errorMessage;
                    w.WriteLine(err);
                    w.WriteLine("__________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
            }
        }

        private static bool Is404Error(Exception ex)
        {
            bool f = false;
            if (ex.GetType().Name.Equals("HttpException"))
            {
                HttpException hex = (HttpException)ex;
                if (hex.GetHttpCode() == 404)
                {
                    f = true;
                }
            }
            return f;
        }

        public static void LogErr(string folder)
        {
            // 在出现未处理的错误时运行的代码
            Exception objErr = HttpContext.Current.Server.GetLastError().GetBaseException();
            if (!Is404Error(objErr))
            {
                string error = string.Empty;
                string errortime = string.Empty;
                string erroraddr = string.Empty;
                string errorinfo = string.Empty;
                string errorsource = string.Empty;
                string errortrace = string.Empty;


                error += "发生时间:" + System.DateTime.Now.ToString() + "<br>";
                errortime = "发生时间:" + System.DateTime.Now.ToString();

                error += "发生异常页: " + HttpContext.Current.Request.Url.ToString() + "<br>";
                erroraddr = "发生异常页: " + HttpContext.Current.Request.Url.ToString();

                error += "异常信息: " + objErr.Message + "<br>";
                errorinfo = "异常信息: " + objErr.Message;
                //error +="错误源:"+objErr.Source+"<br>";
                //error += "堆栈信息:" + objErr.StackTrace + "<br>";
                errorsource = "错误源:" + objErr.Source;
                errortrace = "堆栈信息:" + objErr.StackTrace;
                error += "--------------------------------------<br>";
                HttpContext.Current.Server.ClearError();
                HttpContext.Current.Application["error"] = error;
                //独占方式，因为文件只能由一个进程写入.
                System.IO.StreamWriter writer = null;
                try
                {
                    object obj = new object();
                    lock (obj)
                    {
                        // 写入日志
                        string year = DateTime.Now.Year.ToString();
                        string month = DateTime.Now.Month.ToString();
                        string path = string.Empty;

                        string filename = DateTime.Now.Day.ToString() + ".txt";
                        path = HttpContext.Current.Server.MapPath("" + folder + "/") + year + "/" + month;
                        //如果目录不存在则创建
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }
                        System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);
                        //if (!file.Exists)
                        //    file.Create();
                        //file.Open(System.IO.FileMode.Append);        
                        writer = new System.IO.StreamWriter(file.FullName, true);//文件不存在就创建,true表示追加
                        writer.WriteLine("用户IP:" + HttpContext.Current.Request.UserHostAddress);
                        // if (Session["Identity"] != null)
                        // {
                        //     writer.WriteLine("登录帐号:" System.Web.HttpContext.Current.Session["Identity"]).YongHuInfo.ACCOUNTID);
                        // }
                        writer.WriteLine(errortime);
                        writer.WriteLine(erroraddr);
                        writer.WriteLine(errorinfo);
                        writer.WriteLine(errorsource);
                        writer.WriteLine(errortrace);
                        writer.WriteLine("--------------------------------------------------------------------------------------");
                    }
                }
                finally
                {
                    if (writer != null)
                        writer.Close();
                }

            }
        }
    }
}
