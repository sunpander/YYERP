using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace DbAccess.Sql
{
    /// <summary>
    /// OleDb的操作类
    /// </summary>
    public class OleDbDatabase : IDatabase
    {
        private System.Data.OleDb.OleDbConnection _connection;
        public OleDbDatabase(string strConnectioin)
        {
            _connection = new System.Data.OleDb.OleDbConnection(strConnectioin);
        }
        /// <summary>
        ///构造一个已含有OracleCommand对象的OracleCommand
        /// </summary>
        /// <paramKey colName="storedProcNameOrSqlString">存储过程名或带参数的SQL查询字符串</paramKey>
        /// <paramKey colName="parameters">Array of IDataParameter objects</paramKey>
        /// <paramKey colName="type">是存储过程还是查询字串</paramKey>
        /// <returns>返回SqlCommand对象，已装入参数</returns>
        /// 
        private System.Data.OleDb.OleDbCommand BuildQueryCommand(string storedProcNameOrSqlString, Dictionary<string, object> parameters, CommandType type)
        {
            storedProcNameOrSqlString = convertSql(storedProcNameOrSqlString);
            OleDbCommand command = new OleDbCommand();
            command.CommandType = type;
            //command.CommandText = storedProcNameOrSqlString;
            #region 创建parameter ,,转换sql语句
            int start = 0;
            bool startNow = false;
            int end = 0;
            string paramKey;
            string strSql = storedProcNameOrSqlString;
            // string strSql2 = storedProcNameOrSqlString;
            for (int i = 0; i < strSql.Length; i++)
            {
                if (strSql[i].Equals('@'))
                {
                    start = i;
                    startNow = true;
                }
                if (startNow)
                {
                    if (strSql[i].Equals(' ') || strSql[i].Equals('|')
                        || (i == strSql.Length - 1) || strSql[i].Equals(',')
                        || strSql[i].Equals(')') || strSql[i].Equals('='))
                    {
                        end = i;
                        if (i == strSql.Length - 1 && !strSql[i].Equals(')'))
                        {
                            paramKey = strSql.Substring(start + 1, end - start).Trim();
                        }
                        else
                        {
                            paramKey = strSql.Substring(start + 1, end - start - 1).Trim();
                        }
                        //根据param找 sql参数值
                        object value = DBNull.Value;
                        parameters.TryGetValue(paramKey, out value);
                        //OracleParameter para = new OracleParameter(kvp.Key, kvp.Value);
                        OleDbParameter para = new OleDbParameter(paramKey, value);
                        command.Parameters.Add(para);
                        int tmp = storedProcNameOrSqlString.IndexOf("@" + paramKey);
                        if (tmp > -1)
                        {
                            storedProcNameOrSqlString = storedProcNameOrSqlString.Remove(tmp, paramKey.Length + 1);

                            storedProcNameOrSqlString = storedProcNameOrSqlString.Insert(tmp, "?");
                        }
                        startNow = false;
                    }
                }
            }
            #endregion
            command.CommandText = storedProcNameOrSqlString;
            command.Connection = _connection;
            return command;
        }

        #region InterfaceDatabase 成员
        /// <summary>
        /// 通过sql语句返回DataTable
        /// </summary>
        /// <paramKey colName="strSqlDel"></paramKey>
        /// <returns></returns>
        public DataTable ExecuteQuery(string strSql)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), _connection);
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                adapter.Fill(dataTable);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public DataTable ExecuteQuery(string strSql, Dictionary<string, object> parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), _connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.Fill(dataTable);
            }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        public int ExecuteNonQuery(string strSql)
        {
            int result;
            try
            {
                _connection.Open();
                OleDbCommand command = new OleDbCommand(convertSql(strSql), _connection);
                result = command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }




        public int ExecuteNonQuery(string strSql, Dictionary<string, object> parameters)
        {
            int result;
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                OleDbCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                result = command.ExecuteNonQuery();
            }
            finally
            {
                //_connection.Close();
            }
            return result;
        }
        public int ExecuteNonQuery(string strSql, DbTransaction trans)
        {
            if (trans == null)
                return ExecuteNonQuery(strSql);
            int result;
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbCommand command = new OleDbCommand(convertSql(strSql), oraTrans.Connection, oraTrans);
                result = command.ExecuteNonQuery();
            }
            finally
            {

            }
            return result;
        }

        public int ExecuteNonQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
        {
            if (trans == null)
                return ExecuteNonQuery(strSql, parameters);
            int result;
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                command.Transaction = oraTrans;
                command.Connection = oraTrans.Connection;

                result = command.ExecuteNonQuery();
            }
            finally
            {
            }
            return result;
        }
        #endregion
        private string convertSql(string sql)
        {
            //return sql.Replace("@", ":");
            //去掉类似 @id
            return sql;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            //释放托管资源
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }

        #endregion


        #region InterfaceDatabase 成员


        public DbTransaction GetTransction()
        {
            //throw new NotImplementedException();
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return (DbTransaction)_connection.BeginTransaction();
            }
            return null;
        }

        #endregion

        #region InterfaceDatabase 成员
        public DataTable ExecuteQuery(string strSql, DbTransaction trans)
        {
            if (trans == null)
                return ExecuteQuery(strSql);
            OleDbTransaction oraTrans = (OleDbTransaction)trans;
            OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), oraTrans.Connection);
            DataTable dataTable = new DataTable();
            try
            {
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.Fill(dataTable);
            }
            finally
            {
                //_connection.Close();
            }
            return dataTable;
        }

        public DataTable ExecuteQuery(string strSql, Dictionary<string, object> parameters, DbTransaction trans)
        {
            if (trans == null)
                return ExecuteQuery(strSql, parameters);
            DataTable dataTable = new DataTable();
            try
            {
                OleDbTransaction oraTrans = (OleDbTransaction)trans;
                OleDbDataAdapter adapter = new OleDbDataAdapter(convertSql(strSql), oraTrans.Connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.Fill(dataTable);
            }
            finally
            {

            }
            return dataTable;
        }

        #endregion

        #region IDatabase 成员


        public object ExecuteScalar(string strSql)
        {
            DataTable dt=  ExecuteQuery(strSql);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                return dt.Rows[0][0];
            return null;
        }

        public object ExecuteScalar(string strSql, Dictionary<string, object> parameters)
        {
            DataTable dt = ExecuteQuery(strSql,parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                return dt.Rows[0][0];
            return null;
        }

        #endregion
    }
}
