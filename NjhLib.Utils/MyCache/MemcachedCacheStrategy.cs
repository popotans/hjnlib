using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
namespace NjhLib.Utils.MyCache
{
    public class MemcachedCacheStrategy : ICacheStrategy
    {
        /// <summary>
        /// Memcached 客户端
        /// </summary>
        public MemcachedClient MemcachedClient
        {
            get
            {
                return new MemcachedClient();
            }
        }


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
                    MemcachedClient.Store(Enyim.Caching.Memcached.StoreMode.Set, key, obj);
                }
                else
                {
                    MemcachedClient.Store(Enyim.Caching.Memcached.StoreMode.Set, key, obj, new TimeSpan(0, 0, seconds));

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
                MemcachedClient.Remove(key);
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
                return MemcachedClient.Get(key);
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
                return MemcachedClient.Get<T>(key);
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
