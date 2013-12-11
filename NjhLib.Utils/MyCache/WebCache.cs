using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils.MyCache
{
    public class WebCache
    {
        //缓存策略 对象初始化时使用 DefaultCacheStrategy 
        private static ICacheStrategy CacheStrategy = new DefaultCacheStrategy();

        /// <summary>
        /// 初始化缓存策略的装配
        /// </summary>
        static WebCache()
        {
            //配置信息中的缓存策略信息
            object cacheStrategy = System.Configuration.ConfigurationManager.AppSettings["CacheStrategy"];

            #region 初始化缓存策略
            if (cacheStrategy != null)
            {
                string _cacheStrategy = cacheStrategy.ToString();
                if (_cacheStrategy.Split(',').Length != 2)
                {
                    throw new ArgumentException("缓存策略设置项（AppSettings[\"CacheStrategy\"]）格式必须为：Tension.Cache.MemcachedCacheStrategy,Tension.Cache");
                }
                try
                {
                    object o = Activator.CreateInstance(Type.GetType(_cacheStrategy, false, true));
                    if (o is ICacheStrategy)
                    {
                        CacheStrategy = (ICacheStrategy)o;
                    }
                    else
                    {
                        throw new System.TypeLoadException("加载：" + cacheStrategy + "策略时失败，请确保所加载的缓存策略实现了Tension.Cache.ICacheStrategy 接口");
                    }

                }
                catch (TypeLoadException)
                {

                    throw new System.TypeLoadException("加载：" + cacheStrategy + "策略时失败，请检查所写类路径或程序集名称是否正确");
                }

            }
            #endregion

            //统一缓存过期时间
            object cacheTimeOut = System.Configuration.ConfigurationManager.AppSettings["CacheTimeOut"];

            #region 初始化缓存时间
            if (cacheTimeOut != null)
            {
                int seconds = 0;
                if (int.TryParse(cacheTimeOut.ToString(), out seconds))
                {
                    if (seconds < -1)
                    {
                        throw new ArgumentException("缓存时间：AppSettings[\"CacheTimeOut\"] 必须为大于等于-1的整数，当然为：" + cacheTimeOut);
                    }
                    else
                    {
                        CacheStrategy.TimeOut = seconds;
                    }
                }
                else
                {
                    throw new ArgumentException("缓存时间：AppSettings[\"CacheTimeOut\"] 必须为大于等于-1的整数，当然为：" + cacheTimeOut);
                }
            }
            #endregion
        }


        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        public static void Add(string key, object obj)
        {
            CacheStrategy.Add(key, obj);
        }

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        /// <param name="seconds">缓存时间 单位是秒</param>
        public static void Add(string key, object obj, int seconds)
        {
            CacheStrategy.Add(key, obj, seconds);
        }

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">待移除的对象</param>
        public static void Remove(string key)
        {
            CacheStrategy.Remove(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        public static object Retrieve(string key)
        {
            return CacheStrategy.Retrieve(key);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        public static T Retrieve<T>(string key)
        {
            return Retrieve<T>(key);
        }
    }
}

