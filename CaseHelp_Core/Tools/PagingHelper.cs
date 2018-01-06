using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseHelp_Core.Tools
{
    /// <summary>
    /// 分页 帮助类
    /// </summary>
    public static class PagingHelper
    {

        public static string GetPagingNew(string url, int index, int size, int count, int linkSize, string parms)
        {
            //return GetWebFormPaging(url, index, size, count, itemSize, parms, true, true, true, true);
            return GetMVCPagingNew(url, index, size, count, linkSize, parms, false, true, true, true, true);
        }

        /// <summary>
        /// 获取分页的HTML代码 供MVC风格使用
        /// </summary>
        /// <param name="url">页面地址</param>
        /// <param name="index">当前页码</param>
        /// <param name="size">页尺寸</param>
        /// <param name="count">数据总数</param>
        /// <param name="linkSize">页码按钮数量</param>
        /// <param name="parameter">跟在url后面的参数</param>
        /// <returns></returns>
        public static string GetMVCPaging(string url, int index, int size, int count, int linkSize, string parms,
            bool showInfo, bool showFirstLast, bool showPrevNext, bool showSkip, bool showWite)
        {
            #region V2.0
            StringBuilder sb = new StringBuilder();
            int star = 0, end = 0;
            string infoStr = "", firstStr = "", lastStr = "", prevStr = "", nextStr = "", skipStr = "";

            int pageCount = (count % size) == 0 ? count / size : (count / size) + 1;
            int prev = index > 1 ? index - 1 : 1;
            int next = index < pageCount ? index + 1 : pageCount;
            int median = (linkSize / 2) + 1;
            median = linkSize % 2 == 0 ? median : median + 1;
            showInfo = false;
            if (showInfo)
            {
                infoStr = "<span>共" + count + "条数据　总计" + pageCount + "页</span>";

            }
            //showFirstLast = true;
            //showPrevNext = true;
            if (showFirstLast)
            {
                if (index != 1)
                    firstStr = "<a href=\"" + url + "/" + 1 + parms + "\">首页</a>";
                if (index != pageCount)
                    lastStr = "<a href=\"" + url + "/" + pageCount + parms + "\">尾页</a>";
            }
            if (showPrevNext)
            {
                if (index != 1)
                    prevStr = "<a href=\"" + url + "/" + prev + parms + "\"><上一页</a>";
                if (index != pageCount)
                    nextStr = "<a href=\"" + url + "/" + next + parms + "\">下一页></a>";
            }
            if (showSkip)
            {
                string skipurl = "'" + url + "/' + document.getElementById('skipPage').value + '" + parms + "'";
                skipStr = " <input type=\"text\"style=\"width:50px;height:23px;\" id=\"skipPage\" value=\"" + index + "\">";
                skipStr += "<a id=\"skipBtn\" href=\"javascript:skipPage(document.getElementById('skipPage').value ," + pageCount + ");\">Go</a>";
                skipStr += "<script type=\"text/javascript\">function skipPage(index,pageCount,url){";
                skipStr += "var number = /^\\+?[1-9][0-9]*$/;";
                skipStr += "if(!number.test(index)){alert('页码只能输入大于0的数字！');}";
                skipStr += "else if(index > pageCount){alert('页码不能大于总页数');}";
                skipStr += "else{window.location.href = " + skipurl + ";}";
                skipStr += "}</script>";
            }
            sb.Append("<div class=\"B_pages\">");
            sb.Append(infoStr);
            sb.Append(firstStr);
            sb.Append(prevStr);
            if (pageCount < linkSize)
            {
                star = 0; end = pageCount;
            }
            else
            {
                if (index < median)
                {
                    star = 0; end = linkSize;
                }
                else if ((pageCount - index) < median)
                {
                    star = (pageCount - linkSize); end = pageCount;
                }
                else
                {
                    star = median % 2 == 0 ? (index - (median)) + 1 : (index - (median));
                    end = (index + median - 1);
                }
            }
            star++; end++;
            //showWite = true;
            if (showWite)
            {
                if (star != 1)
                {
                    sb.Append("<a href=\"" + url + "/" + (star - 2) + parms + "\">" + "......" + "</a>");
                }
                for (int i = star; i < end; i++)
                {
                    if (index == i)
                        sb.Append("<a class=\"all_page_on\">" + i + "</a>");
                    else
                        sb.Append("<a href=\"" + url + "/" + i + parms + "\">" + i + "</a>");
                }
                if (end - 1 != pageCount)
                {
                    sb.Append("<a href=\"" + url + "/" + (end + 2) + parms + "\">" + "......" + "</a>");
                }
            }
            else
            {
                for (int i = star; i < end; i++)
                {
                    if (index == i)
                        sb.Append("<a class=\"all_page_on\">" + i + "</a>");
                    else
                        sb.Append("<a href=\"" + url + "/" + i + parms + "\">" + i + "</a>");
                }
            }
            sb.Append(nextStr);
            sb.Append(lastStr);
            sb.Append(skipStr);
            sb.Append("</div>");
            return sb.ToString();
            #endregion
        }


        public static string GetMVCPagingNew(string url, int index, int size, int count, int linkSize, string parms,
            bool showInfo, bool showFirstLast, bool showPrevNext, bool showSkip, bool showWite)
        {

            StringBuilder sb = new StringBuilder();
            int star = 0, end = 0;
            string infoStr = "", firstStr = "", lastStr = "", prevStr = "", nextStr = "", skipStr = "";

            int pageCount = (count % size) == 0 ? count / size : (count / size) + 1;
            int prev = index > 1 ? index - 1 : 1;
            int next = index < pageCount ? index + 1 : pageCount;
            int median = (linkSize / 2) + 1;
            median = linkSize % 2 == 0 ? median : median + 1;
            showInfo = true;
            if (showInfo)
            {
                infoStr = "<li class=\"pste\"><a class=\"a\">共" + count + "条数据　总计" + pageCount + "页</a></li>";

            }
            //showFirstLast = true;
            //showPrevNext = true;
            if (showFirstLast)
            {
                if (index != 1)
                    firstStr = "";
                if (index != pageCount)
                    lastStr = "";
            }
            if (showPrevNext)
            {
                if (index != 1)
                    prevStr = "<li class=\"pste\"><a class=\"a\" href=\"" + url + prev + parms + "\">上一页</a></li>";
                if (index != pageCount)
                    nextStr = "<li class=\"pste\"><a class=\"a\"  href=\"" + url + next + parms + "\">下一页</a></li>";
            }
            if (showSkip)
            {
                string skipurl = "'" + url + "' + document.getElementById('skipPage').value + '" + parms + "'";
                skipStr = " <input type=\"text\"style=\"width:50px;height:23px;\" id=\"skipPage\" value=\"" + index + "\">";
                skipStr += "<a id=\"skipBtn\" href=\"javascript:skipPage(document.getElementById('skipPage').value ," + pageCount + ");\">Go</a>";
                skipStr += "<script type=\"text/javascript\">function skipPage(index,pageCount,url){";
                skipStr += "var number = /^\\+?[1-9][0-9]*$/;";
                skipStr += "if(!number.test(index)){alert('页码只能输入大于0的数字！');}";
                skipStr += "else if(index > pageCount){alert('页码不能大于总页数');}";
                skipStr += "else{window.location.href = " + skipurl + ";}";
                skipStr += "}</script>";
            }
            sb.Append("<ul class=\"list clear\">");
            sb.Append(infoStr);
            sb.Append(firstStr);
            sb.Append(prevStr);
            if (pageCount < linkSize)
            {
                star = 0; end = pageCount;
            }
            else
            {
                if (index < median)
                {
                    star = 0; end = linkSize;
                }
                else if ((pageCount - index) < median)
                {
                    star = (pageCount - linkSize); end = pageCount;
                }
                else
                {
                    star = median % 2 == 0 ? (index - (median)) + 1 : (index - (median));
                    end = (index + median - 1);
                }
            }
            star++; end++;
            //showWite = true;
            if (showWite)
            {
                if (star != 1)
                {
                    sb.Append("<li class=\"pste\"><a class=\"b\" href=\"" + url + (star - 2) + parms + "\">" + "......" + "</a></li>");
                }
                for (int i = star; i < end; i++)
                {
                    if (index == i)
                        sb.Append("<li class=\"pste hover\"><a class=\"b\" class=\"all_page_on\">" + i + "</a></li>");
                    else
                        sb.Append("<li class=\"pste\"><a class=\"b\" href=\"" + url + i + parms + "\">" + i + "</a></li>");
                }
                if (end - 1 != pageCount)
                {
                    sb.Append("<li class=\"pste\"><a class=\"b\" href=\"" + url + (end + 2) + parms + "\">" + "......" + "</a></li>");
                }
            }
            else
            {
                for (int i = star; i < end; i++)
                {
                    if (index == i)
                        sb.Append("<li class=\"pste\"><a class=\"b\" class=\"all_page_on\">" + i + "</a></li>");
                    else
                        sb.Append("<li class=\"pste\"><a class=\"b\" href=\"" + url + i + parms + "\">" + i + "</a></li>");
                }
            }
            sb.Append(nextStr);
            sb.Append(lastStr);
            sb.Append(skipStr);
            sb.Append("</ul>");
            return sb.ToString();
        }




    }
}
