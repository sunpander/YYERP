using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace DbAccess.Sql
{
	/// <summary>
	/// 数据访问层的基类
	/// </summary>
    public abstract class DatabaseDAL<T> :IDatabaseDAL<T> where T : class, new()
	{
        public abstract string GetTableName();

        #region 子类必须实现的函数(用于更新或者插入)
        public virtual List<T> DataTableToEntityList(DataTable dt)
        {
            List<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T obj = new T();
                PropertyInfo[] pis = obj.GetType().GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    try
                    {
                        //for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Columns.Contains(pi.Name))
                            {
                                pi.SetValue(obj, dt.Rows[i][pi.Name] ?? "", null);
                            }
                        }
                    }
                    catch (Exception ex) { }
                }
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 将DataReader的属性值转化为实体类的属性值，返回实体类
        /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
        /// </summary>
        /// <param name="dr">DataRow对象</param>
        /// <returns>实体类对象</returns>
        public virtual T DataRowToEntity(DataRow dr)
        {
            T obj = new T();
            PropertyInfo[] pis = obj.GetType().GetProperties();

            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    if (dr.Table.Columns.Contains(pi.Name))
                    {
                        pi.SetValue(obj, dr[pi.Name] ?? "", null);
                    }
                }
                catch { }
            }
            return obj;
        }

        /// <summary>
        /// 将实体对象的属性值转化为Dictionary<string,object> 对应的键值(用于插入或者更新操作)
        /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
        /// </summary>
        /// <param name="obj">有效的实体对象</param>
        /// <returns>包含键值映射的Dictionary<string,object> </returns>
        protected virtual Dictionary<string, object> GetHashByEntity(T obj)
        {
            Dictionary<string, object> ht = new Dictionary<string, object>();
            PropertyInfo[] pis = obj.GetType().GetProperties();
            for (int i = 0; i < pis.Length; i++)
            {
                //if (pis[i].Name != PrimaryKey)
                {
                    object objValue = pis[i].GetValue(obj, null);
                    objValue = (objValue == null) ? DBNull.Value : objValue;

                    if (!ht.ContainsKey(pis[i].Name))
                    {
                        ht.Add(pis[i].Name, objValue);
                    }
                }
            }
            return ht;
        }

        protected string ConvertToEqualCondition(Dictionary<string, object> recordField)
        {
            string field = ""; // 字段名
            string setValue = ""; // 更新Set () 中的语句

            IEnumerator eKeys = recordField.Keys.GetEnumerator();
            while (eKeys.MoveNext())
            {
                field = eKeys.Current.ToString();
                setValue += string.Format("{0} = @{0},", field);
            }
            setValue = setValue.TrimEnd(',');
            return setValue;
        }
        #endregion


        #region IDatabaseDAL<T> 成员

        public bool IsExistRecord(string condition)
        {
            throw new NotImplementedException();
        }

        public bool IsExistRecord(Dictionary<string, object> recordTable)
        {
            throw new NotImplementedException();
        }

        public bool IsExistRecord(string fieldName, object fildValue)
        {
            throw new NotImplementedException();
        }

        public T FindByID(int key)
        {
            throw new NotImplementedException();
        }

        public T FindByID(string key)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(string condition)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(Dictionary<string, object> paramList)
        {
            throw new NotImplementedException();
        }

        public List<T> FindByIDs(string idString)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(string condition)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(string condition, int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(Dictionary<string, object> paramList)
        {
            throw new NotImplementedException();
        }

        public List<T> Find(Dictionary<string, object> paramList, int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll(int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable(int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable()
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable(string condition)
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable(string condition, int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable(Dictionary<string, object> condition)
        {
            throw new NotImplementedException();
        }

        public DataTable FindToDataTable(Dictionary<string, object> condition, int recordFrom, int pageSize)
        {
            throw new NotImplementedException();
        }

        public DataTable FindByView(string viewName, string condition)
        {
            throw new NotImplementedException();
        }

        public int GetMaxID()
        {
            throw new NotImplementedException();
        }

        public bool Insert(T obj)
        {
            throw new NotImplementedException();
        }

        public bool Insert(T obj, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj, string primaryKeyValue)
        {
            throw new NotImplementedException();
        }

        public bool Update(T obj, string primaryKeyValue, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByCondition(string condition)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByCondition(string condition, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByCondition(Dictionary<string, object> paramList)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByCondition(Dictionary<string, object> paramList, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public DataTable ExecuteQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteNonQuery(string sql, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}