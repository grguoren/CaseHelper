﻿/**  版本信息模板在安装目录下，可自行修改。
* CaseQuestBankDal.cs
*
* 功 能： N/A
* 类 名： CaseQuestBankDal
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
using System.Data;
using System.Text;
using System.Data.SqlClient;
using CaseHelp_DBUtility;
using CaseHelp_Model;//Please add references
namespace CaseHelp_DAL
{
	/// <summary>
    /// 数据访问类:CaseQuestBankDal
	/// </summary>
	public partial class CaseQuestBankDal
	{
        public CaseQuestBankDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "tb_CaseQuestBank"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_CaseQuestBank");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(CaseQuestBank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_CaseQuestBank(");
			strSql.Append("PaperId,TypeId,Problem,ItemList,AddId,AddName,AddTime)");
			strSql.Append(" values (");
			strSql.Append("@PaperId,@TypeId,@Problem,@ItemList,@AddId,@AddName,@AddTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperId", SqlDbType.Int,4),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@Problem", SqlDbType.VarChar,500),
					new SqlParameter("@ItemList", SqlDbType.VarChar,2000),
					new SqlParameter("@AddId", SqlDbType.Int,4),
					new SqlParameter("@AddName", SqlDbType.VarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime)};
			parameters[0].Value = model.PaperId;
			parameters[1].Value = model.TypeId;
			parameters[2].Value = model.Problem;
			parameters[3].Value = model.ItemList;
			parameters[4].Value = model.AddId;
			parameters[5].Value = model.AddName;
			parameters[6].Value = model.AddTime;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(CaseQuestBank model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_CaseQuestBank set ");
			strSql.Append("PaperId=@PaperId,");
			strSql.Append("TypeId=@TypeId,");
			strSql.Append("Problem=@Problem,");
			strSql.Append("ItemList=@ItemList,");
			strSql.Append("AddId=@AddId,");
			strSql.Append("AddName=@AddName,");
			strSql.Append("AddTime=@AddTime");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@PaperId", SqlDbType.Int,4),
					new SqlParameter("@TypeId", SqlDbType.Int,4),
					new SqlParameter("@Problem", SqlDbType.VarChar,500),
					new SqlParameter("@ItemList", SqlDbType.VarChar,2000),
					new SqlParameter("@AddId", SqlDbType.Int,4),
					new SqlParameter("@AddName", SqlDbType.VarChar,50),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.PaperId;
			parameters[1].Value = model.TypeId;
			parameters[2].Value = model.Problem;
			parameters[3].Value = model.ItemList;
			parameters[4].Value = model.AddId;
			parameters[5].Value = model.AddName;
			parameters[6].Value = model.AddTime;
			parameters[7].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_CaseQuestBank ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        public bool DeleteTable(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_CaseQuestBank ");
            strSql.Append(" where  "+strWhere);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_CaseQuestBank ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CaseQuestBank GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,PaperId,TypeId,Problem,ItemList,AddId,AddName,AddTime from tb_CaseQuestBank ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
			};
			parameters[0].Value = Id;

			CaseQuestBank model=new CaseQuestBank();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public CaseQuestBank DataRowToModel(DataRow row)
		{
			CaseQuestBank model=new CaseQuestBank();
			if (row != null)
			{
				if(row["Id"]!=null && row["Id"].ToString()!="")
				{
					model.Id=int.Parse(row["Id"].ToString());
				}
				if(row["PaperId"]!=null && row["PaperId"].ToString()!="")
				{
					model.PaperId=int.Parse(row["PaperId"].ToString());
				}
				if(row["TypeId"]!=null && row["TypeId"].ToString()!="")
				{
					model.TypeId=int.Parse(row["TypeId"].ToString());
				}
				if(row["Problem"]!=null)
				{
					model.Problem=row["Problem"].ToString();
				}
				if(row["ItemList"]!=null)
				{
					model.ItemList=row["ItemList"].ToString();
				}
				if(row["AddId"]!=null && row["AddId"].ToString()!="")
				{
					model.AddId=int.Parse(row["AddId"].ToString());
				}
				if(row["AddName"]!=null)
				{
					model.AddName=row["AddName"].ToString();
				}
				if(row["AddTime"]!=null && row["AddTime"].ToString()!="")
				{
					model.AddTime=DateTime.Parse(row["AddTime"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,PaperId,TypeId,Problem,ItemList,AddId,AddName,AddTime ");
			strSql.Append(" FROM tb_CaseQuestBank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            strSql.Append(" Order by Id ");
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,PaperId,TypeId,Problem,ItemList,AddId,AddName,AddTime ");
			strSql.Append(" FROM tb_CaseQuestBank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM tb_CaseQuestBank ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.Id desc");
			}
			strSql.Append(")AS Row, T.*  from tb_CaseQuestBank T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "tb_CaseQuestBank";
			parameters[1].Value = "Id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}
