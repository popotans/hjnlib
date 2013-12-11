﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils.Alipay;
using System.Collections.Specialized;
namespace NjhLib.Web.Mvc.aLIPAY
{
    public partial class _return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码
                    Response.Write("<br/>正在处理您的订单...</br>");

                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表
                    string trade_no = Request.QueryString["trade_no"];              //支付宝交易号
                    string order_no = Request.QueryString["out_trade_no"];	        //获取订单号
                    string total_fee = Request.QueryString["total_fee"];            //获取总金额
                    string subject = Request.QueryString["subject"];                //商品名称、订单名称
                    string body = Request.QueryString["body"];                      //商品描述、订单备注、描述
                    string buyer_email = Request.QueryString["buyer_email"];        //买家支付宝账号
                    string trade_status = Request.QueryString["trade_status"];      //交易状态
                    string s = "<br>支付宝交易号：" + trade_no;
                    s += "<br>您的订单号：" + order_no;
                    s += "<br>交易金额：" + total_fee;
                    s += "<br>订单名称：" + subject;
                    s += "<br>商品描述：" + body;
                    s += "<br>买家支付宝账号：" + buyer_email;
                    s += "<br>交易状态：" + trade_status;
                    s += "<br><a href='default.aspx'>进入发货流程</a>";
                    Response.Write(s);

                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理（可参考“集成教程”中“3.4返回数据处理”）
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序


                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }

                    //打印页面
                    Response.Write("验证成功<br />");
                    Response.Write("trade_no=" + trade_no);

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    Response.Write("验证失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}