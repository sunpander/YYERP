using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using System.Data.SQLite;

namespace DbAccess.Sql
{
   public class SQLiteDatabase : IDatabase
    {
       private System.Data.SQLite.SQLiteConnection _connection;
        public SQLiteDatabase(string strConnectioin)
        {
            _connection = new System.Data.SQLite.SQLiteConnection(strConnectioin);
        }
        private void AnalyzeConnectionString(string strConnectioin)
        {
              
            
            //StringOperation myStringOperation = new StringOperation();
            //if (myStringOperation.CheckPoint("\\[Passwords=\"[^\"]+\"\\]", strConnectioin))
            //{
            //  string  dataBaseStr = myStringOperation.ReplaceDXSTR(strConnectioin, "\\[Passwords=\"[^\"]+\"\\]", "");
            //    string passwordStr = myStringOperation.GetNoDXSingleSTR("\\[Passwords=\"[^\"]+\"\\]", strConnectioin);
            //    passwordStr = myStringOperation.GetNoDXSingleSTR("(?<=\")[^\"]+(?=\")", passwordStr);
            //    _connection = new System.Data.SQLite.SQLiteConnection(dataBaseStr);
            //    if (passwordStr != string.Empty)
            //        _connection.SetPassword(passwordStr);
                
            //}
            //else
            //    _connection = new System.Data.SQLite.SQLiteConnection(strConnectioin);
        }
        /// <summary>
        ///构造一个已含有OracleCommand对象的OracleCommand
        /// </summary>
        /// <paramKey colName="storedProcNameOrSqlString">存储过程名或带参数的SQL查询字符串</paramKey>
        /// <paramKey colName="parameters">Array of IDataParameter objects</paramKey>
        /// <paramKey colName="type">是存储过程还是查询字串</paramKey>
        /// <returns>返回SqlCommand对象，已装入参数</returns>
        /// 
        private SQLiteCommand BuildQueryCommand(string storedProcNameOrSqlString, Dictionary<string, object> parameters, CommandType type)
        {
            try
            {
                storedProcNameOrSqlString = convertSql(storedProcNameOrSqlString);
                SQLiteCommand command = new SQLiteCommand();
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
                            SQLiteParameter para = new SQLiteParameter(paramKey, value);
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
            catch (Exception ex)
            {
                    throw ex;
            }
        }


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
        /// <summary>
        /// 通过sql语句返回DataTable
        /// </summary>
        /// <paramKey colName="strSqlDel"></paramKey>
        /// <returns></returns>
        public DataTable ExecuteQuery(string strSql)
        {
           
            DataTable dataTable = new DataTable();
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(convertSql(strSql), _connection);
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                    throw ex;
               
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
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(convertSql(strSql), _connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                    throw ex;
              
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
                SQLiteCommand command = new SQLiteCommand(convertSql(strSql), _connection);
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                    throw ex;
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
                SQLiteCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                    throw ex;
               
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
                SQLiteTransaction oraTrans = (SQLiteTransaction)trans;
                SQLiteCommand command = new SQLiteCommand(convertSql(strSql), oraTrans.Connection, oraTrans);
                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                    throw ex;
               
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
                SQLiteTransaction oraTrans = (SQLiteTransaction)trans;
                SQLiteCommand command = BuildQueryCommand(convertSql(strSql), parameters, CommandType.Text);
                command.Transaction = oraTrans;
                command.Connection = oraTrans.Connection;

                result = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                    throw ex;
               
            }
            finally
            {
            }
            return result;
        }







        #endregion

        #region InterfaceDatabase 成员(获取事务)


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

        #region InterfaceDatabase 成员(事务)
        public DataTable ExecuteQuery(string strSql, DbTransaction trans)
        {
            if (trans == null)
                return ExecuteQuery(strSql);
            SQLiteTransaction oraTrans = (SQLiteTransaction)trans;
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(convertSql(strSql), oraTrans.Connection);
            DataTable dataTable = new DataTable();
            try
            {
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                    throw ex;
               
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
                SQLiteTransaction oraTrans = (SQLiteTransaction)trans;
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(convertSql(strSql), oraTrans.Connection);
                adapter.SelectCommand = BuildQueryCommand(strSql, parameters, CommandType.Text);
                adapter.SelectCommand.Transaction = oraTrans;
                adapter.SelectCommand.Connection = oraTrans.Connection;
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                    throw ex;
              
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
            DataTable dt = ExecuteQuery(strSql);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                return dt.Rows[0][0];
            return null;
        }

        public object ExecuteScalar(string strSql, Dictionary<string, object> parameters)
        {
            DataTable dt = ExecuteQuery(strSql, parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                return dt.Rows[0][0];
            return null;
        }

        #endregion



        #region IDatabase 成员


        public DataSet ExecuteQuery(List<string> sqlStrList)
        {
            DataSet ds = new DataSet();
            foreach (string s in sqlStrList)
            {
                DataTable dt = this.ExecuteQuery(s);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                }
            }
            return ds;
            //throw new NotImplementedException();
        }

        public DataSet ExecuteQuery(List<string> sqlStrList, DbTransaction trans)
        {
            DataSet ds = new DataSet();
            foreach (string s in sqlStrList)
            {
                DataTable dt = this.ExecuteQuery(s, trans);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                }
            }
           
            trans.Commit();
            return ds;
        }

        public DataSet ExecuteQuery(List<string> sqlStrList, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteQuery(List<string> sqlStrList, Dictionary<string, object> parameters, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(List<string> strSql)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(List<string> strSqlList, DbTransaction trans)
        {
            trans.Connection.BeginTransaction();
            foreach (string sqlstr in strSqlList)
            {
                ExecuteNonQuery(sqlstr, trans);
            }
            trans.Commit();
            return 0;
        }

        public int ExecuteNonQuery(List<string> strSql, Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(List<string> strSql, Dictionary<string, object> parameters, DbTransaction trans)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
