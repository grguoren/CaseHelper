using System;
using System.Web;
using System.Web.Caching;
using System.Collections.Generic;

namespace CaseHelp_Core.Helper
{
    public class HttpCacheHelper
    {
        private TimeSpan CacheDuration { get; set; }

        private static Object SyncObject = new Object();

        public static void SetCache(String key, object value, TimeSpan CacheDuration)
        {
            lock (SyncObject)
            {
                DateTime expiration = DateTime.Now.Add(CacheDuration);
                HttpRuntime.Cache.Insert(key, value, null, expiration, TimeSpan.Zero, CacheItemPriority.Default, null);
            }
        }

        public static void SetCache(String key, object value)
        {
            lock (SyncObject)
            {
                DateTime expiration = DateTime.Now.Add(new TimeSpan(0, 30, 0));
                HttpRuntime.Cache.Insert(key, value, null, expiration, TimeSpan.Zero, CacheItemPriority.Default, null);
            }
        }

        public static bool CacheKeyHas(String key)
        {
            return HttpRuntime.Cache.Get(key) != null;
        }

        public static int Count()
        {
            return HttpRuntime.Cache.Count;
        }

        public static object GetFromCache(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        public static void RemoveCache(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
