/**  版本信息模板在安装目录下，可自行修改。
* CaseNo.cs
*
* 功 能： N/A
* 类 名： CaseNo
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
	/// CaseNo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CaseNo
	{
		public CaseNo()
		{}
		#region Model
		private int _id;
		private string _no;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 案号
		/// </summary>
		public string No
		{
			set{ _no=value;}
			get{return _no;}
		}
		#endregion Model

	}
}

