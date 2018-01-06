using CaseHelp_BLL;
using CaseHelp_Core.Tools;
using CaseHelp_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class HomeController : Controller
    {
        protected static CaseHelp_BLL.UserInfoBLL userse = new CaseHelp_BLL.UserInfoBLL();
        protected static CasePaperBLL paperse = new CasePaperBLL();
        protected static CaseQuestBankBLL questse = new CaseQuestBankBLL();
        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "后台首页";
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);
            var paperList = paperse.GetModelList(" UserId ="+LoginId);
            ViewBag.PList = paperList;
            return View(user);
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <param name="tpwd"></param>
        /// <returns></returns>
        public string LoginAjax(string name, string pwd, string tpwd)
        {
            try
            {

                string ret = "yes";
                string guid = "";
                if (Session["LoginCheck"] != null)
                {
                    guid = Session["LoginCheck"].ToString();
                }
                //
                if (tpwd != guid)
                {
                    ret = "禁止登陆,请确定您有该后台权限，非法登陆必究！";
                    return ret;
                }
                UserInfo user = new UserInfo();
                user.UserName = name;
                user.Pwd =  name == "admin"?pwd:CaseHelp_Core.Helper.EncryptHelper.Encrypt(pwd);

                //UserCookie empcookie = new UserCookie("zxmanageemp");
                //empcookie.getUserCookie("zxmanageemp");
                //string LoginName = empcookie.Online && !string.IsNullOrEmpty(empcookie.Name) ? empcookie.Name : "";

                user = userse.LoginUser(user);
                UserCookie usercookie = new UserCookie("zxmanageuser");
                if (user != null)//登录成功
                {
                    usercookie = new UserCookie(Convert.ToInt32(user.ID), user.UserName, "zxmanageuser", true);
                    //if (usercookie != null)
                    //{
                    //    Hokkaido.Admin.Models.LoginMember.CurrentModel model = new Hokkaido.Admin.Models.LoginMember.CurrentModel();

                    //    user = userse.GetUserInfoByID(user.ID);
                    //    user.UpdateTime = DateTime.Now;//记录最近登陆时间
                    //    userse.UpdateUser(user);
                    //    model.Id = user.ID;
                    //    model.NickName = user.NickName;
                    //    model.UserName = user.UserName;
                    //    model.DepartName = user.DepartName;

                    //    Hokkaido.Admin.Common.CurrentCache.Insert(model.Id, model);
                    //}


                    try
                    {
                        //userse.SaveLoginRecord(name, ip, 1, cityName, LoginName);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
                else
                {
                    try
                    {
                        //userse.SaveLoginRecord(name, ip, 0, cityName, LoginName);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    ret = "帐号密码错误";
                }
                return ret;
            }
            catch (Exception ex)
            {
                return "数据错误，请稍候再试";

                throw ex;
            }
        }

        /// <summary>
        /// 问题试卷
        /// </summary>
        /// <returns></returns>
        public ActionResult Paper()
        {
            ViewBag.Title = "我的问卷";
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);
            var paperList = paperse.GetModelList(" UserId =" + LoginId);
            ViewBag.PList = paperList;
            return View(user);
        }

        /// <summary>
        /// 创建与编辑问卷
        /// </summary>
        /// <param name="id">问卷ID</param>
        /// <returns></returns>
        public ActionResult NewPaper(int? id=0)
        {
            ViewBag.Title = "新问卷";
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);
            var paper = paperse.GetModel(id.Value);
            ViewBag.Paper = paper;

            var questList = questse.GetModelList(" PaperId =" + id.Value);//问题列表
            ViewBag.QList = questList;
            return View(user);
        }

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Information()
        {
            ViewBag.Title = "个人信息";
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);
            return View(user);
        }

        /// <summary>
        /// 案号导入
        /// </summary>
        /// <returns></returns>
        public ActionResult CaseNo()
        {
            ViewBag.Title = "案号导入";
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            int LoginId = usercookie.Id;
            UserInfo user = new UserInfo();
            user = userse.GetModel(LoginId);
            return View(user);
        }

        /// <summary>
        /// 问卷分享
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Share(int id)
        {
            UserCookie usercookie = new UserCookie("zxmanageuser");
            usercookie.getUserCookie("zxmanageuser");
            if (!usercookie.Online)
                return Redirect("/Login/Index");
            ViewBag.ShareId = id;

            ViewBag.SURL = "/makeqrcode/index?data=" + HttpContext.Request.Url.Host + "/paper/index/" + id.ToString();
            return View();
        }

    }

}
