using CaseHelp_BLL;
using CaseHelp_Core.Tools;
using CaseHelp_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    public class PaperController : Controller
    {
        protected static UserInfoBLL userse = new UserInfoBLL();
        protected static CasePaperBLL paperse = new CasePaperBLL();
        protected static CaseQuestBankBLL questse = new CaseQuestBankBLL();
        protected static CaseAnswerBLL answerse = new CaseAnswerBLL();
        protected static CaseAnsExportBLL exportse = new CaseAnsExportBLL();

        /// <summary>
        /// 试卷预览或正式链接
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id,int u=0,int t=0)
        {
            var paper = paperse.GetModel(id);
            ViewBag.Paper = paper;

            var questList = questse.GetModelList(" PaperId =" + id);//问题列表
            ViewBag.QList = questList;

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">分页ID</param>
        /// <param name="pid">问卷ID</param>
        /// <returns></returns>
        public ActionResult Count(int? id=1,int pid=0)
        {
            int size = 20;
            int sindex = size * (id.Value - 1);
            int eindex = size * id.Value;
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);

            var questlist = questse.GetModelList(" PaperId =" + pid);
            ViewBag.QuestList = questlist;

            CasePaper paper = null;
            var paperList = paperse.GetModelList(" UserId =" + LoginId);
            ViewBag.PList = paperList;
            List<CaseAnswer> answerList = new List<CaseAnswer>();
            if (pid > 0)
            {
                paper = paperse.GetModel(pid);
                answerList = answerse.GetPageModelList(" Pid = " + pid + " and Qid = 0 ","",sindex,eindex);
            }
            ViewBag.Paper = paper;
            ViewBag.AnswerList = answerList;

            ViewBag.PageIndex = id.Value;//当前页
            ViewBag.Total = answerse.GetRecordCount(" Pid = " + pid + " and Qid = 0 ");//满足条件的总条数
            return View(user);
        }

        public ActionResult Export(int? id = 0)
        {
            int eid = exportse.GetMaxId();
            var query = exportse.GetModel(eid - 1);
            string str = "",qstr="";
            if (!string.IsNullOrEmpty(query.IdList))//没勾选就是全部
            {
                str = " and Id in(" + query.IdList + ")";
                qstr = " and Qid in(0," + query.IdList + ")";
            }
            var questlist = questse.GetList(" PaperId="+id+str);//标题
            var answerlist = answerse.GetList(" (AddTime >='" + query.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and AddTime <= '" + query.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "') and Pid=" + id.Value + qstr);

            string data = CaseHelp_Core.Tools.CSVHelper.ExportCSV(questlist.Tables[0],answerlist.Tables[0]);
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.Clear();
            HttpContext.Response.Expires = 0;
            HttpContext.Response.BufferOutput = true;
            HttpContext.Response.Charset = "GB2312";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Response.AppendHeader("Content-Disposition", string.Format("attachment;filename={0}.csv", System.Web.HttpUtility.UrlEncode("excel", System.Text.Encoding.UTF8)));
            HttpContext.Response.ContentType = "text/h323;charset=gbk";
            HttpContext.Response.Write(data);
            HttpContext.Response.End();
            return View();
        }

    }
}
