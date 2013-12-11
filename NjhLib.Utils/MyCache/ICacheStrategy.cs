using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NjhLib.Utils.MyCache
{
    /// <summary>
    /// 缓存策略接口
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        void Add(string key, object obj);

        /// <summary>
        /// 新增缓存
        /// </summary>
        /// <param name="key">缓存对象键</param>
        /// <param name="obj">待缓存的对象</param>
        /// <param name="seconds">缓存时间 单位是秒</param>
        void Add(string key, object obj, int seconds);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">待移除的对象</param>
        void Remove(string key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        object Retrieve(string key);

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="key">缓存对象的key</param>
        /// <returns>缓存对象 如果缓存对象不存在则返回null</returns>
        T Retrieve<T>(string key);

        /// <summary>
        /// 到期时间,单位：秒
        /// </summary>
        int TimeOut { set; get; }
    }
}
