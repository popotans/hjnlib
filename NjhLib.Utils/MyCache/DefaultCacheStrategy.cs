using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils.MyCache
{
    public class DefaultCacheStrategy : ICacheStrategy
    {
        /// <summary>
        /// ASP.NET 运行时缓存对象
        /// </summary>
        protected static volatile System.Web.Caching.Cache cache = System.Web.HttpRuntime.Cache;

        #region ICacheStrategy 成员


        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        public void Add(string key, object obj)
        {
            Add(key, obj, TimeOut);
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        /// <param name="seconds">缓存时间 单位是秒</param>
        public void Add(string key, object obj, int seconds)
        {
            if (!string.IsNullOrEmpty(key))
            {
                if (seconds == -1)
                {
                    cache.Insert(key, key, null, DateTime.MaxValue, TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                }
                else
                {
                    cache.Insert(key, key, null, DateTime.Now.AddSeconds(seconds), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.High, null);
                }
            }
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">待移除的对象</param>
        public void Remove(string key)
        {
            if (!string.IsNullOrEmpty(key))
                cache.Remove(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        public object Retrieve(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return cache.Get(key);
            } return null;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        public T Retrieve<T>(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return (T)cache.Get(key);
            } return default(T);
        }

        /// <summary>
        /// 到期时间,单位：秒
        /// </summary>
        public int TimeOut
        {
            get;
            set;
        }

        #endregion
    }
}
