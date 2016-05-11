namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Text;
    using System.Xml;

    internal class SqlService
    {
        protected bool _autoCloseConnection;
        protected int _commandTimeout;
        protected SqlConnection _connection;
        protected string _connectionString;
        protected bool _convertEmptyValuesToDbNull;
        protected bool _convertMaxValuesToDbNull;
        protected bool _convertMinValuesToDbNull;
        protected bool _isSingleRow;
        protected SqlParameterCollection _parameterCollection;
        protected ArrayList _parameters;
        protected SqlTransaction _transaction;
              
       

        public SqlService(string dbName)
        {
            this._connectionString = string.Empty;
            this._parameters = new ArrayList();
            this._convertEmptyValuesToDbNull = true;
            this._convertMinValuesToDbNull = true;
            this._autoCloseConnection = true;
            this._commandTimeout = 30;
            this._connectionString = dbName;
        }

      

        public SqlParameter AddOutputParameter(string name, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Output,
                ParameterName = name,
                SqlDbType = type
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddOutputParameter(string name, SqlDbType type, int size)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Output,
                ParameterName = name,
                SqlDbType = type,
                Size = size
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public void AddParameter(SqlParameter parameter)
        {
            this._parameters.Add(parameter);
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                SqlDbType = type,
                Value = this.PrepareSqlValue(value)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, DbType type, object value, bool convertZeroToDBNull)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                DbType = type,
                Value = this.PrepareSqlValue(value, convertZeroToDBNull)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, bool convertZeroToDBNull)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                SqlDbType = type,
                Value = this.PrepareSqlValue(value, convertZeroToDBNull)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = direction,
                ParameterName = name,
                SqlDbType = type,
                Value = this.PrepareSqlValue(value)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, int size)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                SqlDbType = type,
                Size = size,
                Value = this.PrepareSqlValue(value)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, int size, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = direction,
                ParameterName = name,
                SqlDbType = type,
                Size = size,
                Value = this.PrepareSqlValue(value)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddReturnValueParameter()
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.ReturnValue,
                ParameterName = "@ReturnValue",
                SqlDbType = SqlDbType.Int
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddStreamParameter(string name, Stream value)
        {
            return this.AddStreamParameter(name, value, SqlDbType.Image);
        }

        public SqlParameter AddStreamParameter(string name, Stream value, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                SqlDbType = type
            };
            value.Position = 0;
            byte[] buffer = new byte[value.Length];
            value.Read(buffer, 0, (int) value.Length);
            parameter.Value = buffer;
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddTextParameter(string name, string value)
        {
            SqlParameter parameter = new SqlParameter {
                Direction = ParameterDirection.Input,
                ParameterName = name,
                SqlDbType = SqlDbType.Text,
                Value = this.PrepareSqlValue(value)
            };
            this._parameters.Add(parameter);
            return parameter;
        }

        public void BeginTransaction()
        {
            if (this._connection == null)
            {
                throw new InvalidOperationException("You must have a valid connection object before calling BeginTransaction.");
            }
            this._transaction = this._connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (this._transaction != null)
            {
                try
                {
                    this._transaction.Commit();
                    return;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new InvalidOperationException("You must call BeginTransaction before calling CommitTransaction.");
        }

        public void Connect()
        {
            if (this._connection != null)
            {
                if (this._connection.State != ConnectionState.Open)
                {
                    this._connection.Open();
                }
            }
            else
            {
                if (!(this._connectionString != string.Empty))
                {
                    throw new InvalidOperationException("You must set a connection object or specify a connection string before calling Connect.");
                }
                StringCollection strings = new StringCollection();
                strings.AddRange(new string[] { "ARITHABORT", "ANSI_NULLS", "ANSI_WARNINGS", "ARITHIGNORE", "ANSI_DEFAULTS", "ANSI_NULL_DFLT_OFF", "ANSI_NULL_DFLT_ON", "ANSI_PADDING", "ANSI_WARNINGS" });
                StringBuilder builder = new StringBuilder();
                StringBuilder builder2 = new StringBuilder();
                Hashtable hashtable = this.ParseConfigString(this._connectionString);
                foreach (string str in hashtable.Keys)
                {
                    if (strings.Contains(str.Trim().ToUpper()))
                    {
                        builder.AppendFormat("SET {0} {1};", str, hashtable[str]);
                    }
                    else if (str.Trim().Length > 0)
                    {
                        builder2.AppendFormat("{0}={1};", str, hashtable[str]);
                    }
                }
                this._connection = new SqlConnection(builder2.ToString());
                this._connection.Open();
                if (builder.Length > 0)
                {
                    SqlCommand command = new SqlCommand {
                        CommandTimeout = this.CommandTimeout,
                        CommandText = builder.ToString(),
                        Connection = this._connection,
                        CommandType = CommandType.Text
                    };
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }
        }

        private void CopyParameters(SqlCommand command)
        {
            for (int i = 0; i < this._parameters.Count; i++)
            {
                command.Parameters.Add(this._parameters[i]);
            }
        }

        public void Disconnect()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
            if (this._connection != null)
            {
                this._connection.Dispose();
            }
            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }
            this._transaction = null;
            this._connection = null;
        }

        public SafeDataReader ExecuteSafeReader(string sql)
        {
            return new SafeDataReader(this.ExecuteSqlReader(sql));
        }

        public void ExecuteSP(string procedureName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            command.ExecuteNonQuery();
            this._parameterCollection = command.Parameters;
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }

        public DataSet ExecuteSPDataSet(string procedureName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
            this._parameterCollection = command.Parameters;
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public DataSet ExecuteSPDataSet(string procedureName, string tableName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            this._parameterCollection = command.Parameters;
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public void ExecuteSPDataSet(ref DataSet dataSet, string procedureName, string tableName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            this._parameterCollection = command.Parameters;
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }

        public SqlDataReader ExecuteSPReader(string procedureName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            CommandBehavior behavior = CommandBehavior.Default;
            if (this.AutoCloseConnection)
            {
                behavior |= CommandBehavior.CloseConnection;
            }
            if (this._isSingleRow)
            {
                behavior |= CommandBehavior.SingleRow;
            }
            SqlDataReader reader = command.ExecuteReader(behavior);
            this._parameterCollection = command.Parameters;
            command.Dispose();
            return reader;
        }

        public XmlReader ExecuteSPXmlReader(string procedureName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = procedureName;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.StoredProcedure;
            this.CopyParameters(command);
            XmlReader reader = command.ExecuteXmlReader();
            this._parameterCollection = command.Parameters;
            command.Dispose();
            return reader;
        }

        public void ExecuteSql(string sql)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = sql;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }

        public DataSet ExecuteSqlDataSet(string sql)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            command.CommandTimeout = this.CommandTimeout;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet);
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public DataSet ExecuteSqlDataSet(string sql, string tableName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            command.CommandTimeout = this.CommandTimeout;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public void ExecuteSqlDataSet(ref DataSet dataSet, string sql, string tableName)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter();
            command.CommandTimeout = this.CommandTimeout;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandText = sql;
            command.CommandType = CommandType.Text;
            adapter.SelectCommand = command;
            adapter.Fill(dataSet, tableName);
            adapter.Dispose();
            command.Dispose();
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }

        public SqlDataReader ExecuteSqlReader(string sql)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = sql;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.Text;
            this.CopyParameters(command);
            CommandBehavior behavior = CommandBehavior.Default;
            if (this.AutoCloseConnection)
            {
                behavior |= CommandBehavior.CloseConnection;
            }
            if (this._isSingleRow)
            {
                behavior |= CommandBehavior.SingleRow;
            }
            SqlDataReader reader = command.ExecuteReader(behavior);
            command.Dispose();
            return reader;
        }

        public XmlReader ExecuteSqlXmlReader(string sql)
        {
            SqlCommand command = new SqlCommand();
            this.Connect();
            command.CommandTimeout = this.CommandTimeout;
            command.CommandText = sql;
            command.Connection = this._connection;
            if (this._transaction != null)
            {
                command.Transaction = this._transaction;
            }
            command.CommandType = CommandType.Text;
            XmlReader reader = command.ExecuteXmlReader();
            command.Dispose();
            return reader;
        }

        private Hashtable ParseConfigString(string config)
        {
            Hashtable hashtable = new Hashtable(10, StringComparer.InvariantCultureIgnoreCase);
            string[] strArray = config.Split(new char[] { ';' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '=' });
                if (strArray2.Length == 2)
                {
                    hashtable.Add(strArray2[0].Trim(), strArray2[1].Trim());
                }
                else
                {
                    hashtable.Add(strArray[i].Trim(), null);
                }
            }
            return hashtable;
        }

        private object PrepareSqlValue(object value)
        {
            return this.PrepareSqlValue(value, false);
        }

        private object PrepareSqlValue(object value, bool convertZeroToDBNull)
        {
            if (value is string)
            {
                if (this.ConvertEmptyValuesToDbNull && (((string) value) == string.Empty))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is Guid)
            {
                if (this.ConvertEmptyValuesToDbNull && (((Guid) value) == Guid.Empty))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is DateTime)
            {
                if ((!this.ConvertMinValuesToDbNull || (((DateTime) value) != DateTime.MinValue)) && (!this.ConvertMaxValuesToDbNull || !(((DateTime) value) == DateTime.MaxValue)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (value is short)
            {
                if (((!this.ConvertMinValuesToDbNull || (((short) value) != -32768)) && (!this.ConvertMaxValuesToDbNull || (((short) value) != 0x7fff))) && (!convertZeroToDBNull || (((short) value) != 0)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (value is int)
            {
                if (((!this.ConvertMinValuesToDbNull || (((int) value) != -2147483648)) && (!this.ConvertMaxValuesToDbNull || (((int) value) != 0x7fffffff))) && (!convertZeroToDBNull || (((int) value) != 0)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (value is long)
            {
                if (((!this.ConvertMinValuesToDbNull || (((long) value) != -9223372036854775808L)) && (!this.ConvertMaxValuesToDbNull || (((long) value) != 0x7fffffffffffffffL))) && (!convertZeroToDBNull || (((long) value) != 0)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (value is float)
            {
                if (((!this.ConvertMinValuesToDbNull || (((float) value) != float.MinValue)) && (!this.ConvertMaxValuesToDbNull || (((float) value) != float.MaxValue))) && (!convertZeroToDBNull || (((float) value) != 0f)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (value is double)
            {
                if (((!this.ConvertMinValuesToDbNull || (((double) value) != double.MinValue)) && (!this.ConvertMaxValuesToDbNull || (((double) value) != double.MaxValue))) && (!convertZeroToDBNull || (((double) value) != 0.0)))
                {
                    return value;
                }
                return DBNull.Value;
            }
            if (!(value is decimal))
            {
                return value;
            }
            if (((!this.ConvertMinValuesToDbNull || (((decimal) value) != -79228162514264337593543950335M)) && (!this.ConvertMaxValuesToDbNull || (((decimal) value) != 79228162514264337593543950335M))) && (!convertZeroToDBNull || !(((decimal) value) == 0M)))
            {
                return value;
            }
            return DBNull.Value;
        }

        public void Reset()
        {
            if (this._parameters != null)
            {
                this._parameters.Clear();
            }
            if (this._parameterCollection != null)
            {
                this._parameterCollection = null;
            }
        }

        public void RollbackTransaction()
        {
            if (this._transaction != null)
            {
                try
                {
                    this._transaction.Rollback();
                    return;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            throw new InvalidOperationException("You must call BeginTransaction before calling RollbackTransaction.");
        }

        public bool AutoCloseConnection
        {
            get
            {
                return this._autoCloseConnection;
            }
            set
            {
                this._autoCloseConnection = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                this._commandTimeout = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
                this.ConnectionString = this._connection.ConnectionString;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }

        public bool ConvertEmptyValuesToDbNull
        {
            get
            {
                return this._convertEmptyValuesToDbNull;
            }
            set
            {
                this._convertEmptyValuesToDbNull = value;
            }
        }

        public bool ConvertMaxValuesToDbNull
        {
            get
            {
                return this._convertMaxValuesToDbNull;
            }
            set
            {
                this._convertMaxValuesToDbNull = value;
            }
        }

        public bool ConvertMinValuesToDbNull
        {
            get
            {
                return this._convertMinValuesToDbNull;
            }
            set
            {
                this._convertMinValuesToDbNull = value;
            }
        }

        public bool IsSingleRow
        {
            get
            {
                return this._isSingleRow;
            }
            set
            {
                this._isSingleRow = value;
            }
        }

        public SqlParameterCollection Parameters
        {
            get
            {
                return this._parameterCollection;
            }
        }

        public int ReturnValue
        {
            get
            {
                if (!this._parameterCollection.Contains("@ReturnValue"))
                {
                    throw new Exception("You must call the AddReturnValueParameter method before executing your request.");
                }
                return (int) this._parameterCollection["@ReturnValue"].Value;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
            set
            {
                this._transaction = value;
            }
        }
    }
}

