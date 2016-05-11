using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Transactions;
using System.Globalization;
using SchemaExplorer;

namespace YYERP.Dao
{
    public abstract partial class Database
    {
        
        private string connectionString;
        public Database()
        {}

         public Database(string _connectionString, System.Data.Common.DbProviderFactory dbProviderFactory)
        {
            if (string.IsNullOrEmpty(_connectionString))
             {
                 throw new ArgumentException("", "connectionString");
             }
             if (dbProviderFactory == null)
             {
                 throw new ArgumentNullException("dbProviderFactory");
             }
             this.connectionString = _connectionString;
             this.dbProviderFactory = dbProviderFactory;
         }
        private System.Data.Common.DbProviderFactory dbProviderFactory;

        public void AddInParameter(DbCommand command, string name, DbType dbType)
        {
            this.AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, null);
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType, object value)
        {
            this.AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, value);
        }

        public void AddInParameter(DbCommand command, string name, DbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            this.AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
        }

        public void AddOutParameter(DbCommand command, string name, DbType dbType, int size)
        {
            this.AddParameter(command, name, dbType, size, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value);
        }

        public void AddParameter(DbCommand command, string name, DbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            this.AddParameter(command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }

        public virtual void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter parameter = this.CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }

        private void AssignParameterValues(DbCommand command, object[] values)
        {
            int num = this.UserParametersStartIndex();
            for (int i = 0; i < values.Length; i++)
            {
                IDataParameter parameter = command.Parameters[i + num];
                this.SetParameterValue(command, parameter.ParameterName, values[i]);
            }
        }

        private DbTransaction BeginTransaction(DbConnection connection)
        {
            return connection.BeginTransaction();
        }

        public virtual string BuildParameterName(string name)
        {
            return name;
        }

        private void CommitTransaction(DbTransaction tran)
        {
            tran.Commit();
        }

        protected virtual void ConfigureParameter(DbParameter param, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.DbType = dbType;
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        public DbCommand CreateCommandByCommandType(CommandType commandType, string commandText)
        {
            DbCommand command = this.dbProviderFactory.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }

        public virtual DbConnection CreateConnection()
        {
            DbConnection connection = this.dbProviderFactory.CreateConnection();
            connection.ConnectionString = this.ConnectionString;
            return connection;
        }

        protected DbParameter CreateParameter(string name)
        {
            DbParameter parameter = this.dbProviderFactory.CreateParameter();
            parameter.ParameterName = this.BuildParameterName(name);
            return parameter;
        }

        protected DbParameter CreateParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter param = this.CreateParameter(name);
            this.ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        protected abstract void DeriveParameters(DbCommand discoveryCommand);
        public void DiscoverParameters(DbCommand command)
        {
            using (ConnectionWrapper wrapper = this.GetOpenConnection())
            {
                using (DbCommand command2 = this.CreateCommandByCommandType(command.CommandType, command.CommandText))
                {
                    command2.Connection = wrapper.Connection;
                    this.DeriveParameters(command2);
                    foreach (IDataParameter parameter in command2.Parameters)
                    {
                        IDataParameter parameter2 = (IDataParameter) ((ICloneable) parameter).Clone();
                        command.Parameters.Add(parameter2);
                    }
                }
            }
        }

        protected int DoExecuteNonQuery(DbCommand command)
        {
            int num2;
            try
            {
                DateTime now = DateTime.Now;
                int num = command.ExecuteNonQuery();
                num2 = num;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return num2;
        }

        private IDataReader DoExecuteReader(DbCommand command, CommandBehavior cmdBehavior)
        {
            IDataReader reader2;
            try
            {
                DateTime now = DateTime.Now;
                IDataReader reader = command.ExecuteReader(cmdBehavior);
                reader2 = reader;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return reader2;
        }

        private object DoExecuteScalar(DbCommand command)
        {
            object obj3;
            try
            {
                DateTime now = DateTime.Now;
                object obj2 = command.ExecuteScalar();
                obj3 = obj2;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return obj3;
        }

        private void DoLoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            if (tableNames == null)
            {
                throw new ArgumentNullException("tableNames");
            }
            if (tableNames.Length == 0)
            {
                throw new ArgumentException("TableNameArrayEmpty", "tableNames");
            }
            for (int i = 0; i < tableNames.Length; i++)
            {
                if (string.IsNullOrEmpty(tableNames[i]))
                {
                    throw new ArgumentException("NullOrEmptyString", "tableNames[" + i + "]");
                }
            }
            using (DbDataAdapter adapter = this.GetDataAdapter(UpdateBehavior.Standard))
            {
                ((IDbDataAdapter) adapter).SelectCommand = command;
                try
                {
                    DateTime now = DateTime.Now;
                    string str = "Table";
                    for (int j = 0; j < tableNames.Length; j++)
                    {
                        string sourceTable = (j == 0) ? str : (str + j);
                        adapter.TableMappings.Add(sourceTable, tableNames[j]);
                    }
                    adapter.Fill(dataSet);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private int DoUpdateDataSet(UpdateBehavior behavior, DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, int? updateBatchSize)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("NullOrEmptyString", "tableName");
            }
            if (dataSet == null)
            {
                throw new ArgumentNullException("dataSet");
            }
            if (((insertCommand == null) && (updateCommand == null)) && (deleteCommand == null))
            {
                throw new ArgumentException("MessageUpdateDataSetArgumentFailure");
            }
            using (DbDataAdapter adapter = this.GetDataAdapter(behavior))
            {
                IDbDataAdapter adapter2 = adapter;
                if (insertCommand != null)
                {
                    adapter2.InsertCommand = insertCommand;
                }
                if (updateCommand != null)
                {
                    adapter2.UpdateCommand = updateCommand;
                }
                if (deleteCommand != null)
                {
                    adapter2.DeleteCommand = deleteCommand;
                }
                if (updateBatchSize.HasValue)
                {
                    adapter.UpdateBatchSize = updateBatchSize.Value;
                    if (insertCommand != null)
                    {
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    }
                    if (updateCommand != null)
                    {
                        adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    }
                    if (deleteCommand != null)
                    {
                        adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                    }
                }
                try
                {
                    DateTime now = DateTime.Now;
                    int num = adapter.Update(dataSet.Tables[tableName]);
                    return num;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public virtual DataSet ExecuteDataSet(DbCommand command)
        {
            DataSet dataSet = new DataSet {
                Locale = CultureInfo.InvariantCulture
            };
            this.LoadDataSet(command, dataSet, "Table");
            return dataSet;
        }

        public virtual DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
              
                return this.ExecuteDataSet(command);
            }
        }

        public virtual DataSet ExecuteDataSet(DbCommand command, DbTransaction transaction)
        {
            DataSet dataSet = new DataSet {
                Locale = CultureInfo.InvariantCulture
            };
            this.LoadDataSet(command, dataSet, "Table", transaction);
            return dataSet;
        }

        public virtual DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteDataSet(command);
            }
        }

        public virtual DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteDataSet(command, transaction);
            }
        }

        public virtual DataSet ExecuteDataSet(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteDataSet(command, transaction);
            }
        }

        public virtual int ExecuteNonQuery(DbCommand command)
        {
            using (ConnectionWrapper wrapper = this.GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return this.DoExecuteNonQuery(command);
            }
        }

        public virtual int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteNonQuery(command);
            }
        }

        public virtual int ExecuteNonQuery(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return this.DoExecuteNonQuery(command);
        }

        public virtual int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteNonQuery(command);
            }
        }

        public virtual int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteNonQuery(command, transaction);
            }
        }

        public virtual int ExecuteNonQuery(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteNonQuery(command, transaction);
            }
        }

        public virtual IDataReader ExecuteReader(DbCommand command)
        {
            IDataReader reader;
            ConnectionWrapper openConnection = this.GetOpenConnection(false);
            try
            {
                PrepareCommand(command, openConnection.Connection);
                if (Transaction.Current != null)
                {
                    return this.DoExecuteReader(command, CommandBehavior.Default);
                }
                reader = this.DoExecuteReader(command, CommandBehavior.CloseConnection);
            }
            catch
            {
                openConnection.Connection.Close();
                throw;
            }
            return reader;
        }

        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteReader(command);
            }
        }

        public virtual IDataReader ExecuteReader(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return this.DoExecuteReader(command, CommandBehavior.Default);
        }

        public IDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteReader(command);
            }
        }

        public IDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteReader(command, transaction);
            }
        }

        public IDataReader ExecuteReader(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteReader(command, transaction);
            }
        }

        public virtual object ExecuteScalar(DbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            using (ConnectionWrapper wrapper = this.GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return this.DoExecuteScalar(command);
            }
        }

        public virtual object ExecuteScalar(CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteScalar(command);
            }
        }

        public virtual object ExecuteScalar(DbCommand command, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return this.DoExecuteScalar(command);
        }

        public virtual object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteScalar(command);
            }
        }

        public virtual object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                return this.ExecuteScalar(command, transaction);
            }
        }

        public virtual object ExecuteScalar(DbTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return this.ExecuteScalar(command, transaction);
            }
        }

        public DbDataAdapter GetDataAdapter()
        {
            return this.GetDataAdapter(UpdateBehavior.Standard);
        }

        protected DbDataAdapter GetDataAdapter(UpdateBehavior updateBehavior)
        {
            DbDataAdapter adapter = this.dbProviderFactory.CreateDataAdapter();
            if (updateBehavior == UpdateBehavior.Continue)
            {
                this.SetUpRowUpdatedEvent(adapter);
            }
            return adapter;
        }

       

        internal DbConnection GetNewOpenConnection()
        {
            DbConnection connection = null;
            try
            {
                try
                {
                    connection = this.CreateConnection();
                    connection.Open();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            catch
            {
                if (connection != null)
                {
                    connection.Close();
                }
                throw;
            }
            return connection;
        }

        protected ConnectionWrapper GetOpenConnection()
        {
            return this.GetOpenConnection(true);
        }

        protected ConnectionWrapper GetOpenConnection(bool disposeInnerConnection)
        {
            DbConnection connection = TransactionScopeConnections.GetConnection(this);
            if (connection != null)
            {
                return new ConnectionWrapper(connection, false);
            }
            return new ConnectionWrapper(this.GetNewOpenConnection(), disposeInnerConnection);
        }

        public virtual object GetParameterValue(DbCommand command, string name)
        {
            return command.Parameters[this.BuildParameterName(name)].Value;
        }

        public DbCommand GetSqlStringCommand(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentException("NullOrEmptyString", "query");
            }
            return this.CreateCommandByCommandType(CommandType.Text, query);
        }

        public virtual DbCommand GetStoredProcCommand(string storedProcedureName)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentException("NullOrEmptyString", "storedProcedureName");
            }
            return this.CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
        }

        public virtual DbCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentException("NullOrEmptyString", "storedProcedureName");
            }
            DbCommand command = this.CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
           
            //if (!this.SameNumberOfParametersAndValues(command, parameterValues))
            //{
            //    throw new Exception("MessageParameterMatchFailure");
            //}
            this.AssignParameterValues(command, parameterValues);
            return command;
        }

        public DbCommand GetStoredProcCommandWithSourceColumns(string storedProcedureName, params string[] sourceColumns)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentException("NullOrEmptyString", "storedProcedureName");
            }
            if (sourceColumns == null)
            {
                throw new ArgumentNullException("sourceColumns");
            }
            DbCommand storedProcCommand = this.GetStoredProcCommand(storedProcedureName);
            using (DbConnection connection = this.CreateConnection())
            {
                storedProcCommand.Connection = connection;
                this.DiscoverParameters(storedProcCommand);
            }
            int index = 0;
            foreach (IDataParameter parameter in storedProcCommand.Parameters)
            {
                if ((parameter.Direction == ParameterDirection.Input) | (parameter.Direction == ParameterDirection.InputOutput))
                {
                    parameter.SourceColumn = sourceColumns[index];
                    index++;
                }
            }
            return storedProcCommand;
        }

        public virtual void LoadDataSet(DbCommand command, DataSet dataSet, string tableName)
        {
            this.LoadDataSet(command, dataSet, new string[] { tableName });
        }

        public virtual void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames)
        {
            using (ConnectionWrapper wrapper = this.GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                this.DoLoadDataSet(command, dataSet, tableNames);
            }
        }

        public virtual void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                this.LoadDataSet(command, dataSet, tableNames);
            }
        }

        public virtual void LoadDataSet(DbCommand command, DataSet dataSet, string tableName, DbTransaction transaction)
        {
            this.LoadDataSet(command, dataSet, new string[] { tableName }, transaction);
        }

        public virtual void LoadDataSet(DbCommand command, DataSet dataSet, string[] tableNames, DbTransaction transaction)
        {
            PrepareCommand(command, transaction);
            this.DoLoadDataSet(command, dataSet, tableNames);
        }

        public virtual void LoadDataSet(string storedProcedureName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                this.LoadDataSet(command, dataSet, tableNames);
            }
        }

        public void LoadDataSet(DbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (DbCommand command = this.CreateCommandByCommandType(commandType, commandText))
            {
                this.LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        public virtual void LoadDataSet(DbTransaction transaction, string storedProcedureName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            using (DbCommand command = this.GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                this.LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        [Obsolete("Use GetOpenConnection instead.")]
        protected DbConnection OpenConnection()
        {
            return this.GetNewOpenConnection();
        }

        protected static void PrepareCommand(DbCommand command, DbConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }
            command.Connection = connection;
        }

        protected static void PrepareCommand(DbCommand command, DbTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }
            PrepareCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }

        private void RollbackTransaction(DbTransaction tran)
        {
            tran.Rollback();
        }

        protected virtual bool SameNumberOfParametersAndValues(DbCommand command, object[] values)
        {
            int count = command.Parameters.Count;
            int length = values.Length;
            return (count == length);
        }

        public virtual void SetParameterValue(DbCommand command, string parameterName, object value)
        {
            command.Parameters[this.BuildParameterName(parameterName)].Value = (value == null) ? DBNull.Value : value;
        }

        protected virtual void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
        }

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, UpdateBehavior updateBehavior)
        {
            return this.UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBehavior, null);
        }

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, DbTransaction transaction)
        {
            return this.UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, transaction, null);
        }

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, UpdateBehavior updateBehavior, int? updateBatchSize)
        {
            using (ConnectionWrapper wrapper = this.GetOpenConnection())
            {
                if ((updateBehavior == UpdateBehavior.Transactional) && (Transaction.Current == null))
                {
                    DbTransaction transaction = this.BeginTransaction(wrapper.Connection);
                    try
                    {
                        int num = this.UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, transaction, updateBatchSize);
                        this.CommitTransaction(transaction);
                        return num;
                    }
                    catch
                    {
                        this.RollbackTransaction(transaction);
                        throw;
                    }
                }
                if (insertCommand != null)
                {
                    PrepareCommand(insertCommand, wrapper.Connection);
                }
                if (updateCommand != null)
                {
                    PrepareCommand(updateCommand, wrapper.Connection);
                }
                if (deleteCommand != null)
                {
                    PrepareCommand(deleteCommand, wrapper.Connection);
                }
                return this.DoUpdateDataSet(updateBehavior, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
            }
        }

        public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, DbTransaction transaction, int? updateBatchSize)
        {
            if (insertCommand != null)
            {
                PrepareCommand(insertCommand, transaction);
            }
            if (updateCommand != null)
            {
                PrepareCommand(updateCommand, transaction);
            }
            if (deleteCommand != null)
            {
                PrepareCommand(deleteCommand, transaction);
            }
            return this.DoUpdateDataSet(UpdateBehavior.Transactional, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
        }

        

        protected virtual int UserParametersStartIndex()
        {
            return 0;
        }

        protected internal string ConnectionString
        {
            get
            {
                return this.connectionString.ToString();
            }
        }

        
       

        public System.Data.Common.DbProviderFactory DbProviderFactory
        {
            get
            {
                return this.dbProviderFactory;
            }
        }

        public class ConnectionWrapper : IDisposable
        {
            private DbConnection connection;
            private bool disposeConnection;

            public ConnectionWrapper(DbConnection connection, bool disposeConnection)
            {
                this.connection = connection;
                this.disposeConnection = disposeConnection;
            }

            public void Dispose()
            {
                if (this.disposeConnection)
                {
                    this.connection.Dispose();
                }
            }

            public DbConnection Connection
            {
                get
                {
                    return this.connection;
                }
            }
        }
    }

    
}
