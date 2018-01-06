using CaseHelp_Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {

            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.DelUserCookie("zxmanageuser");
            Response.Redirect("/");
            return View();
        }

    }
}
