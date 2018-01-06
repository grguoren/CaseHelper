using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using ServiceStack.Redis;

namespace CaseHelp_Core.Cache
{
    public class RedisCacheManager : ICacheManager, IDisposable
    {
        protected IRedisClient redisClient { get; private set; }
        private bool _disposed = false;
        private object _lock = new object();

        public RedisCacheManager()
        {
            redisClient = RedisManager.GetClient();
        }

        public T Get<T>(string key)
        {
            lock (_lock)
            {
                return redisClient.Get<T>(key);
            }
        }

        public void Set(string key, object data, long cacheTime)
        {
            lock (_lock)
            {
                if (data == null)
                    return;
                DateTime span = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
                redisClient.Add(key, data, span);
            }
        }

        public bool HasKey(string key)
        {
            lock (_lock)
            {
                return redisClient.ContainsKey(key);
            }
        }

        public void Remove(string key)
        {
            lock (_lock)
            {
                redisClient.Remove(key);
            }
        }


        public void Clear()
        {
            redisClient.FlushAll();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    redisClient.Dispose();
                    redisClient = null;
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            redisClient.Save();
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            redisClient.SaveAsync();
        }
    }
}