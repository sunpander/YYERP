using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YYERP.Service
{
    public class ServiceTransfer
    {
        public static string tableName = "wms_transfer";
        public static string idColName = "TransferId";
#region 列属性
public int  TransferId ;
public int  CompanyId ;
public int  WarehouseIdFrom ;
public int  WarehouseIdTo ;
public string  CreateUser ;
public int  CreateDate ;
public string  PickUser ;
public string  ConfirmOutboundUser ;
public int  ConfirmOutboundTime ;
public int  TransitCompanyId ;
public string  ConfirmInboundUser ;
public int  ConfirmInboundTime ;
public string  ConfirmLocationUser ;
public string  CommentOfClient ;
public string  CommentOfWarehouse ;
public int  Disabled ;
public int  TimeId ;
#endregion
        
        public static List<string> getColumnNames()
        {
            List<string> listCols = new  List<string>();
#region 添加列名称信息
//此处添加的列名称,全大写
listCols.Add("TransferId".ToUpper());
listCols.Add("CompanyId".ToUpper());
listCols.Add("WarehouseIdFrom".ToUpper());
listCols.Add("WarehouseIdTo".ToUpper());
listCols.Add("CreateUser".ToUpper());
listCols.Add("CreateDate".ToUpper());
listCols.Add("PickUser".ToUpper());
listCols.Add("ConfirmOutboundUser".ToUpper());
listCols.Add("ConfirmOutboundTime".ToUpper());
listCols.Add("TransitCompanyId".ToUpper());
listCols.Add("ConfirmInboundUser".ToUpper());
listCols.Add("ConfirmInboundTime".ToUpper());
listCols.Add("ConfirmLocationUser".ToUpper());
listCols.Add("CommentOfClient".ToUpper());
listCols.Add("CommentOfWarehouse".ToUpper());
listCols.Add("Disabled".ToUpper());
listCols.Add("TimeId".ToUpper());
#endregion
            return listCols;
        }


        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public static DataTable queryAll()
        {
            string strSql = "select * from " + tableName;
            if (!string.IsNullOrEmpty(AppConstants.CompanyId))
            {
                strSql += " where CompanyId=" + AppConstants.CompanyId ;
            }
            if (!string.IsNullOrEmpty(AppConstants.TimeId))
            {
                strSql += " and TimeId=" + AppConstants.TimeId;
            }
            DataTable dt = DbAccess.ServiceDB.ExecuteSql(strSql);
            return dt;
        }
        /// <summary>
        /// 按指定列查询数据
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable queryByCol(string colName,string value)
        {
            string strSql = "select * from " + tableName + " where 1=1 ";
            if (!string.IsNullOrEmpty(AppConstants.CompanyId))
            {
                strSql += " and CompanyId=" + AppConstants.CompanyId;
            }
            if (!string.IsNullOrEmpty(AppConstants.TimeId))
            {
                strSql += " and TimeId=" + AppConstants.TimeId;
            }
            if (getColumnNames().Contains(colName.ToUpper()))
            {
                strSql += " and " + colName + "=@" + colName;
            }
            else
            {
                throw new Exception("按列查询时,不包含名称为"+colName+"的列.");
            }
            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add(colName, value);

            DataTable dt = DbAccess.ServiceDB.ExecuteSql(strSql, parm);
            return dt;
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int deleteById(String id)
        {
            string strSql = "select * from " + tableName;
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("根据id删除,id不能为空.");
            }
            return DbAccess.ServiceDB.ExecuteNonSql(strSql);
        }
        /// <summary>
        /// 按指定列删除
        /// </summary>
        /// <param name="colName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int deleteByCol(string colName, string value)
        {
            string strSql = "delete from " + tableName;
            if (getColumnNames().Contains(colName.ToUpper()))
            {
                strSql += " where " + colName + "=@" + colName;
            }
            else
            {
                throw new Exception("按列删除时,不包含名称为" + colName + "的列.");
            }
            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add(colName, value);
            return DbAccess.ServiceDB.ExecuteNonSql(strSql, parm);
        }

        public static DataTable getEmptyTable()
        {
            DataTable dt = DbAccess.ServiceDB.ExecuteSql("select * from   "+tableName+" where 1=2   limit 1");
            dt.TableName = tableName;
            return dt;
        }

        public static DataTable toDataTable(ServiceTransfer bean)
        {
            DataTable dt = getEmptyTable();      //new DataTable(tableName);
            #region 添加表列
            //可以省事,查询下数据库就有了
            #endregion

            DataRow row = dt.NewRow();
            #region 给表添加一行
			//此处添加的列名称,全大写
						row["TransferId"] = bean.TransferId;
						row["CompanyId"] = bean.CompanyId;
						row["WarehouseIdFrom"] = bean.WarehouseIdFrom;
						row["WarehouseIdTo"] = bean.WarehouseIdTo;
						row["CreateUser"] = bean.CreateUser;
						row["CreateDate"] = bean.CreateDate;
						row["PickUser"] = bean.PickUser;
						row["ConfirmOutboundUser"] = bean.ConfirmOutboundUser;
						row["ConfirmOutboundTime"] = bean.ConfirmOutboundTime;
						row["TransitCompanyId"] = bean.TransitCompanyId;
						row["ConfirmInboundUser"] = bean.ConfirmInboundUser;
						row["ConfirmInboundTime"] = bean.ConfirmInboundTime;
						row["ConfirmLocationUser"] = bean.ConfirmLocationUser;
						row["CommentOfClient"] = bean.CommentOfClient;
						row["CommentOfWarehouse"] = bean.CommentOfWarehouse;
						row["Disabled"] = bean.Disabled;
						row["TimeId"] = bean.TimeId;
			            #endregion

            return dt;
        }

        public static ServiceTransfer toClassBean(DataRow row)
        {
            ServiceTransfer bean = new ServiceTransfer();      //new DataTable(tableName);
        
            #region 给表添加一行
            
                        if (row.Table.Columns.IndexOf("TransferId") > -1)
            {
		bean.TransferId = Convert.ToInt32(row["TransferId"]);
            }
			            if (row.Table.Columns.IndexOf("CompanyId") > -1)
            {
		bean.CompanyId = Convert.ToInt32(row["CompanyId"]);
            }
			            if (row.Table.Columns.IndexOf("WarehouseIdFrom") > -1)
            {
		bean.WarehouseIdFrom = Convert.ToInt32(row["WarehouseIdFrom"]);
            }
			            if (row.Table.Columns.IndexOf("WarehouseIdTo") > -1)
            {
		bean.WarehouseIdTo = Convert.ToInt32(row["WarehouseIdTo"]);
            }
			            if (row.Table.Columns.IndexOf("CreateUser") > -1)
            {
		bean.CreateUser = row["CreateUser"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("CreateDate") > -1)
            {
		bean.CreateDate = Convert.ToInt32(row["CreateDate"]);
            }
			            if (row.Table.Columns.IndexOf("PickUser") > -1)
            {
		bean.PickUser = row["PickUser"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("ConfirmOutboundUser") > -1)
            {
		bean.ConfirmOutboundUser = row["ConfirmOutboundUser"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("ConfirmOutboundTime") > -1)
            {
		bean.ConfirmOutboundTime = Convert.ToInt32(row["ConfirmOutboundTime"]);
            }
			            if (row.Table.Columns.IndexOf("TransitCompanyId") > -1)
            {
		bean.TransitCompanyId = Convert.ToInt32(row["TransitCompanyId"]);
            }
			            if (row.Table.Columns.IndexOf("ConfirmInboundUser") > -1)
            {
		bean.ConfirmInboundUser = row["ConfirmInboundUser"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("ConfirmInboundTime") > -1)
            {
		bean.ConfirmInboundTime = Convert.ToInt32(row["ConfirmInboundTime"]);
            }
			            if (row.Table.Columns.IndexOf("ConfirmLocationUser") > -1)
            {
		bean.ConfirmLocationUser = row["ConfirmLocationUser"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("CommentOfClient") > -1)
            {
		bean.CommentOfClient = row["CommentOfClient"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("CommentOfWarehouse") > -1)
            {
		bean.CommentOfWarehouse = row["CommentOfWarehouse"].ToString();;
            }
			            if (row.Table.Columns.IndexOf("Disabled") > -1)
            {
		bean.Disabled = Convert.ToInt32(row["Disabled"]);
            }
			            if (row.Table.Columns.IndexOf("TimeId") > -1)
            {
		bean.TimeId = Convert.ToInt32(row["TimeId"]);
            }
			            
            //if (row.Table.Columns.IndexOf("ColName") > -1)
            //{
            //    bean.ColName = row["ColName"];
            //}
            #endregion

            return bean;
        }

        /// <summary>
        /// 按bean文件更新
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int update(DataTable dt)
        {
           dt.TableName = tableName;
           //去除多余的列
           List<string> listCols = getColumnNames();
           for (int i = dt.Columns.Count-1; i >=0; i--)
           {
               if (!listCols.Contains(dt.Columns[i].ColumnName.ToUpper()))
               {
                   //不包含,则删除
                   dt.Columns.RemoveAt(i);
               }
           }
           //通用字段不能修改
           if (dt.Columns.Contains("TimeId"))
           {
               dt.Columns.Remove("TimeId");
           }
           if (dt.Columns.Contains("CompanyId"))
           {
               dt.Columns.Remove("CompanyId");
           }
           return DbAccess.ServiceDB.UpdateRow(dt, idColName);
        }
        /// <summary>
        /// 按表更新
        /// </summary>
        /// <param name="bean"></param>
        /// <returns></returns>
        public static int update(ServiceTransfer bean)
        {
            DataTable dt = toDataTable(bean);
            return update(dt);
        }
        /// <summary>
        /// 按id修改某一列的值
        /// </summary>
        /// <param name="idValue"></param>
        /// <param name="colName"></param>
        /// <param name="colValue"></param>
        /// <returns></returns>
        public static int update(string idValue, string colName, string colValue)
        {
            string strSql = "update " + tableName;
            if (string.IsNullOrEmpty(idValue))
            {
                throw new Exception("按id修改,id值不能为空.");
            }
            if (getColumnNames().Contains(colName.ToUpper()))
            {
                strSql += " set " + colName + "=@" + colName;
            }
            else
            {
                throw new Exception("按列修改时,不包含名称为" + colName + "的列.");
            }
            strSql += " where " + idColName + "=@" + idColName;

            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add(colName, colValue);
            parm.Add(idColName, idValue);
            return DbAccess.ServiceDB.ExecuteNonSql(strSql, parm);
        }
 
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int insert(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                throw new Exception("新增数据时,值为空.");
            dt.TableName = tableName;
            //去除多余的列
            List<string> listCols = getColumnNames();
            for (int i = dt.Columns.Count - 1; i >= 0; i--)
            {
                if (!listCols.Contains(dt.Columns[i].ColumnName.ToUpper()))
                {
                    //不包含,则删除
                    dt.Columns.RemoveAt(i);
                }
            }
            //如果id是自动增长的,则新增的时候不需要id
            dt.Columns.Remove(idColName);
            //为通用字段赋值
            if (dt.Columns.Contains("TimeId"))
            {
                dt.Rows[0]["TimeId"] = AppConstants.TimeId;
            }
            if (dt.Columns.Contains("CompanyId"))
            {
                dt.Rows[0]["CompanyId"] = AppConstants.CompanyId;
            }
            return DbAccess.ServiceDB.InsertRow(dt);
        }

        public static int insert(ServiceTransfer bean)
        {
            DataTable dt = toDataTable(bean);
            return insert(dt);
        }
        //列比较少的时候,可以直接拼接sql执行
        //public static int insert(string brandName)
        //{
        //    Dictionary<String, Object> parm = new Dictionary<String, Object>();
        //    parm.Add("brandName", brandName);
        //    parm.Add("companyId", AppContanst.CompanyId);
        //    string sqlInsert = " insert into cus_brand(companyId,brandName) values(@companyId,@brandName)  ";
        //    DbAccess.ServiceDB.ExecuteSql(sqlInsert, parm);
        //    return 1;
        //}

 
    }
}
