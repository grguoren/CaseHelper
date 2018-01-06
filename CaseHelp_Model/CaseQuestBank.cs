/**  版本信息模板在安装目录下，可自行修改。
* CaseQuestBank.cs
*
* 功 能： N/A
* 类 名： CaseQuestBank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017/10/24 16:23:16   N/A    初版
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
	/// CaseQuestBank:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CaseQuestBank
	{
		public CaseQuestBank()
		{}
		#region Model
		private int _id;
		private int _paperid;
		private int? _typeid;
		private string _problem;
		private string _itemlist;
		private int? _addid;
		private string _addname;
		private DateTime? _addtime;
		/// <summary>
		/// 问卷题库 ID
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 问卷ID
		/// </summary>
		public int PaperId
		{
			set{ _paperid=value;}
			get{return _paperid;}
		}
		/// <summary>
		/// 问题分类ID 1 单选 2 多选 3 下拉 4 单行文本 5 多行文本
		/// </summary>
		public int? TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 问题标题
		/// </summary>
		public string Problem
		{
			set{ _problem=value;}
			get{return _problem;}
		}
		/// <summary>
		/// 问题选项
		/// </summary>
		public string ItemList
		{
			set{ _itemlist=value;}
			get{return _itemlist;}
		}
		/// <summary>
		/// 添加人员ID
		/// </summary>
		public int? AddId
		{
			set{ _addid=value;}
			get{return _addid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AddName
		{
			set{ _addname=value;}
			get{return _addname;}
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime? AddTime
		{
			set{ _addtime=value;}
			get{return _addtime;}
		}
		#endregion Model

	}
}

