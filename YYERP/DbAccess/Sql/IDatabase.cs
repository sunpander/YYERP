using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DbAccess.Sql
{
    /// <summary>
    /// 数据库操作类接口
    /// </summary>
    public interface IDatabase:IDisposable
    {
        DataTable ExecuteQuery(string strSql);
        DataTable ExecuteQuery(string strSql, DbTransaction trans);
        DataTable ExecuteQuery(string strSql, Dictionary<string,object>  parameters);
        DataTable ExecuteQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans);

        object ExecuteScalar(string strSql);
        object ExecuteScalar(string strSql, Dictionary<string, object> parameters);

        int ExecuteNonQuery(string strSql);
        int ExecuteNonQuery(string strSql, DbTransaction trans);
        int ExecuteNonQuery(string strSql, Dictionary<string, object> parameters);
        int ExecuteNonQuery(string strSql, Dictionary<string, object> parameters,DbTransaction trans );
 
        DbTransaction GetTransction();
 
    }

}
