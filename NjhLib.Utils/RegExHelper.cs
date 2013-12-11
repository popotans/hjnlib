using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace NjhLib.Utils
{
    public sealed class RegExHelper
    {
        // Methods
        public static bool IsCellNumber(string input_string)
        {
            string pattern = @"^1(3[0-9]|5[0-9]|8[0-9])\d{8}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input_string);
        }

        public static bool IsChinese(string str)
        {
            return Regex.IsMatch(str, @"^[\u4e00-\u9fa5]{0,}$");
        }

        public static bool IsChineseIdCard(string CardNo)
        {
            string pattern = @"^\d{17}[\d|X]|\d{15}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(CardNo);
        }

        public static bool IsEmailAddress(string input_string)
        {
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input_string);
        }

        public static bool IsEnglish(string str)
        {
            return Regex.IsMatch(str, "^[A-Za-z]+$");
        }

        public static bool IsInt(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*$");
        }

        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        public static bool IsNumeric(string str)
        {
            bool flag = new Regex(@"^-?\d+$").IsMatch(str.Trim());
            bool flag2 = new Regex(@"^(-?\d+)(\.\d+)?$").IsMatch(str.Trim());
            return (flag || flag2);
        }

        public static bool IsTelephoneNumber(string input_string)
        {
            string pattern = "^[0-9-]{5,32}$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(input_string);
        }

        public static bool IsURL(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(http|https)\://([a-zA-Z0-9\.\-]+(\:[a-zA-Z0-9\.&%\$\-]+)*@)*((25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])|localhost|([a-zA-Z0-9\-]+\.)*[a-zA-Z0-9\-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{1,10}))(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\?\'\\\+&%\$#\=~_\-]+))*$");
        }
    }




}
