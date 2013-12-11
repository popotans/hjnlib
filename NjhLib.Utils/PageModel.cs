using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils
{
    public class PageModel
    {
        private int _page;
        public int Page
        {
            get
            {
                int n = _page > TotalPage ? TotalPage : _page;
                return n < 1 ? 1 : n;
            }
            set { _page = value; }
        }
        private int _pagesize;

        public int Pagesize
        {
            get { return _pagesize; }
            set { _pagesize = value; }
        }
        private int _totalRecord;

        public int TotalRecord
        {
            get { return _totalRecord; }
            set { _totalRecord = value; }
        }
     //   private int _totalPage;

        public int TotalPage
        {
            get
            {
                int n = TotalRecord % Pagesize == 0 ? TotalRecord / Pagesize : TotalRecord / Pagesize + 1;
                return n < 1 ? 1 : n;
            }

        }
        private List<Object> _list;

        public List<Object> List
        {
            get { return _list; }
            set { _list = value; }
        }

        private int _nextpage;

        public int Nextpage
        {
            get { return Page + 1 > TotalPage ? Page : Page + 1; }
            set { _nextpage = value; }
        }
        private int _prevpage;

        public int Prevpage
        {
            get { return Page < 2 ? 1 : Page - 1; }
            set { _prevpage = value; }
        }

        private string _pagepath;

        public string Pagepath
        {
            get { return _pagepath; }
            set { _pagepath = value; }
        }
        private IDictionary<string, string> _pageQueryStrings;

        public IDictionary<string, string> PageQueryStrings
        {
            get { return _pageQueryStrings; }
            set { _pageQueryStrings = value; }
        }
     //   private int _firstpage;

        public int Firstpage
        {
            get { return 1; }
            //set { _firstpage = value; }
        }

     //   private int _lastpage;

        public int Lastpage
        {
            get { return TotalPage; }
            //set { _lastpage = value; }
        }

        #region fenye

        /// <summary>
        /// 写出分页
        /// </summary>
        /// <param name="pageCount">页数</param>
        /// <param name="currentPage">当前页</param>
        public static string GetPager(int pageCount, int currentPage)
        {
            return GetPager(pageCount, currentPage, null);
        }

        /// <summary>
        /// 写出分页
        /// </summary>
        /// <param name="pageCount">页数</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="FieldName">地址栏参数</param>
        /// <param name="FieldValue">地址栏参数值</param>
        /// <returns></returns>
        public static string GetPager(int pageCount, int currentPage, Dictionary<string, string> dic)
        {
            StringBuilder style = new StringBuilder();
            style.Append("<style type=\"text/css\">");
            style.Append(".pager{ background-color:#fff;}");
            style.Append(" .pager a { text-decoration:none; color:#039;padding:0 7px;} ");
            style.Append(" .pager a:hover { text-decoration:none;padding:0 7px;} ");
            style.Append(" .pager td  { text-decoration:none; border:#e5e5e5 solid 1px;color:#039; text-align:center; }");
            style.Append(" .pager td:hover  { background-color:#eee; border:#06c solid 1px;}");
            style.Append(" .pager .current{ background-color:#06c;color:White; border:#06c solid 1px; padding:0 7px; }");
            style.Append("</style>");

            string pString = "";
            StringBuilder _sb = new StringBuilder();
            if (null != dic)
            {
                foreach (KeyValuePair<string, string> item in dic)
                {
                    // pString += "&" + item.Key + "=" + item.Value;
                    _sb.Append("&").Append(item.Key).Append("=").Append(item.Value);
                }
            }
            pString = _sb.ToString();

            int stepNum = 5;
            int pageRoot = 1;
            pageCount = pageCount == 0 ? 1 : pageCount;
            currentPage = currentPage == 0 ? 1 : currentPage;

            StringBuilder sb = new StringBuilder(style.ToString());
            sb.Append("<table cellpadding=0 cellspacing=1 class=\"pager\">\r<tr>\r");
            sb.Append("<td class=pagerTitle>分页</td>\r");
            sb.Append("<td class=pagerTitle>" + currentPage.ToString() + "/" + pageCount.ToString() + "</td>\r");
            if (currentPage - stepNum < 2)
                pageRoot = 1;
            else
                pageRoot = currentPage - stepNum;
            int pageFoot = pageCount;
            if (currentPage + stepNum >= pageCount)
                pageFoot = pageCount;
            else
                pageFoot = currentPage + stepNum;
            if (pageRoot == 1)
            {
                if (currentPage > 1)
                {
                    sb.Append("<td><a href='?page=1" + pString + "' title='首页'>首页</a></td>\r");
                    sb.Append("<td><a href='?page=" + Convert.ToString(currentPage - 1) + pString + "' title='上页'>上页</a></td>\r");
                }
            }
            else
            {
                sb.Append("<td><a href='?page=1" + pString + "' title='首页'>首页</a>&nbsp;</td>");
                sb.Append("<td><a href='?page=" + Convert.ToString(currentPage - 1) + pString + "' title='上页'>上页</a></td>\r");
            }
            for (int i = pageRoot; i <= pageFoot; i++)
            {
                if (i == currentPage)
                {
                    sb.Append("<td class='current'>" + i.ToString() + "</td>\r");
                }
                else
                {
                    sb.Append("<td><a href='?page=" + i.ToString() + pString + "' title='第" + i.ToString() + "页'>" + i.ToString() + "</a></td>\r");
                }
                if (i == pageCount)
                    break;
            }
            if (pageFoot == pageCount)
            {
                if (pageCount > currentPage)
                {
                    sb.Append("<td><a href='?page=" + Convert.ToString(currentPage + 1) + pString + "' title='下页'>下页</a></td>\r");
                    sb.Append("<td><a href='?page=" + pageCount.ToString() + pString + "' title='尾页'>尾页</a></td>\r");
                }
            }
            else
            {
                sb.Append("<td><a href='?page=" + Convert.ToString(currentPage + 1) + pString + "' title='下页'>下页</a></td>\r");
                sb.Append("<td><a href='?page=" + pageCount.ToString() + pString + "' title='尾页'>尾页</a></td>\r");
            }
            sb.Append("</tr>\r</table>");

            return  sb.ToString();
        }



        /// <summary>
        /// 写出分页
        /// </summary>
        /// <param name="pageCount">总页数</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="prefix">上一页</param>
        /// <param name="suffix">下一页</param>
        /// <returns></returns>
        public static string GetHtmlPager(int pageCount, int currentPage, string prefix, string suffix)
        {

            StringBuilder style = new StringBuilder();
            style.Append("<style type=\"text/css\">");
            style.Append(".pager{ background-color:#fff;}");
            style.Append(" .pager a { text-decoration:none; color:#039;padding:0 7px;} ");
            style.Append(" .pager a:hover { text-decoration:none;padding:0 7px;} ");
            style.Append(" .pager td  { text-decoration:none; border:#e5e5e5 solid 1px;color:#039; text-align:center; }");
            style.Append(" .pager td:hover  { background-color:#eee; border:#06c solid 1px;}");
            style.Append(" .pager .current{ background-color:#06c;color:White; border:#06c solid 1px; padding:0 7px; }");
            style.Append("</style>");
            int stepNum = 4;
            int pageRoot = 1;
            pageCount = pageCount == 0 ? 1 : pageCount;
            currentPage = currentPage == 0 ? 1 : currentPage;

            StringBuilder sb = new StringBuilder(style.ToString());
            sb.Append("<table cellpadding=0 cellspacing=1 class=\"pager\">\r<tr>\r");
            sb.Append("<td class=pagerTitle>&nbsp;分页&nbsp;</td>\r");
            sb.Append("<td class=pagerTitle>&nbsp;" + currentPage.ToString() + "/" + pageCount.ToString() + "&nbsp;</td>\r");
            if (currentPage - stepNum < 2)
                pageRoot = 1;
            else
                pageRoot = currentPage - stepNum;
            int pageFoot = pageCount;
            if (currentPage + stepNum >= pageCount)
                pageFoot = pageCount;
            else
                pageFoot = currentPage + stepNum;
            if (pageRoot == 1)
            {
                if (currentPage > 1)
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + "1" + suffix + "' title='首页'>首页</a>&nbsp;</td>\r");
                    sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage - 1) + suffix + "' title='上页'>上页</a>&nbsp;</td>\r");
                }
            }
            else
            {
                sb.Append("<td>&nbsp;<a href='" + prefix + "1" + suffix + "' title='首页'>首页</a>&nbsp;</td>");
                sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage - 1) + suffix + "' title='上页'>上页</a>&nbsp;</td>\r");
            }
            for (int i = pageRoot; i <= pageFoot; i++)
            {
                if (i == currentPage)
                {
                    sb.Append("<td class='current'>&nbsp;" + i.ToString() + "&nbsp;</td>\r");
                }
                else
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + i.ToString() + suffix + "' title='第" + i.ToString() + "页'>" + i.ToString() + "</a>&nbsp;</td>\r");
                }
                if (i == pageCount)
                    break;
            }
            if (pageFoot == pageCount)
            {
                if (pageCount > currentPage)
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage + 1) + suffix + "' title='下页'>下页</a>&nbsp;</td>\r");
                    sb.Append("<td>&nbsp;<a href='" + prefix + pageCount.ToString() + suffix + "' title='尾页'>尾页</a>&nbsp;</td>\r");
                }
            }
            else
            {
                sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage + 1) + suffix + "' title='下页'>下页</a>&nbsp;</td>\r");
                sb.Append("<td>&nbsp;<a href='" + prefix + pageCount.ToString() + suffix + "' title='尾页'>尾页</a>&nbsp;</td>\r");
            }
            sb.Append("</tr>\r</table>");
            return sb.ToString();
        }

        #endregion

    }
}
