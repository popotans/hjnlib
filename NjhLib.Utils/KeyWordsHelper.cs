using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NjhLib.Utils
{
    public class KeyWordsHelper
    {
        static RegexOptions RegexOptions = ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
        public static string AddKwdsLink(Dictionary<string, string> kwdDic, string source)
        {
            List<string> keys = new List<string>();

            foreach (KeyValuePair<string, string> item in kwdDic)
            {
                if (!keys.Contains(item.Key))
                {
                    keys.Add(item.Key);
                }
            }
            keys = keys.OrderBy(x => x.Length).ToList();

            Regex reg = null;
            foreach (string s in keys)
            {
                string to = "<a href='" + kwdDic[s] + "'>" + s + "</a>";
                //source = Regex.Replace(source, s, "<a href='" + kwdDic[s] + "'>" + s + "</a>");
                //   source = Regex.Replace(source, "" + s + "(?!</a>)", to);
                reg = new Regex(s + "(?!</a>)", RegexOptions);
                source = reg.Replace(source, to, 1);
            }


            return source;
        }

        public static string RemoveALink(string source)
        {
            string pattern = "(?s)(?i)<a.*?>(?<title>.*?)</a>";
            MatchCollection mc = Regex.Matches(source, pattern, RegexOptions);
            foreach (Match m in mc)
            {
                string title = m.Groups["title"].Value;
                source = Regex.Replace(source, pattern, title);
            }

            // source=Regex.Replace(source,pattern,
            return source;
        }

    }


}
