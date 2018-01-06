using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CaseHelp_Core.Helper
{
    public class HttpHelper
    {
        public static string SendHttpGet(string Url, string postDataStr, out string exinfo)
        {
            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr); 
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                response = (HttpWebResponse)request.GetResponse();
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                exinfo = "";
                return retString;
            }
            catch(Exception ex)
            {
                exinfo = ex.Message;
                myStreamReader.Close();
                myResponseStream.Close();
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if(request != null)
                {
                    request.Abort();
                }
                return null;
            }
        }
    }
}
