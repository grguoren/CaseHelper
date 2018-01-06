using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching.Configuration;
using System.Configuration;

namespace CaseHelp_Core.Cache
{
    public class MemcachedManager
    {
        /// <summary>
        /// Memcached配置文件信息
        /// </summary>
        private static readonly MemcachedConfigInfo memcachedConfigInfo = MemcachedConfigInfo.GetConfig();
        private static ICacheClient _client;

        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        static MemcachedManager()
        {
            CreateManager();
        }

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            MemcachedClientConfiguration conf = new MemcachedClientConfiguration();
            var serverList = SplitString(memcachedConfigInfo.ServerList, ",");
            foreach (string host in serverList)
                conf.AddServer(host);
            conf.Protocol = Enyim.Caching.Memcached.MemcachedProtocol.Binary;
            conf.SocketPool.ConnectionTimeout = memcachedConfigInfo.ConnectionTimeout;
            conf.SocketPool.DeadTimeout = memcachedConfigInfo.DeadTimeout;
            conf.SocketPool.MaxPoolSize = memcachedConfigInfo.MaxPoolSize;
            conf.SocketPool.MinPoolSize = memcachedConfigInfo.MinPoolSize;

            _client = new MemcachedClientCache(conf);
        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public static ICacheClient GetClient()
        {
            if (_client == null)
            {
                CreateManager();
            }
            return _client;
        }
    }
}

