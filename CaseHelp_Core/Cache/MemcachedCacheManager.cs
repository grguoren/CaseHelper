using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Memcached;

namespace CaseHelp_Core.Cache
{
    public class MemcachedCacheManager : ICacheManager, IDisposable
    {
        private static ICacheClient _client ;
        private bool _disposed = false;
        private object _lock = new object();

        public MemcachedCacheManager()
        {
            _client =  MemcachedManager.GetClient();
        }

        public T Get<T>(string key)
        {
            lock (_lock)
            {
                if (_client != null)
                {
                    return _client.Get<T>(key);
                }
                else
                {
                    return default(T);
                }
            }
        }

        public void Set(string key, object data, long cacheTime)
        {
            lock (_lock)
            {
                if (data == null)
                    return;
                DateTime span = DateTime.Now + TimeSpan.FromSeconds(cacheTime);
                if (_client != null)
                {
                    _client.Set(key, data, span);
                }
            }
        }

        public bool HasKey(string key)
        {
            lock (_lock)
            {
                return false;
            }
        }

        public void Remove(string key)
        {
            lock (_lock)
            {
                if (_client != null)
                {
                    _client.Remove(key);
                }
            }
        }
      


        public void Clear()
        {
            if (_client != null)
            {
                _client.FlushAll();
            }
        }

        public void Save()
        {
        }

        public void SaveAsync()
        {
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_client != null)
                    {
                        _client.Dispose();
                        _client = null;
                    }
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
