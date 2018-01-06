using CaseHelp_Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CaseHelp_Core.Tools
{
    /// <summary>
    /// XML数据文件操作 李浩军 2015/4/1
    /// </summary>
    public class XMLHelper
    {
        private static XMLHelper _xml = null;
        private static string newPath = "/App_Data/Rate.xml";
        //private MemoryCacheManager localCache = new MemoryCacheManager();
        private static RedisCacheManager localCache = new RedisCacheManager();


        private XMLHelper(string path = "/App_Data/UserSubmitlimit.xml")
        {
        }

        public static XMLHelper getInstance(string path)
        {
            newPath = path;
            if (null == _xml)
                _xml = new XMLHelper(path);
            return _xml;
        }

        #region 用户限制
        /// <summary>
        /// 查找 同一IP、用户名、类型的集合 在同一天
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int SubmitLimitList(UserSubmitlimit model)
        {
            string key = getKey(model);
            if (localCache.HasKey(key))
                return localCache.Get<int>(key);
            return 0;
        }

        /// <summary>
        /// 添加限制条目
        /// </summary>
        /// <param name="t">限制模型</param>
        /// <returns></returns>
        public bool LimitAdd(UserSubmitlimit model)
        {
            DateTime dt = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            TimeSpan ts = dt - DateTime.Now;
            string key = getKey(model);

            int count = 1;
            if (localCache.HasKey(key))
                count = localCache.Get<int>(key)+1;

            localCache.Remove(key);//先去掉，再加上
            localCache.Set(key, count, (long)ts.TotalSeconds);
            return true;
        }

        public string getKey(UserSubmitlimit model)
        {
            model.IP = string.IsNullOrEmpty(model.IP) ? "" : model.IP;
            model.BName = string.IsNullOrEmpty(model.BName) ? "" : model.BName;
            string mType = string.IsNullOrEmpty(model.Type.ToString()) ? "" : model.Type.ToString();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            return model.BName + model.IP + mType + date;
        }

        /// <summary>
        /// 删除今天之前的所有记录（需要服务方法每天自动运行一次即可）
        /// </summary>
        /// <returns></returns>
        public bool LimitDelete()
        {
                return false;
        }

        private static bool EndsWithToDay(UserSubmitlimit limit)
        {
            if (Convert.ToDateTime(limit.Time).Date < DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        public  string Read(string nodeName)
        {
            string rel = "1";
            try
            {
                XmlDocument doc = new XmlDocument();

                // 获得配置文件的全路径　　  
                string strFileName = newPath;
                doc.Load(strFileName);
                XmlNode node = doc.GetElementById("font");
                XmlNodeList nodes = node.ChildNodes;
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].NodeType == XmlNodeType.Element)
                    {
                        if (nodes[i].Name == nodeName)
                        {
                            rel = nodes[i].InnerText;
                            break;
                        }
                    }
                }
                return rel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }  
    }

    
    /// <summary>
    /// 用户提交限制类 李浩军 2015/4/1
    /// </summary>
    public class UserSubmitlimit
    {
        /// <summary>
        /// 提交限制的枚举类型（没有的自己加）
        /// </summary>
        public enum UserSubmitlimitType
        {
            用户投票 = 0,
            微博评论 = 1,
            相册评论 = 2,
            资讯评论 = 3,
            论坛评论 = 4,
            顾客需求提交 = 5,
            加入事业提交=6,
            产品评论=7,
            订单提交=8,
            知识问答=9,
            顾客留言=10,
            资讯发布=11,
            微博发布=12,
            微博点赞=13,
            资讯转载=14,
            微博转播=15,
            用户注册=16,
            文章投票=17,
            其它=100
        }
        /// <summary>
        /// 用户名称（如果用户名是游客请写人计算机主机名称）
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 提交的地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 提交的类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 提交的时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 被投票的人用户名称
        /// </summary>
        public string BName { get; set; }
    }
}
