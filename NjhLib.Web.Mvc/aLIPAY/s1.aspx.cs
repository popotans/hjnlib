using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NjhLib.Utils.Alipay;
namespace NjhLib.Web.Mvc.aLIPAY
{
    public partial class s1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"])) this.Do();
        }
        private void Do()
        {

            ////////////////////////////////////////////请求参数////////////////////////////////////////////

            //必填参数//

            //请与贵网站订单系统中的唯一订单号匹配
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss");
            //订单名称，显示在支付宝收银台里的“商品名称”里，显示在支付宝的交易管理的“商品名称”的列表里。
            string subject = Request["TxtSubject"].Trim();
            //订单描述、订单详细、订单备注，显示在支付宝收银台里的“商品描述”里
            string body = Request["TxtBody"].Trim();
            //订单总金额，显示在支付宝收银台里的“应付总额”里
            string total_fee = Request["TxtTotal_fee"].Trim();

            //扩展功能参数——默认支付方式//
            //默认支付方式，代码见“即时到帐接口”技术文档
            string paymethod = "bankPay";
            //默认网银代号，代号列表见“即时到帐接口”技术文档“附录”→“银行列表”
            string defaultbank = "CCBBTB";

            //扩展功能参数——防钓鱼//

            //防钓鱼时间戳
            string anti_phishing_key = "";
            //获取客户端的IP地址，建议：编写获取客户端IP地址的程序
            string exter_invoke_ip = "";
            //注意：
            //请慎重选择是否开启防钓鱼功能
            //exter_invoke_ip、anti_phishing_key一旦被设置过，那么它们就会成为必填参数
            //建议使用POST方式请求数据
            //示例：
            //exter_invoke_ip = "";
            //Service aliQuery_timestamp = new Service();
            //anti_phishing_key = aliQuery_timestamp.Query_timestamp();               //获取防钓鱼时间戳函数

            //扩展功能参数——其他//

            //商品展示地址，要用http:// 格式的完整路径，不允许加?id=123这类自定义参数
            string show_url = "http://www.XINGFUMM.com/ALIPAY.aspx";
            //自定义参数，可存放任何内容（除=、&等特殊字符外），不会显示在页面上
            string extra_common_param = "可以传递会员信息";
            //默认买家支付宝账号
            string buyer_email = "";

            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("show_url", show_url);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("body", body);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("paymethod", paymethod);
            sParaTemp.Add("defaultbank", defaultbank);
            //  sParaTemp.Add("anti_phishing_key", anti_phishing_key);
            //  sParaTemp.Add("exter_invoke_ip", exter_invoke_ip);
            sParaTemp.Add("extra_common_param", extra_common_param);
            //  sParaTemp.Add("buyer_email", buyer_email);

            Service ali = new Service();
            string htmlstr = ali.Create_direct_pay_by_user(sParaTemp);
            Response.Write(htmlstr);

        }
    }
}