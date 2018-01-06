using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseHelp_Core.Helper
{
    /// <summary>
    /// 短信发送类
    /// </summary>
    public class SmsHelper
    {
        private SmsHelper() { }

        private static string url = "http://sendcloud.sohu.com/smsapi/send";
        private static string smsUser = "SMS_MSG";
        private static string smsKey = "Bus02uzHlnQCSfC6TgQAn4ZhuvOS46Mj";
        public static ReturnModel SendMsg(int tempid, string phone, string vars)
        {
            url = "http://sendcloud.sohu.com/smsapi/send";
            SortedList sl = new SortedList();
            sl["smsUser"] = smsUser;
            sl["templateId"] = tempid;
            sl["phone"] = phone;
            sl["vars"] = vars;

            string para = string.Empty;
            foreach (DictionaryEntry item in sl)
            {
                para += "&" + item.Key + "=" + item.Value;
            }

            string signature = smsKey + para + "&" + smsKey;
            para += "&signature=" + EncryptHelper.MD5_Encrypt(signature);
            string post_data = para.Trim('&');

            url = url + "?" + post_data;

            ReturnModel model = new ReturnModel();
            
            string errInfo;
            string res = HttpHelper.SendHttpGet(url, "", out errInfo);
            if (!string.IsNullOrEmpty(errInfo))
            {
                model.statusCode = -100;
                model.message = errInfo;
            }
            else
            {
                if (string.IsNullOrEmpty(res))
                {
                    model.statusCode = -101;
                    model.message = "接口返回值为空";
                }
                else
                {
                    model = JsonConvert.DeserializeObject<ReturnModel>(res);
                }
            }

            return model;
        }

        public static string GetTemp(int tempid, string vars)
        {
            string temp = "";
            var model = JsonConvert.DeserializeObject<ParaModel>(vars);
            switch (tempid)
            {
                case 462:
                    temp = string.Format("尊敬的{0}：你在直销同城网的登录信息如下：登录帐号：{1} 密码：{2} 请妥善保管。【直销同城网】", model.name, model.uname, model.password);
                    break;
                case 461:
                    temp = string.Format("尊敬的{0}：您收到了一条新伙伴加盟通知。{1}的{2}（{3}、{4}）于{5}在您的网站申请了加盟事业，请及时回复。【直销同城网】", model.name1, model.area,model.name2,model.phone, model.qq,model.time);
                    break;
                case 460:
                    temp = string.Format("尊敬的{0}：您于{1}成功申请了直销同城网名片设计{2}，我们将尽快与您取得联系并提供相应服务。【直销同城网】", model.name, model.date, model.type);
                    break;
                case 459:
                    temp = string.Format("尊敬的{0}：{1}您收到了一条新的留言。客户{2}（手机{3}、QQ{4}）对您网站上的{5}感兴趣并留言。请及时登录网站查看并回复！【直销同城网】", model.name, model.date, model.uname, model.mobile, model.qq, model.brand);
                    break;
                case 458:
                    temp = string.Format("尊敬的{0}：您有一笔新的订单。{1}{2}{3}（{4}）在您网站订购了{5}，请抓紧联系！【直销同城网】", model.name1, model.time, model.area, model.name2, model.phone, model.product);
                    break;
                case 457:
                    temp = string.Format("尊敬的{0}：{1}{2}{3}（{4}、{5}）在直销同城网发布了{6}需求信息。请及时登录网站查看详情并回复！【直销同城网】", model.name, model.dtime, model.area, model.uname, model.phone, model.qq, model.title);
                    break;
                case 456:
                    temp = string.Format("尊敬的{0}：您好，您的会员即将到期，请及时续费。您的直销同城网会员有效期至{1}【直销同城网】", model.name, model.date);
                    break;
                case 453:
                    temp = string.Format("恭喜您成功注册了直销同城网免费会员（您的用户名：{0} 密码：{1}）,请妥善保管。【直销同城网】", model.name, model.password);
                    break;
                case 452:
                    temp = string.Format("验证码：{0}，您正在通过手机号注册直销同城网会员账号。30分钟内有效，请勿泄露。【直销同城网】", model.code);
                    break;
                default:
                    break;
            }
            return temp;
        }
    }

    public enum SmsType : int
    {
        //找回密码通知 tp=尊敬的%name%：你在直销同城网的登录信息如下：登录帐号：%uname% 密码：%password% 请妥善保管。【直销同城网】。
        FindPassword = 462,
        //新加盟通知 tp=尊敬的%name1%：您收到了一条新伙伴加盟通知。%area%的%name2%（%phone%、%qq%）于%time%在您的网站申请了加盟事业，请及时回复。【直销同城网】
        Joino = 461,
        //名片设计通知 tp=尊敬的%name%：您于%date%成功申请了直销同城网名片设计%type%，我们将尽快与您取得联系并提供相应服务。【直销同城网】
        CardDesign = 460,
        //新留言通知  tp=尊敬的%name%：%date%您收到了一条新的留言。客户%uname%（手机%mobile%、QQ%qq%）对您网站上的%brand%感兴趣并留言。请及时登录网站查看并回复！【直销同城网】
        Message = 459,
        //新订单通知 tp=尊敬的%name1%：您有一笔新的订单。%time%%area%%name2%（%phone%）在您网站订购了%product%，请抓紧联系！【直销同城网】
        Order = 458,
        //新需求通知 tp=尊敬的%name%：%dtime%%area%%uname%（%phone%、%qq%）在直销同城网发布了%title%需求信息。请及时登录网站查看详情并回复！【直销同城网】
        Demand = 457,
        //到期提醒通知 tp=尊敬的%name%：您好，您的会员即将到期，请及时续费。您的直销同城网会员有效期至%date%【直销同城网】
        UserExpired = 456,
        //会员注册成功通知 tp=恭喜您成功注册了直销同城网免费会员（您的用户名：%name% 密码：%password%）,请妥善保管。【直销同城网】
        RegisterSuccess = 453,
        //手机号注册验证码  tp=验证码：%code%，您正在通过手机号注册直销同城网会员账号。30分钟内有效，请勿泄露。【直销同城网】
        Code = 452
    }
    public class ParaModel
    {
        public string name { get; set; }
        public string uname { get; set; }
        public string password { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string area { get; set; }
        public string phone { get; set; }
        public string qq { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string type { get; set; }
        public string mobile { get; set; }
        public string brand { get; set; }
        public string product { get; set; }
        public string dtime { get; set; }
        public string title { get; set; }
        public string code { get; set; }
    }
    public class ReturnModel
    {
        public string message { get; set; }
        public object info { get; set; }
        public bool result { get; set; }
        public int statusCode { get; set; }
    }
}
