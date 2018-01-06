using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CaseHelp_Core.Tools
{
    public class FilterWordHelper
    {
        private string _words = string.Empty;
        private string _minganci = string.Empty;
        private Dictionary<string, Dictionary<string, int[]>> _badWords;
        private Dictionary<int, List<string>> _result;
        const string type1 = "_";               //含1个字符
        const string type2 = "*";               //含0-5个字符

        /// <summary>
        /// 词库
        /// </summary>
        public Dictionary<string, Dictionary<string, int[]>> badWords
        {
            get { return _badWords; }
            set { _badWords = value; }
        }

        /// <summary>
        /// 敏感词字符串
        /// </summary>
        public string minganci
        {
            get { return _minganci; }
            set { _minganci = value; }
        }

        /// <summary>
        /// 敏感词级别，列表
        /// </summary>
        public Dictionary<int, List<string>> result
        {
            get { return _result; }
        }

        /// <summary>
        /// 要查找的字符串
        /// </summary>
        public string words
        {
            get { return _words; }
            set { _words = value; }
        }

        //加载词库
        private void LoadDic()
        {
            if (this.minganci == "" || this.minganci == null) return;

            Dictionary<string, int[]> wordlist = null;
            string[] sp = { "\r\n" };
            string[] keys = this.minganci.Split(sp, StringSplitOptions.RemoveEmptyEntries);
            string[] split = null;
            bool con = false;//记录是否含有通配符,0,无　1,有

            int count = 0;//记录敏感字包含通配符时的字数

            foreach (string s in keys)
            {
                split = s.Split('|');
                if (badWords.ContainsKey(s[0].ToString()))
                {
                    if (!badWords[s[0].ToString()].ContainsKey(split[0]))
                    {
                        badWords[s[0].ToString()].Add(ReplaceWord(split[0], out con, out count), new int[3] { int.Parse(split[1]), con ? 1 : 0, count });
                    }
                }
                else
                {
                    wordlist = new Dictionary<string, int[]>();
                    wordlist.Add(ReplaceWord(split[0], out con, out count), new int[3] { int.Parse(split[1]), con ? 1 : 0, count });
                    badWords.Add(s[0].ToString(), wordlist);
                }
            }
        }

        //替换为正则

        private string ReplaceWord(string str, out bool contain, out int count)
        {
            string s = "";
            int c = 0;
            bool b = false;
            if (str.Contains(type1))
            {
                c += str.Length;
                s = str.Replace("_", "(.){1}");
                b = true;
            }
            if (str.Contains(type2))
            {
                c += str.Length + 4;
                s = str.Replace("*", "(.){0,5}");
                b = true;
            }
            contain = b;
            count = c;
            return s == "" ? str : s;
        }

        /// <summary>
        /// 过滤(这里只是把敏感词找出来，不支持对敏感词进行处理)
        /// </summary>
        public void Filter()
        {
            if (this.badWords == null) return;

            this._result = new Dictionary<int, List<string>>();

            for (int i = 0; i < this.words.Length; i++)
            {
                if (badWords.ContainsKey(words[i].ToString()))
                {
                    Dictionary<string, int[]> dic = badWords[words[i].ToString()];
                    int pos = 0;
                    foreach (string str in dic.Keys)
                    {
                        if (dic[str][1] == 1)//有通配符
                        {
                            pos = (this.words.Length - i) >= dic[str][2] ? dic[str][2] : (this.words.Length - i);
                            string s = words.Substring(i, pos);
                            if (new Regex(str).Match(s).Success)
                            {
                                AddToList(s, dic[str][0]);
                            }
                        }
                        else
                        {
                            pos = (this.words.Length - i) >= str.Length ? str.Length : (this.words.Length - i);
                            if (str == words.Substring(i, pos))
                            {
                                AddToList(str, dic[str][0]);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 存入结果
        /// </summary>
        /// <param name="str"></param>
        /// <param name="level"></param>
        private void AddToList(string str, int level)
        {
            if (this._result.ContainsKey(level))
            {
                _result[level].Add(str);
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(str);
                _result.Add(level, list);
            }
        }
    }
}
