using System;
using System.Web;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using TopSpace.Bll;
using SpaceTime;
using SpaceTime.DataBase;
using SpaceTime.Page;
namespace NjhLib.Utils
{
    public enum TaobaoSortType : int
    {
        /*
            price_desc
            * credit_desc
            * commissionRate_desc
            * commissionNum_desc
            * commissionVolume_desc
            */
        Default = 11,
        PriceDesc = 21,
        PriceAsc = 22,
        CreditDesc = 31,
        CreditAsc = 32,
        CommissionRateDesc = 41,
        CommissionRateAsc = 42,
        CommissionNumDesc = 51,
        CommissionNumAsc = 52,
        CommissionVolumeDesc = 61,
        CommissionVolumeAsc = 62

    }
    public class TaobaoLib
    {
        public static string AppKey { get { return System.Configuration.ConfigurationManager.AppSettings["AppKey"]; } }
        public static string AppSecret { get { return System.Configuration.ConfigurationManager.AppSettings["AppSecret"]; } }
        public static string Nick { get { return System.Configuration.ConfigurationManager.AppSettings["Nick"]; } }
        private static bool _SandBox = false;
        /// <summary>
        /// 是否沙箱环境 (true=沙箱,false=正式环境,默认是正式环境)
        /// </summary>
        public static bool SendBox
        {
            set { _SandBox = value; }
            get
            {
                if (System.Configuration.ConfigurationManager.AppSettings["SandBox"] != null)
                {
                    if (System.Configuration.ConfigurationManager.AppSettings["SandBox"].ToString() == "1")
                    {
                        _SandBox = true;
                    }
                    else
                    {
                        _SandBox = false;
                    }
                }
                return _SandBox;
            }
        }
        private static string _serverURL = SendBox ? "http://gw.api.tbsandbox.com/router/rest" : "http://gw.api.taobao.com/router/rest"; //http://gw.api.tbsandbox.com/router/rest
        public static string ServerURL { set { _serverURL = value; } get { return _serverURL; } }
        public TaobaoLib()
        {
            TopAPI.AppKey = AppKey;
            TopAPI.AppSecret = AppSecret;
            TopAPI.RestUrl = ServerURL;

        }

        /// <summary>
        /// cats cats
        /// </summary>
        /// <param name="parentcid"></param>
        /// <returns></returns>
        public static List<ItemCat> GetCatList(string parentcid)
        {
            if (string.IsNullOrEmpty(parentcid)) parentcid = "0";
            List<ItemCat> list = new List<ItemCat>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("fields", "cid,parent_cid,name,is_parent");//需要返回的字段列表，见ItemCat，默认返回：cid,parent_cid,name,is_parent 
            param.Add("parent_cid", parentcid);//父商品类目 id，0表示根节点, 传输该参数返回所有子类目。 (cids、parent_cid至少传一个) 

            string strXml = TopAPI.Post("taobao.itemcats.get", param);
            Parser parser = new Parser();
            ErrorRsp err = new ErrorRsp();
            parser.XmlToObject2<ItemCat>(strXml, "itemcats_get", "item_cats/item_cat", list, err);
            if (err.IsError)
            {
                throw new Exception("获取商品类目发生错误！\\r错误代码：" + err.code + "\\r错误原因：" + err.msg + "\\r错误描述：" + err.sub_code + "-" + err.sub_msg + "");
            }
            return list;
        }

        /// <summary>
        /// 获取商品详细信息
        /// </summary>
        /// <param name="numiid"></param>
        /// <returns></returns>
        public static TaobaokeItemDetail GetItemDetail(string numiid)
        {
            List<TaobaokeItemDetail> list = new List<TaobaokeItemDetail>();
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("fields", "click_url,shop_click_url,seller_credit_score,title,pic_url,nick,price,list_time,delist_time,desc");//商品详细信息. fields中需要设置Item下的字段,如设置:iid,detail_url等; 只设置item_detail,则不返回的Item下的所有信息. 
            param.Add("num_iids", numiid);//淘宝客商品数字id串.最大输入10个.格式如:"value1,value2,value3" 用" , "号分隔商品id. 
            param.Add("nick", Nick);//淘宝用户昵称，注：指的是淘宝的会员登录名.如果昵称错误,那么客户就收不到佣金.每个淘宝昵称都对应于一个pid，在这里输入要结算佣金的淘宝昵称，当推广的商品成功后，佣金会打入此输入的淘宝昵称的账户。具体的信息可以登入阿里妈妈的网站查看. 
            //查询淘宝客推广商品详细信息  --  taobao.taobaoke.items.detail.get ---  http://open.taobao.com/dev/index.php/API2.0:Taobao.taobaoke.items.detail.get
            string strXml = TopAPI.Post("taobao.taobaoke.items.detail.get", param);

            Parser parser = new Parser();//定义解析XML对象

            ErrorRsp err = new ErrorRsp();//对象错误对象
            parser.XmlToObject2<TaobaokeItemDetail>(strXml, "taobaoke_items_detail_get", "taobaoke_item_details/taobaoke_item_detail", list, err);//解析item对象
            if (err.IsError == true)
            {
                throw new Exception("获取商品详情发生错误！\\r错误代码：" + err.code + "\\r错误原因：" + err.msg + "\\r错误描述：" + err.sub_code + "-" + err.sub_msg + "");
            }

            if (list.Count == 0)
            {
                throw new Exception("无商品信息。");
            }
            return list[0];
        }

        #region item list

        public static List<TaobaokeItem> GetItemListByCid(string cid, string sort, string pno, string psize)
        {
            return GetItemList(cid, "", "", "", "", "", "", "", sort, pno, psize);
        }
        public static List<TaobaokeItem> GetItemListByCid(string cid, string sprice, string endprice, string sort, string pno, string psize)
        {
            return GetItemList(cid, "", sprice, endprice, "", "", "", "", sort, pno, psize);
        }

        public static List<TaobaokeItem> GetItemListBySearch(string kwd, string sort, string pno, string psize)
        {
            return GetItemList("", kwd, "", "", "", "", "", "", sort, pno, psize);
        }
        public static List<TaobaokeItem> GetItemListBySearch(string kwd, string sprice, string endprice, string sort, string pno, string psize)
        {
            return GetItemList("", kwd, sprice, endprice, "", "", "", "", sort, pno, psize);
        }
        public static List<TaobaokeItem> GetItemListByCidSearch(string cid, string kwd, string sprice, string endprice, string sort, string pno, string psize)
        {
            return GetItemList(cid, kwd, sprice, endprice, "", "", "", "", sort, pno, psize);
        }



        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="kwd"></param>
        /// <param name="sprice"></param>
        /// <param name="eprice"></param>
        /// <param name="scredit"></param>
        /// <param name="ecredit"></param>
        /// <param name="srate"></param>
        /// <param name="erate"></param>
        /// <param name="sort"></param>
        /// <param name="pno"></param>
        /// <param name="psize"></param>
        /// <returns></returns>
        public static List<TaobaokeItem> GetItemList(string cid, string kwd, string sprice, string eprice, string scredit, string ecredit, string srate, string erate, string sort, string pno, string psize)
        {
            List<TaobaokeItem> list = new List<TaobaokeItem>();
            Dictionary<string, string> param = new Dictionary<string, string>();//定义 API应用级输入参数
            //需返回的字段列表。
            param.Add("fields", "iid,num_iid,title,nick,pic_url,price,click_url,commission,commission_rate,commission_num,commission_volume,shop_click_url,seller_credit_score,item_location,volume");

            param.Add("nick", Nick);//淘宝用户昵称，注：指的是淘宝的会员登录名.如果昵称错误,那么客户就收不到佣金.每个淘宝昵称都对应于一个pid，在这里输入要结算佣金的淘宝昵称，当推广的商品成功后，佣金会打入此输入的淘宝昵称的账户。具体的信息可以登入阿里妈妈的网站查看. 


            //商品标题中包含的关键字. 注意:查询时keyword,cid至少选择其中一个参数 
            if (!string.IsNullOrEmpty(kwd))
                param.Add("keyword", kwd);
            if (!string.IsNullOrEmpty(cid))
                //商品所属分类id
                param.Add("cid", cid);

            if (!string.IsNullOrEmpty(scredit) && !string.IsNullOrEmpty(ecredit))
            {
                //起始信用
                param.Add("start_credit", scredit);
                //截止信用
                param.Add("end_credit", ecredit);
            }

            if (!string.IsNullOrEmpty(sprice) && !string.IsNullOrEmpty(eprice))
            {
                param.Add("start_price", sprice);
                param.Add("end_price", eprice);
            }
            if (!string.IsNullOrEmpty(srate) && !string.IsNullOrEmpty(erate))
            {
                param.Add("start_commissionRate", srate);
                param.Add("end_commissionRate", erate);
            }

            if (string.IsNullOrEmpty(sort)) sort = "default";
            /*
             price_desc
             * credit_desc
             * commissionRate_desc
             * commissionNum_desc
             * commissionVolume_desc
             */
            param.Add("sort", sort);

            //每页返回结果数.最大每页40 
            param.Add("page_size", psize);
            //结果页数.1~99 
            param.Add("page_no", pno);

            Parser parser = new Parser();//定义解析XML对象
            ErrorRsp err = new ErrorRsp();//定义错误对象
            int total = 0;//定义 记录总数
            string strXml = string.Empty;

            //查询淘宝客推广商品   --- taobao.taobaoke.items.get --- 详见：  http://open.taobao.com/dev/index.php/API2.0:Taobao.taobaoke.items.get
            strXml = TopAPI.Post("taobao.taobaoke.items.get", param);
            total = parser.XmlToTotalResults(strXml, "taobaoke_items_get");
            parser.XmlToObject2<TaobaokeItem>(strXml, "taobaoke_items_get", "taobaoke_items/taobaoke_item", list, err);

            if (err.IsError == true)
            {
                throw new Exception("发生错误！\\r错误代码：" + err.code + "\\r错误原因：" + err.msg + "\\r错误描述：" + err.sub_code + "-" + err.sub_msg + "");
            }


            return list;
        }

        #endregion


    }
}
