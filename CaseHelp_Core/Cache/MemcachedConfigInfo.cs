using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CaseHelp_Core.Cache
{
    public sealed class MemcachedConfigInfo : ConfigurationSection
    {
        public static MemcachedConfigInfo GetConfig()
        {
            var section = (MemcachedConfigInfo)ConfigurationManager.GetSection("MemcachedConfig");
            return section;
        }
        public static MemcachedConfigInfo GetConfig(string sectionName)
        {
            var section = (MemcachedConfigInfo)ConfigurationManager.GetSection("MemcachedConfig");
            if (section == null)
            {
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            }
            return section;
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        [ConfigurationProperty("ServerList", IsRequired = true)]
        public string ServerList
        {
            get
            {
                return (string)base["ServerList"];
            }
            set
            {
                base["ServerList"] = value;
            }
        }

        /// <summary>
        /// 最大链接数
        /// </summary>
        [ConfigurationProperty("maxPoolSize", IsRequired = false, DefaultValue = 100)]
        public int MaxPoolSize
        {
            get
            {
                var maxPoolSize = (int)base["maxPoolSize"];
                return maxPoolSize > 0 ? maxPoolSize : 100;
            }
            set
            {
                base["maxPoolSize"] = value;
            }
        }

        /// <summary>
        /// 最小链接数
        /// </summary>
        [ConfigurationProperty("minPoolSize", IsRequired = false, DefaultValue = 10)]
        public int MinPoolSize
        {
            get
            {
                var minPoolSize = (int)base["minPoolSize"];
                return minPoolSize > 0 ? minPoolSize : 10;
            }
            set
            {
                base["minPoolSize"] = value;
            }
        }

        /// <summary>
        /// 连接到期时间，单位:秒
        /// </summary>
        [ConfigurationProperty("connectionTimeout", IsRequired = false, DefaultValue = "00:00:30")]
        public TimeSpan ConnectionTimeout
        {
            get
            {
                return (TimeSpan)base["connectionTimeout"];
            }
        }

        /// <summary>
        /// 到期时间，单位:秒
        /// </summary>
        [ConfigurationProperty("deadTimeout", IsRequired = false, DefaultValue = "00:02:00")]
        public TimeSpan DeadTimeout
        {
            get
            {
                return (TimeSpan)base["deadTimeout"];
            }
        }
    }
}