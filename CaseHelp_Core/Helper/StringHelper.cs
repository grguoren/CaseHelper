using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 字符串操作工具集
    /// </summary>
    public static class StringHelper
    {
        private readonly static string siteUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["HostMechine"].ToString();
        /// <summary>
        /// 从字符串中的尾部删除指定的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="removedString"></param>
        /// <returns></returns>
        public static string Remove(string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0)
                    throw new Exception("原字符串中不包含移除字符串！");
                string result = sourceString;
                int lengthOfSourceString = sourceString.Length;
                int lengthOfRemovedString = removedString.Length;
                int startIndex = lengthOfSourceString - lengthOfRemovedString;
                string tempSubString = sourceString.Substring(startIndex);
                if (tempSubString.ToUpper() == removedString.ToUpper())
                {
                    result = sourceString.Remove(startIndex, lengthOfRemovedString);
                }
                return result;
            }
            catch
            {
                return sourceString;
            }
        }

        /// <summary>
        /// 获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1].ToString();
            }
            return result;
        }

        /// <summary>
        /// 获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0].ToString();
            }
            return result;
        }

        /// <summary>
        /// 去掉最后一个逗号
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        /// <summary>
        /// 删除不可见字符
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            System.Text.StringBuilder sBuilder = new System.Text.StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// 获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        /// <summary>
        ///		获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string[]">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="findString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A int value...
        /// </returns>
        public static int GetStringCount(string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray);
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        /// <summary>
        ///     获取某一字符串在字符串中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string">
        ///     <para>
        ///         原字符串
        ///     </para>
        /// </param>
        /// <param name="findString" type="string">
        ///     <para>
        ///         匹配字符串
        ///     </para>
        /// </param>
        /// <returns>
        ///     匹配字符串数量
        /// </returns>
        public static int GetStringCount(string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }

        /// <summary>
        /// 截取从startString开始到原字符串结尾的所有字符   
        /// </summary>
        /// <param name="sourceString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <param name="startString" type="string">
        ///     <para>
        ///         
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public static string GetSubString(string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString);
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        public static string GetSubString(string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                    beginRemovedString = "";

                if (sourceString.LastIndexOf(endRemovedString, sourceString.Length - endRemovedString.Length) < 0)
                    endRemovedString = "";

                int startIndex = beginRemovedString.Length;
                int length = sourceString.Length - beginRemovedString.Length - endRemovedString.Length;
                if (length > 0)
                {
                    return sourceString.Substring(startIndex, length);
                }
                return sourceString;
            }
            catch
            {
                return sourceString; ;
            }
        }

        /// <summary>
        /// 按字节数取出字符串的长度
        /// </summary>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字符串的字节数</returns>
        public static int GetByteCount(string strTmp)
        {
            int intCharCount = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intCharCount = intCharCount + 2;
                }
                else
                {
                    intCharCount = intCharCount + 1;
                }
            }
            return intCharCount;
        }

        /// <summary>
        /// 按字节数要在字符串的位置
        /// </summary>
        /// <param name="intIns">字符串的位置</param>
        /// <param name="strTmp">要计算的字符串</param>
        /// <returns>字节的位置</returns>
        public static int GetByteIndex(int intIns, string strTmp)
        {
            int intReIns = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (System.Text.UTF8Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intReIns = intReIns + 2;
                }
                else
                {
                    intReIns = intReIns + 1;
                }
                if (intReIns >= intIns)
                {
                    intReIns = i + 1;
                    break;
                }
            }
            return intReIns;
        }

        /// <summary>
        /// 截取指定长度
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string CutRightString(string inputString, int len)
        {
            if (string.IsNullOrEmpty(inputString))
                return string.Empty;

            var input = Reverse(inputString);
            var output = CutString(input, len);
            return Reverse(output);
        }

        /// <summary>
        /// 从包含中英文的字符串中截取固定长度的一段，inputString为传入字符串，len为截取长度（一个汉字占两个位）。
        /// </summary>
        public static string CutString(this string inputString, int len)
        {
            if (inputString == null || inputString == "")
            {
                return "";
            }

            inputString = inputString.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(inputString);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < inputString.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + "...";
            }
            else
            {
                return inputString;
            }
        }

        /// <summary>
        /// 从包含中英文的字符串中截取固定长度的一段，inputString为传入字符串，len为截取长度（一个汉字占两个位）。
        /// </summary>
        public static string CutString(this string inputString, int len, string end)
        {
            inputString = inputString.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(inputString);
            if (myByte.Length > len)
            {
                string result = "";
                for (int i = 0; i < inputString.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(result);
                    if (tempByte.Length < len)
                    {
                        result += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return result + end;
            }
            else
            {
                return inputString;
            }
        }

        /// <summary>
        /// 去除文本中的html代码。
        /// </summary>
        public static string RemoveHtml(this string inputString)
        {

            if (inputString != null)
            {
                inputString = inputString.Replace("&nbsp;", "");
                return System.Text.RegularExpressions.Regex.Replace(inputString, @"[<].*?[>]", "");
            }
            return "";
        }

        /// <summary>
        /// 替换文本中的html代码。
        /// </summary>
        public static string ReplaceHtml(this string Htmlstring)
        {
            if (Htmlstring != null)
            {
                Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
                return Htmlstring;
            }
            return "";
        }

        /// <summary>
        /// 半角转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>        
        public static string ToSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }


        /// <summary>
        /// 全角转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static string HtmlEncode(string str, bool encodeBlank = true)
        {
            if ((str == "") || (str == null))
                return "";

            StringBuilder builder1 = new StringBuilder(str);

            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            builder1.Replace("\t", "&nbsp; &nbsp; ");

            if (encodeBlank)
                builder1.Replace(" ", "&nbsp;");

            builder1.Replace("\r", "");
            builder1.Replace("\n\n", "<p><br/></p>");
            builder1.Replace("\n", "<br />");
            return builder1.ToString();
        }

        public static string TextEncode(string s)
        {
            StringBuilder builder1 = new StringBuilder(s);
            builder1.Replace("&", "&amp;");
            builder1.Replace("<", "&lt;");
            builder1.Replace(">", "&gt;");
            builder1.Replace("\"", "&quot;");
            builder1.Replace("'", "&#39;");
            return builder1.ToString();
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        /// <summary>
        /// 是否完全包含id
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool YContains(this string[] source, string value)
        {
            if (source == null || source.Count() <= 0) return false;
            foreach (var item in source)
            {
                if (item == value) return true;
            }
            return false;
        }
        /// <summary>
        /// 修改时间显示形式
        /// </summary>
        /// <param name="nows"></param>
        /// <returns></returns>
        public static string FormartTimeSpan(this DateTime nows)
        {
            TimeSpan span = DateTime.Now - nows;
            string result = "";

            if (span.TotalDays > 1)
            {
                if (span.TotalDays > 30)
                {
                    if (Convert.ToInt16(span.TotalDays / 30) > 12)
                    {
                        result = nows.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        result = String.Format("{0}个月前", Convert.ToInt16(span.TotalDays / 30));
                    }
                }
                else
                {
                    result = String.Format("{0}天前", Convert.ToInt16(span.TotalDays));
                }
            }
            else if (span.TotalHours > 1)
            {
                result = String.Format("{0}小时前", Convert.ToInt16(span.TotalHours));
            }
            else if (span.TotalMinutes > 1)
            {
                result = String.Format("{0}分钟前", Convert.ToInt16(span.TotalMinutes));
            }
            else
            {
                result = String.Format("{0}秒钟前", Convert.ToInt16(span.TotalSeconds));
            }
            return result;
        }
        /// <summary>
        /// 去掉图片路径_c
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string removetoC(this string str)
        {
            str = str.Replace("_c.", ".");
            return str;
        }

        public static string AddtoC(this string str)
        {
            if (!str.Contains("_c."))
            {
                str = str.Replace(".jpg", "_c.jpg");
                str = str.Replace(".gif", "_c.gif");
                str = str.Replace(".png", "_c.png");
            }
            return str;
        }



        /// <summary>
        /// 对url的改造 用户界面
        /// </summary>
        /// <param name="str"></param>
        /// <param name="str2"></param>
        /// <param name="str3"></param>
        /// <param name="hostname"></param>
        /// <returns></returns>
        public static string UserTwoUrl(this string str, string str2, string str3)
        {
            string strx = "http://www.";
            if (str.Contains("/user/"))
            {
                strx += siteUrl + "/" + str3;
            }
            else
            {
                if (!string.IsNullOrEmpty(str2))
                {
                    strx += str2 + "." + siteUrl + "/" + str3;
                }
                else
                {
                    strx += siteUrl + "/" + str3;
                }
            }
            return strx;
        }

        #region C#执行DOS命令
        public static string Execute(string dosCommand)
        {
            return ExecuteCmd(dosCommand);
        }
        public static string ExecuteCmd(string command)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(command + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();
            string str = "";
            StreamReader reader = p.StandardOutput;
            string line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                str += line + "  ";
                line = reader.ReadLine();
            }

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();
            if (output.Contains("exit"))
            {
                var strArray = Regex.Split(output, "exit");

                string UCodeSrc = strArray[1].Trim();
                return UCodeSrc.Contains("weixin.qq.com") || UCodeSrc.Contains("wechat.com") ? (UCodeSrc.Length > 50 ? "" : UCodeSrc) : "";
            }
            else
            {
                return "";
            }
        }
        #endregion


        #region xiao
        /// <summary>
        /// 去除文本中的html代码。
        /// </summary>
        public static string RemoveHtml(this string inputString, int len)
        {

            if (inputString != null)
            {
                inputString = inputString.Replace("&nbsp;", "");
                inputString = System.Text.RegularExpressions.Regex.Replace(inputString, @"[<].*?[>]", "");

                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
                int tempLen = 0;
                string tempString = "";
                byte[] s = ascii.GetBytes(inputString);
                for (int i = 0; i < s.Length; i++)
                {
                    if ((int)s[i] == 63)
                        tempLen += 2;
                    else
                        tempLen += 1;
                    if (i + 1 <= inputString.Length)
                    {
                        tempString += inputString.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                    if (tempLen > len)
                    {
                        tempString += "…";
                        break;
                    }
                }
                return tempString;
            }
            return "";
        }
        #endregion




        /// <summary>
        /// 价格显示
        /// </summary>
        /// <param name="html"></param>
        /// <param name="Price"></param>
        /// <param name="PreferentialPrice"></param>
        /// <returns></returns>
        public static string PriceString(decimal Price, decimal PreferentialPrice, int bid, int unit = 0, int preferential = 0)
        {
            if (Price <= 0 && PreferentialPrice <= 0)
            {
                return "暂无报价";
            }
            else
            {
                string fuhao = "";
                if (bid == 3680)
                {
                    fuhao = "HK$";
                }
                else if (bid == 378 || bid == 171 || bid == 721)
                {
                    fuhao = "$";
                }
                else
                {
                    fuhao = "￥";
                }
                if (unit > 0)
                {
                    if (PreferentialPrice > 0)
                    {
                        if (bid != 106)
                        {
                            ///需要显示优惠价格
                            if (preferential > 0)
                            {
                                return fuhao + Price.ToString() + "元 (优惠价格:" + PreferentialPrice.ToString() + "元)";
                            }
                            else
                            {
                                return fuhao + Price.ToString() + "元";
                            }
                        }
                        else
                        {
                            return fuhao + Price.ToString() + "元";
                        }
                    }
                    else
                    {
                        return fuhao + Price.ToString() + "元";
                    }
                }
                else
                {
                    if (PreferentialPrice > 0)
                    {
                        return fuhao + PreferentialPrice.ToString();
                    }
                    else
                    {
                        return fuhao + Price.ToString();
                    }
                }
            }
        }
        public static string PriceString(int bid)
        {
            string fuhao = "";
            switch (bid)
            {
                case 3680: fuhao = "HK$"; break;
                case 378: fuhao = "$"; break;
                case 171: fuhao = "$"; break;
                case 721: fuhao = "$"; break;
                default: fuhao = "￥"; break;
            }
            return fuhao;
        }
    }
}
