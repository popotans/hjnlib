using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Memcached.ClientLibrary;
using System.Collections;
namespace NjhLib.Utils.MyCache
{
    public class MemcacheClient2
    {
        private class Config
        {
            public string Id;

            public string[] Servers;

            public int InitConnections;

            public int MinConnections;

            public int MaxConnections;

            public int SocketConnectTimeout;

            public int SocketTimeout;

            public int MaintenanceSleep;

            public bool FailOver;

            public bool Nagle;

            public bool EnableCompression;

            public TimeSpan Duration;
            public Config(string path)
            {
                //XmlUtil util = new XmlUtil(path);
                //int h = 0, m = 0, s = 0;
                //string[] duarr = util.GetAttrVal("Duration").Split(':');
                //h = int.Parse(duarr[0]); s = int.Parse(duarr[1]); s = int.Parse(duarr[2]);
                //this.Duration = new TimeSpan(h, m, s);
                //this.EnableCompression = bool.Parse(util.GetAttrVal("EnableCompression"));
                //this.FailOver = bool.Parse(util.GetAttrVal("FailOver"));

                this.Duration = new TimeSpan(1, 11, 11);
                this.EnableCompression = false;
                this.FailOver = false;
                this.Id = "q11";
                this.InitConnections = 12;
                this.MaintenanceSleep = 30;
                this.MaxConnections = 55;
                this.MinConnections = 3;
                this.Nagle = false;
                this.Servers = new string[] { 
                "192.168.112.199:11211",
                "192.168.112.200:11211"
                };
                this.SocketConnectTimeout = 1000;
                this.SocketTimeout = 3333;
            }
        }

        private class XmlUtil
        {
            private static string _path;
            private XmlDocument _doc;
            private XmlNodeList xns;
            public XmlUtil(string path)
            {
                _path = path;
                if (_doc == null)
                {
                    _doc = new XmlDocument();
                    _doc.LoadXml(System.IO.File.ReadAllText(_path));
                    xns = _doc.SelectNodes("/configuration/memcached/property");
                }
            }

            public string GetAttrVal(string key)
            {
                string rs = "";
                string name = "";
                int i = 0;
                foreach (XmlNode xn in xns)
                {
                    name = xn.Attributes[i].Value;
                    if (string.Compare(name, key, true) == 0)
                    {
                        rs = xn.Attributes["value"].Value;
                        break;
                    } i++;
                }
                return rs;
            }
        }

        private SockIOPool pool;
        private MemcachedClient mc;
        private TimeSpan? duration;
        Config _cfg;

        /// <summary>
        /// 这个客户端是跟怒网上例子自己整理的
        /// </summary>
        /// <param name="configPath"></param>
        public MemcacheClient2(string configPath)
        {
            Config config = new Config(configPath);
            _cfg = config;

            pool = SockIOPool.GetInstance(config.Id);
            pool.SetServers(config.Servers);
            pool.InitConnections = config.InitConnections;//初始化链接数
            pool.MinConnections = config.MinConnections;//最少链接数
            pool.MaxConnections = config.MaxConnections;//最大连接数
            pool.SocketConnectTimeout = config.SocketConnectTimeout;//Socket链接超时时间
            pool.SocketTimeout = config.SocketTimeout;// Socket超时时间
            pool.MaintenanceSleep = config.MaintenanceSleep;//维护线程休息时间
            pool.Failover = config.FailOver; //失效转移(一种备份操作模式)
            pool.Nagle = config.Nagle;//是否用nagle算法启动socket
            pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
            pool.Initialize();
            if (config.Duration.TotalSeconds <= 0)
                this.duration = null;
            else
                this.duration = config.Duration;

            mc = new MemcachedClient();
            mc.PoolName = config.Id;
            mc.EnableCompression = config.EnableCompression;
        }
        public bool Set(string key, object value)
        {
            if (duration.HasValue)
                return mc.Set(key, value, DateTime.Now.Add(duration.Value));
            else
            {
                return mc.Set(key, value);
            }
        }

        public bool Set(string key, object value, int seconds)
        {
            return mc.Set(key, value, DateTime.Now.AddSeconds(seconds));
        }

        public bool Add(string key, object value)
        {
            if (duration.HasValue)
                return mc.Add(key, value, DateTime.Now.Add(duration.Value));
            else
            {
                return mc.Add(key, value);
            }
        }

        public bool Replace(string key, string value)
        {
            if (duration.HasValue)
                return mc.Replace(key, value, DateTime.Now.Add(duration.Value));
            else
            {
                return mc.Replace(key, value);
            }
        }

        public object Get(string key)
        {
            return mc.Get(key);
        }

        public bool Delete(string key)
        {
            return mc.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>永远返回true</returns>
        public bool Delete(string[] keys)
        {
            foreach (var s in keys)
            {
                this.Delete(s);
            }
            return true;
        }

        public bool FlushAll()
        {
            return mc.FlushAll();
        }


        public bool KeyExists(string key)
        {
            return mc.KeyExists(key);
        }

        /// <summary>
        /// 获取当前缓存键值所存储在的服务器
        /// </summary>
        /// <param name="key">当前缓存键</param>
        /// <returns>当前缓存键值所存储在的服务器</returns>
        public string GetSocketHost(string key)
        {
            string hostName = "";
            SockIO sock = null;
            try
            {
                sock = SockIOPool.GetInstance(_cfg.Id).GetSock(key);
                if (sock != null)
                {
                    hostName = sock.Host;
                }
            }
            finally
            {
                if (sock != null)
                    sock.Close();
            }
            return hostName;
        }

        /// <summary>
        /// 获取有效的服务器地址
        /// </summary>
        /// <returns>有效的服务器地</returns>
        public string[] GetConnectedSocketHost()
        {
            SockIO sock = null;
            IList<string> hosts = new List<string>();
            foreach (string hostName in _cfg.Servers)
            {
                if (!string.IsNullOrEmpty(hostName))
                {
                    try
                    {
                        sock = SockIOPool.GetInstance(_cfg.Id).GetConnection(hostName);

                        if (sock != null)
                        {
                            // connectedHost = Discuz.Common.Utils.MergeString(hostName, connectedHost);
                            hosts.Add(hostName);
                        }
                    }
                    finally
                    {
                        if (sock != null)
                            sock.Close();
                    }
                }
            }
            return hosts.ToArray<string>();
        }

        /// <summary>
        /// 获取服务器端缓存的数据信息
        /// </summary>
        /// <returns>返回信息</returns>
        public ArrayList GetStats()
        {
            ArrayList arrayList = new ArrayList();
            foreach (string server in _cfg.Servers)
            {
                arrayList.Add(server);
            }
            return GetStats(arrayList, MemcachedStats.Default, null);
        }

        /// <summary>
        /// 获取服务器端缓存的数据信息
        /// </summary>
        /// <param name="serverArrayList">要访问的服务列表</param>
        /// <returns>返回信息</returns>
        public ArrayList GetStats(ArrayList serverArrayList, MemcachedStats statsCommand, string param)
        {
            ArrayList statsArray = new ArrayList();
            // param = Utils.StrIsNullOrEmpty(param) ? "" : param.Trim().ToLower();
            if (null == param) param = "";
            string commandstr = "stats";
            //转换stats命令参数
            switch (statsCommand)
            {
                case MemcachedStats.Reset: { commandstr = "stats reset"; break; }
                case MemcachedStats.Malloc: { commandstr = "stats malloc"; break; }
                case MemcachedStats.Maps: { commandstr = "stats maps"; break; }
                case MemcachedStats.Sizes: { commandstr = "stats sizes"; break; }
                case MemcachedStats.Slabs: { commandstr = "stats slabs"; break; }
                case MemcachedStats.Items: { commandstr = "stats"; break; }
                case MemcachedStats.CachedDump:
                    {
                        string[] statsparams = param.Split(' ');// Utils.SplitString(param, " ");
                        if (statsparams.Length == 2)
                            // if (Utils.IsNumericArray(statsparams))
                            commandstr = "stats cachedump  " + param;

                        break;
                    }
                case MemcachedStats.Detail:
                    {
                        if (string.Equals(param, "on") || string.Equals(param, "off") || string.Equals(param, "dump"))
                            commandstr = "stats detail " + param.Trim();

                        break;
                    }
                default: { commandstr = "stats"; break; }
            }
            //加载返回值
            Hashtable stats =// MemCachedManager.CacheClient.Stats(serverArrayList, commandstr);
            mc.Stats(serverArrayList);
            foreach (string key in stats.Keys)
            {
                statsArray.Add(key);
                Hashtable values = (Hashtable)stats[key];
                foreach (string key2 in values.Keys)
                {
                    statsArray.Add(key2 + ":" + values[key2]);
                }
            }
            return statsArray;
        }

    }

    /// <summary>
    /// Stats命令行参数
    /// </summary>
    public enum MemcachedStats
    {
        /// <summary>
        /// stats : 显示服务器信息, 统计数据等
        /// </summary>
        Default = 0,
        /// <summary>
        /// stats reset : 清空统计数据
        /// </summary>
        Reset = 1,
        /// <summary>
        /// stats malloc : 显示内存分配数据
        /// </summary>
        Malloc = 2,
        /// <summary>
        /// stats maps : 显示"/proc/self/maps"数据
        /// </summary>
        Maps = 3,
        /// <summary>
        /// stats sizes
        /// </summary>
        Sizes = 4,
        /// <summary>
        /// stats slabs : 显示各个slab的信息,包括chunk的大小,数目,使用情况等
        /// </summary>
        Slabs = 5,
        /// <summary>
        /// stats items : 显示各个slab中item的数目和最老item的年龄(最后一次访问距离现在的秒数)
        /// </summary>
        Items = 6,
        /// <summary>
        /// stats cachedump slab_id limit_num : 显示某个slab中的前 limit_num 个 key 列表
        /// </summary>
        CachedDump = 7,
        /// <summary>
        /// stats detail [on|off|dump] : 设置或者显示详细操作记录   on:打开详细操作记录  off:关闭详细操作记录 dump: 显示详细操作记录(每一个键值get,set,hit,del的次数)
        /// </summary>
        Detail = 8
    }
}
