/**  版本信息模板在安装目录下，可自行修改。
* UserInfoDal.cs
*
* 功 能： N/A
* 类 名： UserInfoDal
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
    /// 数据访问类:UserInfoDal
	/// </summary>
	public partial class UserInfoDal
	{
		public UserInfoDal()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "tb_UserInfo"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from tb_UserInfo");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(UserInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into tb_UserInfo(");
			strSql.Append("UserName,Pwd,NickName,UpdateTime,CreateTime,Status,ImgPath,TeamMember,TeamOwer,TeamAim,Telephone)");
			strSql.Append(" values (");
			strSql.Append("@UserName,@Pwd,@NickName,@UpdateTime,@CreateTime,@Status,@ImgPath,@TeamMember,@TeamOwer,@TeamAim,@Telephone)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@Pwd", SqlDbType.VarChar,100),
					new SqlParameter("@NickName", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ImgPath", SqlDbType.VarChar,200),
					new SqlParameter("@TeamMember", SqlDbType.VarChar,500),
					new SqlParameter("@TeamOwer", SqlDbType.VarChar,200),
					new SqlParameter("@TeamAim", SqlDbType.VarChar,500),
					new SqlParameter("@Telephone", SqlDbType.VarChar,50)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.Pwd;
			parameters[2].Value = model.NickName;
			parameters[3].Value = model.UpdateTime;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.Status;
			parameters[6].Value = model.ImgPath;
			parameters[7].Value = model.TeamMember;
			parameters[8].Value = model.TeamOwer;
			parameters[9].Value = model.TeamAim;
			parameters[10].Value = model.Telephone;

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
		public bool Update(UserInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update tb_UserInfo set ");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Pwd=@Pwd,");
			strSql.Append("NickName=@NickName,");
			strSql.Append("UpdateTime=@UpdateTime,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("Status=@Status,");
			strSql.Append("ImgPath=@ImgPath,");
			strSql.Append("TeamMember=@TeamMember,");
			strSql.Append("TeamOwer=@TeamOwer,");
			strSql.Append("TeamAim=@TeamAim,");
			strSql.Append("Telephone=@Telephone");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@Pwd", SqlDbType.VarChar,100),
					new SqlParameter("@NickName", SqlDbType.VarChar,20),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@Status", SqlDbType.Int,4),
					new SqlParameter("@ImgPath", SqlDbType.VarChar,200),
					new SqlParameter("@TeamMember", SqlDbType.VarChar,500),
					new SqlParameter("@TeamOwer", SqlDbType.VarChar,200),
					new SqlParameter("@TeamAim", SqlDbType.VarChar,500),
					new SqlParameter("@Telephone", SqlDbType.VarChar,50),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.UserName;
			parameters[1].Value = model.Pwd;
			parameters[2].Value = model.NickName;
			parameters[3].Value = model.UpdateTime;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.Status;
			parameters[6].Value = model.ImgPath;
			parameters[7].Value = model.TeamMember;
			parameters[8].Value = model.TeamOwer;
			parameters[9].Value = model.TeamAim;
			parameters[10].Value = model.Telephone;
			parameters[11].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_UserInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from tb_UserInfo ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
		public UserInfo GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,UserName,Pwd,NickName,UpdateTime,CreateTime,Status,ImgPath,TeamMember,TeamOwer,TeamAim,Telephone from tb_UserInfo ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			UserInfo model=new UserInfo();
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

        public UserInfo GetModel(string strwhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,UserName,Pwd,NickName,UpdateTime,CreateTime,Status,ImgPath,TeamMember,TeamOwer,TeamAim,Telephone from tb_UserInfo ");
            strSql.Append(strwhere);
            
            UserInfo model = new UserInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
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
		public UserInfo DataRowToModel(DataRow row)
		{
			UserInfo model=new UserInfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["Pwd"]!=null)
				{
					model.Pwd=row["Pwd"].ToString();
				}
				if(row["NickName"]!=null)
				{
					model.NickName=row["NickName"].ToString();
				}
				if(row["UpdateTime"]!=null && row["UpdateTime"].ToString()!="")
				{
					model.UpdateTime=DateTime.Parse(row["UpdateTime"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["Status"]!=null && row["Status"].ToString()!="")
				{
					model.Status=int.Parse(row["Status"].ToString());
				}
				if(row["ImgPath"]!=null)
				{
					model.ImgPath=row["ImgPath"].ToString();
				}
				if(row["TeamMember"]!=null)
				{
					model.TeamMember=row["TeamMember"].ToString();
				}
				if(row["TeamOwer"]!=null)
				{
					model.TeamOwer=row["TeamOwer"].ToString();
				}
				if(row["TeamAim"]!=null)
				{
					model.TeamAim=row["TeamAim"].ToString();
				}
				if(row["Telephone"]!=null)
				{
					model.Telephone=row["Telephone"].ToString();
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
			strSql.Append("select ID,UserName,Pwd,NickName,UpdateTime,CreateTime,Status,ImgPath,TeamMember,TeamOwer,TeamAim,Telephone ");
			strSql.Append(" FROM tb_UserInfo ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
			strSql.Append(" ID,UserName,Pwd,NickName,UpdateTime,CreateTime,Status,ImgPath,TeamMember,TeamOwer,TeamAim,Telephone ");
			strSql.Append(" FROM tb_UserInfo ");
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
			strSql.Append("select count(1) FROM tb_UserInfo ");
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
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from tb_UserInfo T ");
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
			parameters[0].Value = "tb_UserInfo";
			parameters[1].Value = "ID";
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

