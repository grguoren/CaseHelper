/**  版本信息模板在安装目录下，可自行修改。
* CasePaper.cs
*
* 功 能： N/A
* 类 名： CasePaper
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
	/// CasePaper:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CasePaper
	{
		public CasePaper()
		{}
		#region Model
		private int _id;
		private int _userid;
		private string _title;
		private DateTime? _addtime;
		private string _remark;
		private int? _status;
		private int _caseno;
		/// <summary>
		/// 问卷表 ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 问卷创建人员ID
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 问卷标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 问卷创建时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		/// <summary>
		/// 问卷备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 问卷状态  0  待编辑 1 已生成
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 案号
		/// </summary>
		public int CaseNo
		{
			set{ _caseno=value;}
			get{return _caseno;}
		}
		#endregion Model

	}
}

