using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.IO;
namespace NjhLib.Utils.Alipay
{
    internal class Config
    {

        #region 字段
        private static string partner = "";
        private static string key = "";
        private static string seller_email = "";
        private static string return_url = "";
        private static string notify_url = "";
        private static string input_charset = "";
        private static string sign_type = "";
        private static string transport = "";
        #endregion

        static Config()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = "2088002089534461";

            //交易安全检验码，由数字和字母组成的32位字符串
            key = "ghx1n7wcyn85pohs4c21qbjaaoz129gg";

            //签约支付宝账号或卖家支付宝帐户
            seller_email = "popotans@163.com";

            //页面跳转同步返回页面文件路径 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            return_url = "http://www.xingfumm.com/aLIPAY/return_url.aspx";

            //服务器通知的页面文件路径 要用 http://格式的完整路径，不允许加?id=123这类自定义参数
            notify_url = "http://www.xingfumm.com/aLIPAY/notify_url.aspx";


            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑



            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式 不需修改
            sign_type = "MD5";

            //访问模式,根据自己的服务器是否支持ssl访问，若支持请选择https；若不支持请选择http
            transport = "http";
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置交易安全检验码
        /// </summary>
        public static string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 获取或设置签约支付宝账号或卖家支付宝帐户
        /// </summary>
        public static string Seller_email
        {
            get { return seller_email; }
            set { seller_email = value; }
        }

        /// <summary>
        /// 获取页面跳转同步通知页面路径
        /// </summary>
        public static string Return_url
        {
            get { return return_url; }
        }

        /// <summary>
        /// 获取服务器异步通知页面路径
        /// </summary>
        public static string Notify_url
        {
            get { return notify_url; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }

        /// <summary>
        /// 获取访问模式
        /// </summary>
        public static string Transport
        {
            get { return transport; }
        }
        #endregion
    }


   

 
   
}
