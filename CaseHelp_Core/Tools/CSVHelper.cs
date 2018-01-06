using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CaseHelp_Core.Tools
{
    public class CSVHelper
    {
        public static void SaveCSV(DataTable dt, string fullPath)//table数据写入csv  
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            System.IO.FileStream fs = new System.IO.FileStream(fullPath, System.IO.FileMode.Create,
                System.IO.FileAccess.Write);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";

            for (int i = 0; i < dt.Columns.Count; i++)//写入列名  
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);

            for (int i = 0; i < dt.Rows.Count; i++) //写入各行数据  
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号  
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中  
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
        }  

        public void DataToCSV(string fileName, DataTable dt)   {    
           
        }

        public  static string ExportCSV(DataTable dttitle,DataTable dtitem)
        {
            StringBuilder strbData = new StringBuilder();
            string data = "案号,";

            string first = "";
            for (int i = 0; i < dttitle.Rows.Count; i++)//写入列名  
            {
                data += dttitle.Rows[i]["Problem"].ToString();
                if (i < dttitle.Rows.Count - 1)
                {
                    data += ",";
                }
            }
            strbData.Append(data); 
            //strbData.Append("\n");

            for (int i = 0; i < dtitem.Rows.Count; i++) //写入各行数据  
            {
                first = dtitem.Rows[i]["Qid"].ToString();
                data = dtitem.Rows[i]["AnswerList"].ToString();


                data = data.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号  
                if (data.Contains(',') || data.Contains('"')
                    || data.Contains('\r') || data.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中  
                {
                    data = string.Format("\"{0}\"", data);
                }
                if (first == "0")
                {
                    strbData.Append("\n"); 
                }
                strbData.Append(data += ","); 
                //for (int j = 0; j < dt.Columns.Count; j++)
                //{
                //    string str = dt.Rows[i][j].ToString();
                //    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号  
                //    if (str.Contains(',') || str.Contains('"')
                //        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中  
                //    {
                //        str = string.Format("\"{0}\"", str);
                //    }

                //    data += str;
                //    if (j < dt.Columns.Count - 1)
                //    {
                //        data += ",";
                //    }
                //}
                //strbData.Append(data);
                //strbData.Append("\n"); 
            }
            return strbData.ToString(); 
        } 
    }
}
