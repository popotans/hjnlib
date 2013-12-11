using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helper.IO;
namespace NjhLib.Web.Mvc.ParseTemp
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FileHelper fh = new FileHelper();
            string source = FileHelper.ReadFile(Server.MapPath("html/index.htm"), System.Text.Encoding.UTF8, false);
            //jiexi quanbu bianliang
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["{sitename}"] = "www.qq.com";
            dic["{sitekwd}"] = "腾讯";
            dic["{sitedesc}"] = "腾讯第一门户";
            foreach (KeyValuePair<string, string> item in dic)
            {
                source = source.Replace(item.Key, item.Value);
            }
            //jiexi nav
            source = ParseNav(source);

            //jiexi weizhi
            source = source.Replace("{position}", "首页");

            //lieniao
            source = ParseArticleList(source);
            //click
            source = ParseClick(source);

            Response.Write(source);
        }

        public string ParseNav(string source)
        {
            List<Nav> navList = new List<Nav>()
            {
            };
            navList.Add(new Nav { Name = "盛大游戏", Url = "http://www.sdg.com" });
            navList.Add(new Nav { Name = "网易", Url = "http://www.163.com" });
            navList.Add(new Nav { Name = "百度", Url = "http://baidu.com" });
            navList.Add(new Nav { Name = "新郎", Url = "http://www.sina.com.cn" });
            navList.Add(new Nav { Name = "腾讯", Url = "http://www.qq.com" });
            navList.Add(new Nav { Name = "搜狐", Url = "http://www.sohu.com" });

            string rs = string.Empty;
            //从模版提取循环部分
            string seg = Helper.String.StringHelper.SubStr(source, "{nav}", "{/nav}");
            foreach (var nav in navList)
            {
                string __s = seg;
                __s = __s.Replace("{linkname}", nav.Name);
                __s = __s.Replace("{linkhref}", nav.Url);
                rs += __s;
            }
            //    source = source.Replace("{nav}" + _s + "{/nav}", navRs);
            source = ReplaceFlag(source, "nav", seg, rs);
            return source;
        }

        public string ParseArticleList(string source)
        {
            string seg = Helper.String.StringHelper.SubStr(source, "{article}", "{/article}");
            List<Article> list = new List<Article>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(new Article { Title = "zhe is标题阿克讲话" + i, Url = "http://im222.com" });
            }
            string rs = "";
            foreach (var a in list)
            {
                string _s = seg;
                _s = _s.Replace("{linkname}", a.Title).Replace("{linkhref}", a.Url);
                rs += _s;
            }
            // source = source.Replace("{article}" + seg + "{/article}", rs);
            source = ReplaceFlag(source, "article", seg, rs);
            return source;
        }

        public string ParseClick(string source)
        {
            string seg = GetSegment(source, "click");

            List<Article> list = new List<Article>();
            for (int i = 0; i < 15; i++)
            {
                list.Add(new Article { Title = "click:::::" + i, Url = "http://im222.com" });
            }
            string rs = "";
            foreach (var a in list)
            {
                string _s = seg;
                _s = _s.Replace("{linkname}", a.Title).Replace("{linkhref}", a.Url);
                rs += _s;
            }
            source = ReplaceFlag(source, "click", seg, rs);
            return source;
        }

        public string ReplaceFlag(string source, string glag, string mobanSeg, string rs)
        {
            return source.Replace("{" + glag + "}" + mobanSeg + "{/" + glag + "}", rs);
        }

        public string GetSegment(string source, string flag)
        {
            return Helper.String.StringHelper.SubStr(source, "{" + flag + "}", "{/" + flag + "}");
        }
    }
}