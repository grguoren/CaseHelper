using System;
using System.Web;
using System.Net;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Compression;

namespace CaseHelp_Core.Tools
{
    public class RequestHelper
    {
        /// <summary>
        /// 延迟毫秒数
        /// </summary>
        protected int defer;
        /// <summary>
        /// 与请求相关的cookie（用于保持session）
        /// </summary>
        public CookieContainer cookies = new CookieContainer();

        public RequestHelper()
        {
            this.cookies = new CookieContainer();
            //默认为1秒
            this.defer = 1000;
        }

        /// <summary>
        /// 发送Post类型请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public WebResponse doPost(string url, string postData)
        {
            return this.doPost(url, postData, null);
        }

        /// <summary>
        /// 发送Post类型请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public WebResponse doPost(string url, string postData, string referer)
        {
            HttpWebRequest webRequest = null;
            try
            {
                Thread.Sleep(this.defer);
                byte[] paramByte = Encoding.UTF8.GetBytes(postData); // 转化
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.CookieContainer = this.cookies;
                webRequest.Method = "POST";
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Referer = referer;
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
                webRequest.ContentLength = paramByte.Length;
                webRequest.Timeout = 50000;
                Stream newStream = webRequest.GetRequestStream();
                newStream.Write(paramByte, 0, paramByte.Length);    //写入参数
                newStream.Close();
                return webRequest.GetResponse();
            }
            catch (Exception ce)
            {
                throw ce;
            }
            //finally
            //{
            //    if (webRequest != null)
            //    {
            //        webRequest.Abort();
            //    }
            //}
        }
        /// <summary>
        /// 发送Get类型请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public WebResponse doGet(string url)
        {
            HttpWebRequest webRequest = null;
            try
            {
                //Thread.Sleep(1000);
                 webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.CookieContainer = this.cookies;
                webRequest.Method = "get";
                webRequest.Timeout = 5000;
                return webRequest.GetResponse();
            }
            catch (Exception ce)
            {
                throw ce;
            }
            //finally
            //{
            //    if (webRequest != null)
            //    {
            //        webRequest.Abort();
            //    }
            //}
        }
        /// <summary>
        /// 发送Get类型请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">参数</param>
        /// <returns></returns>
        public WebResponse doGet(string url, string referer)
        {
            HttpWebRequest webRequest = null;
            try
            {
                //Thread.Sleep(1000);
                webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.CookieContainer = this.cookies;
                webRequest.Method = "get";
                webRequest.Referer = referer;
                webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; CIBA)";
                return webRequest.GetResponse();
            }
            catch (Exception ce)
            {
                throw ce;
            }
            //finally
            //{
            //    if (webRequest != null)
            //    {
            //        webRequest.Abort();
            //    }
            //}
        }
        /// <summary>
        /// 根据相应返回字符串
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string ResponseToString(WebResponse response)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                string ContentType = response.ContentType.Trim();
                if (ContentType.IndexOf("utf-8") != -1)
                    encoding = Encoding.UTF8;
                else if (ContentType.IndexOf("utf-7") != -1)
                    encoding = Encoding.UTF7;
                else if (ContentType.IndexOf("unicode") != -1)
                    encoding = Encoding.Unicode;

                StreamReader stream = new StreamReader(response.GetResponseStream(), encoding);
                string code = stream.ReadToEnd();
                stream.Close();
                response.Close();
                return code;
            }
            catch (Exception ce)
            {
                throw ce;
            }
        }

        /// <summary>
        /// 根据相应返回字符串
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string ResponseToStringWithGzip(WebResponse response)
        {
            try
            {
                Encoding encoding = Encoding.Default;
                string ContentType = response.ContentType.Trim().ToLower();
                if (ContentType.IndexOf("utf-8") != -1)
                    encoding = Encoding.UTF8;
                else if (ContentType.IndexOf("utf-7") != -1)
                    encoding = Encoding.UTF7;
                else if (ContentType.IndexOf("unicode") != -1)
                    encoding = Encoding.Unicode;
                else if (ContentType.IndexOf("gbk") != -1)
                    encoding = Encoding.GetEncoding("GBK");

                StreamReader stream;
                GZipStream gz = null;

                if (!string.IsNullOrEmpty(response.Headers["Content-Encoding"]) && response.Headers["Content-Encoding"].ToLower() == "gzip")
                {
                    gz = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    stream = new StreamReader(gz, encoding);
                }
                else
                {
                    stream = new StreamReader(response.GetResponseStream(), encoding);
                }
                string code = stream.ReadToEnd();
                stream.Close();
                if (gz != null)
                    gz.Close();
                response.Close();
                return code;
            }
            catch (Exception ce)
            {
                throw ce;
            }
            
        }
    }
}