using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CaseHelp_Core.Helper
{
    /// <summary>
    /// 邮件发送帮助类
    /// </summary>
    public class MailHelper
    {
        private MailHelper() { }


        private static void SendEmail(string clientHost, string emailAddress, string receiveAddress,
          string userName, string password, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(emailAddress);
            mail.To.Add(new MailAddress(receiveAddress));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            SmtpClient client = new SmtpClient();
            client.Host = clientHost;
            client.Credentials = new NetworkCredential(userName, password);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.SendAsync(mail, null);
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 发送测试成功邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public static string SendZXEmail(string email, string body, string subject)
        {
            try
            {
                MailMessage mailMsg = new MailMessage();

                // 收件人地址，用正确邮件地址替代
                mailMsg.To.Add(new MailAddress(email));
                // 发信人，用正确邮件地址替代
                mailMsg.From = new MailAddress(Mail.Plugin.EmailConfig.SMTPUserName, "直销同城网");

                // 邮件主题
                mailMsg.Subject = subject;
                string html = body;
                // 邮件正文内容
                string text = "来自直销同城网的一封邮件！";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // 添加附件
                //string file = "C:\\file.pdf ";
                //Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                //mailMsg.Attachments.Add(data);

                // 连接到sendcloud服务器
                SmtpClient smtpClient = new SmtpClient("smtpcloud.sohu.com", Mail.Plugin.EmailConfig.SMTPPort);
                // 使用api_user和api_key进行验证
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("zhihumail", Mail.Plugin.EmailConfig.SMTPPassword);
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
                return "true";//发送成功 
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 找回密码邮件提醒
        /// </summary>
        /// <param name="realname">用户姓名</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="email">邮件地址</param>
        /// <returns></returns>
        public static string getfindpwdbody(string realname, string username, string pwd, string email, string key)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))

                .AppendFormat(" <div class=\"mail-m\"><h2>尊敬的{0}：</h2><div class=\"mail-m1\">", realname)
                .AppendFormat("<p>您在{0}提交了邮箱重置密码请求，请点击下面的链接修改密码。</p>", DateTime.Now.ToString("yyyy年MM月dd日  HH:mm:ss"))
                .AppendFormat("<p><a href=\"http://www.zx58.cn/home/ChangePwd?key={1}&uname={0}\" class=\"red\">http://www.zx58.cn/home/ChangePwd?key={1}&uname={0} </a></p>", username, key)
                .AppendFormat("<p>(如果您无法点击此链接，请将它复制到浏览器地址栏后访问)</p>")
                .Append("<p>为了保证您用户名的安全，该链接有效期为24小时，并且点击一次后失效！</p><div class=\"height\"></div>")
                .AppendFormat("<div class=\"mail-m2\"><p>直销同城团队敬上</p><p><a href=\"http://www.zx58.cn\">http://www.zx58.cn</a></p><p>{0}</p></div></div></div>", DateTime.Now.ToString("yyyy年MM月dd日"))
                .Append(getpublicfoot());
            return strText.ToString();
        }

        /// <summary>
        /// 新订单邮件提醒
        /// </summary>
        /// <param name="realname">用户姓名</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="ordertime">定购时间 年月日 时：分</param>
        /// <param name="custname">客户姓名</param>
        /// <param name="fullname">省市名称</param> 
        /// <param name="phone">手机号码</param>
        /// <param name="productstr">产品信息</param>
        /// <returns></returns>
        public static string getneworderbody(string realname, string username, string pwd, string ordertime, string custname, string fullname, string phone, string productstr)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))
                .AppendFormat(" <div class=\"mail-m\"><h2>尊敬的{0}：</h2><div>", realname)
                .AppendFormat("<p>您好，您的网站收到了一条<em class=\"red\">新的订单</em>。订购信息如下：</p>")
                .AppendFormat("<p>订购时间：{0}</p>", ordertime)
                .AppendFormat("<p>客户信息：{0} {1} {2}</p>", fullname, custname, phone)
                .AppendFormat("<p>订购产品：{0}</p>", productstr)
                .AppendFormat("<p>请及时<a href=\"{0}\" class=\"red\">登录网站</a>查看并回复顾客。</p>", "http://www.zx58.cn/home/UserLogin/")
                .Append("<p>加盟热线：0731- 84122129&nbsp;&nbsp;&nbsp;&nbsp;客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=3&uin=800024819&site=qq&menu=yes\" >800024819</a></p></div></div>")
                .Append(getpublicfoot());
            return strText.ToString();
        }

        /// <summary>
        /// 新加盟邮件提醒
        /// </summary>
        /// <param name="realname">姓名</param>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="custname">客户姓名</param>
        /// <param name="intime">加盟时间 月日 时：分</param>
        /// <param name="fullname">所在地</param>
        /// <param name="phone">手机</param>
        /// <param name="qq">QQ号码</param>
        /// <returns></returns>
        public static string getnewjoinbody(string realname, string username, string pwd, string custname, string intime, string fullname, string phone, string qq)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))
                .AppendFormat(" <div class=\"mail-m\"><h2>尊敬的{0}：</h2><div>", realname)
                .AppendFormat("<p>您好，{0}您的网站收到了一条<em class=\"red\">新的加盟信息</em>。</p>", intime)
                .AppendFormat("<p>客户信息：<em class=\"red\">{0} {1} 手机{2} QQ{3}</em></p>", fullname, custname, phone, qq)
                .AppendFormat("<p>请及时与客户取得联系，谢谢！</p>")
                .Append("<p>加盟热线：0731- 84122129&nbsp;&nbsp;&nbsp;&nbsp;客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=3&uin=800024819&site=qq&menu=yes\" >800024819</a></p></div></div>")
                .Append(getpublicfoot());
            return strText.ToString();
        }

        /// <summary>
        /// 注册成功发送邮件
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static string getregister(string username, string pwd)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))
                    .AppendFormat("<div class=\"mail-m\"><h2>尊敬的{0}：</h2><div class=\"mail-m1\"><p>您好，恭喜您成功注册了直销同城网免费会员。", username)
                    .AppendFormat("（您的<em class=\"red\">用户名：{0}</em>&nbsp;&nbsp;&nbsp;<em class=\"red\">密码：{1}</em>）</p>", username, pwd)
                    .Append("<div class=\"height\">&nbsp;</div><p>温馨提醒您，您的网站还不能显示联系资料，并只能预览网站功能。</p>")
                    .Append("<p>如果需要显示联系资料和获得更多服务，请成为我们的正式会员。<a href=\"http://www.zx58.cn/aboutlist/57/\">最新收费标准和优惠政策！</a></p>")
                    .Append("<p>服务热线：0731- 84122129&nbsp;&nbsp;&nbsp;&nbsp;客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=3&uin=800024819&site=qq&menu=yes\" >800024819</a></p></div></div><div class=\"mail-b\"><p>直销同城网&mdash;</p>")
                    .Append("<em>此为系统邮件请勿回复</em></div><div class=\"mail-f clear\"><ul class=\"list\"><li><span><img src=\"http://img.zx58.cn/mail/pic2.jpg\" /></span>")
                    .Append("<div><p>官方公众平台</p><em>关注微信<br />随时随地做直销</em></div></li><li><span><img src=\"http://img.zx58.cn/mail/pic3.jpg\" /></span>")
                    .Append("<div><p>直销讯息订阅</p><em>关注微信<br />更多商机等着您</em></div></li></ul>")
                    .Append("<span class=\"txt\">Copyright&copy;2012 直销同城网版权所有</span></div></div></body>");
            return strText.ToString();
        }

        public static string custAddContent(string rname, string tel, string weixin, string email, string add, string work, string rate, string exp)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append("<body><style type=\"text/css\">body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}body{ color:#333; font-family:\"微软雅黑\"; font-size:12px;}em{ font-style:normal;}img { border:0 none;}ul,li { list-style-type:none;}a { cursor:pointer;  text-decoration:none;  color:#333;}.red{ color:#c30009;}.clear:after { display: block; clear: both; height: 0px; content: \" \";}.height{ display:block; width:100%; height:20px; padding:0px !important;}.mail{ width:800px; margin:0 auto; border:1px solid #e6e6e6;}.mail .mail-t{ width:100%; height:50px; line-height:50px; background:#c30009;}.mail .mail-t .pic{ float:left; display:inline; margin:10px 0 0 15px;}.mail .mail-t .txt{ float:right;  color:#fff;}.mail .mail-t .txt a{ font-size:14px; color:#fff; padding:0 20px;}.mail .mail-b{ padding:15px 20px;}.mail .mail-b p{ display:block; height:35px; line-height:35px; border-bottom:1px dotted #ddd; font-size:14px;}.mail .mail-b em{ color:#999; line-height:28px;}.mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}.mail .mail-m h2{ font-weight:normal; font-size:16px; margin-top:15px;}.mail .mail-m .mail-m1{ padding:20px 30px;}.mail .mail-m .mail-m2{ text-align:right;}.mail .mail-m p{ line-height:28px;  font-size:14px;}.mail .mail-f{ background:#f4f4f4; height:100px;}.mail .mail-f .list{ float:left; width:500px;}.mail .mail-f .list li{ float:left; width:230px;}.mail .mail-f .list li span{ float:left; display:inline; margin:10px 12px 0 20px;}.mail .mail-f .list li span img{ width:80px; height:80px;}.mail .mail-f .list li p{ font-size:16px; margin-top:10px; line-height:35px;}.mail .mail-f .list li em{ color:#999;}.mail .mail-f .txt{ line-height:100px; font-size:14px; float:right; display:inline; margin-right:20px;}.table{width:100%; padding-top:10px;}.table td{ line-height:1.5em; font-family:Microsoft Yahei; font-size:14px; padding:5px 10px; background:#f5f5f5 ; color:#666;}.table .td1{text-align:right; width:100px; background:#eee;}</style><div class=\"mail\"><div class=\"mail-t\"><span class=\"pic\"><a href=\"http://www.zx58.cn/\" target=\"_blank\"><img src=\"http://img.zx58.cn/mail/pic1.jpg\" /></a></span> <span class=\"txt\"><a href=\"http://www.zx58.cn/\" target=\"_blank\">www.zx58.cn</a></span></div>")
                .AppendFormat("<div class=\"mail-m\"><h2>尊敬的益鑫泰：</h2><div class=\"mail-m1\"><p>您好，用户{0}于{1} 提交了加盟申请，以下是他/她的个人基本信息：</p>", rname, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
                .AppendFormat("<table class=\"table\" width=\"100%\"><tbody>")
                .AppendFormat("<tr><td class=\"td1\">姓名/称呼：</td><td>{0}</td></tr>", rname)
                .AppendFormat("<tr><td class=\"td1\">联系电话：</td><td>{0}</td></tr>", tel)
                .AppendFormat("<tr><td class=\"td1\">微信号：</td><td>{0}</td></tr>", weixin)
                .AppendFormat("<tr><td class=\"td1\">电子邮箱：</td><td>{0}</td></tr>", email)
                .AppendFormat("<tr><td class=\"td1\">所在地：</td><td>{0}</td></tr>", add)
                .AppendFormat("<tr><td class=\"td1\">工作经历：</td><td>{0}</td></tr>", work)
                .AppendFormat("<tr><td class=\"td1\">自我评价：</td><td>{0}</td></tr>", rate)
                .AppendFormat("<tr><td class=\"td1\">期望工作：</td><td>{0}</td></tr></tbody></table>", exp)
                .Append("<p>&nbsp;</p></div></div>")
                .Append("<div class=\"mail-b\"><p>直销同城网&mdash;</p><em>此为系统邮件请勿回复</em></div><div class=\"mail-f clear\"><ul class=\"list\"><li><span><img src=\"http://img.zx58.cn/mail/pic2.jpg\" /></span><div><p>官方公众平台</p><em>关注微信<br />随时随地做直销</em></div></li><li><span><img src=\"http://img.zx58.cn/mail/pic3.jpg\" /></span><div><p>直销讯息订阅</p><em>关注微信<br />更多商机等着您</em></div></li></ul><span class=\"txt\">Copyright&copy;2012 直销同城网版权所有</span></div></div></body>");
            return strText.ToString();
        }

        public static string SendCodeContent(string name, string code)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append("<body><style type=\"text/css\">body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}body{ color:#333; font-family:\"微软雅黑\"; font-size:12px;}em{ font-style:normal;}img { border:0 none;}ul,li { list-style-type:none;}a { cursor:pointer;  text-decoration:none;  color:#333;}.red{ color:#c30009;}.clear:after { display: block; clear: both; height: 0px; content: \" \";}.height{ display:block; width:100%; height:20px; padding:0px !important;}.mail{ width:800px; margin:0 auto; border:1px solid #e6e6e6;}.mail .mail-t{ width:100%; height:50px; line-height:50px; background:#c30009;}.mail .mail-t .pic{ float:left; display:inline; margin:10px 0 0 15px;}.mail .mail-t .txt{ float:right;  color:#fff;}.mail .mail-t .txt a{ font-size:14px; color:#fff; padding:0 20px;}.mail .mail-b{ padding:15px 20px;}.mail .mail-b p{ display:block; height:35px; line-height:35px; border-bottom:1px dotted #ddd; font-size:14px;}.mail .mail-b em{ color:#999; line-height:28px;}.mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}.mail .mail-m h2{ font-weight:normal; font-size:16px; margin-top:15px;}.mail .mail-m .mail-m1{ padding:20px 30px;}.mail .mail-m .mail-m2{ text-align:right;}.mail .mail-m p{ line-height:28px;  font-size:14px;}.mail .mail-f{ background:#f4f4f4; height:100px;}.mail .mail-f .list{ float:left; width:500px;}.mail .mail-f .list li{ float:left; width:230px;}.mail .mail-f .list li span{ float:left; display:inline; margin:10px 12px 0 20px;}.mail .mail-f .list li span img{ width:80px; height:80px;}.mail .mail-f .list li p{ font-size:16px; margin-top:10px; line-height:35px;}.mail .mail-f .list li em{ color:#999;}.mail .mail-f .txt{ line-height:100px; font-size:14px; float:right; display:inline; margin-right:20px;}.table{width:100%; padding-top:10px;}.table td{ line-height:1.5em; font-family:Microsoft Yahei; font-size:14px; padding:5px 10px; background:#f5f5f5 ; color:#666;}.table .td1{text-align:right; width:100px; background:#eee;}</style><div class=\"mail\"><div class=\"mail-t\"><span class=\"pic\"><a href=\"http://www.zx58.cn/\" target=\"_blank\"><img src=\"http://img.zx58.cn/mail/pic1.jpg\" /></a></span> <span class=\"txt\"><a href=\"http://www.zx58.cn/\" target=\"_blank\">www.zx58.cn</a></span></div>")
                .AppendFormat("<div class=\"mail-m\"><h2>尊敬的{0}：</h2><div class=\"mail-m1\">", name)
                .AppendFormat("<p>您正在通过邮箱为{0}帐号找回密码。验证码：{1},30分钟内有效，请勿泄露。【直销同城网】</p>", name, code)
                .Append("<p>&nbsp;</p></div></div>")
                .Append("<div class=\"mail-b\"><p>直销同城网&mdash;</p><em>此为系统邮件请勿回复</em></div><div class=\"mail-f clear\"><ul class=\"list\"><li><span><img src=\"http://img.zx58.cn/mail/pic2.jpg\" /></span><div><p>官方公众平台</p><em>关注微信<br />随时随地做直销</em></div></li><li><span><img src=\"http://img.zx58.cn/mail/pic3.jpg\" /></span><div><p>直销讯息订阅</p><em>关注微信<br />更多商机等着您</em></div></li></ul><span class=\"txt\">Copyright&copy;2012 直销同城网版权所有</span></div></div></body>");
            return strText.ToString();
        }


        /// <summary>
        /// 会员到期
        /// </summary>
        /// <param name="username"></param>
        /// <param name="realname"></param>
        /// <param name="pwd"></param>
        /// <param name="vipendtime"></param>
        /// <returns></returns>
        public static string getexpirebody(string username, string realname, string pwd, string vipendtime)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))
                    .AppendFormat(" <div class=\"mail-m\"><h2>尊敬的{0}：</h2><div>", realname)
                    .AppendFormat("<p>您好，您的会员即将到期，请您及时续费。</p>")
                    .AppendFormat("<p>您的直销同城网会员有效期至{0}</p>", vipendtime)
                    .AppendFormat("<p><a href=\"http://www.zx58.cn/aboutlist/57/\" class=\"red\" target=\"_blank\">最新收费标准和优惠政策！</a></p>")
                    .Append("<p>加盟热线：0731- 84122129&nbsp;&nbsp;&nbsp;&nbsp;客服QQ：<a href=\"http://wpa.qq.com/msgrd?v=3&uin=800024819&site=qq&menu=yes\" >800024819<a></p></div></div>")
                    .Append(getpublicfoot());
            return strText.ToString();
        }

        /// <summary>
        /// 名片设计邮件文本
        /// </summary>
        /// <param name="realname">设计名片人员姓名</param>
        /// <param name="username">设计名片人员帐号</param>
        /// <param name="pwd">密码</param>
        /// <param name="begintime">设计时间：2015年11月11日10:30</param>
        /// <param name="pagename">套餐</param>
        /// <param name="tel">联系电话</param>
        /// <returns></returns>
        public static string getsurecardinfobody(string realname, string username, string pwd, string begintime, string pagename, string tel, string orderno)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append(getpublichead(username, pwd))
                .AppendFormat(" <div class=\"mail-m\"><h2>管理员：</h2><div class=\"mail-m1\">")
                .AppendFormat("<p>您好，用户{0}于{1}成功申请了直销同城网名片设计{2}（订单号：{4}），请尽快联系{0}（电话：{3}）。</p>", realname, begintime, pagename, tel, orderno)
                .AppendFormat(" <div class=\"height\"></div>")
                .Append("<p>&nbsp;</p></div></div>")
                .Append(getpublicfoot());
            return strText.ToString();
        }

    


        /// <summary>
        /// 邮箱链接验证
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string EmailVerification(string Url, string name)
        {
            StringBuilder strText = new StringBuilder();
            //strText.Append(getpublichead())
            strText.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />")
 .Append("<style>")
 .Append("body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}")
 .Append("body{ color:#333; font-family:\"微软雅黑\"; font-size:12px;}")
 .Append("em{ font-style:normal;}")
 .Append("img { border:0 none;}")
 .Append("ul,li { list-style-type:none;}")
 .Append("a { cursor:pointer;  text-decoration:none;  color:#333;}")
 .Append(".red{ color:#c30009;}")
 .Append(".clear:after { display: block; clear: both; height: 0px; content: \" \";}")
 .Append(".height{ display:block; width:100%; height:20px; padding:0px !important;}")
 .Append(".mail{ width:800px; margin:0 auto; border:1px solid #e6e6e6;}")
 .Append(".mail .mail-t{ width:100%; height:50px; line-height:50px; background:#c30009;}")
 .Append(".mail .mail-t .pic{ float:left; display:inline; margin:10px 0 0 15px;}")
 .Append(".mail .mail-t .txt{ float:right;  color:#fff;}")
 .Append(".mail .mail-t .txt a{ font-size:14px; color:#fff; padding:0 20px;}")
 .Append(".mail .mail-b{ padding:15px 20px;}")
 .Append(".mail .mail-b p{ display:block; height:35px; line-height:35px; border-bottom:1px dotted #ddd; font-size:14px;}")
 .Append(".mail .mail-b em{ color:#999; line-height:28px;}")
 .Append(".mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}")
 .Append(".mail .mail-m h2{ font-weight:normal; font-size:16px; margin-top:15px;}")
 .Append(".mail .mail-m .mail-m1{ padding:20px 30px;}")
 .Append(".mail .mail-m .mail-m2{ text-align:right;}")
 .Append(".mail .mail-m p{ line-height:28px;  font-size:14px;}")
 .Append("</style>")
 .Append("<div class=\"mail\">")
     .Append("<div class=\"mail-t\">")
         .Append("<span class=\"pic\"><a href=\"#\"><img src=\"http://img.zx58.cn/images/pic1.jpg\" /></a></span>")
    .Append(" </div>")
     .Append("<div class=\"mail-m\">")
         .AppendFormat("<h2>尊敬的{0}：</h2>", name)
        .Append(" <div class=\"mail-m1\">")
           .Append("  <p>感谢注册 qiye.zx58.cn ！</p>")
             .Append("<div class=\"height\"></div>")
             .Append("<p>请点击下面的链接验证您的邮箱。</p>")
             .AppendFormat("<p><a href=\"{0}\" style=\"color:#2a62d1\">{0}</a></p>", Url)
             .Append("<p>(如果上面不是链接形式，请将该地址手工粘贴到浏览器地址栏再访问)</p>")
        .Append(" </div>")
    .Append(" </div>")
    .Append(" <div class=\"mail-b\">")
         .Append("<p>直销同城网企业平台—</p>")
        .Append(" <em>请不要直接回复该邮件。 如需帮助，请联系 QQ 800024819 </em>")
    .Append(" </div>")
 .Append("</div>");
            return strText.ToString();
        }


        #region 公共头部底部 更改时间：2017-02-24 张孝峰
        /// <summary>
        /// 公共头部
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        protected static string getpublichead(string username, string pwd)
        {
            StringBuilder strText = new StringBuilder();
            strText.Append("<meta name=\"viewport\" content=\"initial-scale=1, maximum-scale=3, minimum-scale=1, user-scalable=no\">")
            .Append("<style>")
              .Append("body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}")
                 .Append("html { font-size:12px; -webkit-tap-highlight-color:rgba(0, 0, 0, 0);}")
                 .Append("body{font-family:Microsoft Yahei,Helvetica;}")
                 .Append("em{ font-style:normal;}")
                 .Append("img { border:0 none;}")
                 .Append("ul,li { list-style-type:none; list-style: none !important;}")
                 .Append("a { cursor:pointer;  text-decoration:none;  color:#333;}")
                 .Append(".red{ color:#c30009;} ")
                 .Append(".clear:after { display: block; clear: both; height: 0px; content:'';}")
                 .Append(".height{ display:block; width:100%; height:20px; padding:0px !important;}")
                 .Append(".mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}")
                 .Append(".mail{ min-width:300px;}")
                 .Append(".mail .mail-t .pic{ display:none;}")
                 .Append(".mail .mail-t .txt{ float:inherit; width:auto; height:4rem; line-height:3rem;}	")
                 .Append(".mail .mail-t .txt a{ font-size:1.5rem; width:25%; text-align:center; float:left; color:#fff;}")
                 .Append(".mail .mail-t{ height:9rem; background:#c30009;}")
                 .Append(".head{ width:100%; background:#c30009; height:5rem; position:relative; display:block;  }")
                 .Append(".head a{position:absolute; top:0.3rem; height:4.6rem; background-repeat:no-repeat;background-size:contain;}")
                 .Append(".head .logo{left:0.5rem; width:12.2rem; background-image:url(http://img.zx58.cn/mail/logo_01.png);}")
                 .Append(".head .logo1{left:12.5rem; width:18rem; background-image:url(http://img.zx58.cn/mail/logo_02.png);}	")
                 .Append(".mail .mail-m h2{ font-size:1.8rem; line-height:4.5rem; margin-top:0;}")
                 .Append(".mail .mail-m p{ font-size:1.5rem; line-height:3.5rem; color:#666; }")
                 .Append(".mail .mail-b p{height:auto; line-height:3rem; padding-bottom:0.5rem; font-size:1.6rem; border-bottom:1px dotted #ddd;}")
                 .Append(".mail .mail-f{ height:auto;}")
                 .Append(".mail .mail-f .list{ width:100%; padding-top:1rem;}")
                 .Append(".mail .mail-f .list li{ width:100%; text-align:center;}")
                 .Append(".mail .mail-f .list li span{ float:inherit; width:100%; margin:0;}")
                 .Append(".mail .mail-f .list li span img{ width:80%; height:auto;}")
                 .Append(".mail .mail-f .list li p{ font-size:1.6rem; line-height:4rem; margin-bottom:0;}")
                 .Append(".mail .mail-f .list li em{ font-size:1.4rem; line-height:2rem;}")
                 .Append(".mail .mail-f .txt{ float:inherit; width:100%; font-size:1.4rem; padding:0 1.2rem; box-sizing:border-box; line-height:3rem; display:block; text-align:center;}")
                 .Append(".mail .mail-m .mail-m1{ padding:0;}")
                 .Append(".mail .mail-b em{ color:#999; line-height:35px; font-size:1.4rem;}")
                 .Append(".mail .mail-t .txt a em{ float:right;}")
                 .Append(".mail .mail-b{ padding:15px 20px;}")
                 .Append("@media screen and (min-width:640px) and (max-width:1920px) {")
                 .Append(".mail{ max-width:800px; margin:0 auto; border:1px solid #e6e6e6; min-width:300px;}")
                 .Append(".mail .mail-t{ width:100%; height:60px !important; background:#c30009;}")
                 .Append(".mail .mail-t .pic{ float:left; display:inline-block !important; margin:15px 0 0 15px; line-height: 0px !important;}")
                 .Append(".mail .mail-t .txt{ float:right !important; width:400px !important;  color:#fff; background:#c30009; border:none; height:60px !important; line-height:60px !important ; display:block;}")
                 .Append(".mail .mail-t .txt a{ font-size:15px; color:#fff; float:left; width:25%; text-align:center;}")
                 .Append(".mail .mail-b p{ display:block; height:45px !important; line-height:45px !important;  font-size:16px !important;}")
                 .Append(".mail .mail-b em{ color:#999; line-height:35px; font-size:14px;}")
                 .Append(".mail .mail-m h2{ font-weight:normal; font-size:18px !important; margin-top:15px;}")
                 .Append(".mail .mail-m .mail-m1{ padding:20px 30px;}")
                 .Append(".mail .mail-m .mail-m2{ text-align:right;}")
                 .Append(".mail .mail-m p{ line-height:35px !important;  font-size:16px !important;}")
                 .Append(".mail .mail-f{ background:#f4f4f4; height:100px;}")
                 .Append(".mail .mail-f .list{ float:left !important;  width:300px; padding-top:0;}")
                 .Append(".mail .mail-f .list li{ float:left !important; width:300px !important;  text-align:left;}")
                 .Append(".mail .mail-f .list li span{ float:left; display:inline; margin:10px 12px 0 20px; width:auto;}")
                 .Append(".mail .mail-f .list li span img{ width:80px; height:80px;}")
                 .Append(".mail .mail-f .list li p{ font-size:18px; margin-top:10px; line-height:35px; margin-bottom:6px; text-align:left;}")
                 .Append(".mail .mail-f .list li em{ color:#999; font-size:14px; }")
                 .Append(".mail .mail-f .txt{ line-height:100px; width:300px; text-align:right; font-size:14px; float:right; display:inline; margin-right:20px; background:#f4f4f4; border:none;}")
                 .Append(".head{ display:none !important;}")
            .Append("</style>")
     .AppendFormat("<div class='mail'> <div class='mail-t'> <span class='pic'><a href='http://www.zx58.cn/'><img src=\"http://img.zx58.cn/mail/pic1.jpg\" /></a></span> <div class='head'><a href=\"#\" class=\"logo\"></a> <a href=\"#\" class=\"logo1\"></a></div> <span class=\"txt\"><a href=\"http://www.zx58.cn/\">总站首页<em>|</em></a><a href=\"http://www.zx58.cn/demandlist.html\">顾客需求<em>|</em></a><a href=\"http://www.{0}.zx58.cn/\">会员首页<em>|</em></a><a href=\"http://www.zx58.cn/Member/UserManage/UserInfo?Name={1}&Pwd={2}&State=1\">用户中心</a></span></div>", username, username, pwd);
            return strText.ToString();
        }

        /// <summary>
        /// 公共的底部
        /// </summary>
        /// <returns></returns>
        protected static string getpublicfoot()
        {
            StringBuilder strText = new StringBuilder();
            strText.Append("<div class=\"mail-b\"> <p>直销同城网—</p>  <em>全国统一服务热线：400 803 6808，此为系统邮件请勿回复</em> </div> <div class=\"mail-f clear\">")
             .Append("<ul class=\"list\"> <li><span><img src=\"http://img.zx58.cn/mail/pic3.jpg\" /></span><div><p>官方公众平台</p><em>关注微信 随时随地做直销</em></div></li> </ul>")
           .Append(" <span class=\"txt\">Copyright©2012 直销同城网版权所有</span></div></div>");
            return strText.ToString();
        }
        #endregion


        ///// <summary>
        ///// 公共的头部
        ///// </summary>
        ///// <param name="username"></param>
        ///// <param name="pwd"></param>
        ///// <returns></returns>
        //protected static string getpublichead(string username, string pwd)
        //{
        //    StringBuilder strText = new StringBuilder();
        //    strText.Append("<body><style>body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}body{ color:#333; font-family:\"微软雅黑\"; font-size:12px;}em{ font-style:normal;}img { border:0 none;}ul,li { list-style-type:none; list-style: none !important;}a { cursor:pointer;  text-decoration:none;  color:#333;}.red{ color:#c30009;}.clear:after { display: block; clear: both; height: 0px; content: \" \";}.height{ display:block; width:100%; height:20px; padding:0px !important;}.mail{ width:800px; margin:0 auto; border:1px solid #e6e6e6;}.mail .mail-t{ width:100%; height:50px; background:#c30009;}.mail .mail-t .pic{ float:left; display:inline; margin:10px 0 0 15px; line-height: 0px !important;}.mail .mail-t .txt{ float:right;  color:#fff; background:#c30009; border:none; height:50px; line-height:50px; display:block;}.mail .mail-t .txt a{ font-size:14px; color:#fff; padding:0 20px;}.mail .mail-b{ padding:15px 20px;}.mail .mail-b p{ display:block; height:35px; line-height:35px; border-bottom:1px dotted #ddd; font-size:14px;}.mail .mail-b em{ color:#999; line-height:28px;}.mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}.mail .mail-m h2{ font-weight:normal; font-size:16px; margin-top:15px;}.mail .mail-m .mail-m1{ padding:20px 30px;}.mail .mail-m .mail-m2{ text-align:right;}.mail .mail-m p{ line-height:28px;  font-size:14px;}.mail .mail-f{ background:#f4f4f4; height:100px;}.mail .mail-f .list{ float:left; width:500px;}.mail .mail-f .list li{ float:left; width:230px;}.mail .mail-f .list li span{ float:left; display:inline; margin:10px 12px 0 20px;}.mail .mail-f .list li span img{ width:80px; height:80px;}.mail .mail-f .list li p{ font-size:16px; margin-top:10px; line-height:35px;}.mail .mail-f .list li em{ color:#999;}.mail .mail-f .txt{ line-height:100px; font-size:14px; float:right; display:inline; margin-right:20px; background:#f4f4f4; border:none;}</style>")
        //        .Append("<div class=\"mail\"><div class=\"mail-t\"><span class=\"pic\"><a href=\"#\"><img src=\"http://img.zx58.cn/mail/pic1.jpg\" /></a></span>")
        //        .AppendFormat("<span class=\"txt\"><a href=\"http://www.zx58.cn/\" target=\"_blank\">总站首页</a>|<a href=\"http://www.zx58.cn/demand.html\" target=\"_blank\">顾客需求</a>|<a href=\"http://www.{0}.zx58.cn/\" target=\"_blank\">会员首页</a>|<a href=\"http://www.zx58.cn/Member/UserManage/UserInfo?Name={1}&Pwd={2}&State=1\" target=\"_blank\">用户中心</a></span></div>", username, username, pwd);
        //    return strText.ToString();
        //}

        ///// <summary>
        ///// 公共的底部
        ///// </summary>
        ///// <returns></returns>
        //protected static string getpublicfoot()
        //{
        //    StringBuilder strText = new StringBuilder();
        //    strText.Append(" <div class=\"mail-b\"><p>直销同城网—</p> <em>此为系统邮件请勿回复</em></div>")
        //        .Append(" <div class=\"mail-f clear\"><ul class=\"list\"><li><span><img src=\"http://img.zx58.cn/mail/pic2.jpg\" /></span><div><p>官方公众平台</p><em>关注微信<br />随时随地做直销</em></div></li>")
        //        .Append("<li><span><img src=\"http://img.zx58.cn/mail/pic3.jpg\" /></span><div><p>直销讯息订阅</p><em>关注微信<br />更多商机等着您</em></div></li> </ul> <span class=\"txt\">Copyright©2012 直销同城网版权所有</span></div></div></body>");
        //    return strText.ToString();
        //}

        /// <summary>
        /// 公共的头部
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        protected static string getpublichead()
        {
            StringBuilder strText = new StringBuilder();
            strText.Append("<style>body,img,span,p,a,b,h2,em,ul,li{ margin:0; padding:0;}body{ color:#333; font-family:\"微软雅黑\"; font-size:12px;}em{ font-style:normal;}img { border:0 none;}ul,li { list-style-type:none;}a { cursor:pointer;  text-decoration:none;  color:#333;}.red{ color:#c30009;}.clear:after { display: block; clear: both; height: 0px; content:'';}.height{ display:block; width:100%; height:20px; padding:0px !important;}.mail{ width:800px; margin:0 auto; border:1px solid #e6e6e6;}.mail .mail-t{ width:100%; height:50px; line-height:50px; background:#c30009;}.mail .mail-t .pic{ float:left; display:inline; margin:10px 0 0 15px;}.mail .mail-t .txt{ float:right;  color:#fff;}.mail .mail-t .txt a{ font-size:14px; color:#fff; padding:0 20px;}.mail .mail-b{ padding:15px 20px;}.mail .mail-b p{ display:block; height:35px; line-height:35px; border-bottom:1px dotted #ddd; font-size:14px;}.mail .mail-b em{ color:#999; line-height:28px;}.mail .mail-m{ padding:15px 20px 0 20px; min-height:200px;}.mail .mail-m h2{ font-weight:normal; font-size:16px; margin-top:15px;}.mail .mail-m .mail-m1{ padding:20px 30px;}.mail .mail-m .mail-m2{ text-align:right;}.mail .mail-m p{ line-height:28px;  font-size:14px;}</style>")
                .Append("<div class=\"mail-t\"><span class=\"pic\"><a href=\"#\"><img src=\"images/pic1.jpg\" /></a></span></div");
            //.AppendFormat("<span class=\"txt\"><a href=\"http://www.zx58.cn/\" target=\"_blank\">总站首页</a>|<a href=\"http://www.zx58.cn/demand.html\" target=\"_blank\">顾客需求</a>|<a href=\"http://www.{0}.zx58.cn/\" target=\"_blank\">会员首页</a>|<a href=\"http://www.zx58.cn/Member/UserManage/UserInfo?Name={1}&Pwd={2}&State=1\" target=\"_blank\">用户中心</a></span></div>", username, username, pwd);
            return strText.ToString();
        }

       

    }
}
