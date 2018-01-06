using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CaseHelp_Core.Cache
{
    public class RedisCache
    {
        private static int expire_hour = 0;
        private static int expire_min = 5;
        private static int expire_second = 0;

        private IDatabase db = null;

        private string redis_config = string.Empty;

        public RedisCache(string config)
        {
            if (!string.IsNullOrEmpty(config))
            {
                redis_config = config;
            }
            else
            {
                //Log.Error("RedisConfig Is Null");
                return;
            }
//#if DEBUG
//            redis_config = AppContext.GetAppSetting("RedisConfigDebug");
//#endif

            try
            {
                string[] temp = redis_config.Split(':');
                string ip = temp[0];
                int port = int.Parse(temp[1]);
                string pass = temp[2];

                ConfigurationOptions conf = new ConfigurationOptions
                {
                    Password = pass,
                    EndPoints = { { ip, port } }
                };

                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf);
                //redis.GetStatus()
                db = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                //Log.Error("Redis Connect Exception RedisConfig={1}, Error:{0}", ex.ToString(), redis_config);
            }
        }

        public bool AddString(string key, string val)
        {
            TimeSpan ts = new TimeSpan(expire_hour, expire_min, expire_second);
            return db.StringSet(key, val, ts);
        }

        public bool AddString(string key, string val, int second)
        {
            return db.StringSet(key, val, new TimeSpan(0, 0, second));
        }

        public string GetString(string key)
        {
            return db.StringGet(key);
        }

        public bool KeyExists(string key)
        {
            return db.KeyExists(key);
        }
        public bool HashExists(string key, string filed)
        {
            return db.HashExists(key, filed);
        }
        
        public bool AddSet(string set_key, string val)
        {
            return db.SetAdd(set_key, val);
        }

        public bool AddHash(string key, string filed, string val)
        {
            db.HashSet(key, filed, val);

            if (!HasLiveTime(key))
            {
                TimeSpan ts = new TimeSpan(expire_hour, expire_min, expire_second);
                db.KeyExpire(key, ts);
            }

            return true;
        }

        public bool AddHash(string key, string filed, string val, int min)
        {
            db.HashSet(key, filed, val);
            if (!HasLiveTime(key))
            {
                TimeSpan ts = new TimeSpan(0, min, 0);
                db.KeyExpire(key, ts);
            }

            return true;
        }

        public string GetHash(string key, string filed)
        {
            return db.HashGet(key, filed);
        }

        public bool HasLiveTime(string key)
        {
            return db.KeyTimeToLive(key).HasValue;
        }

        public bool Status
        {
            get
            {
                bool tag = true;
                if (db == null)
                {
                    tag = false;
                }
                return tag;
            }
        }
    }
}
