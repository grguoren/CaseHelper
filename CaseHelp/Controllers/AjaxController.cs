using CaseHelp_BLL;
using CaseHelp_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaseHelp.Controllers
{
    public class AjaxController : Controller
    {
        protected static UserInfoBLL userse = new UserInfoBLL();
        protected static CasePaperBLL paperse = new CasePaperBLL();
        protected static CaseQuestBankBLL questse = new CaseQuestBankBLL();
        protected static CaseAnswerBLL answerse = new CaseAnswerBLL();
        protected static CaseNoBLL casenose = new CaseNoBLL();
        protected static CaseAnsExportBLL exportse = new CaseAnsExportBLL();

        /// <summary>
        /// 提交问卷信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string AddNewPaper(string title, int userid, string desc)
        {
            try
            {
                CasePaper model = new CasePaper();
                model.CaseNo = 0;

                model.AddTime = DateTime.Now;
                model.Remark = desc;
                model.Status = 0;
                model.Title = title;
                model.UserId = userid;

                if (paperse.Add(model) > 0)//新增试卷
                {
                    int maxid = paperse.GetMaxId()-1;//减一才是
                    return maxid.ToString();
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "新增问卷失败！" + ex.Message;
            }
        }

        /// <summary>
        /// 生成问卷
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdatePaper(int id)
        {
            var model = paperse.GetModel(id);
            model.Status = 1;
            paperse.Update(model);
            return "ok";
        }

        /// <summary>
        /// 删除问卷 (有人答题则不让删除)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeletePaper(int id)
        {
            var hotUrl = answerse.GetModel(" Where  Pid = '" + id + "'");
            if (null != hotUrl)//已提交过。
            {
                return "no";//已存在
            }
            else
            {
                if (paperse.Delete(id))
                {
                    questse.DeleteTable("  PaperId = '" + id + "'");//删除所有问题
                    return "ok";
                }
            }
            return "no";
        }

        /// <summary>
        /// 单选问题提交
        /// </summary>
        /// <param name="title">题目</param>
        /// <param name="typeid">题目分类</param>
        /// <param name="itemlist">选项</param>
        /// <param name="pid">问卷ID</param>
        /// <returns></returns>
        [HttpPost]
        public string AddDanxuanQuest(string title, int typeid, string itemlist, int userid, int pid)
        {
            try
            {
                CaseQuestBank model = new CaseQuestBank();
                model.AddId = userid;

                model.AddTime = DateTime.Now;
                model.ItemList = itemlist;
                model.PaperId = pid;
                model.Problem = title;
                model.TypeId = typeid;

                if (questse.Add(model) > 0)//新增单选题
                {
                    return "ok";
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                return "新增单选题失败！" + ex.Message;
            }
        }

        public string DeleteQuest(int id)
        {
            var model = questse.Delete(id);
            return "ok";
        }
        /// <summary>
        /// 保存答案
        /// </summary>
        /// <param name="json">答案列表</param>
        /// <param name="pid">问卷ID</param>
        /// <param name="caseno">案号</param>
        /// <returns></returns>
        [HttpPost]
        public string AddAnswer(string json, int pid, string caseno)
        {
            try
            {
                var hotUrl = answerse.GetModel(" Where  AnswerList = '" + caseno + "'");
                if (null != hotUrl)//已提交过。
                {
                    return "two";//已存在
                }

                answerlist datares = JsonConvert.DeserializeObject<answerlist>(json);
                if (datares.qListItems.Count() > 0)
                {
                   

                    DateTime nowtime = DateTime.Now;
                    CaseAnswer modelno = new CaseAnswer();
                    modelno.AddTime = nowtime;
                    modelno.AnswerList = caseno;
                    modelno.Pid = pid.ToString();
                    modelno.Qid = 0;//0预留给案号
                    answerse.Add(modelno);

                    foreach (item it in datares.qListItems)
                    {
                        CaseAnswer model = new CaseAnswer();
                        model.AddTime = nowtime;
                        model.AnswerList = (it.name.EndsWith("，")?it.name.Substring(0,it.name.Length-1):it.name);
                        model.Pid = pid.ToString();
                        model.Qid = it.id;
                        answerse.Add(model);
                    }
                }
                return "ok";
                //CasePaper model = new CasePaper();
                //model.CaseNo = 0;

                //model.AddTime = DateTime.Now;
                //model.Remark = desc;
                //model.Status = 0;
                //model.Title = title;
                //model.UserId = userid;

                //if (paperse.Add(model) > 0)//新增试卷
                //{
                //    int maxid = paperse.GetMaxId();
                //    return maxid.ToString();
                //}
                //else
                //{
                //    return "0";
                //}
            }
            catch (Exception ex)
            {
                return "提交失败！" + ex.Message;
            }
        }

        /// <summary>
        /// 案号判断
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostCaseNo(string cno)
        {
            string result = "0";
            if (string.IsNullOrEmpty(cno))
                return result;

            var model = casenose.GetModel(" Where  No = '" + cno + "'");//只是判断是否存在，无所谓字段
            var hotUrl = answerse.GetModel(" Where  AnswerList = '" + cno + "'");
            if (null == model)//不存在
            {
                result = "1";//不存在
            }
            else if (null != hotUrl)//已提交过。
            {
                result = "2";//已存在
            }
            return result;

        }

        /// <summary>
        /// 修改基本资料
        /// </summary>
        /// <param name="ower"></param>
        /// <param name="member"></param>
        /// <param name="aim"></param>
        /// <param name="tel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public string UpdateUser(string ower,string member,string aim,string tel,int id)
        {
            UserInfo model = userse.GetModel(id);
            model.TeamAim = aim;
            model.TeamMember = member;
            model.TeamOwer = ower;
            model.Telephone = tel;
            if (userse.Update(model))
                return "ok";

            return "no";

        }

        /// <summary>
        /// 修改会员密码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldpwd"></param>
        /// <param name="newpwd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public string UpdatePwd(string name, string oldpwd, string newpwd,int id)
        {
            UserInfo model = userse.GetModel(id);
            string spwd = "";
            if (model.Pwd.Length <= 6)
            {
                spwd = model.Pwd;
            }
            else
            {
                spwd = CaseHelp_Core.Helper.EncryptHelper.Decrypt(model.Pwd);
            }
            if (oldpwd != spwd)
            {
                return "bad";
            }
            else
            {
                model.UserName = name;
                model.Pwd = CaseHelp_Core.Helper.EncryptHelper.Encrypt(newpwd);

                if (userse.Update(model))
                    return "ok";
            }
            return "no";

        }

        /// <summary>
        /// 上传案号文件
        /// </summary>
        /// <param name="pic_upload"></param>
        /// <returns></returns>
        public ActionResult ImgAdd(HttpPostedFileBase pic_upload)
        {
            if (pic_upload != null)
            {
                if (pic_upload.ContentLength > 0)
                {
                    //获得文件的后缀名
                    string suffix = pic_upload.FileName.Substring(pic_upload.FileName.LastIndexOf(".")).ToLower();
                    string filePath = Path.Combine(HttpContext.Server.MapPath("/UploadFile"), "CaseNoExcel" + suffix);
                    if (!System.IO.Directory.Exists(HttpContext.Server.MapPath("/UploadFile")))
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Server.MapPath("/UploadFile"));//不存在就创建目录   
                    }  
                    pic_upload.SaveAs(filePath);

                    DataSet ds = new DataSet();
   
                    string strConn = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1;'";
                    OleDbConnection conn = new OleDbConnection(strConn);

                    conn.Open();

                    string tableName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0][2].ToString().Trim();

                    OleDbDataAdapter odda = new OleDbDataAdapter("select * from [" + tableName + "]", conn);

                    odda.Fill(ds, "tb_CaseNo");
                    conn.Close();
                    //删除文件
                    System.IO.File.Delete(filePath);
                    try
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (!string.IsNullOrEmpty(row[0].ToString()))
                            {
                                CaseNo model = new CaseNo();
                                model.No = row[0].ToString();
                                casenose.Add(model);
                            }
                        }

                    }
                    catch (Exception)
                    {
                        
                    }
                }
                else
                {
                   
                }
            }
            else
            {
                return Content("请选择上传的文件");
            }
            return View();

        }

        /// <summary>
        /// 保存查询条件
        /// </summary>
        /// <param name="sdate"></param>
        /// <param name="edate"></param>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public string ExportCsv(string sdate,string edate,string idlist,int type=0)
        {
            CaseAnsExport model = new CaseAnsExport();
            model.AddTime = DateTime.Now;
            if (!string.IsNullOrEmpty(sdate))
            {
                model.StartDate = Convert.ToDateTime(sdate);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                model.EndDate = Convert.ToDateTime(edate);
            }
            model.Type = type;
            model.IdList = (idlist.EndsWith(",") ? idlist.Substring(0, idlist.Length - 1) : idlist);
            exportse.Add(model);
            return "ok";
        }
    }

    public class answerlist 
    {
        public List<item> qListItems { get; set; }
    }

    public class item 
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
