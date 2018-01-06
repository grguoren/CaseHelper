/**  版本信息模板在安装目录下，可自行修改。
* CaseAnswerBLL.cs
*
* 功 能： N/A
* 类 名： CaseAnswerBLL
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
using System.Data;
using System.Collections.Generic;
using CaseHelp_Common;
using CaseHelp_Model;
namespace CaseHelp_BLL
{
	/// <summary>
	/// CaseAnswerBLL
	/// </summary>
	public partial class CaseAnswerBLL
	{
        private readonly CaseHelp_DAL.CaseAnswerDal dal = new CaseHelp_DAL.CaseAnswerDal();
		public CaseAnswerBLL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			return dal.Exists(Id);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(CaseHelp_Model.CaseAnswer model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CaseHelp_Model.CaseAnswer model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			return dal.Delete(Id);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			return dal.DeleteList(Idlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CaseHelp_Model.CaseAnswer GetModel(int Id)
		{
			
			return dal.GetModel(Id);
		}

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public CaseAnswer GetModel(string strWhere)
        {
            return dal.GetModel(strWhere);
        }

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public CaseHelp_Model.CaseAnswer GetModelByCache(int Id)
		{
			
            //string CacheKey = "CaseAnswerBLLModel-" + Id;
            object objModel = null;// CaseHelp_Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(Id);
                    //if (objModel != null)
                    //{
                    //    int ModelCache = CaseHelp_Common.ConfigHelper.GetConfigInt("ModelCache");
                    //    CaseHelp_Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    //}
				}
				catch{}
			}
			return (CaseHelp_Model.CaseAnswer)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CaseHelp_Model.CaseAnswer> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<CaseHelp_Model.CaseAnswer> DataTableToList(DataTable dt)
		{
			List<CaseHelp_Model.CaseAnswer> modelList = new List<CaseHelp_Model.CaseAnswer>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				CaseHelp_Model.CaseAnswer model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}

        public List<CaseAnswer> GetPageModelList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            DataSet ds = dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

