using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace DbAccess.Sql
{
    /// <summary>
    /// 一些基本的，作为辅助函数的接口
    /// :主要是对单表的操作.
    /// (1)判断记录是否存在()
    /// (2)查找单条记录转换为T类对象实例
    /// 
    /// </summary>
    public interface IDatabaseDAL<T> where T : class
    {
        #region 1。判断记录是否存在
        /// <summary>
        /// 根据condition条件，判断是否存在记录
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>如果存在返回True，否则False</returns>
        bool IsExistRecord(string condition);

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="recordTable">Dictionary<string,object> :键[key]为字段名;值[value]为字段对应的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistRecord(Dictionary<string, object> recordTable);

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistRecord(string fieldName, object fildValue);
        #endregion

        #region 2。查找单条记录对象
        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于整型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        T FindByID(int key);

        /// <summary>
        /// 查询数据库,检查是否存在指定ID的对象(用于字符型主键)
        /// </summary>
        /// <param name="key">对象的ID值</param>
        /// <returns>存在则返回指定的对象,否则返回Null</returns>
        T FindByID(string key);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        T FindSingle(string condition);

        /// <summary>
        /// 根据条件查询数据库,如果存在返回第一个对象
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定的对象</returns>
        T FindSingle(Dictionary<string, object> paramList);
        #endregion

        #region 3。返回对象List<T>集合
        /// <summary>
        /// 根据ID字符串(逗号分隔)获取对象列表
        /// </summary>
        /// <param name="idString">ID字符串(逗号分隔)</param>
        /// <returns>符合条件的对象列表</returns>
        List<T> FindByIDs(string idString);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <returns>指定对象的集合</returns>
        List<T> Find(string condition);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="recordFrom">记录起始条数</param>
        /// <param name="pageSize">查询记录数</param>
        /// <returns>对象列表</returns>
        List<T> Find(string condition, int recordFrom, int pageSize);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <returns>指定对象的集合</returns>
        List<T> Find(Dictionary<string, object> paramList);

        /// <summary>
        /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="condition">查询的条件</param>
        /// <param name="info">分页实体</param>
        /// <returns>指定对象的集合</returns>
        List<T> Find(Dictionary<string, object> paramList, int recordFrom, int pageSize);

        /// <summary>
        /// 返回数据库所有的对象集合
        /// </summary>
        /// <returns>指定对象的集合</returns>
        List<T> FindAll();

        /// <summary>
        /// 返回数据库所有的对象集合(用于分页数据显示)
        /// </summary>
        /// <param name="recordFrom"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<T> FindAll(int recordFrom, int pageSize);
        #endregion

        #region 4.返回DataTable集合

        /// <summary>
        /// 根据分页条件，返回DataTable对象
        /// </summary>
        /// <param name="info">分页条件</param>
        /// <returns></returns>
        DataTable FindToDataTable(int recordFrom, int pageSize);

        /// <summary>
        /// 返回所有记录到DataTable集合中
        /// </summary>
        /// <returns></returns>
        DataTable FindToDataTable();

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataTable FindToDataTable(string condition);

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataTable FindToDataTable(string condition,int recordFrom, int pageSize);

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataTable FindToDataTable(Dictionary<string,object> condition);

        /// <summary>
        /// 根据查询条件，返回记录到DataTable集合中
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataTable FindToDataTable(Dictionary<string, object> condition, int recordFrom, int pageSize);
        /// <summary>
        /// 根据条件，从视图里面获取记录
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        DataTable FindByView(string viewName, string condition);
        #endregion

        #region 5.获取最大id值
        /// <summary>
        /// 获取数据库中该对象的最大ID值
        /// </summary>
        /// <returns>最大ID值</returns>
        int GetMaxID();
        #endregion

        #region 6.插入记录
        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回True</returns>
        bool Insert(T obj);

        /// <summary>
        /// 插入指定对象到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回True</returns>
        bool Insert(T obj, DbTransaction trans);
        #endregion

        #region 7.更新记录
        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Update(T obj, string primaryKeyValue);

        /// <summary>
        /// 更新对象属性到数据库中
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="trans">事务</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Update(T obj, string primaryKeyValue, DbTransaction trans);
        #endregion

        #region 8. 删除记录
        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByKey(string key);

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByKey(string key, DbTransaction trans);

        /// <summary>
        /// 根据条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByCondition(string condition);

        /// <summary>
        /// 根据条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByCondition(string condition, DbTransaction trans);

        /// <summary>
        /// 根据条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByCondition(Dictionary<string, object> paramList);

        /// <summary>
        /// 根据指定条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool DeleteByCondition(Dictionary<string, object> paramList, DbTransaction trans);
        #endregion

        #region 9.直接执行sql语句
        /// <summary>
        /// 执行SQL查询语句，返回所有记录的DataTable集合。
        /// </summary>
        /// <param name="sql">SQL查询语句</param>
        /// <returns></returns>
        DataTable ExecuteQuery(string sql);
        bool ExecuteNonQuery(string sql);
        bool ExecuteNonQuery(string sql, DbTransaction trans);
        #endregion

    }
}