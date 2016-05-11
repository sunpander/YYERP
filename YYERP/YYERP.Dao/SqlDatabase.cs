using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace YYERP.Dao
{
    public class SqlDatabase:Database
    {
        public SqlDatabase(string connectionString)
            : base(connectionString, SqlClientFactory.Instance)
        {
        }

        public void AddInParameter(DbCommand command, string name, SqlDbType dbType)
        {
            this.AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, null);
        }

        public void AddInParameter(DbCommand command, string name, SqlDbType dbType, object value)
        {
            this.AddParameter(command, name, dbType, ParameterDirection.Input, string.Empty, DataRowVersion.Default, value);
        }

        public void AddInParameter(DbCommand command, string name, SqlDbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            this.AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
        }

        public void AddOutParameter(DbCommand command, string name, SqlDbType dbType, int size)
        {
            this.AddParameter(command, name, dbType, size, ParameterDirection.Output, true, 0, 0, string.Empty, DataRowVersion.Default, DBNull.Value);
        }

        public void AddParameter(DbCommand command, string name, SqlDbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            this.AddParameter(command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }

        public virtual void AddParameter(DbCommand command, string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            DbParameter parameter = this.CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }

        public override string BuildParameterName(string name)
        {
            if (name[0] != this.ParameterToken)
            {
                return name.Insert(0, new string(this.ParameterToken, 1));
            }
            return name;
        }

        private static SqlCommand CheckIfSqlCommand(DbCommand command)
        {
            SqlCommand command2 = command as SqlCommand;
            if (command2 == null)
            {
                throw new ArgumentException("CommandNotSqlCommand", "command");
            }
            return command2;
        }

        protected virtual void ConfigureParameter(SqlParameter param, string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.SqlDbType = dbType;
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        protected DbParameter CreateParameter(string name, SqlDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            SqlParameter param = base.CreateParameter(name) as SqlParameter;
            this.ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        protected override void DeriveParameters(DbCommand discoveryCommand)
        {
            SqlCommandBuilder.DeriveParameters((SqlCommand)discoveryCommand);
        }

        private XmlReader DoExecuteXmlReader(SqlCommand sqlCommand)
        {
            XmlReader reader2;
            try
            {
                DateTime now = DateTime.Now;
                XmlReader reader = sqlCommand.ExecuteXmlReader();
                reader2 = reader;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return reader2;
        }

        public XmlReader ExecuteXmlReader(DbCommand command)
        {
            SqlCommand sqlCommand = CheckIfSqlCommand(command);
            Database.ConnectionWrapper openConnection = base.GetOpenConnection(false);
            Database.PrepareCommand(command, openConnection.Connection);
            return this.DoExecuteXmlReader(sqlCommand);
        }

        public XmlReader ExecuteXmlReader(DbCommand command, DbTransaction transaction)
        {
            SqlCommand command2 = CheckIfSqlCommand(command);
            Database.PrepareCommand(command2, transaction);
            return this.DoExecuteXmlReader(command2);
        }

        private void OnSqlRowUpdated(object sender, SqlRowUpdatedEventArgs rowThatCouldNotBeWritten)
        {
            if ((rowThatCouldNotBeWritten.RecordsAffected == 0) && (rowThatCouldNotBeWritten.Errors != null))
            {
                rowThatCouldNotBeWritten.Row.RowError = "UpdateDataSetRowFailure";
                rowThatCouldNotBeWritten.Status = UpdateStatus.SkipCurrentRow;
            }
        }

        protected override bool SameNumberOfParametersAndValues(DbCommand command, object[] values)
        {
            int num = 1;
            int num2 = command.Parameters.Count - num;
            int length = values.Length;
            return (num2 == length);
        }

        protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
            ((SqlDataAdapter)adapter).RowUpdated += new SqlRowUpdatedEventHandler(this.OnSqlRowUpdated);
        }

        protected override int UserParametersStartIndex()
        {
            return 1;
        }

        protected char ParameterToken
        {
            get
            {
                return '@';
            }
        }
    }
}

