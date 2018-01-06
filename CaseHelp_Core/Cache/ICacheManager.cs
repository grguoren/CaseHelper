using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseHelp_Core.Cache
{
    public interface ICacheManager
    {
        /// <summary>
        /// 根据key获取缓存对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        void Set(string key, object data, long cacheTime);

        /// <summary>
        /// 查询key是否被缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasKey(string key);

        /// <summary>
        /// 从缓存移除
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void Clear();

        /// <summary>
        /// 保存数据DB文件到硬盘
        /// </summary>
        void Save();

        /// <summary>
        /// 异步保存数据DB文件到硬盘
        /// </summary>
        void SaveAsync();
    }
}
