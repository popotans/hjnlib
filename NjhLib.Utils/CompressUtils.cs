using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
namespace NjhLib.Utils
{
    public class CompressUtils
    {
        /// <summary>
        /// 使用7za在线压缩
        /// </summary>
        /// <param name="exe">7za.exe文件路径，本项目位于refers\exe\7za.exe路径下</param>
        /// <param name="target">目标文件地址</param>
        /// <param name="source">原文件地址</param>
        private void Execute7za(string exe, string target, string source)
        {
            DeleteFile(target);
            System.Threading.Thread.Sleep(10000);
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = exe;
            start.Arguments = string.Format(" a \"{0}\" {1}", target, source);
            start.CreateNoWindow = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardInput = true;
            start.UseShellExecute = false;
            Process p = Process.Start(start);

            StreamReader reader = p.StandardOutput;
            try
            {
                string line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                }
                p.WaitForExit();

            }
            catch
            {
                p.Kill();
            }
            finally
            {
                p.Close();
                reader.Close();
            }
        }
        private void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                FileInfo fi = new FileInfo(fileName);
                if (fi.IsReadOnly)
                    fi.Attributes = FileAttributes.Normal;
                File.Delete(fileName);
            }
        }
    }
}
