using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace NjhLib.Utils
{
    public class StringUtil
    {

        public static string Paging(int pageIndex, int pageSize, int total, string link, Dictionary<string, string> parameters, bool compactMode)
        {
            int left = total % pageSize;
            int totalPages = total / pageSize;
            if (left > 0)
            {
                totalPages += 1;
            }
            if (totalPages == 0)
            {
                totalPages = 1;
            }

            StringBuilder sb = new StringBuilder("<ul style='float:right;' class='pagination'>");
            sb.Append(string.Format("<li class='totalRecords'>&nbsp;共{0}条记录&nbsp;</li>", total));
            sb.Append(string.Format("<li class='totalPages'>&nbsp;第{0}/{1}页&nbsp;</li>", pageIndex + 1, totalPages));
            if (totalPages == 1)
            {
                sb.Append(string.Format("<li class='currentPage'>&nbsp;{0}&nbsp;</li>", totalPages));
            }
            else
            {
                sb.Append(string.Format("<li class='firstPage'>&nbsp;<a href='{0}'>第一页</a>&nbsp;</li>", GetFinalLink(link, 0, pageSize, parameters, compactMode)));

                if (pageIndex > 0)
                {
                    sb.Append(string.Format("<li class='lastPage'>&nbsp;<a href='{0}'>上一页</a>&nbsp;</li>", GetFinalLink(link, pageIndex - 1, pageSize, parameters, compactMode)));
                }
                int startPage = pageIndex - 4;
                if (startPage < 1)
                {
                    startPage = 1;
                }
                int endPage = startPage + 9;
                if (endPage > totalPages)
                {
                    endPage = totalPages;
                }
                for (int i = startPage; i <= endPage; i++)
                {
                    if (i == pageIndex + 1)
                    {
                        sb.Append(string.Format("<li class='currentPage'>&nbsp;{0}&nbsp;</a></li>", i));
                    }
                    else
                    {
                        sb.Append(string.Format("<li class='indexPage'>&nbsp;<a href='{0}'>[{1}]</a>&nbsp;</li>", GetFinalLink(link, i - 1, pageSize, parameters, compactMode), i));
                    }
                }
                if (pageIndex < totalPages - 1)
                {
                    sb.Append(string.Format("<li class='nextPage'>&nbsp;<a href='{0}'>下一页</a>&nbsp;</li>", GetFinalLink(link, pageIndex + 1, pageSize, parameters, compactMode)));
                    sb.Append(string.Format("<li class='nextPage'>&nbsp;<a href='{0}'>最后页</a>&nbsp;</li>", GetFinalLink(link, totalPages - 1, pageSize, parameters, compactMode)));

                }

            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        private static string GetFinalLink(string link, int pageIndex, int pageSize, Dictionary<string, string> otherParameters, bool compactMode)
        {
            string flink = link;
            if (compactMode && !flink.Contains('?'))
            {
                if (!flink.EndsWith("/"))
                {
                    flink = flink + "/"; ;
                }
                flink = flink + pageIndex + "/" + pageSize + "?";

            }
            else
            {
                if (flink.Contains('?'))
                {
                    flink = flink + "&PageIndex=" + pageIndex + "&PageSize=" + pageSize + "&";
                }
                else
                {
                    flink = flink + "?PageIndex=" + pageIndex + "&PageSize=" + pageSize + "&";
                }
            }
            if (otherParameters != null && otherParameters.Count > 0)
            {
                foreach (string key in otherParameters.Keys)
                {
                    flink = flink + key + "=" + otherParameters[key] + "&";
                }
                flink = flink.Remove(flink.Length - 1);
            }
            return flink;
        }


        //// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>       
        public static string ToSDC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDSC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 格式化日期字符串
        /// </summary>
        /// <param name="datetimestr"></param>
        /// <returns></returns>
        public static string FormatDate(string datetimestr)
        {
            DateTime dr = new DateTime();
            DateTime.TryParse(datetimestr, out dr);
            string s = dr.ToString("yyyy-MM-dd");
            if (s == "0001-01-01") { return ""; }
            else
            { return s; }
        }
        /// <summary>
        /// 指定格式的日期格式化
        /// </summary>
        /// <param name="datetimestr">时间字符串</param>
        /// <param name="format"格式化格式></param>
        /// <returns></returns>
        public static string FormatDateTime(string datetimestr, string format)
        {
            DateTime dr = new DateTime();
            DateTime.TryParse(datetimestr, out dr);
            string s = dr.ToString(format);
            return s;
        }
        #region 日期比较
        /// <summary>
        /// 比较两个日期大小,精确到天
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int DateCompare(DateTime date1, DateTime date2)
        {
            int result = 0;
            System.TimeSpan ts = date1.Subtract(date2);

            if (ts.Days > 0) result = 1;
            if (ts.Days == 0) result = 0;
            if (ts.Days < 0) result = -1;
            return result;

        }
        /// <summary>
        /// 比较两个日期大小,精确到秒
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static int DataCompareInSeconds(DateTime date1, DateTime date2)
        {
            int result = 0;
            double scs = date1.Subtract(date2).TotalMilliseconds;
            if (scs > 0) result = 1;
            if (scs == 0) result = 0;
            if (scs < 0) result = -1;
            return result;
        }

        #endregion
        /// <summary>
        /// 把字符串转换为整形
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Int32 ToInt32(string str)
        {
            Int32 i = 0;
            Int32.TryParse(str, out i);
            return i;
        }
        public static float ToFloat(string str)
        {
            float i = 0f;
            float.TryParse(str, out i);
            return i;
        }
        public static double ToDouble(string str)
        {
            double i = 0;
            double.TryParse(str, out i);
            return i;
        }
        public static decimal ToDecimal(string str)
        {
            decimal i = 0;
            decimal.TryParse(str, out i);
            return i;
        }
        public static DateTime ToDateTime(string str)
        {
            DateTime dd = new DateTime();
            DateTime.TryParse(str, out dd);
            return dd;
        }
        public static string GetFileNameByDateTime()
        {
            return (DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + GetRandomStrByNum2(4));
        }
        public static string GetRandomStrByNum2(int codeCount)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            int length = strArray.Length;
            string str2 = "";
            int num2 = -1;
            Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (num2 != -1)
                {
                    random = new Random((i * num2) * ((int)DateTime.Now.Ticks));
                }
                int index = random.Next(length);
                if (num2 == index)
                {
                    return GetRandomStrByNum(codeCount);
                }
                num2 = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }
        public static string GetRandomStrByNum(int codeCount)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9".Split(new char[] { ',' });
            int length = strArray.Length;
            string str2 = "";
            int num2 = -1;
            Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (num2 != -1)
                {
                    random = new Random((i * num2) * ((int)DateTime.Now.Ticks));
                }
                int index = random.Next(length);
                if (num2 == index)
                {
                    return GetRandomStrByNum(codeCount);
                }
                num2 = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }
        public static string GetRandomStr(int codeCount)
        {
            string[] strArray = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,m,n,l,o,p,q,r,s,t,u,v,w,x,y,z".Split(new char[] { ',' });
            int length = strArray.Length;
            string str2 = "";
            int num2 = -1;
            Random random = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (num2 != -1)
                {
                    random = new Random((i * num2) * ((int)DateTime.Now.Ticks));
                }
                int index = random.Next(length);
                if (num2 == index)
                {
                    return GetRandomStr(codeCount);
                }
                num2 = index;
                str2 = str2 + strArray[index];
            }
            return str2;
        }
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        /// <summary>
        /// 取得文件扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileEXT(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return "";
            }
            if (filename.IndexOf(@".") == -1)
            {
                return "";
            }
            int pos = -1;
            if (!(filename.IndexOf(@"\") == -1))
            {
                pos = filename.LastIndexOf(@"\");
            }
            string[] s = filename.Substring(pos + 1).Split('.');
            return s[1];
        }

        public static string ConvertStr(string inputString)
        {
            string retVal = inputString;
            retVal = retVal.Replace("&", "&amp;");
            retVal = retVal.Replace("\"", "&quot;");
            retVal = retVal.Replace("<", "&lt;");
            retVal = retVal.Replace(">", "&gt;");
            retVal = retVal.Replace(" ", "&nbsp;");
            retVal = retVal.Replace("  ", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\t", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\r", "<br>");
            return retVal;
        }

        public static string SqlEncode(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            return s.Replace("'", "''");
        }
        #region form

        public static string GetRequest(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string ss = HttpContext.Current.Request[s];
            if (string.IsNullOrEmpty(ss)) return "";
            return SqlEncode(ss);
        }
        public static string getForm(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string ss = HttpContext.Current.Request.Form[s];
            if (string.IsNullOrEmpty(ss)) return "";
            return SqlEncode(ss).Trim();
        }
        public static string GetQuery(string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            string ss = HttpContext.Current.Request.QueryString[s];
            if (string.IsNullOrEmpty(ss)) return "";
            return SqlEncode(ss);
        }

        #endregion

        /// <summary>
        /// 去除B标签<b></b>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CleanBTag(string s)
        {
            string pattern = "<b>(.*)</b>";
            s = System.Text.RegularExpressions.Regex.Replace(s, pattern, "$1", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            return s;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length *= -1;
                    if ((startIndex - length) < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex -= length;
                    }
                }
                if (startIndex > str.Length)
                {
                    return "";
                }
            }
            else if ((length >= 0) && ((length + startIndex) > 0))
            {
                length += startIndex;
                startIndex = 0;
            }
            else
            {
                return "";
            }
            if ((str.Length - startIndex) < length)
            {
                length = str.Length - startIndex;
            }
            return str.Substring(startIndex, length);
        }
        #region 获取字符串长度
        /// <summary>
        /// 说明：获取字符串长度
        /// 作者：janksen
        /// 历史：
        ///     2009-12-29：创建
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetStringLength(string str)
        {
            return Regex.Replace(str, "[\u4e00-\u9fa5]", "zz", RegexOptions.IgnoreCase).Length;
        }
        #endregion


        #region
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        public static string ReplaceHtml(string html)
        {
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");
            return StrNohtml;
        }


        /// <summary>
        /// 过滤html代码，返回纯文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ReplaceHtml2(string html)
        {
            string StrNohtml = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "");
            StrNohtml = System.Text.RegularExpressions.Regex.Replace(StrNohtml, "&[^;]+;", "");
            StrNohtml = StrNohtml.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            return StrNohtml;
        }

        /// <summary>
        /// 过滤掉UBB 代码
        /// </summary>
        /// <returns></returns>
        public static string ReplaceUBB(string UBBSTR)
        {
            string pattern = @"\[([^\]]+)\](.*?)\[\/\1\]";
            UBBSTR = System.Text.RegularExpressions.Regex.Replace(UBBSTR, pattern, "", RegexOptions.IgnoreCase);
            pattern = @"\[[^\]]+\]";
            UBBSTR = System.Text.RegularExpressions.Regex.Replace(UBBSTR, pattern, "", RegexOptions.IgnoreCase);
            return UBBSTR;
        }

        #region 过滤掉内容中的html代码
        /// <summary>
        /// 过滤掉内容总很难过的html代码
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static String StripHTML(string strHtml)
        {
            string[] aryReg ={
@"<script[^>]*?>.*?</script>",

@"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
@"([\r\n])[\s]+",
@"&(quot|#34);",
@"&(amp|#38);",
@"&(lt|#60);",
@"&(gt|#62);",
@"&(nbsp|#160);",
@"&(iexcl|#161);",
@"&(cent|#162);",
@"&(pound|#163);",
@"&(copy|#169);",
@"&#(\d+);",
@"-->",
@"<!--.*\n"
};

            string[] aryRep = {"","","","\"","&","<",">"," ",
                                  "\xa1",//chr(161),
                                  "\xa2",//chr(162),
                                "\xa3",//chr(163),
                                "\xa9",//chr(169),
                                "",
                                "\r\n",
                                ""
};

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(aryReg[i], System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            strOutput = ReplaceHtml(strOutput);
            return strOutput;
        }
        #endregion
        #endregion
        #region jiami jiemi

        /// <summary>
        /// MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string str2 = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str2 = str2 + bytes[i].ToString("x").PadLeft(2, '0');
            }
            return str2;
        }
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="ConvertString"></param>
        /// <returns></returns>
        public static string MD516(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            t2 = t2.ToLower();
            return t2;
        }

        /// <summary>
        /// 把字符串转换为base64编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Base64Encrypt(string s)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));
        }
        public static string Base64Decrypt(string s)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }

        /// <summary>
        /// SHA1加密，不可逆转
        /// </summary>
        /// <param name="str">string str:被加密的字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string SHA1(string str)
        {
            System.Security.Cryptography.SHA1 s1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] byte1;
            byte1 = s1.ComputeHash(Encoding.Default.GetBytes(str));
            s1.Clear();
            return Convert.ToBase64String(byte1);
        }

        #region 进制转换

        /// <summary>
        /// 把十进制转换为toBase指定的进制数
        /// </summary>
        /// <param name="tenvalue"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static string ConvertFromTen(Int64 tenvalue, int toBase)
        {
            string rs = string.Empty;
            Int64 result = tenvalue;
            while (result > 0)
            {
                Int64 val = result % toBase;
                rs = Convert1((int)val).ToString() + rs;
                result = (Int64)(result / toBase);
            }
            return rs;
        }

        /// <summary>
        /// 把toBase表示的进制数转换为十进制
        /// </summary>
        /// <param name="str"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static Int64 ConvertToTen(string str, int toBase)
        {
            int length = str.Length;
            Int64 result = 0;
            for (int i = 0; i < length; i++)
            {
                Int64 val = (Int64)Math.Pow(toBase, (length - i - 1));
                char c = str[i];
                Int64 tmp = Convert1(c);
                result += tmp * val;
            }
            return result;
        }

        /// <summary>
        /// 10进制转换为62进制
        /// </summary>
        /// <param name="tenvalue"></param>
        /// <returns></returns>
        public static string ConvertTenToSixtyTwo(Int64 tenvalue)
        {
            string rs = string.Empty;
            Int64 result = tenvalue;
            while (result > 0)
            {
                Int64 val = result % 62;
                rs = Convert1((int)val).ToString() + rs;
                result = (Int64)(result / 62);
            }
            return rs;
        }
        /// <summary>
        /// 62进制转换为10进制
        /// </summary>
        /// <param name="sixtytwo"></param>
        /// <returns></returns>
        public static Int64 ConvertSixtytwoToTen(string sixtytwo)
        {
            int length = sixtytwo.Length;
            Int64 result = 0;
            for (int i = 0; i < length; i++)
            {
                Int64 val = (Int64)Math.Pow(62, (length - i - 1));
                char c = sixtytwo[i];
                Int64 tmp = Convert1(c);
                result += tmp * val;
            }
            return result;
        }
        private static int Convert1(char c)
        {
            switch (c)
            {
                case '0':
                    return 0;
                case '1':
                    return 1;
                case '2':
                    return 2;
                case '3':
                    return 3;
                case '4':
                    return 4;
                case '5':
                    return 5;
                case '6':
                    return 6;
                case '7':
                    return 7;
                case '8':
                    return 8;
                case '9':
                    return 9;

                case 'a':
                    return 10;
                case 'b':
                    return 11;
                case 'c':
                    return 12;
                case 'd':
                    return 13;
                case 'e':
                    return 14;
                case 'f':
                    return 15;
                case 'g':
                    return 16;
                case 'h':
                    return 17;
                case 'i':
                    return 18;
                case 'j':
                    return 19;
                case 'k':
                    return 20;
                case 'l':
                    return 21;
                case 'm':
                    return 22;
                case 'n':
                    return 23;
                case 'o':
                    return 24;
                case 'p':
                    return 25;
                case 'q':
                    return 26;
                case 'r':
                    return 27;
                case 's':
                    return 28;
                case 't':
                    return 29;
                case 'u':
                    return 30;
                case 'v':
                    return 31;
                case 'w':
                    return 32;
                case 'x':
                    return 33;
                case 'y':
                    return 34;
                case 'z':
                    return 35;

                case 'A':
                    return 36;
                case 'B':
                    return 37;
                case 'C':
                    return 38;
                case 'D':
                    return 39;
                case 'E':
                    return 40;
                case 'F':
                    return 41;
                case 'G':
                    return 42;
                case 'H':
                    return 43;
                case 'I':
                    return 44;
                case 'J':
                    return 45;
                case 'K':
                    return 46;
                case 'L':
                    return 47;
                case 'M':
                    return 48;
                case 'N':
                    return 49;
                case 'O':
                    return 50;
                case 'P':
                    return 51;
                case 'Q':
                    return 52;
                case 'R':
                    return 53;
                case 'S':
                    return 54;
                case 'T':
                    return 55;
                case 'U':
                    return 56;
                case 'V':
                    return 57;
                case 'W':
                    return 58;
                case 'X':
                    return 59;
                case 'Y':
                    return 60;
                case 'Z':
                    return 61;
                default:
                    return 0;
            }


        }

        private static char Convert1(int val)
        {
            switch (val)
            {
                case 0:
                    return '0';
                case 1:
                    return '1';
                case 2:
                    return '2';
                case 3:
                    return '3';
                case 4:
                    return '4';
                case 5:
                    return '5';
                case 6:
                    return '6';
                case 7:
                    return '7';
                case 8:
                    return '8';
                case 9:
                    return '9';

                case 10:
                    return 'a';
                case 11:
                    return 'b';
                case 12:
                    return 'c';
                case 13:
                    return 'd';
                case 14:
                    return 'e';
                case 15:
                    return 'f';
                case 16:
                    return 'g';
                case 17:
                    return 'h';
                case 18:
                    return 'i';
                case 19:
                    return 'j';
                case 20:
                    return 'k';
                case 21:
                    return 'l';
                case 22:
                    return 'm';
                case 23:
                    return 'n';
                case 24:
                    return 'o';
                case 25:
                    return 'p';
                case 26:
                    return 'q';
                case 27:
                    return 'r';
                case 28:
                    return 's';
                case 29:
                    return 't';
                case 30:
                    return 'u';
                case 31:
                    return 'v';
                case 32:
                    return 'w';
                case 33:
                    return 'x';
                case 34:
                    return 'y';
                case 35:
                    return 'z';

                case 36:
                    return 'A';
                case 37:
                    return 'B';
                case 38:
                    return 'C';
                case 39:
                    return 'D';
                case 40:
                    return 'E';
                case 41:
                    return 'F';
                case 42:
                    return 'G';
                case 43:
                    return 'H';
                case 44:
                    return 'I';
                case 45:
                    return 'J';
                case 46:
                    return 'K';
                case 47:
                    return 'L';
                case 48:
                    return 'M';
                case 49:
                    return 'N';
                case 50:
                    return 'O';
                case 51:
                    return 'P';
                case 52:
                    return 'Q';
                case 53:
                    return 'R';
                case 54:
                    return 'S';
                case 55:
                    return 'T';
                case 56:
                    return 'U';
                case 57:
                    return 'V';
                case 58:
                    return 'W';
                case 59:
                    return 'X';
                case 60:
                    return 'Y';
                case 61:
                    return 'Z';
            }
            return '0';
        }
        #endregion




        #region aes加密 解密
        /// <summary>
        ///256位 aes加密方法
        /// </summary>
        /// <param name="toEncrypt">要加密的字符串</param>
        /// <param name="key">32 密钥</param>
        /// <returns></returns>
        public static string AESEncrypt(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 256位 aes算法解密
        /// </summary>
        /// <param name="toDecrypt">要解密的字符串</param>
        /// <param name="key">密钥 32</param>
        /// <returns></returns>
        public static string AESDecrypt(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        private static string checkKeyLength(string key)
        {
            string origal = "!zIpuTdR@sEf=as6FygUoW8kQmGhlXcV";
            int length = key.Length;
            if (length < 32)
            {
                key += origal.Substring(0, 32 - length);
            }
            else if (length > 32)
            {
                key = MD5(key);
            }
            return key;
        }


        /// <summary>
        /// aes  2.0 返回62 进制
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt62(string toEncrypt, string key)
        {
            key = checkKeyLength(key);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultArray)
            {
                //sb.AppendFormat("{0:X2}", b);
                sb.Append(ConvertTenToSixtyTwo(b).PadLeft(2, '0'));
            }
            // return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return sb.ToString();
        }
        /// <summary>
        /// aes解密 2.0 由62进制解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt62(string toDecrypt, string key)
        {
            key = checkKeyLength(key);
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //  byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            int halfInputLength = toDecrypt.Length / 2;
            byte[] toDecryptArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                Int64 i = ConvertSixtytwoToTen(toDecrypt.Substring(x * 2, 2));
                toDecryptArray[x] = (byte)i;
            }

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// aes  2.0 返回16 进制
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESEncrypt2(string toEncrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in resultArray)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            // return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return sb.ToString();
        }

        /// <summary>
        /// aes解密 2.0  由16进制解密
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string AESDecrypt2(string toDecrypt, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            //  byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            int halfInputLength = toDecrypt.Length / 2;
            byte[] toEncryptArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                toEncryptArray[x] = (byte)i;
            }

            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// aes 加密 4.0 
        /// </summary>
        /// <param name="toEncrypt">加密前字符串</param>
        /// <param name="key">32位的key</param>
        /// <returns>加密后字符串</returns>
        public static string AESEncryptHEX(string toEncrypt, string key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncrypt);

            aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 16));

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
        /// <summary>
        /// aes 解密 4.0
        /// </summary>
        /// <param name="toDecrypt">aes加密的字符串</param>
        /// <param name="key">32位解密密钥</param>
        /// <returns>加密前字符串</returns>
        public static string AESDecryptHEX(string toDecrypt, string key)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = ASCIIEncoding.ASCII.GetBytes(key);
            aes.IV = ASCIIEncoding.ASCII.GetBytes(key.Substring(0, 16));

            int halfInputLength = toDecrypt.Length / 2;
            byte[] inputByteArray = new byte[halfInputLength];
            for (int x = 0; x < halfInputLength; x++)
            {
                int i = (Convert.ToInt32(toDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        #endregion

        #region des加密 解密

        ///DES加密,可用
        public static string DESEncryptHEX(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(pToEncrypt);

            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            // ret.ToString();
            return ret.ToString();

        }
        ///DES解密 可用
        public static string DESDecryptHEX(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// des 加密2
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string DESEncrypt2(string encryptString, string encryptKey)
        {
            encryptKey = GetSubString(encryptKey, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] keys = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
            byte[] buffer = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, keys), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            return Convert.ToBase64String(stream.ToArray());

        }
        /// <summary>
        /// des解密2
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string DesDecrypt2(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = GetSubString(decryptKey, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey);
                byte[] keys = new byte[] { 0x12, 0x34, 0x56, 120, 0x90, 0xab, 0xcd, 0xef };
                byte[] buffer = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, keys), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch
            {
                return "";
            }

        }
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string str = p_SrcString;
            if (p_Length < 0)
            {
                return str;
            }
            byte[] bytes = Encoding.Default.GetBytes(p_SrcString);
            if (bytes.Length <= p_StartIndex)
            {
                return str;
            }
            int length = bytes.Length;
            if (bytes.Length > (p_StartIndex + p_Length))
            {
                length = p_Length + p_StartIndex;
            }
            else
            {
                p_Length = bytes.Length - p_StartIndex;
                p_TailString = "";
            }
            int num2 = p_Length;
            int[] numArray = new int[p_Length];
            byte[] destinationArray = null;
            int num3 = 0;
            for (int i = p_StartIndex; i < length; i++)
            {
                if (bytes[i] > 0x7f)
                {
                    num3++;
                    if (num3 == 3)
                    {
                        num3 = 1;
                    }
                }
                else
                {
                    num3 = 0;
                }
                numArray[i] = num3;
            }
            if ((bytes[length - 1] > 0x7f) && (numArray[p_Length - 1] == 1))
            {
                num2 = p_Length + 1;
            }
            destinationArray = new byte[num2];
            Array.Copy(bytes, p_StartIndex, destinationArray, 0, num2);
            return (Encoding.Default.GetString(destinationArray) + p_TailString);
        }


        /// <summary>
        /// des 加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DESEncode(string encryptString, string key)
        {
            //加密加密字符串是否为空
            if (string.IsNullOrEmpty(encryptString))
            {
                throw new ArgumentNullException("encryptString", "不能为空");
            }
            //加查密钥是否为空
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            //将密钥转换成字节数组
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            //设置初始化向量
            byte[] keyIV = keyBytes;
            //将加密字符串转换成UTF8编码的字节数组
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            //调用EncryptBytes方法加密
            byte[] resultByteArray = EncryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成字符串并返回
            return Convert.ToBase64String(resultByteArray);
        }
        public static string DESDecode(string decryptString, string key)
        {
            if (string.IsNullOrEmpty(decryptString))
            {
                throw new ArgumentNullException("decryptString", "不能为空");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "不能为空");
            }
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyIV = keyBytes;
            //将解密字符串转换成Base64编码字节数组
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            //调用DecryptBytes方法解密
            byte[] resultByteArray = DecryptBytes(inputByteArray, keyBytes, keyIV);
            //将字节数组转换成UTF8编码的字符串
            return Encoding.UTF8.GetString(resultByteArray);
        }
        /// <summary>
        /// 采用DES算法对字节数组解密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] DecryptBytes(byte[] soureBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (soureBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("soureBytes和keyBytes及keyIV", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(soureBytes, 0, soureBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }

        /// <summary>
        /// 采用DES算法对字节数组加密
        /// </summary>
        /// <param name="sourceBytes">要加密的字节数组</param>
        /// <param name="keyBytes">算法的密钥，长度为8的倍数，最大长度64</param>
        /// <param name="keyIV">算法的初始化向量，长度为8的倍数，最大长度64</param>
        /// <returns></returns>
        public static byte[] EncryptBytes(byte[] sourceBytes, byte[] keyBytes, byte[] keyIV)
        {
            if (sourceBytes == null || keyBytes == null || keyIV == null)
            {
                throw new ArgumentNullException("sourceBytes和keyBytes", "不能为空。");
            }
            else
            {
                //检查密钥数组长度是否是8的倍数并且长度是否小于64
                keyBytes = CheckByteArrayLength(keyBytes);
                //检查初始化向量数组长度是否是8的倍数并且长度是否小于64
                keyIV = CheckByteArrayLength(keyIV);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                //实例化内存流MemoryStream
                MemoryStream mStream = new MemoryStream();
                //实例化CryptoStream
                CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
                cStream.Write(sourceBytes, 0, sourceBytes.Length);
                cStream.FlushFinalBlock();
                //将内存流转换成字节数组
                byte[] buffer = mStream.ToArray();
                mStream.Close();//关闭流
                cStream.Close();//关闭流
                return buffer;
            }
        }
        /// <summary>
        /// 检查密钥或初始化向量的长度，如果不是8的倍数或长度大于64则截取前8个元素
        /// </summary>
        /// <param name="byteArray">要检查的数组</param>
        /// <returns></returns>
        private static byte[] CheckByteArrayLength(byte[] byteArray)
        {
            byte[] resultBytes = new byte[8];
            //如果数组长度小于8
            if (byteArray.Length < 8)
            {
                return Encoding.UTF8.GetBytes("12345678");
            }
            //如果数组长度不是8的倍数
            else if (byteArray.Length % 8 != 0 || byteArray.Length > 64)
            {
                Array.Copy(byteArray, 0, resultBytes, 0, 8);
                return resultBytes;
            }
            else
            {
                return byteArray;
            }
        }
        #endregion


        #endregion
        #region 编码转换

        public static string GetUTF8String(string str)
        {
            //gb2312编码
            System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //UTF8编码
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //将gb2312编码的字符串转换成gb2312编码的字节数组
            byte[] byteSource = gb2312.GetBytes(str);
            //将gb2312编码的字节数组转换成UTF8编码的字节数组
            byte[] resulSource = System.Text.Encoding.Convert(gb2312, utf8, byteSource);
            //将UTF8编码的字节数组转换成字符串并返回
            return utf8.GetString(resulSource);

        }

        public static string GetGBString(string str)
        {
            //gb2312编码
            System.Text.Encoding gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //UTF8编码
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            //将gb2312编码的字符串转换成gb2312编码的字节数组
            byte[] byteSource = utf8.GetBytes(str);
            //将gb2312编码的字节数组转换成UTF8编码的字节数组
            byte[] resulSource = System.Text.Encoding.Convert(utf8, gb2312, byteSource);
            //将UTF8编码的字节数组转换成字符串并返回
            return gb2312.GetString(resulSource);
        }

        /// <summary>
        /// 将一种编码的字符串转换成另一种编码的字符串
        /// </summary>
        /// <param name="sourceEncoding">源字符串的编码</param>
        /// <param name="targetEncoding">目标字符串的编码</param>
        /// <param name="source">源字符串</param>
        /// <returns>返回转换成按照目标编码的字符串</returns>
        public static string ConvertString(Encoding sourceEncoding, Encoding targetEncoding, string source)
        {
            //如果源字符串为空
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }
            else
            {
                //将源字符串转换成源字符串编码对应的字节数组
                byte[] sourceBytes = sourceEncoding.GetBytes(source);
                //将源字符串的字节数组转换成目标字符串对应的字节数组
                byte[] targetBytes = Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);
                //将字节数组转换成字符串
                return targetEncoding.GetString(targetBytes);
            }
        }

        #endregion

        /// <summary>
        /// 根据文件后缀来获取MIME类型字符串
        /// </summary>
        /// <param name="extension">文件后缀</param>
        /// <returns></returns>
        public static string GetMimeType(string extension)
        {
            string mime = string.Empty;
            extension = extension.ToLower();
            switch (extension)
            {
                case ".avi": mime = "video/x-msvideo"; break;
                case ".bin": mime = "application/octet-stream"; break;
                case ".exe": mime = "application/octet-stream"; break;
                case ".dll": mime = "application/octet-stream"; break;
                case ".class": mime = "application/octet-stream"; break;
                case ".csv": mime = "text/comma-separated-values"; break;
                case ".css": mime = "text/css"; break;
                case ".doc": mime = "application/msword"; break;
                case ".dot": mime = "application/msword"; break;
                case ".gz": mime = "application/gzip"; break;
                case ".gif": mime = "image/gif"; break;
                case ".jpeg": mime = "image/jpeg"; break;
                case ".jpg": mime = "image/jpeg"; break;
                case ".jpe": mime = "image/jpeg"; break;
                case ".mpeg": mime = "video/mpeg"; break;
                case ".mpg": mime = "video/mpeg"; break;
                case ".mpe": mime = "video/mpeg"; break;
                case ".mp3": mime = "audio/mpeg"; break;
                case ".pdf": mime = "application/pdf"; break;
                case ".rar": mime = "application/octet-stream"; break;
                case ".txt": mime = "text/plain"; break;
                case ".xls": mime = "application/msexcel"; break;
                case ".xla": mime = "application/msexcel"; break;
                case ".z": mime = "application/x-compress"; break;
                case ".zip": mime = "application/x-zip-compressed"; break;
                default:
                    break;
            }
            return mime;
        }


        public static string GetFileNameNotSuffix(string name)
        {

            int index1 = name.LastIndexOf('.');
            if (index1 == -1) { return ""; }
            string name1 = name.Substring(0, index1);
            int index2 = name1.LastIndexOf('/');
            string name2 = name1.Substring(index2 + 1);
            return name2;
        }
        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
        public static bool IsUrl2(string s)
        {
            return Regex.IsMatch(s, @"(http\:\/\/)?([\w.]+)(\/[\w-   \.\/\?%&=]*)?", RegexOptions.IgnoreCase);
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, "^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }
        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }
        public static bool IsEmail(string email)
        {
            string regexEmail = "\\w{1,}@\\w{1,}\\.\\w{1,}";

            System.Text.RegularExpressions.RegexOptions options = ((System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace

                | System.Text.RegularExpressions.RegexOptions.Multiline)

                       | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.Regex regEmail = new System.Text.RegularExpressions.Regex(regexEmail, options);

            return regEmail.IsMatch(email);
        }

        public static bool isMobile(string s)
        {
            string p = @"^\s*[1](3|5|8)\d\s*[0-9]{4}\s*\d{4}\s*$";
            return System.Text.RegularExpressions.Regex.IsMatch(s, p);
        }
        public static bool isTel(string s)
        {
            string p = @"^\s*（?\(?(\s*\d{3,4})?\s*\)?）?\s*-?\s*[\d|\s]+\s*(-?(\s*\d+\s*)+)?$";
            return System.Text.RegularExpressions.Regex.IsMatch(s, p);
        }
        public static bool isInput(string s)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(s, @"^[a-zA-Z\u4E00-\u9FA5\s]*$");
        }
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsMac(string mac)
        {
            return Regex.IsMatch(mac, "^[0-9A-F]{2}([-:]?)(?:[0-9A-F]{2}\\1){4}[0-9A-F]{2}$", RegexOptions.IgnoreCase);
        }

        public static string[] SplitString(string strContent, string strSplit)
        {
            if (strContent.IndexOf(strSplit) < 0)
            {
                return new string[] { strContent };
            }
            return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
        }


        /// <summary>
        ///   反转字符串
        /// </summary>
        /// <param name="str">要反转的字符串</param>
        /// <returns>返回翻转后的结果</returns>
        public static string StringReverse(string str)
        {
            string encrystr = str;
            char[] chars = encrystr.ToCharArray();
            Array.Reverse(chars);
            encrystr = new string(chars);

            return encrystr;
        }

        public static string GetGuid()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 生成随机码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string BuiderRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder buider = new StringBuilder(length);

            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                buider.Append(chars[(int)rnd.Next(0, chars.Length)]);
            }
            return buider.ToString();
        }


        /// <summary>
        /// <函数：Decode>
        ///作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string HexDecode(string strDecode)
        {
            if (strDecode.IndexOf(@"\u") == -1)
                return strDecode;

            int startIndex = 0;
            if (strDecode.StartsWith(@"\u") == false)
            {
                startIndex = 1;
            }

            string[] codes = Regex.Split(strDecode, @"\\u");

            StringBuilder result = new StringBuilder();
            if (startIndex == 1)
                result.Append(codes[0]);
            for (int i = startIndex; i < codes.Length; i++)
            {
                try
                {
                    if (codes[i].Length > 4)
                    {
                        result.Append((char)short.Parse(codes[i].Substring(0, 4), global::System.Globalization.NumberStyles.HexNumber));
                        result.Append(codes[i].Substring(4));
                    }
                    else
                    {
                        result.Append((char)short.Parse(codes[i].Substring(0, 4), global::System.Globalization.NumberStyles.HexNumber));
                    }
                }
                catch
                {
                    result.Append(codes[i]);
                }
            }

            return result.ToString();
        }


        /// <summary>
        /// 分页显示 showPage
        /// <style type="text/css">
        /// #fenye{ font-weight:normal}
        ///#fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}
        ///#fenye a:hover{ background-color:#eee}
        ///#fenye .active { color:#990000; font-style:italic}
        ///#fenye .nob{ }是否显示前后导航的样式
        /// </summary>
        /// <param name="currNum">当前页码</param>
        /// <param name="totalNum">总页数</param>
        /// <param name="limitNum">前后页扩展列表数量</param>
        /// <param name="pageName">页面名称，包含后缀名</param>
        /// <param name="pnoName">内传递页码的表单元素名</param>
        public static string showPage(long currNum, long totalNum, long limitNum, System.Collections.Generic.IDictionary<string, string> paramdic, string endstr)
        {
            /*
             默认样式
             *  <style type="text/css">
        #fenye{ font-weight:normal}
        #fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}
        #fenye a:hover{ background-color:#eee}
        #fenye .active { color:#990000; font-style:italic}
        #fenye .nob{ }
    </style>
             */

            //默认样式
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(" <style type='text/css'>");
            sb2.Append("  #fenye{ font-weight:normal}");
            sb2.Append(" #fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}");
            sb2.Append("  #fenye a:hover{ background-color:#eee}");
            sb2.Append(" #fenye .active { color:#990000; font-style:italic}");
            sb2.Append("  #fenye .nob{ }");
            sb2.Append(" </style>");

            string sa = "&";
            if (paramdic == null) paramdic = new System.Collections.Generic.Dictionary<string, string>();
            if (paramdic == null || paramdic.Count == 0)
            {
                sa = "?";
            }

            string pageName = "";
            pageName = HttpContext.Current.Request.PhysicalPath;
            pageName = pageName.Substring(pageName.LastIndexOf('\\') + 1);
            StringBuilder sb = new StringBuilder();
            int i1 = 0;
            foreach (KeyValuePair<string, string> item in paramdic)
            {
                i1++;
                if (i1 == 1)
                    sb.Append("?").Append(item.Key).Append("=").Append(item.Value);
                else
                    sb.Append("&").Append(item.Key).Append("=").Append(item.Value);
            }
            pageName += sb.ToString();
            if (currNum <= 0 || totalNum <= 0) return string.Empty;
            StringBuilder sbResult = new StringBuilder();


            //计算上一页和下一页
            long prevPage = currNum > 1 ? currNum - 1 : 1;
            long nextPage = currNum < totalNum ? currNum + 1 : totalNum;

            //上一页
            /*<li style="border:none"><a href="#"><img src="../../Img/bbs/arrow_left.gif" width="6" height="11" align="absmiddle"/></a></li>
          <li style="border:none"><a href="#" class="view_a">上一页</a></li>*/

            if (currNum <= 1)
            {
                sbResult.Append("<a  title='首页'  class='nob'  href=\"javascript:void(0)\"><span style='color:#ccc'><<</span></a>");
                sbResult.Append("<a  title='上一页'  class='nob' href=\"javascript:void(0)\" class=\"view_a\"><span style='color:#ccc'> < </span></a>");
            }
            else
            {
                sbResult.Append("<a title='首页'  class='nob' href=\"" + pageName + endstr + "\"><<</a>");
                if (prevPage != 1)
                {
                    sbResult.Append("<a title='上一页' href=\"" + pageName + "" + sa + "pno=" + prevPage + endstr + "\" class=\"nob\"> < </a>");
                }
                else
                {
                    sbResult.Append("<a title='上一页' href=\"" + pageName + endstr + "\" class=\"nob\"> < </a>");
                }
            }



            // 计算需要显示的页码，前后各显示若干页
            long iPageNoStart = 0;
            long iPageNoEnd = 0;
            long iPageNoShowLimit = limitNum; // 每页显示页数
            long iLeftLimit, iRightLimit; // 当前页左右的页数
            if ((iPageNoShowLimit % 2) == 0)
            {
                iLeftLimit = iPageNoShowLimit / 2 - 1;
                iRightLimit = iPageNoShowLimit / 2;
            }
            else
            {
                iLeftLimit = (iPageNoShowLimit - 1) / 2;
                iRightLimit = (iPageNoShowLimit - 1) / 2;
            }
            iPageNoStart = currNum - iLeftLimit;
            iPageNoEnd = currNum + iRightLimit;

            if (iPageNoStart < 1)
            {
                iPageNoStart = 1;
                iPageNoEnd = iPageNoShowLimit;
                if (totalNum < iPageNoShowLimit)
                {
                    iPageNoEnd = totalNum;
                }
            }
            if (iPageNoEnd > totalNum)
            {
                iPageNoStart = totalNum - iPageNoShowLimit + 1;
                iPageNoEnd = totalNum;
                if (totalNum < iPageNoShowLimit)
                {
                    iPageNoStart = 1;
                }
            }
            //前页省略符
            if (iPageNoStart > 2)
            {
                sbResult.Append("<a class=\"view_a\" href=\"" + pageName + "" + sa + "pno=" + (iPageNoStart - 1).ToString() + endstr + "\"  title=\"1-" + (iPageNoStart - 1) + "页\">1-" + (iPageNoStart - 1) + "</a>");
            }
            //输出页码
            for (long i = iPageNoStart; i <= iPageNoEnd; i++)
            {
                if (i != 1)
                {
                    if (i != currNum)
                    {
                        sbResult.Append("<a class=\"view_a\" href=\"" + pageName + "" + sa + "pno=" + i + endstr + "\"  title=\"第" + i.ToString() + "页\">" + i.ToString() + "</a>");
                    }
                    else
                    {
                        sbResult.Append("<a href=\"" + pageName + "" + sa + "pno=" + i + endstr + "\"><span class=\"active\">" + i.ToString() + "</span></a>");
                    }
                }
                else
                {
                    if (i != currNum)
                    {
                        sbResult.Append("<a class=\"view_a\" href=\"" + pageName + endstr + "\"  title=\"第" + i.ToString() + "页\">" + i.ToString() + "</a>");
                    }
                    else
                    {
                        sbResult.Append("<a href=\"" + pageName + endstr + "\"><span class=\"active\">" + i.ToString() + "</span></a>");
                    }
                }
            }

            //后页省略符
            if (iPageNoEnd < totalNum - 1)
            {
                sbResult.Append("<a class=\"view_a\" href=\"" + pageName + "" + sa + "pno=" + (iPageNoEnd + 1) + endstr + "\"  title=\"" + (iPageNoEnd + 1) + "-" + totalNum + "页\">" + (iPageNoEnd + 1) + "-" + totalNum + "</a>");
            }
            //下一页、末页
            /* <li style="border:none"><a href="#" class="view_a">下一页</a></li>
          <li style="border:none"><a href="#"><img src="../../Img/bbs/arrow_right.gif" width="6" height="11" align="absmiddle"/></a></li>*/
            if (currNum >= totalNum)
            {
                sbResult.Append("<a title='下一页' href=\"javascript:void(0)\" class=\"nob\"><span style='color:#ccc'> > </span></a>");
                sbResult.Append("<a title='尾页' class='nob' href=\"javascript:void(0)\"<span style='color:#ccc'>>></span></a></li>");
            }
            else
            {
                sbResult.Append("<a title='下一页' class='nob' href=\"" + pageName + "" + sa + "pno=" + nextPage + endstr + "\" class=\"view_a\"> > </a>");
                sbResult.Append("<a title='尾页'   class='nob' href=\"" + pageName + "" + sa + "pno=" + totalNum + endstr + "\">>></a>");
            }

            StringBuilder sb3 = new StringBuilder();
            sb3.Append(sb2.ToString());
            sb3.Append("<p id='fenye'>");
            sb3.Append(sbResult.ToString());
            sb3.Append("</p>");
            return sb3.ToString();
        }
        /// <summary>
        /// 伪静态分页显示 showPage，以aspx结尾
        /// <style type="text/css">
        /// #fenye{ font-weight:normal}
        ///#fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}
        ///#fenye a:hover{ background-color:#eee}
        ///#fenye .active { color:#990000; font-style:italic}
        ///#fenye .nob{ }是否显示前后导航的样式
        /// </summary>
        /// <param name="currNum">当前页码</param>
        /// <param name="totalNum">总页数</param>
        /// <param name="limitNum">前后页扩展列表数量</param>
        /// <param name="pageName">页面名称，包含后缀名</param>
        /// <param name="pnoName">内传递页码的表单元素名</param>
        public static string showPageRewriter(string pageprefix, long currNum, long totalNum, long limitNum, string[] paramss, string endstr)
        {
            string split = "-";
            string endPrefix = ".aspx";
            //string ad = pageprefix + split;
            /*currnum改为 以零为开始*/
            /*
             默认样式
             *  <style type="text/css">
        #fenye{ font-weight:normal}
        #fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}
        #fenye a:hover{ background-color:#eee}
        #fenye .active { color:#990000; font-style:italic}
        #fenye .nob{ }
    </style>
             */

            //默认样式
            StringBuilder sb2 = new StringBuilder();
            sb2.Append(" <style type='text/css'>");
            sb2.Append("  #fenye{ font-weight:normal}");
            sb2.Append(" #fenye a{ text-decoration:none; color:#0B3B8C; padding:0px 3px; text-align:center;  border:#ccdbe4 solid 1px; display:block; float:left; margin:0px 2px;}");
            sb2.Append("  #fenye a:hover{ background-color:#eee}");
            sb2.Append(" #fenye .active { color:#990000; font-style:italic}");
            sb2.Append("  #fenye .nob{ }");
            sb2.Append(" </style>");

            string pageName = pageprefix;
            foreach (string item in paramss)
            {
                pageName += split + item;
            }

            if (currNum <= 0 || totalNum <= 0) return string.Empty;
            StringBuilder sbResult = new StringBuilder();



            //计算上一页和下一页
            long prevPage = currNum > 1 ? currNum - 1 : 1;
            long nextPage = currNum < totalNum ? currNum + 1 : totalNum;

            //上一页
            /*<li style="border:none"><a href="#"><img src="../../Img/bbs/arrow_left.gif" width="6" height="11" align="absmiddle"/></a></li>
          <li style="border:none"><a href="#" class="view_a">上一页</a></li>*/

            if (currNum <= 1)
            {
                sbResult.Append("<a  title='首页'  class='nob'  href=\"javascript:void(0)\"><span style='color:#ccc'><<</span></a>");
                sbResult.Append("<a  title='上一页'  class='nob' href=\"javascript:void(0)\" class=\"view_a\"><span style='color:#ccc'> < </span></a>");
            }
            else
            {
                sbResult.Append("<a title='首页'  class='nob' href=\"" + pageName + endPrefix + "\"><<</a>");
                if (prevPage != 1)
                {
                    sbResult.Append("<a title='上一页' href=\"" + pageName + split + (currNum - 1) + endPrefix + "\" class=\"nob\"> < </a>");
                }
                else
                {
                    sbResult.Append("<a title='上一页' href=\"" + pageName + endPrefix + "\" class=\"nob\"> < </a>");
                }
            }



            // 计算需要显示的页码，前后各显示若干页
            long iPageNoStart = 0;
            long iPageNoEnd = 0;
            long iPageNoShowLimit = limitNum; // 每页显示页数
            long iLeftLimit, iRightLimit; // 当前页左右的页数
            if ((iPageNoShowLimit % 2) == 0)
            {
                iLeftLimit = iPageNoShowLimit / 2 - 1;
                iRightLimit = iPageNoShowLimit / 2;
            }
            else
            {
                iLeftLimit = (iPageNoShowLimit - 1) / 2;
                iRightLimit = (iPageNoShowLimit - 1) / 2;
            }
            iPageNoStart = currNum - iLeftLimit;
            iPageNoEnd = currNum + iRightLimit;

            if (iPageNoStart < 1)
            {
                iPageNoStart = 1;
                iPageNoEnd = iPageNoShowLimit;
                if (totalNum < iPageNoShowLimit)
                {
                    iPageNoEnd = totalNum;
                }
            }
            if (iPageNoEnd > totalNum)
            {
                iPageNoStart = totalNum - iPageNoShowLimit + 1;
                iPageNoEnd = totalNum;
                if (totalNum < iPageNoShowLimit)
                {
                    iPageNoStart = 1;
                }
            }
            //前页省略符
            if (iPageNoStart > 2)
            {
                sbResult.Append("<a class=\"view_a\" href=\"" + pageName + split + (iPageNoStart - 1).ToString() + endPrefix + endstr + "\"  title=\"1-" + (iPageNoStart - 1) + "页\">1-" + (iPageNoStart - 1) + "</a>");
            }
            //输出页码
            for (long i = iPageNoStart; i <= iPageNoEnd; i++)
            {
                if (i != 1)
                {
                    if (i != currNum)
                    {
                        sbResult.Append("<a class=\"view_a\" href=\"" + pageName + split + i.ToString() + endPrefix + endstr + "\"  title=\"第" + i.ToString() + "页\">" + i.ToString() + "</a>");
                    }
                    else
                    {
                        sbResult.Append("<a href='javascript:'><span class=\"active\">" + i.ToString() + "</span></a>");
                    }
                }
                else
                {
                    if (i != currNum)
                    {
                        sbResult.Append("<a class=\"view_a\" href=\"" + pageName + endPrefix + endstr + "\"  title=\"第" + i.ToString() + "页\">" + i.ToString() + "</a>");
                    }
                    else
                    {
                        sbResult.Append("<a href='javascript:'><span class=\"active\">" + i.ToString() + "</span></a>");
                    }
                }
            }

            //后页省略符
            if (iPageNoEnd < totalNum - 1)
            {
                sbResult.Append("<a class=\"view_a\" href=\"" + pageName + split + (iPageNoEnd + 1) + endPrefix + endstr + "\"  title=\"" + (iPageNoEnd + 1) + "-" + totalNum + "页\">" + (iPageNoEnd + 1) + "-" + totalNum + "</a>");
            }
            //下一页、末页
            /* <li style="border:none"><a href="#" class="view_a">下一页</a></li>
          <li style="border:none"><a href="#"><img src="../../Img/bbs/arrow_right.gif" width="6" height="11" align="absmiddle"/></a></li>*/
            if (currNum >= totalNum)
            {
                sbResult.Append("<a title='下一页' href=\"javascript:void(0)\" class=\"nob\"><span style='color:#ccc'> > </span></a>");
                sbResult.Append("<a title='尾页' class='nob' href=\"javascript:void(0)\"<span style='color:#ccc'>>></span></a></li>");
            }
            else
            {
                sbResult.Append("<a title='下一页' class='nob' href=\"" + pageName + split + nextPage + endPrefix + endstr + "\" class=\"view_a\"> > </a>");
                sbResult.Append("<a title='尾页'   class='nob' href=\"" + pageName + split + totalNum + endPrefix + endstr + "\">>></a>");
            }

            StringBuilder sb3 = new StringBuilder();
            sb3.Append(sb2.ToString());
            sb3.Append("<p id='fenye'>");
            sb3.Append(sbResult.ToString());
            sb3.Append("</p>");
            return sb3.ToString();
        }
        public static int GetInArrayID(string strSearch, string[] stringArray)
        {
            return GetInArrayID(strSearch, stringArray, true);
        }

        public static int GetInArrayID(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            for (int i = 0; i < stringArray.Length; i++)
            {
                if (caseInsensetive)
                {
                    if (strSearch.ToLower() == stringArray[i].ToLower())
                    {
                        return i;
                    }
                }
                else if (strSearch == stringArray[i])
                {
                    return i;
                }
            }
            return -1;
        }
        public static bool InArray(string str, string stringarray)
        {
            return InArray(str, SplitString(stringarray, ","), false);
        }

        public static bool InArray(string str, string[] stringarray)
        {
            return InArray(str, stringarray, false);
        }

        public static bool InArray(string strSearch, string[] stringArray, bool caseInsensetive)
        {
            return (GetInArrayID(strSearch, stringArray, caseInsensetive) >= 0);
        }

        public static bool InArray(string str, string stringarray, string strsplit)
        {
            return InArray(str, SplitString(stringarray, strsplit), false);
        }

        public static bool InArray(string str, string stringarray, string strsplit, bool caseInsensetive)
        {
            return InArray(str, SplitString(stringarray, strsplit), caseInsensetive);
        }



    }
}
