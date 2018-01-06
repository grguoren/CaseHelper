using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;
using System.IO;

namespace CaseHelp_Core.Helper
{
    public static class CommonEx
    {
        /// <summary>
        /// 去掉字符串中间与两端的全部全角和半角空格
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ClearWriteSpace(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
                return src;
            return Regex.Replace(src, @"( |　)+", "");
        }
        /// <summary>
        /// 验证字符串是否严格是手机号码格式
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsStrictCellphone(this string src)
        {
            if (string.IsNullOrWhiteSpace(src)) return false;
            if (Regex.IsMatch(src, @"(\d)\1{6,}"))//相同数字连续7次以上就不是手机号码
                return false;
            if (Regex.IsMatch(src, @"^13[0-9]{1}[0-9]{8}$"))//130-139开头
                return true;
            if (Regex.IsMatch(src, @"^15[^4]{1}[0-9]{8}$"))//15开头，但不是154
                return true;
            if (Regex.IsMatch(src, @"^18[0-9]{1}[0-9]{8}$"))//18开头，但不是184
                return true;
            if (Regex.IsMatch(src, @"^14[0-9]{1}[0-9]{8}$"))//140-149开头
                return true;
            if (Regex.IsMatch(src, @"^17[0-9]{1}[0-9]{8}$"))//170-179开头  新加
                return true;
            //不符合上面所有条件就不包含手机号码
            return false;
        }
        /// <summary>
        /// 验证字符串是否包含一个手机号码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsCellphone(this string src)
        {
            if (string.IsNullOrWhiteSpace(src)) return false;
            if (Regex.IsMatch(src, @"1[0-9]{2}[0-9]{8}"))//130-139开头
                return true;
            //不符合上面所有条件就不包含手机号码
            return false;
        }
        /// <summary>
        /// 验证字符串是否严格是固定电话号码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsStrictTelephone(this string src)
        {
            if (string.IsNullOrWhiteSpace(src)) return false;
            if (Regex.IsMatch(src, @"^0{1}[1-9]{1}[0-9]{1,2}\-[1-9]{1}[0-9]{6,7}(((\-|转)[^(\-|转)].+)*)$"))
                return true;
            if (Regex.IsMatch(src, @"^\(0{1}[1-9]{1}[0-9]{1,2}\)[1-9]{1}[0-9]{6,7}(((\-|转)[^(\-|转)].+)*)$"))
                return true;
            if (Regex.IsMatch(src, @"^0{1}[1-9]{1}[0-9]{1,2}[1-9]{1}[0-9]{6,7}(((\-|转)[^(\-|转)].+)*)$") && !src.IsCellphone())
                return true;
            if (Regex.IsMatch(src, @"^400[0123456789-]+$"))
                return true;
            if (Regex.IsMatch(src, @"^800[0123456789-]+$"))
                return true;
            return false;
        }
        /// <summary>
        /// 验证字符串是否包含一个固定电话号码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsTelephone(this string src)
        {
            if (string.IsNullOrWhiteSpace(src)) return false;
            if (Regex.IsMatch(src, @"0{1}[1-9]{1}[0-9]{1,2}\-[1-9]{1}[0-9]{6,7}"))
                return true;
            if (Regex.IsMatch(src, @"\(0{1}[1-9]{1}[0-9]{1,2}\)[1-9]{1}[0-9]{6,7}"))
                return true;
            if (Regex.IsMatch(src, @"0{1}[1-9]{1}[0-9]{1,2}[1-9]{1}[0-9]{6,7}") && !src.IsCellphone())
                return true;
            if (Regex.IsMatch(src, @"400[0123456789-]+"))
                return true;
            if (Regex.IsMatch(src, @"800[0123456789-]+"))
                return true;
            return false;
        }
        /// <summary>
        /// 验证字符串是否包含一个电话号码（手机或座机）
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string src)
        {
            return src.IsCellphone() || src.IsTelephone();
        }
        /// <summary>
        /// 是不是Int型的
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsInt(this string source)
        {
            Regex regex = new Regex(@"^(-){0,1}\d+$");
            if (regex.Match(source).Success)
            {
                if ((long.Parse(source) > 0x7fffffffL) || (long.Parse(source) < -2147483648L))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// 验证字符串是否严格是银行帐号格式
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsBankCard(this string src)
        {
            if (string.IsNullOrWhiteSpace(src)) return false;
            //清除空格
            src = Regex.Replace(src, @" ", "");
            //去掉检验位
            string code = src.Substring(0, src.Length - 1);
            if (!Regex.IsMatch(code, @"^\d+$")) return false;
            char[] chs = code.ToCharArray();
            int luhmSum = 0;
            for (int i = chs.Length - 1, j = 0; i >= 0; i--, j++)
            {
                int k = chs[i] - '0';
                if (j % 2 == 0)
                {
                    k *= 2;
                    k = k / 10 + k % 10;
                }
                luhmSum += k;
            }
            char r = (luhmSum % 10 == 0) ? '0' : (char)((10 - luhmSum % 10) + '0');
            return r.ToString() == src.Substring(src.Length - 1, 1);
        }
        /// <summary>
        /// 验证字符串是否严格是一个车牌号码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsStrictLicensePlate(this string src)
        {
            return Regex.IsMatch(src, @"^[\u4E00-\u9FA5]{1}[A-Z]{1} ?[a-zA-Z0-9]{5}$");
        }
        /// <summary>
        /// 验证字符串是否中文
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsCHNCharacter(this string src)
        {
            return Regex.IsMatch(src, "^[\u4E00-\u9FA5]+$");
        }  
        /// <summary>
        /// 中文加小数点
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsCHNCharacterMore(this string src)
        {
            return Regex.IsMatch(src, "^[\u4e00-\u9fa5]+(·[\u4e00-\u9fa5]+)*$");
        }
        /// <summary>
        /// 只能数字和英文
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool isusername(this string src)
        {
            return Regex.IsMatch(src, "^[a-z0-9]+$");
        }
        //public static bool (this string src) 
        //{
        //   //return Regex.IsMatch(src, "/^[a-z0-9]+$/g");
        //    return Regex.IsMatch(src, "/[\x01-\xFF]*/");
        //}
        #region 身份证号码格式验证
        /// <summary>
        /// 验证字符串是否严格是一个身份证号码
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static bool IsStrictIDNumber(this string src)
        {
            if (src.Length == 15)
            {
                return checkIDNumber15(src);
            }
            else if (src.Length == 18)
            {
                return checkIDNumber18(src);
            }
            return false;
        }
        //验证15位身份证号码
        private static bool checkIDNumber15(string src)
        {
            long n = 0;
            if (long.TryParse(src, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(src.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = src.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        //验证18位身份证号码
        private static bool checkIDNumber18(string src)
        {
            long n = 0;
            if (long.TryParse(src.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(src.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(src.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = src.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2,X").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = src.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != src.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        #endregion

        #region 得到一周的周一和周日的日期
        /// <summary>          
        /// C#日期函数计算本周的周一日期          
        /// </summary>          
        /// <returns></returns>          
        public static DateTime GetMondayDate()
        { return GetMondayDate(DateTime.Now); }
        /// <summary>           
        /// 计算本周周日的日期           
        /// </summary>           
        /// <returns></returns>           
        public static DateTime GetSundayDate()
        { return GetSundayDate(DateTime.Now); }
        /// <summary>           
        /// 计算某日起始日期（礼拜一的日期）           
        /// </summary>           
        /// <param name=someDate>该周中任意一天</param>           
        /// <returns>返回礼拜一日期，后面的具体时、分、秒和传入值相等</returns>           
        public static DateTime GetMondayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Monday;
            if (i == -1)          // i值 > = 0 ，因为枚举原因，Sunday排在最前，此时Sunday-Monday=-1，必须+7=6。 
                i = 6;
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Subtract(ts);
        }
        /// <summary>          
        /// 计算某日结束日期（礼拜日的日期）           
        /// </summary>           
        /// <param name=someDate>该周中任意一天</param>           
        /// <returns>返回礼拜日日期，后面的具体时、分、秒和传入值相等</returns>           
        public static DateTime GetSundayDate(DateTime someDate)
        {
            int i = someDate.DayOfWeek - DayOfWeek.Sunday;
            if (i != 0) i = 7 - i;// 因为枚举原因，Sunday排在最前，相减间隔要被7减。               
            TimeSpan ts = new TimeSpan(i, 0, 0, 0);
            return someDate.Add(ts);
        }
        #endregion
        /// <summary>
        /// 将源字符串中包含的数字转换为手机拨号链接
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ConvertToDialLink(this string source)
        {
            Regex reg = new Regex(@"(0{1}[1-9]{1}[0-9]{1,2}\-[1-9]{1}[0-9]{6,7}|\(0{1}[1-9]{1}[0-9]{1,2}\)[1-9]{1}[0-9]{6,7}|0{1}[1-9]{1}[0-9]{1,2}[1-9]{1}[0-9]{6,7}|400[0123456789-]+|1[3584]{1}[0-9]{9})(?=\D|$)?");
            MatchCollection collect = reg.Matches(source);
            if (collect != null)
            {
                foreach (Match m in collect)
                {
                    string number = m.ToString();
                    string link = string.Format("<a href=\"tel:{0}\">{1}</a>", Regex.Replace(number, @"\D", ""), number);
                    source = source.Replace(number, link);
                }
            }
            return source;
        }
        /// <summary>
        /// 是否包含平台认为的特殊字符
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool ContainSpecialCharacter(this string source)
        {
            return Regex.IsMatch(source, @"[#￥@&\$\^\*\＋－×÷﹢﹣±／＝∥∠≌∽≦≧≒﹤﹥≈≡≠＝,?，。.≤≥＜＞≮≯∷∶∫∮∝∞∧∨∑∏∪∩∈∵∴⊥∥∠⌒⊙√∟⊿㏒㏑％‰㏄㏎㏑㏒㏕℡％‰℃℉°′″＄￡￥￠♂♀℅]+");
        }
        //是否邮箱格式
        public static bool ISEmail(this string source)
        {
            if (string.IsNullOrEmpty(source)) return false;
            return Regex.IsMatch(source, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }
        //是否qq号码
        public static bool ISQQ(this string source)
        {
            if (string.IsNullOrEmpty(source)) return false;
            return Regex.IsMatch(source, @"^\d{5,10}$");
        }


        // <summary>
        /// 屏蔽非法字符串（如果有出现非法字符,那么用"***"来替换）
        /// </summary>
        /// <param name="strText">要检测的字符串</param>
        /// <returns>返还一个健康的字符</returns>
        public static string CheckKeyword(string strText)
        {
            IList<string> list = new List<string>();     //实例化一个数据集
            string strpath = System.Web.HttpContext.Current.Server.MapPath("function/keyword.txt");   //获取文本文档路径
            int a = strpath.LastIndexOf("IFSns");
            int b = strpath.IndexOf("function");
            string m = strpath.Substring(a + 5, b - a - 6);
            string PathTxt = strpath.Replace(m, "");    //获取调用这个方法的相对路径
            FileStream fs = new FileStream(PathTxt, FileMode.Open, FileAccess.Read);  //打开txt文档,将数据存到文件流中
            StreamReader reader = new StreamReader(fs, Encoding.Default); //文件读取
            string strLine = reader.ReadLine();
            while (strLine != null && strLine.Length != 0)    //有数据
            {
                list.Add(strLine.Trim().Replace(" ", ""));    //如果读取到的数据有空格,则删除空格,并且存到string数据集中
                strLine = reader.ReadLine();   //每读取一次,从该行下一行开始继续读取
            }
            fs.Close();  //关闭文件流
            foreach (string str in list)    //循环遍历文件流
            {
                if (strText.Contains(str))
                {
                    int lg = str.Length;
                    string sg = "";
                    for (int i = 0; i < lg; i++)
                    {
                        sg += "*";
                    }
                    strText = strText.Replace(str, sg);  //如果含有txt文档中的关键字,则替换为"***"
                }
            }
            return strText;
        }
       
        #region DataTable转换成实体
        /// <summary>
        /// DataTable转为实体List
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static IList<T> ToEntityList<T>(this DataTable dt) where T : class, new()
        {
            IList<T> list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T entity = row.ToEntity<T>();
                if (entity != null)
                    list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// DataTable转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataTable dt) where T : class ,new()
        {
            return dt.Rows[0].ToEntity<T>();
        }

        /// <summary>
        /// DataRow转为实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T ToEntity<T>(this DataRow dr) where T : class ,new()
        {
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            Array.ForEach<PropertyInfo>(typeof(T).GetProperties(), p => { if (dr.Table.Columns.IndexOf(p.Name) != -1 && p.CanWrite) prlist.Add(p); });
            T entity = new T();

            prlist.ForEach(p =>
            {
                if (dr[p.Name] != DBNull.Value)
                {
                    if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))//可为空
                    {
                        Type temp = p.PropertyType.GetGenericArguments()[0];
                        if (p.PropertyType.GetGenericArguments()[0].IsEnum)//可为空的枚举
                            p.SetValue(entity, Enum.ToObject(Type.GetType(temp.FullName + "," + temp.Namespace), dr[p.Name]), null);//加上",命名空间 "
                        else
                            p.SetValue(entity, Convert.ChangeType(dr[p.Name], Type.GetType(temp.FullName)), null);
                    }
                    else //不可为空
                    {
                        if (p.PropertyType.IsEnum)
                            p.SetValue(entity, Enum.ToObject(Type.GetType(p.PropertyType.FullName + "," + p.PropertyType.Namespace), dr[p.Name]), null);
                        else
                            if (dr[p.Name] != DBNull.Value)
                            {
                                p.SetValue(entity, dr[p.Name], null);
                            }
                    }
                }
            });
            return entity;
        }
        #endregion
    }
}