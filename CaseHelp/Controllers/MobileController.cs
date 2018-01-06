using CaseHelp_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    public class MobileController : Controller
    {
        protected static UserInfoBLL userse = new UserInfoBLL();
        protected static CasePaperBLL paperse = new CasePaperBLL();
        protected static CaseQuestBankBLL questse = new CaseQuestBankBLL();

        /// <summary>
        /// 手机版问卷
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int id, int u = 0, int t = 0)
        {
            var paper = paperse.GetModel(id);
            ViewBag.Paper = paper;

            var questList = questse.GetModelList(" PaperId =" + id);//问题列表
            ViewBag.QList = questList;

            return View();
        }

    }
}
