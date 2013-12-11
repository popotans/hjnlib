using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
namespace NjhLib.Utils
{
    public class ZipLib
    {
        //    public static void CreateZipfromFiles(IList<string> filespathtoziped, string zipfilepath)
        //    {
        //        using (ZipFile zf = ZipFile.Create(zipfilepath))
        //        {
        //            zf.BeginUpdate();
        //            foreach (string item in filespathtoziped)
        //            {
        //                zf.Add(item);
        //            }
        //            zf.CommitUpdate();
        //        }
        //    }

        //    public static void CreateZipfromDir(string dirpath, string zipfilepath)
        //    {
        //        FastZip fz = new FastZip();
        //        fz.CreateZip(zipfilepath, dirpath, true, "");
        //        //最后一个参数是使用正则表达式表示的过滤文件规则。CreateZip方法有3个重载版本，其中有目录过滤参数、文件过滤参数及用于指定是否进行子目录递归的一个bool类型的参数。

        //    }

        //    public static void AppendZipfromFile(string appendedfile, string zipfilepath)
        //    {
        //        using (ZipFile zf = new ZipFile(zipfilepath))
        //        {
        //            zf.BeginUpdate();
        //            zf.Add(appendedfile);
        //            zf.CommitUpdate();
        //        }
        //    }

        //    public IList<string> ShowFilesInZip(string zipfilepath)
        //    {
        //        IList<string> list = new List<string>();
        //        using (ZipFile zip = new ZipFile(zipfilepath))
        //        {
        //            foreach (ZipEntry entry in zip)
        //            {
        //                list.Add(entry.Name);
        //            }
        //        }
        //        return list;
        //    }

        //    public static void ExtractZipToDir(string topath, string zipfilepath)
        //    {
        //        (new FastZip()).ExtractZip(zipfilepath, topath, "");
        //    }
    }
}
