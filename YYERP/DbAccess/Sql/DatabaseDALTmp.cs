using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using EIExtend.ExceptionEX;

namespace DbAccess.Sql
{
	/// <summary>
	/// 数据访问层的基类
	/// </summary>
    public abstract class DatabaseDALTmp<T>  where T : class, new()
	{
        public abstract string GetTableName();


		#region 构造函数
		protected string tableName;//需要初始化的对象表名
		protected string primaryKey="ID";//数据库的主键字段名
        protected string sortField = "ID";//排序字段
        private bool isDescending = false;//

        protected string selectedFields = " * ";//选择的字段，默认为所有(*)
        private string seqField = "";//指定那个字段是用序列来控制它的值的，一般为主键
        private string seqName = "";//指定的序列名称，建议规则为：SEQ_表名称
        
        /// <summary>
        /// 指定那个字段是用序列来控制它的值的，一般为主键
        /// </summary>
        public string SeqField
        {
            get { return seqField; }
            set { seqField = value; }
        }

        /// <summary>
        /// 指定的序列名称，建议规则为：SEQ_表名称
        /// </summary>
        public string SeqName
        {
            get { return seqName; }
            set { seqName = value; }
        }
        
        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortField
        {
            get 
            {
                return sortField; 
            }
            set 
            {
                sortField = value; 
            }
        }

        /// <summary>
        /// 选择的字段，默认为所有(*)
        /// </summary>
        protected string SelectedFields
        {
            get { return selectedFields; }
            set { selectedFields = value; }
        }

        /// <summary>
        /// 是否为降序
        /// </summary>
        public bool IsDescending
        {
            get { return isDescending; }
            set { isDescending = value; }
        }       

		/// <summary>
		/// 数据库访问对象的表名
		/// </summary>
		public string TableName
		{
			get
			{
				return tableName;
			}
		}

		/// <summary>
		/// 数据库访问对象的外键约束
		/// </summary>
		public string PrimaryKey
		{
			get
			{
				return primaryKey;
			}
		}
		
		public DatabaseDALTmp()
		{
            this.tableName = GetTableName();
        }

		/// <summary>
		/// 指定表名以及主键,对基类进构造
		/// </summary>
		/// <param name="tableName">表名</param>
		/// <param name="primaryKey">表主键</param>
		public DatabaseDALTmp(string tableName, string primaryKey)
		{
			this.tableName = tableName;
			this.primaryKey = primaryKey;
		}
		#endregion

		#region 通用操作方法

		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
		public bool Insert(Dictionary<string,object>  recordField, DbTransaction trans)
		{
			return this.Insert(recordField, tableName, trans);
		}

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public int Insert2(Dictionary<string,object>  recordField, DbTransaction trans)
        {
            return this.Insert2(recordField, tableName, trans);
        }

		/// <summary>
		/// 添加记录
		/// </summary>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="targetTable">需要操作的目标表名称</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public bool Insert(Dictionary<string, object> recordField, string targetTable, DbTransaction trans)
        {
            bool result = false;
            string fields = ""; // 字段名
            string vals = ""; // 字段值
            if (recordField == null || recordField.Count < 1)
            {
                return result;
            }

            IEnumerator eKeys = recordField.Keys.GetEnumerator();
            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += field + ",";
                vals += ("@" + field) + ",";
            }

            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTable, fields, vals);

            #region 数据库相关操作
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                if (trans != null)
                {
                    result = db.ExecuteNonQuery(sql, recordField) > 0;
                }
                else
                {
                    result = db.ExecuteNonQuery(sql, recordField, trans) > 0;
                }
            }
            #endregion
            return result;
        }

        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
        /// <param name="targetTable">需要操作的目标表名称</param>
        /// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public int Insert2(Dictionary<string,object>  recordField, string targetTable, DbTransaction trans)
        {
            int result = -1;
            if (recordField == null || recordField.Count < 1)
            {
                return result;
            }

            #region 拼接sql
            string fields = ""; // 字段名
            string vals = ""; // 字段值
            IEnumerator eKeys = recordField.Keys.GetEnumerator();
            while (eKeys.MoveNext())
            {
                string field = eKeys.Current.ToString();
                fields += field + ",";
                vals += ("@" + fields) + ",";
            }

            fields = fields.Trim(',');//除去前后的逗号
            vals = vals.Trim(',');//除去前后的逗号
            string sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", targetTable, fields, vals);
            #endregion

            #region 数据库操作
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                if (trans != null)
                {
                    result = db.ExecuteNonQuery(sql, recordField);
                }
                else
                {
                    result = db.ExecuteNonQuery(sql, recordField, trans);
                }
            }
            //TODO:返回插入记录的主键id值
            #endregion
            return result;
        }
		
		/// <summary>
		/// 更新某个表一条记录(只适用于用单键,用int类型作键值的表)
		/// </summary>
		/// <param name="id">ID号</param>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public bool Update(int id, Dictionary<string,object>  recordField, DbTransaction trans)
		{
			return this.Update(id, recordField, tableName, trans);
		}

		/// <summary>
		/// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
		/// </summary>
		/// <param name="id">ID号</param>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public bool Update(string id, Dictionary<string,object>  recordField, DbTransaction trans)
		{
			return this.Update(id, recordField, tableName, trans);
		}

		/// <summary>
		/// 更新某个表一条记录(只适用于用单键,用int类型作键值的表)
		/// </summary>
		/// <param name="id">ID号</param>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="targetTable">需要操作的目标表名称</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        public bool Update(int id, Dictionary<string,object>  recordField, string targetTable, DbTransaction trans)
		{
			return Update(id.ToString(), recordField, targetTable, trans);
		}
        private string ConvertToEqualCondition(Dictionary<string, object> recordField)
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
		/// <summary>
		/// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
		/// </summary>
		/// <param name="id">ID号</param>
		/// <param name="recordField">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <param name="targetTable">需要操作的目标表名称</param>
		/// <param name="trans">事务对象,如果使用事务,传入事务对象,否则为Null不使用事务</param>
        private bool Update(string id, Dictionary<string,object>  recordField, string targetTable, DbTransaction trans)
		{
			if ( recordField == null || recordField.Count < 1 )
			{
				return false;
			}
            #region 拼接sql

            string setValue = ConvertToEqualCondition(recordField);
            string sql = string.Format("UPDATE {0} SET {1} WHERE {2} = @ID ", targetTable, setValue.Substring(0, setValue.Length - 1), primaryKey);

            #endregion

            int result = -1;
            #region 数据库操作
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                if (trans != null)
                {
                    result = db.ExecuteNonQuery(sql, recordField);
                }
                else
                {
                    result = db.ExecuteNonQuery(sql, recordField, trans);
                }
            }
            #endregion
 
            return result>0;
		}
        
        /// <summary>
        /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>
        /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
        /// </returns>
        public string SqlValueList(string sql)
        {
            StringBuilder result = new StringBuilder();
            DataTable dt = null;

            #region 数据库操作
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
               dt = db.ExecuteQuery(sql);
            }            
            #endregion
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    result.AppendFormat("{0},",dt.Rows[i][0]!=DBNull.Value? dt.Rows[i][0].ToString():string.Empty);
                }
            }
            string strResult = result.ToString().Trim(',');
            return strResult;
        }

        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        public DataTable SqlTable(string sql)
        {
            DataTable dt = null;
            #region 数据库操作
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                dt = db.ExecuteQuery(sql);
            }
            #endregion
            return dt;
        }

		#endregion

		#region 对象添加、修改、查询接口

		/// <summary>
		/// 插入指定对象到数据库中
		/// </summary>
		/// <param name="obj">指定的对象</param>
		/// <returns>执行成功返回新增记录的自增长ID。</returns>
        public bool Insert(T obj)
		{
            return Insert(obj, null);
		}

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="trans">事务</param>
        /// <returns>执行成功返回新增记录的自增长ID。</returns>
        public bool Insert(T obj,DbTransaction trans)
        {
            ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");
 
            Dictionary<string,object>  hash = GetHashByEntity(obj);
            return Insert(hash, trans);
        }

        /// <summary>
        /// 插入指定对象到数据库中,并返回自增长的键值
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回True</returns>
        public int Insert2(T obj)
        {
            ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");

            Dictionary<string,object>  hash = GetHashByEntity(obj);
            return Insert2(hash, null);
        }
		
		/// <summary>
		/// 更新对象属性到数据库中
		/// </summary>
		/// <param name="obj">指定的对象</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool Update(T obj, string primaryKeyValue)
		{
            return Update(obj, primaryKeyValue, null);
		}

		/// <summary>
		/// 更新对象属性到数据库中
		/// </summary>
		/// <param name="obj">指定的对象</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool Update(T obj, string primaryKeyValue, DbTransaction trans)
		{
			ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");
		
			Dictionary<string,object>  hash = GetHashByEntity(obj);
            return Update(primaryKeyValue, hash, trans);
		}

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns>bool</returns>
        public bool Update( string sql)
        {
            IDatabase db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery(sql) > 0;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="trans">事务</param>
        /// <returns>bool</returns>
        public bool Update(string sql,DbTransaction trans)
        {
            IDatabase db = DatabaseFactory.CreateDatabase();
            return db.ExecuteNonQuery(sql,trans) > 0;
        }
        
		/// <summary>
		/// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
		/// </summary>
		/// <param name="key">对象的ID值</param>
		/// <returns>存在则返回指定的对象,否则返回Null</returns>
        public T FindByID(int key)
		{			
			return FindByID(key.ToString());
		}		
		
		/// <summary>
		/// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
		/// </summary>
		/// <param name="key">对象的ID值</param>
		/// <returns>存在则返回指定的对象,否则返回Null</returns>
        public T FindByID(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            string sql = string.Format("Select {0} From {1} Where ({2} = @ID)", selectedFields, tableName, primaryKey);


            IDatabase db = DatabaseFactory.CreateDatabase();
            Dictionary<string, object> para = new Dictionary<string, object>();
            para.Add("ID", key);

            T entity = null;

            DataTable dt= db.ExecuteQuery(sql, para);
            if (dt!=null && dt.Rows.Count>0)
            {
                entity = DataRowToEntity(dt.Rows[0]);
            }

            return entity;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        public T FindSingle(string condition)
        {
            T entity = null;
            List<T> list = Find(condition);
            if (list.Count > 0)
            {
                entity = list[0];                
            }
            return entity;
        }

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">paramList</param>
        /// <returns>指定的对象</returns>
        public T FindSingle(Dictionary<string,object> paramList)
        {
            T entity = null;
            List<T> list = Find( paramList);
            if (list.Count > 0)
            {
                entity = list[0];
            }
            return entity;
        }

        /// <summary>
        /// 查找记录表中最旧的一条记录
        /// </summary>
        /// <returns></returns>
        public T FindFirst()
        {
 
            T entity = null;
 
            return entity;
        }

        /// <summary>
        /// 查找记录表中最新的一条记录
        /// </summary>
        /// <returns></returns>
        public T FindLast()
        {
   
            T entity = null;
 
            return entity;
        }

		#endregion

		#region 返回集合的接口
		
		/// <summary>
		/// 根据ID字符串(逗号分隔)获取对象列表
		/// </summary>
		/// <param name="idString">ID字符串(逗号分隔)</param>
		/// <returns>符合条件的对象列表</returns>
        public List<T> FindByIDs(string idString)
		{
			string condition = string.Format("{0} in({1})", primaryKey, idString);
			return this.Find(condition);
		}

        /// <summary>
        /// 通用获取集合对象方法
        /// </summary>
        /// <param name="sql">查询的Sql语句</param>
        /// <param name="paramList">参数列表，如果没有则为null</param>
        /// <returns></returns>
        private List<T> GetList(string sql, Dictionary<string,object> paramList)
        {
            List<T> list = new List<T>();
            IDatabase db = DatabaseFactory.CreateDatabase();
            DataTable dt = db.ExecuteQuery(sql, paramList);
            list = DataTableToEntityList(dt);
            return list;
        }


		
		/// <summary>
		/// 根据条件查询数据库,并返回对象集合
		/// </summary>
		/// <param name="condition">查询的条件</param>
		/// <returns>指定对象的集合</returns>
        public List<T> Find(string condition)
		{
			//串连条件语句为一个完整的Sql语句
			string sql = string.Format("Select {0} From {1} Where ", selectedFields, tableName);
			sql +=  condition;
            sql += string.Format(" Order by {0} {1}", sortField, isDescending ? "DESC" : "ASC"); 

            List<T> list = GetList(sql, null);
			return list;
		}

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        public List<T> Find(Dictionary<string,object> paramList)
        {
            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select {0} From {1} Where ", selectedFields, tableName);
             



            List<T> list = GetList(sql, paramList);
            return list;
        }
		
		/// <summary>
		/// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
		/// </summary>
		/// <param name="condition">查询的条件</param>
		/// <param name="info">分页实体</param>
		/// <returns>指定对象的集合</returns>
        public List<T> Find(string condition, int recordFrom, int pageSize)
		{
            List<T> list = new List<T>();
 
		 
			return list;
		}

        public DataTable FindToDataTable(string condition)
        {
            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select {0} From {1} Where ", selectedFields, tableName);
            sql += condition;
            sql += string.Format(" Order by {0} {1}", sortField, isDescending ? "DESC" : "ASC");

            return GetDataTableBySql(sql);
        }

        public DataTable FindToDataTable(string condition, Dictionary<string, object> paramList)
        {
            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select {0} From {1} Where ", selectedFields, tableName);
            sql += condition;
            sql += string.Format(" Order by {0} {1}", sortField, isDescending ? "DESC" : "ASC");

            return GetDataTableBySql(sql);
        }
		/// <summary>
		/// 返回数据库所有的对象集合
		/// </summary>
		/// <returns>指定对象的集合</returns>
        public List<T> GetAll()
		{
			string sql = string.Format("Select {0} From {1}", selectedFields, tableName);
            sql += string.Format(" Order by {0} {1}", sortField, isDescending ? "DESC" : "ASC");

            List<T> list = GetList(sql, null);
            return list;
		}
		
		/// <summary>
		/// 返回数据库所有的对象集合(用于分页数据显示)
		/// </summary>
		/// <param name="info">分页实体信息</param>
		/// <returns>指定对象的集合</returns>
        public List<T> GetAll(int recordFrom, int pageSize)
		{
            List<T> list = new  List<T>();
 
			return list;
		}

        public DataSet GetAllToDataSet(int recordFrom, int pageSize)
        {
            DataSet ds = new DataSet();
           
            return ds;
        }

        public DataTable GetAllToDataTable()
        {
            string sql = string.Format("Select {0} From {1} ", selectedFields, tableName);
            sql += string.Format(" Order by {0} {1}", sortField, isDescending ? "DESC" : "ASC");
            return GetDataTableBySql(sql);
        }



        protected DataTable GetDataTableBySql(string sql)
        {
            IDatabase db = DatabaseFactory.CreateDatabase();
            return db.ExecuteQuery(sql);
        }

        public List<string> GetFieldList(string fieldName)
        {
            string sql = string.Format("Select distinct {0} From {1} order by {0}", fieldName, tableName);

            List<string> list = new List<string>();
            IDatabase db = DatabaseFactory.CreateDatabase();

            string number = string.Empty;
            DataTable dt = db.ExecuteQuery(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                int col = dt.Columns.IndexOf(fieldName);
                if (col>-1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][col] != DBNull.Value)
                        {
                            number = dt.Rows[i][col].ToString();
                            list.Add(number);
                        }
                    }
                }
            }
            return list;
        }

		#endregion
		
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
                    catch(Exception ex){ }
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
        protected virtual Dictionary<string,object>  GetHashByEntity(T obj)
        {
            Dictionary<string,object>  ht = new Dictionary<string,object> ();
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

		#endregion
		
		#region IBaseDAL接口

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount(string condition)
        {
            string sql = string.Format("Select Count(*) from {0} WHERE {1} ", tableName, condition);

            IDatabase db = DatabaseFactory.CreateDatabase();
            
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取表的所有记录数量
        /// </summary>
        /// <returns></returns>
        public int GetRecordCount()
        {
            string sql = string.Format("Select Count(*) from {0} ", tableName);
        
            IDatabase db = DatabaseFactory.CreateDatabase();
          
            return Convert.ToInt32(db.ExecuteScalar(sql));
        }

        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        public bool IsExistRecord(string condition)
        {
            return GetRecordCount(condition) > 0;
        }

		/// <summary>
		/// 查询数据库,检查是否存在指定键值的对象
		/// </summary>
		/// <param name="recordTable">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
		/// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool IsExistKey(Dictionary<string,object>  recordTable)
		{
 
			IEnumerator eKeys = recordTable.Keys.GetEnumerator();

			string fields = "";// 字段名
			while (eKeys.MoveNext())
			{
				string field = eKeys.Current.ToString();
				fields += string.Format(" {0} = @{1} AND", field, field);
			}
            if (!string.IsNullOrEmpty(fields))
            {
                fields = fields.Substring(0, fields.Length - 3);//除去最后的AND
            }
			string sql = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tableName, fields);

            IDatabase db = DatabaseFactory.CreateDatabase();
            return Convert.ToInt32(db.ExecuteScalar(sql,recordTable)) > 0;
		}
		
		/// <summary>
		/// 查询数据库,检查是否存在指定键值的对象
		/// </summary>
		/// <param name="fieldName">指定的属性名</param>
		/// <param name="key">指定的值</param>
		/// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool IsExistKey(string fieldName, object key)
		{
			Dictionary<string,object>  table = new Dictionary<string,object> ();
			table.Add(fieldName, key);

			return IsExistKey(table);
		}						
		
		/// <summary>
		/// 获取数据库中该对象的最大ID值
		/// </summary>
		/// <returns>最大ID值</returns>
        public int GetMaxID()
		{
			string sql = string.Format("SELECT MAX({0}) AS MaxID FROM {1}", primaryKey, tableName);

            IDatabase db = DatabaseFactory.CreateDatabase();


            object obj = db.ExecuteScalar(sql);
			if(Convert.IsDBNull(obj))
			{
				return 0;//没有记录的时候为0
			}
			return Convert.ToInt32(obj);
		}
		
		/// <summary>
		/// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
		/// </summary>
		/// <param name="key">指定对象的ID</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool DeleteByKey(string key)
		{

            return DeleteByKey(key, null);
		}				

		/// <summary>
		/// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
		/// </summary>
		/// <param name="key">指定对象的ID</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool DeleteByKey(string key, DbTransaction trans)
		{

            string condition = string.Format("{0} = @ID", primaryKey);
            Dictionary<string, object> para = new Dictionary<string, object>();
            para.Add("ID", key);
 
			return DeleteByCondition(condition, trans,para);
		}				
		
		/// <summary>
		/// 根据指定条件,从数据库中删除指定对象
		/// </summary>
		/// <param name="condition">删除记录的条件语句</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool DeleteByCondition(string condition)
		{
            return DeleteByCondition(condition, null);
		}

		/// <summary>
		/// 根据指定条件,从数据库中删除指定对象
		/// </summary>
		/// <param name="condition">删除记录的条件语句</param>
		/// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool DeleteByCondition(string condition, DbTransaction trans)
		{
			return DeleteByCondition(condition, trans, null);
		} 
		
		/// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        public bool DeleteByCondition(string condition, DbTransaction trans, Dictionary<string,object> paramList)
        {
            string sql = string.Format("DELETE FROM {0} WHERE {1} ", tableName, condition);

            IDatabase db = DatabaseFactory.CreateDatabase();
          

            bool result = false;
            if (trans != null)
            {
                result = db.ExecuteNonQuery(sql,paramList, trans) > 0;
            }
            else
            {
                result = db.ExecuteNonQuery(sql, paramList) > 0;
            }

            return result;
        }

        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable FindByView(string viewName, string condition)
        {
            //串连条件语句为一个完整的Sql语句
            string sql = string.Format("Select * From {0} Where ", viewName);
            sql += condition;
           
            IDatabase db = DatabaseFactory.CreateDatabase();

            DataTable dt = db.ExecuteQuery(sql);
            return dt;
        }               		
		#endregion
	}
}