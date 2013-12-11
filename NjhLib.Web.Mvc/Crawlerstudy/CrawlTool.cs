using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCrawler;
using NCrawler.DbServices;
using NCrawler.Events;
using NCrawler.Extensions;
using NCrawler.FileStorageServices;
using NCrawler.HtmlProcessor;
using NCrawler.HtmlProcessor.Extensions;
using NCrawler.HtmlProcessor.Interfaces;
using NCrawler.HtmlProcessor.Properties;
using NCrawler.IFilterProcessor;
using NCrawler.Interfaces;
using NCrawler.IsolatedStorageServices;
using NCrawler.LanguageDetection;
using NCrawler.LanguageDetection.Google;
using NCrawler.MP3Processor;
using NCrawler.Services;
using NCrawler.SitemapProcessor;
using NCrawler.Utils;
namespace NjhLib.Web.Mvc.Crawlerstudy
{
    public class CrawlTool
    {

        public NCrawler.Crawler GetCrawler(string Absoluteurl)
        {
            Crawler c = null;
            c = new Crawler(new Uri(Absoluteurl, UriKind.Absolute), new HtmlDocumentProcessor(), new DumpResult());
            return c;
        }

        public void CrawlerSetting(Crawler c)
        {
            c.MaximumThreadCount = 2;
            c.MaximumCrawlDepth = 1;
        }
        public void RunCrawl(Crawler c)
        {
            c.Crawl();
        }


    }
    /// <summary>
    /// 获取连接 啊
    /// </summary>
    class DumpResult : IPipelineStep
    {
        public void Process(Crawler crawler, PropertyBag propertyBag)
        {
            HttpContext.Current.Response.Write("<br>FindUrl:" + propertyBag.Step.Uri);
        }
    }
}