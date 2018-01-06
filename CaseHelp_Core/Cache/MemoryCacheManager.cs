using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace CaseHelp_Core.Cache
{
    public class MemoryCacheManager : ICacheManager
    {
        protected ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        public T Get<T>(string key)
        {
            return (T)Cache.GetCacheItem(key).Value;
        }

        public void Set(string key, object data, long cacheTime)
        {
            if (data == null)
                return;
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
            //Cache.Add(new CacheItem(key, data), policy);
            Cache.Set(new CacheItem(key, data), policy);
        }

        public bool HasKey(string key)
        {
            return Cache.Contains(key);
        }

        public void Remove(string key)
        {
            Cache.Remove(key);
        }


        public void Clear()
        {
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }
        }

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        public void Save()
        {
            
        }

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        public void SaveAsync()
        {
            
        }
    }
}