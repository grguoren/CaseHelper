/**  版本信息模板在安装目录下，可自行修改。
* CaseAnswer.cs
*
* 功 能： N/A
* 类 名： CaseAnswer
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/10/24 16:23:15   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace CaseHelp_Model
{
	/// <summary>
	/// CaseAnswer:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CaseAnswer
	{
		public CaseAnswer()
		{}
        #region Model
        private int _id;
        private string _pid;
        private int? _qid;
        private string _answerlist;
        private DateTime? _addtime;
        private int? _answerno;
        private int? _status;
        private DateTime? _exportdate;
        /// <summary>
        /// 回答表Id
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 问卷ID
        /// </summary>
        public string Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 问题ID
        /// </summary>
        public int? Qid
        {
            set { _qid = value; }
            get { return _qid; }
        }
        /// <summary>
        /// 回答项
        /// </summary>
        public string AnswerList
        {
            set { _answerlist = value; }
            get { return _answerlist; }
        }
        /// <summary>
        /// 回答时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AnswerNo
        {
            set { _answerno = value; }
            get { return _answerno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ExportDate
        {
            set { _exportdate = value; }
            get { return _exportdate; }
        }
        #endregion Model

	}
}

