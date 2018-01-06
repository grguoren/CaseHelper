using CaseHelp_Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        /// 用户基本资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            return View();
        }

    }
}
