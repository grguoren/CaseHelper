using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CaseHelp_Core.Helper
{
    public class IPHelper
    {
        #region IP地址互转整数
        /// <summary>
        /// 将IP地址转为整数形式
        /// </summary>
        /// <returns>整数</returns>
        public static long IP2Long(IPAddress ip)
        {
            int x = 3;
            long o = 0;
            foreach (byte f in ip.GetAddressBytes())
            {
                o += (long)f << 8 * x--;
            }
            return o;
        }
        /// <summary>
        /// 将整数转为IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        public static IPAddress Long2IP(long l)
        {
            byte[] b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                b[3 - i] = (byte)(l >> 8 * i & 255);
            }
            return new IPAddress(b);
        }
        #endregion
        /// <summary>
        /// 取得客户端真实IP。如果有代理则取第一个非内网地址   
        /// </summary>
        public static string ClientIP
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                //FileHelper.FileAdd(HttpContext.Current.Server.MapPath("/IPRemarkEx.txt"), "时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "，HTTP_X_FORWARDED_FOR：" + HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ",REMOTE_ADDR:" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                if (result != null && result != String.Empty)
                {
                    //可能有代理     
                    if (result.IndexOf(".") == -1)    //没有"."肯定是非IPv4格式     
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1) //有","，估计多个代理。取第一个不是内网的IP。
                        {
                            result = result.Replace(" ", "").Replace("\"", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //不是内网的地址     
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式     
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP     
                    }
                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return string.IsNullOrEmpty(result) ? "127.0.0.1" : result;
            }
        }

        public static string NewClientIP
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理     
                    if (result.IndexOf(".") == -1)    //没有"."肯定是非IPv4格式     
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1) //有","，估计多个代理。取第一个不是内网的IP。
                        {
                            result = result.Replace(" ", "").Replace("\"", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //找到不是内网的地址     
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式     
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP     
                    }
                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return result;
            }
        }

        public static string SftClientIP
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                FileHelper.FileAdd(HttpContext.Current.Server.MapPath("/IPRemarkEx.txt"), "时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "，HTTP_X_FORWARDED_FOR：" + HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ",REMOTE_ADDR:" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                if (result != null && result != String.Empty)
                {
                    //可能有代理     
                    if (result.IndexOf(".") == -1)    //没有"."肯定是非IPv4格式     
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1) //有","，估计多个代理。取第一个不是内网的IP。
                        {
                            result = result.Replace(" ", "").Replace("\"", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i]) && temparyip[i].Substring(0, 3) != "10." && temparyip[i].Substring(0, 7) != "192.168" && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];    //不是内网的地址     
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式     
                            return result;
                        else
                            result = null;    //代理中的内容 非IP，取IP     
                    }
                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                return string.IsNullOrEmpty(result) ? "127.0.0.1" : result;
            }
        }
        /// <summary>
        ///判断是否是IP格式 
        ///判断是否是IP地址格式 0.0.0.0 
        /// </summary>
        public static bool IsIPAddress(string str1)
        {
        if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
        string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
        Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
        return regex.IsMatch(str1);
        }
    }
}
