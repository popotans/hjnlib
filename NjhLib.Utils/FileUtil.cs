using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Net;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
namespace NjhLib.Utils
{
    public class FileUtil
    {
        /// <summary>
        /// 备份xml文件
        /// </summary>
        /// <param name="sourceFileName">原文件</param>
        /// <param name="destFileName">目标文件</param>
        /// <returns></returns>
        public static bool BackupFile(string sourceFileName, string destFileName)
        {
            return BackupFile(sourceFileName, destFileName, true);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="dirpath"></param>
        public static void CreateDir(string dirpath)
        {
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);

            }

        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool IsFileExists(string file)
        {
            return File.Exists(file);
        }
        /// <summary>
        /// 返回文件创建时间
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static string GetFileCreateTime(string filepath, string format)
        {
            FileInfo fi = new FileInfo(filepath);
            return StringUtil.FormatDateTime(fi.CreationTime.ToString(), format);
        }
        public static DateTime GetFileCreateTime(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            return fi.CreationTime;
        }
        public static DateTime GetFileLastModifyTime(string filepath)
        {
            FileInfo fi = new FileInfo(filepath);
            DateTime d = fi.LastWriteTime;
            return d;//.ToString();
        }
        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            bool flag;
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void DelFile(string path)
        {
            // string str = HttpContext.Current.Server.MapPath(path);
            string str = path;
            if (File.Exists(str))
            {
                File.Delete(str);
            }
        }


        /// <summary>
        /// 生成html文件
        /// </summary>
        /// <param name="htmlFilePath"></param>
        /// <param name="htmlFileContent"></param>
        public static void WriteToHtmlFile(string htmlFilePath, string htmlFileContent)
        {
            FileInfo info = new FileInfo(htmlFilePath);
            if (info.Exists)
            {
                info.Delete();
            }
            using (FileStream stream = info.OpenWrite())
            {
                StreamWriter writer = new StreamWriter(stream, Encoding.GetEncoding("utf-8"));
                writer.BaseStream.Seek(0L, SeekOrigin.End);
                writer.Write(htmlFileContent);
                writer.Flush();
                writer.Close();
            }
        }
        /// <summary>
        /// 还原备份文件
        /// </summary>
        /// <param name="backupFileName"></param>
        /// <param name="targetFileName"></param>
        /// <param name="backupTargetFileName"></param>
        /// <returns></returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName)
        {
            try
            {
                if (!File.Exists(backupFileName))
                {
                    throw new FileNotFoundException(backupFileName + "文件不存在！");
                }
                if (backupTargetFileName != null)
                {
                    if (!File.Exists(targetFileName))
                    {
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    }
                    File.Copy(targetFileName, backupTargetFileName, true);
                }
                File.Delete(targetFileName);
                File.Copy(backupFileName, targetFileName);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return true;
        }
        /// <summary>
        /// 向客户端输出文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="filename"></param>
        /// <param name="filetype"></param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream stream = null;
            byte[] buffer = new byte[0x2710];
            try
            {
                stream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                long length = stream.Length;
                HttpContext.Current.Response.ContentType = filetype;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlEncode(filename.Trim()).Replace("+", " "));
                while (length > 0L)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int count = stream.Read(buffer, 0, 0x2710);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
                        HttpContext.Current.Response.Flush();
                        buffer = new byte[0x2710];
                        length -= count;
                    }
                    else
                    {
                        length = -1L;
                    }
                }
            }
            catch (Exception exception)
            {
                HttpContext.Current.Response.Write("Error : " + exception.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }
        #region 获取远程或本地文件内容
        /// <summary>
        /// 读取本地文本文件内容
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetHtmlStringByFilePath(string filePath)
        {
            StringBuilder builder = new StringBuilder();
            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                builder.Append("");
            }
            StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding("utf-8"));
            builder.Append(reader.ReadToEnd());
            reader.Close();
            return builder.ToString();
        }
        public static string GetHtmlStringByFilePath(string filePath, string encodingname)
        {
            StringBuilder builder = new StringBuilder();
            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                builder.Append("");
            }
            StreamReader reader = new StreamReader(filePath, Encoding.GetEncoding(encodingname));
            builder.Append(reader.ReadToEnd());
            reader.Close();
            return builder.ToString();
        }
        /// <summary>
        /// 远程抓取网页内容
        /// </summary>
        /// <param name="httppath"></param>
        /// <returns></returns>
        public static string GetWebHtmlStr(string httppath)
        {
            string s = string.Empty;
            WebClient wc = new WebClient();
            StreamReader r = null;
            Stream sr = null;
            try
            {
                sr = wc.OpenRead(httppath);
                r = new StreamReader(sr, Encoding.Default);
                s = r.ReadToEnd();
            }
            catch { }
            finally
            {
                r.Close();
                sr.Close();
            }

            return s;
        }
        /// <summary>
        /// 获取远程网页内容，指定编码
        /// </summary>
        /// <param name="httppath"></param>
        /// <param name="encodingname"></param>
        /// <returns></returns>
        public static string GetWebHtmlStr3(string httppath, string encodingname)
        {
            if (string.IsNullOrEmpty(encodingname)) encodingname = "gb2312";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httppath);
            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 60 * 1000;
            request.Headers.Set("pragma", "no-cache");
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.0; SLCC1; .NET CLR 2.0.50727; Media Center PC 5.0; .NET CLR 3.0.04506)";
            string s = string.Empty;
            try
            {
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    using (StreamReader r = new StreamReader(stream, System.Text.Encoding.GetEncoding(encodingname)))
                    {
                        s = r.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return s;
        }

        /// <summary>
        /// 远程抓取网页内容2，默认utf8编码
        /// </summary>
        /// <param name="httppath"></param>
        /// <returns></returns>
        public static string GetWebHtmlStr2(string httppath)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httppath);
            //HttpContext.Current.Response.Write(httppath);
            //HttpContext.Current.Response.End();
            string s = string.Empty;
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader r = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    s = r.ReadToEnd();
                }
            }
            return s;
        }
        /// <summary>
        /// 获取远程网页内容，指定编码
        /// </summary>
        /// <param name="httppath"></param>
        /// <param name="encodingname"></param>
        /// <returns></returns>
        public static string GetWebHtmlStr2(string httppath, string encodingname)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httppath);
            string s = string.Empty;
            using (Stream stream = request.GetResponse().GetResponseStream())
            {
                using (StreamReader r = new StreamReader(stream, System.Text.Encoding.GetEncoding(encodingname)))
                {
                    s = r.ReadToEnd();
                }
            }
            return s;
        }

        /// <summary>
        /// 指定同步或者异步方式，抓取远程内容，默认编码utf8
        /// </summary>
        /// <param name="httppath">请求的文件路径</param>
        /// <param name="IsAsync">false同步； true 异步</param>
        /// <returns></returns>
        public static string GetWebHtmlStr2(string httppath, bool IsAsync)
        {
            if (!IsAsync) return GetWebHtmlStr2(httppath);
            string s = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httppath);
            request.BeginGetResponse(delegate(IAsyncResult ar)
            {
                WebResponse response = request.EndGetResponse(ar);
                using (Stream sm = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(sm, Encoding.UTF8))
                    {
                        s = sr.ReadToEnd();
                    }
                }
            }, null);
            return s;
        }
        /// <summary>
        /// 指定同步或者异步方式，抓取远程内容，指定编码
        /// </summary>
        /// <param name="httppath">请求的文件路径</param>
        /// <param name="IsAsync">false同步； true 异步</param>
        /// <returns></returns>
        public static string GetWebHtmlStr2(string httppath, string encodingname, bool IsAsync)
        {
            if (!IsAsync) return GetWebHtmlStr2(httppath, encodingname);
            string s = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(httppath);
            request.BeginGetResponse(delegate(IAsyncResult ar)
            {
                WebResponse response = request.EndGetResponse(ar);
                using (Stream sm = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(sm, Encoding.GetEncoding(encodingname)))
                    {
                        s = sr.ReadToEnd();
                    }
                }
            }, null);
            return s;
        }



        /// <summary>
        /// 根据路径读取远程或本地文件路径
        /// </summary>
        /// <param name="filepath">路径地址，可以本地路径，也可以是远程服务器上的路径</param>
        /// <returns></returns>
        public static string GetHtmlString(string filepath)
        {
            if (filepath.StartsWith("http:") || filepath.StartsWith("https:") || filepath.StartsWith("ftp:"))
                return GetWebHtmlStr(filepath);
            else
                return GetHtmlStringByFilePath(filepath);
        }
        public static string GetHtmlString(string filepath, string encodingname)
        {
            if (filepath.StartsWith("http:") || filepath.StartsWith("https:") || filepath.StartsWith("ftp:"))
                return GetWebHtmlStr2(filepath, encodingname);
            else
                return GetHtmlStringByFilePath(filepath, encodingname);
        }
        public static string GetHtmlString(string filepath, bool IsAsync)
        {
            if (filepath.StartsWith("http:") || filepath.StartsWith("https:") || filepath.StartsWith("ftp:"))
                if (!IsAsync)
                    return GetWebHtmlStr2(filepath);
                else
                    return GetWebHtmlStr2(filepath, IsAsync);
            else
                return GetHtmlStringByFilePath(filepath);
        }

        public static string GetHtmlString(string filepath, string encodingname, bool isAsync)
        {
            if (filepath.StartsWith("http:") || filepath.StartsWith("https:") || filepath.StartsWith("ftp:"))
                if (!isAsync)
                    return GetWebHtmlStr2(filepath, encodingname);
                else
                    return GetWebHtmlStr2(filepath, encodingname, isAsync);
            else
                return GetHtmlStringByFilePath(filepath, encodingname);
        }
        #endregion

        #region 生成静态页文件
        /// <summary>
        /// 读取动态页面，生成静态页文件.默认utf8编码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="savename"></param>
        /// <returns></returns>
        public static bool CreateStaticHtml(string url, string savePathAndName)
        {
            if (IsFileExists(savePathAndName)) DelFile(savePathAndName);
            bool f = true;
            StringBuilder sb = new StringBuilder();
            sb.Append(GetWebHtmlStr2(url));
            try
            {
                using (StreamWriter sw = new StreamWriter(savePathAndName, false, System.Text.Encoding.UTF8))
                {
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            }
            catch
            {
                f = false;
            }
            finally
            {

            }
            return f;
        }
        /// <summary>
        /// 生成静态文件，指定编码
        /// </summary>
        /// <param name="url">访问远程地址</param>
        /// <param name="savename">保存的文件完整路径和文件名</param>
        /// <param name="encodingname">编码名称</param>
        /// <returns></returns>
        public static bool CreateStaticHtml(string url, string savename, string encodingname)
        {
            if (IsFileExists(savename)) DelFile(savename);
            bool f = true;
            StringBuilder sb = new StringBuilder();
            sb.Append(GetWebHtmlStr2(url, encodingname));
            try
            {
                using (StreamWriter sw = new StreamWriter(savename, false, System.Text.Encoding.GetEncoding(encodingname)))
                {
                    sw.Write(sb.ToString());
                    sw.Close();
                }
            }
            catch
            {
                f = false;
            }
            finally
            {

            }
            return f;
        }
        #endregion



        #region 从服务器下载文件 serverfilename为服务器端文件的完整路径
        public static void DownloadFileByTransmit(string serverfilename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            FileInfo f = new FileInfo(serverfilename);
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlPathEncode(f.Name));
            HttpContext.Current.Response.TransmitFile(serverfilename);
            HttpContext.Current.Response.End();
        }
        public static void DownloadFileByStream(string serverfilename)
        {
            string fileName = serverfilename;
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(fileName);
            if (fileInfo.Exists)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100K，这样可以缓解服务器的压力 51.      
                byte[] buffer = new byte[ChunkSize];
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ClearContent();
                System.IO.FileStream iStream = System.IO.File.OpenRead(fileName);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小 56.          
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileInfo.Name));
                while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小 61.            
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    HttpContext.Current.Response.Flush();
                    dataLengthToRead -= lengthRead;
                }
                HttpContext.Current.Response.Close();
            }
        }
        public static void DownloadFileByWriteFile(string serverfilename)
        {
            string fileName = serverfilename;//客户端保存的文件名 24     
            FileInfo fileInfo = new FileInfo(fileName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpContext.Current.Server.UrlPathEncode(fileInfo.Name));
            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        #endregion

        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strContent"></param>
        public static void AppendStr(string path, string strContent)
        {
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(strContent);
            sw.Flush();
            sw.Close();
        }
        /// <summary>
        /// 递归删除文件夹
        /// </summary>
        /// <param name="folderPath"></param>
        public static void DelDir(string folderPath)
        {
            string dir = folderPath;
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件 
                    else
                        DelDir(d); //递归删除子文件夹 
                }
                Directory.Delete(dir); //删除已空文件夹 

            }
            else

                throw new Exception(dir + " 该文件夹不存在");
        }

        /// <summary>
        /// 实现一个静态方法将指定文件夹下面的所有内容copy到目标文件夹下面
        // 如果目标文件夹为只读属性就会报错。
        // April 18April2005 In STU
        /// </summary>
        /// <param name="origialPath"></param>
        /// <param name="copyToPath"></param>
        public static void CopyDir(string origialPath, string copyToPath)
        {
            string srcPath = origialPath, aimPath = copyToPath;
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath)) Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                // 如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                // string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                // 遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    // 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    // 否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static System.IO.Stream Read(string physicalPath)
        {
            return System.IO.File.OpenRead(physicalPath);
        }
        public static void CopyFile(string physicalSourcePath, string physicalDestPath, bool overwrite)
        {
            System.IO.File.Copy(physicalSourcePath, physicalDestPath, overwrite);
        }

        public static void MoveFile(string physicalSourcePath, string physicalDestPath, bool overwrite)
        {
            if (overwrite)
            {
                if (System.IO.File.Exists(physicalDestPath) && physicalSourcePath.ToLower() != physicalDestPath.ToLower())
                {
                    System.IO.File.Delete(physicalDestPath);
                }
            }
            System.IO.File.Move(physicalSourcePath, physicalDestPath);
        }
        public static byte[] ReadBytes(string physicalPath)
        {
            // HaiLi.IOUtility.IdentifyEncoding identifyEncoding = new HaiLi.IOUtility.IdentifyEncoding();
            return System.IO.File.ReadAllBytes(physicalPath);
        }
        public string DataTableToExcel(DataTable dt, string excelPath)
        {
            if (dt == null)
            {
                return "DataTable不能为空";
            }
            dt.TableName = "Sheet1";
            int count = dt.Rows.Count;
            int num2 = dt.Columns.Count;
            if (count == 0)
            {
                return "没有数据";
            }
            StringBuilder builder = new StringBuilder();
            string connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelPath);
            builder.Append("CREATE TABLE ");
            builder.Append(dt.TableName + " ( ");
            for (int i = 0; i < num2; i++)
            {
                if (i < (num2 - 1))
                {
                    builder.Append(string.Format("{0} nvarchar,", dt.Columns[i].ColumnName));
                }
                else
                {
                    builder.Append(string.Format("{0} nvarchar)", dt.Columns[i].ColumnName));
                }
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = builder.ToString();
                OleDbCommand command = command2;
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                    return ("在Excel中创建表失败，错误信息：" + exception.Message);
                }
                builder.Remove(0, builder.Length);
                builder.Append("INSERT INTO ");
                builder.Append(dt.TableName + " ( ");
                for (int j = 0; j < num2; j++)
                {
                    if (j < (num2 - 1))
                    {
                        builder.Append(dt.Columns[j].ColumnName + ",");
                    }
                    else
                    {
                        builder.Append(dt.Columns[j].ColumnName + ") values (");
                    }
                }
                for (int k = 0; k < num2; k++)
                {
                    if (k < (num2 - 1))
                    {
                        builder.Append("@" + dt.Columns[k].ColumnName + ",");
                    }
                    else
                    {
                        builder.Append("@" + dt.Columns[k].ColumnName + ")");
                    }
                }
                command.CommandText = builder.ToString();
                OleDbParameterCollection parameters = command.Parameters;
                for (int m = 0; m < num2; m++)
                {
                    parameters.Add(new OleDbParameter("@" + dt.Columns[m].ColumnName, OleDbType.VarChar));
                }
                foreach (DataRow row in dt.Rows)
                {
                    for (int n = 0; n < parameters.Count; n++)
                    {
                        parameters[n].Value = row[n];
                    }
                    command.ExecuteNonQuery();
                }
                return "数据已成功导入Excel";
            }
        }

        public string ExecCmd(string strCmd)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            string message = "";
            try
            {
                process.Start();
                process.StandardInput.WriteLine(strCmd);
                process.StandardInput.WriteLine("exit");
                message = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception exception)
            {
                message = exception.Message;
            }
            return message;
        }
        // Properties
        public static string AvailableRAM
        {
            get
            {
                try
                {
                    PerformanceCounter counter = new PerformanceCounter();
                    counter.CategoryName = "Memory";
                    counter.CounterName = "Available MBytes";
                    return (counter.NextValue().ToString() + "MB 可用");
                }
                catch (Exception exception)
                {
                    return ("程序错误,可能是没有权限;" + exception.Message);
                }
            }
        }

        public static string CPUNumber
        {
            get
            {
                try
                {
                    return Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS");
                }
                catch
                {
                    return "IOHelper错误: 获取CPU个数失败";
                }
            }
        }
        public static string CPUType
        {
            get
            {
                try
                {
                    return Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
                }
                catch
                {
                    return null;
                }
            }
        }
        public static string DotNetVersion
        {
            get
            {
                return Environment.Version.ToString();
            }
        }
        public static string DriverList
        {
            get
            {
                string[] logicalDrives = Directory.GetLogicalDrives();
                string str = "";
                for (int i = 0; i <= (Directory.GetLogicalDrives().Length - 1); i++)
                {
                    str = str + logicalDrives[i].ToString() + " ";
                }
                return str;
            }
        }

        public static string OSLanguage
        {
            get
            {
                return CultureInfo.InstalledUICulture.EnglishName;
            }
        }

        public static string OSVersion
        {
            get
            {
                try
                {
                    return Environment.OSVersion.ToString();
                }
                catch
                {
                    return null;
                }
            }
        }
        public static string TimeZone
        {
            get
            {
                TimeSpan span = (TimeSpan)(DateTime.Now - DateTime.UtcNow);
                return ((span.TotalHours > 0.0) ? ("+" + (span = (TimeSpan)(DateTime.Now - DateTime.UtcNow)).TotalHours.ToString()) : (span = (TimeSpan)(DateTime.Now - DateTime.UtcNow)).TotalHours.ToString());
            }
        }




    }
}
