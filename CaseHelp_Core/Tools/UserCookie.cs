using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CaseHelp_Core.Tools
{
    public class UserCookie
    {
        #region "自定义参数"
        /// <summary>
        /// 自定义字符 用于第一层加解密密匙
        /// </summary>
        /// <remarks></remarks>
        private const string CustomCode = "zhihunet@2013";
        /// <summary>
        /// Cookie作用域
        /// </summary>
        /// <remarks></remarks>
        private static string CookieDomain =  System.Web.Configuration.WebConfigurationManager.AppSettings["Www_domain"].ToString();
        //private const string CookieDomain = "";
        /// <summary>
        /// 编码
        /// </summary>
        /// <remarks></remarks>
        private static Encoding Encoder = Encoding.UTF8;
        /// <summary>
        /// 用户名的正则检测 我的是：首位由字母或者汉字构成，由字母、数字、下划线、和汉字的 2-20位的字符 组合而成 的
        /// </summary>
        /// <remarks></remarks>
        private const string RegexUserName = "[a-zA-Z\\u4e00-\\u9fa5][\\w\\u4e00-\\u9fa5]{1,19}";
        /// <summary>
        /// 区域化信息设置
        /// </summary>
        /// <remarks></remarks>
        private static System.Globalization.CultureInfo Format = new System.Globalization.CultureInfo("zh-CN", true);
        #endregion
        #region "回调参数"

        /// <summary>
        /// 是否在线
        /// </summary>
        /// <remarks></remarks>
        public bool Online
        {
            get { return _Online; }
        }
        private bool _Online = false;
        /// <summary>
        /// 用户ID (Online=true情况下使用)
        /// </summary>
        /// <remarks></remarks>
        public int Id
        {
            get { return _Id; }
        }
        private int _Id;
        /// <summary>
        /// 用户名 (Online=true情况下使用)
        /// </summary>
        /// <remarks></remarks>
        public string Name
        {
            get { return _Name; }
        }
        private string _Name;
        /// <summary>
        /// 有效期是否为7天
        /// </summary>
        /// <remarks></remarks>
        public bool IsWeek
        {
            get { return _isWeek; }
        }
        private bool _isWeek;
        #endregion
        public UserCookie() { }
        /// <summary>
        /// 初始化用户信息(检测当前请求用户是否登录)
        /// </summary>
        /// <remarks></remarks>
        public UserCookie(string CookieName)
        {
            //读取cookie
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                //存在cookie
                string value = cookie.Values["u"];
                string key = cookie.Values["i"];
                string tname = cookie.Values["n"];
                string _tname = cookie.Values["n"];
                string id = cookie.Values["id"];

                cookie = null;
                if (tname != null && value != null && key != null && Regex.IsMatch(key, "^[1-8A-H]{2}(-[1-8A-H]{2}){7}$", System.Text.RegularExpressions.RegexOptions.None))
                {
                    //存在对应键值
                    byte[] keybyte = toByte(DEChar(key));
                    //解密密匙的后8位字节 由参数i构成                    
                    if (keybyte != null)
                    {
                        byte[] autocode;
                        //解密密匙的前16位字节 由用户UserAgent，用户名，自定义字符 组合而成 的 md5 
                        using (System.Security.Cryptography.MD5CryptoServiceProvider m = new System.Security.Cryptography.MD5CryptoServiceProvider())
                        {
                            autocode = m.ComputeHash(Encoder.GetBytes(string.Format(Format, "{0}_{2}_zh{1}", "360", tname, CustomCode)));
                        }
                        byte[] keyboard = new byte[keybyte.Length + autocode.Length];
                        autocode.CopyTo(keyboard, 0);
                        keybyte.CopyTo(keyboard, autocode.Length);
                        value = DesDecrypt(value, keyboard);
                        if (value.Length > 0)
                        {
                            //解密成功 第一层合法
                            Match values = Regex.Match(value, "^(?<md5>[\\w]{32})(?<isweek>[01])(?<id>[\\d]{1,10})(?<name>" + RegexUserName + ")\\|(?<exp>[\\d]{1,19})$");
                            if (values.Success)
                            {
                                long LostDateTime = 0;
                                if (int.TryParse(values.Groups["id"].Value, out this._Id) && this._Id > 0 && long.TryParse(values.Groups["exp"].Value, out LostDateTime) && LostDateTime > 0)
                                {
                                    //解密后的字符串格式正确
                                    this._isWeek = (values.Groups["isweek"].Value == "1");
                                    //此md5用于验证解密后的字符串 由用户id，用户名，cookie写入时间，自定义字符串 以及有效期是否是1周 组合
                                    string md5 = MD5Public(string.Format(Format, "{0}{5}{1}{2}:jeson.{3};{4}", values.Groups["id"].Value, values.Groups["exp"].Value, values.Groups["name"].Value, CookieDomain, _isWeek, CustomCode));
                                    if (md5 == values.Groups["md5"].Value)
                                    {
                                        //md5正常
                                        double lostdate = (DateTime.Now - new DateTime(LostDateTime)).TotalMinutes;
                                        int l_a = 0;
                                        if (_isWeek)//1天
                                        {
                                            l_a = 1440;
                                        }
                                        else
                                        {
                                            l_a = 60;
                                        }
                                        if (lostdate > 0 && lostdate < l_a)
                                        {
                                            //cookie在有效期内
                                            this._Name = _tname;
                                            this._Id = values.Groups["id"].Value == null ? 0 : Convert.ToInt32(values.Groups["id"].Value);
                                            this._Online = true;
                                            if (lostdate > 15)
                                            {
                                                //cookie以写入超过15分钟，从新写入1次cookie
                                                new UserCookie().DelUserCookie(CookieName);
                                                SetUser(this._Id, this._Name, CookieName, this._isWeek, autocode);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this._Id = 0;
                                        this._Name = null;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 初始化(写入新用户)
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="username">用户名</param>
        /// <param name="isweek">是否保持一周登录状态</param>
        /// <remarks></remarks>
        public UserCookie(int userId, string userName, string CookieName, bool isWeek)
        {
            new UserCookie().DelUserCookie(CookieName);
            SetUser(userId, userName, CookieName, isWeek, null);
            this._Id = userId;
            this._Name = userName;
            this._isWeek = isWeek;
            this._Online = true;
        }

        public void DelUserCookie(string CookieName)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Values.Add("id", null);
            cookie.Values.Add("n", null);
            cookie.Values.Add("u", null);
            cookie.Values.Add("i", null);
            cookie.Path = "/";
            cookie.Expires = DateTime.Now.AddYears(-1); ;
            cookie.Domain = CookieDomain;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public void getUserCookie(string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                //存在cookie

                string tname = cookie.Values["n"];
                string id = cookie.Values["id"];
                this._Name = HttpUtility.UrlDecode(tname);
                this._Id = string.IsNullOrEmpty(id) ? 0 : Convert.ToInt32(id);
                this._Online = true;
            }
            else
            {
                this._Name = null;
                this._Id = 0;
                this._Online = false;

            }
        }

        public void setUserCookie(int userid, string username, string CookieName, bool isweek)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[CookieName];
            cookie.Values.Add("id", userid.ToString());
            cookie.Values.Add("n", HttpUtility.UrlEncode(username));
            cookie.Values.Add("u", null);
            cookie.Values.Add("i", null);
            cookie.Path = "/";
            cookie.Expires = DateTime.Now.AddDays(1);
            cookie.Domain = CookieDomain;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }
        /// <summary>
        /// 写入用户
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="username">用户名</param>
        /// <param name="isweek">是否保持一周登录状态</param>
        /// <param name="autocode"></param>
        /// <remarks></remarks>
        private static void SetUser(int userid, string username, string CookieName, bool isweek, byte[] autocode)
        {
            if (autocode == null)
            {
                //解密密匙的前16位字节 由用户UserAgent，用户名，自定义字符 组合而成 的 md5 
                using (System.Security.Cryptography.MD5CryptoServiceProvider m = new System.Security.Cryptography.MD5CryptoServiceProvider())
                {
                    autocode = m.ComputeHash(Encoder.GetBytes(string.Format(Format, "{0}_{2}_zh{1}", "360", username, CustomCode)));
                    m.Clear();
                }
            }
            DateTime expires = default(DateTime);
            char isweekint;
            if (isweek)//一天
            {
                expires = DateTime.Now.AddDays(1);
                isweekint = '1';
            }
            else
            {
                expires = DateTime.Now.AddHours(1);
                isweekint = '0';
            }
            //解密密匙的后8位字节 随机生成参数i
            byte[] rbyte = Encoder.GetBytes(RandomCode(8));
            byte[] keyboard = new byte[24];
            autocode.CopyTo(keyboard, 0);
            //组合密匙 长度为24位
            rbyte.CopyTo(keyboard, autocode.Length);
            autocode = null;
            string exp = DateTime.Now.Ticks.ToString("D", Format);
            //加密字符串
            string value = DesEncrypt(string.Format(Format, "{4}{0}{1}zh{2}|{3}", isweekint, userid, username, exp, MD5Public(string.Format(Format, "{0}{5}{1}zh{2}:jeson.{3};{4}", userid, exp, username, CookieDomain, isweek, CustomCode))), keyboard);
            keyboard = null;
            string key = ENChar(System.BitConverter.ToString(rbyte));
            //混淆参数i
            rbyte = null;
            //写入cookie
            HttpCookie cookie = HttpContext.Current.Response.Cookies[CookieName];
            cookie.Values.Add("id", userid.ToString());
            cookie.Values.Add("n", username);
            cookie.Values.Add("u", value);
            cookie.Values.Add("i", key);
            cookie.Values.Add("p", "");
            cookie.Path = "/";
            cookie.Expires = expires;
            cookie.Domain = CookieDomain;
            HttpContext.Current.Response.Cookies.Set(cookie);
        }




        /// <summary>
        /// TripleDESC解密
        /// </summary>
        /// <param name="strText">待解密字符串</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static internal string DesDecrypt(string strText, byte[] key)
        {
            try
            {
                using (System.Security.Cryptography.TripleDESCryptoServiceProvider provider = new System.Security.Cryptography.TripleDESCryptoServiceProvider())
                {
                    provider.Key = key;
                    provider.Mode = System.Security.Cryptography.CipherMode.ECB;
                    byte[] inputBuffer = Convert.FromBase64String(strText);
                    return Encoder.GetString(provider.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length)).Trim();
                }
            }
            catch (CryptographicException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
            catch (DecoderFallbackException)
            {
                return string.Empty;
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }
            catch (FormatException)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// TripleDESC加密
        /// </summary>
        /// <param name="strText">待加密字符串</param>
        /// <param name="key">密匙</param>
        /// <returns></returns>
        /// <remarks></remarks>
        protected static internal string DesEncrypt(string strText, byte[] key)
        {
            try
            {
                using (System.Security.Cryptography.TripleDESCryptoServiceProvider provider = new System.Security.Cryptography.TripleDESCryptoServiceProvider())
                {
                    provider.Key = key;
                    provider.Mode = System.Security.Cryptography.CipherMode.ECB;
                    byte[] bytes = Encoder.GetBytes(strText);
                    return Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
                }
            }
            catch (CryptographicException)
            {
                return string.Empty;
            }
            catch (ArgumentNullException)
            {
                return string.Empty;
            }
            catch (DecoderFallbackException)
            {
                return string.Empty;
            }
            catch (ArgumentException)
            {
                return string.Empty;
            }
            catch (FormatException)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <returns>返回加密后字符串</returns>
        /// <remarks></remarks>
        private static string MD5Public(string str)
        {
            using (System.Security.Cryptography.MD5CryptoServiceProvider m = new System.Security.Cryptography.MD5CryptoServiceProvider())
            {
                byte[] MDByte = m.ComputeHash(Encoder.GetBytes(str));
                return System.BitConverter.ToString(MDByte).Replace("-", "");
            }
        }
        /// <summary>
        /// 随机数
        /// </summary>
        /// <remarks></remarks>
        private static Random Randoms = new Random();
        /// <summary>
        /// 随机字符集合
        /// </summary>
        /// <remarks></remarks>
        private static char[] xarrChar = new char[] {
                    '0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string RandomCode(int length)
        {
            string str = "";
            int mlength = xarrChar.Length;
            for (int i = 0; i < length; i++)
            {
                str += xarrChar[Randoms.Next(0, mlength)];
            }
            return str;
        }
        /// <summary>
        /// 16进制字符串转Byte数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static byte[] toByte(string value)
        {
            try
            {
                string[] chars = value.Split('-');
                int length = chars.Length;
                byte[] byte_ = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    byte_[i] = Convert.ToByte(chars[i], 16);
                }
                return byte_;
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
            catch (OverflowException)
            {
                return null;
            }
        }
        /// <summary>
        /// TripleDESC-部分密匙 字符混淆 如果要修改下面的字符 请注意修改上面的正则
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string ENChar(string value)
        {
            value = value.Replace("A", "H");
            value = value.Replace("B", "G");
            value = value.Replace("0", "B");
            value = value.Replace("9", "A");
            return value;
        }
        /// <summary>
        /// TripleDESC-部分密匙 字符反混淆 如果要修改下面的字符 请注意修改上面的正则
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string DEChar(string value)
        {
            value = value.Replace("A", "9");
            value = value.Replace("B", "0");
            value = value.Replace("G", "B");
            value = value.Replace("H", "A");
            return value;
        }
    }
}
