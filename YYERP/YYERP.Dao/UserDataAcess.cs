using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using YYERP.Dao.SqlServerSchemaProvider;
using System.Data.Common;
using SchemaExplorer;
 

namespace YYERP.Dao
{
    public class UserDataAccess
    {
        static string _ConnectString;
        public static string ConnectString
        {
            get
            {
                if (_ConnectString == null)
                {
                    string connString = "Driver={MySQL ODBC 5.1 Driver};server=127.0.0.1;user id=root;database=yyerp";
                    //if (connString == null)
                    //    throw new Exception("请在Web.config中AppSettings添加数据库连接字符串配置项ConnectString");

                    _ConnectString = connString;
                    // _ConnectString = "Data Source=.;Initial Catalog=TDataQISTest;Persist Security Info=True;User ID=sa;Password=sqladmin";
                    //_ConnectString = "Data Source=10.25.69.42;Initial Catalog=TDataQISTest;Persist Security Info=True;User ID=sa;Password=sa";
                    //_ConnectString = "Data Source=.;Initial Catalog=TDataQISTest;Persist Security Info=True;User ID=sa;Password=sqladmin";//DataSet ds = new DataSet();
                    //ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory +"\Configuration\Config.xml");

                }
                return _ConnectString;
            }
        }
        public static DataTable ExecuteDataSet(string sql)
        {
            return SysDataBase.ExecuteDataSet(CommandType.Text, sql).Tables[0];
        }

        public static int ExecuteNonQuery(string sql)
        {
            return SysDataBase.ExecuteNonQuery(CommandType.Text, sql);
        }
        static SqlDatabase _Database = null;
        public static SqlDatabase SysDataBase
        {
            get
            {
                if (_Database == null)
                {
                    _Database = new SqlDatabase(ConnectString);
                }
                return _Database;
            }
        }

        #region tableschem
        static SqlSchemaProvider _DataBaseSchemaProvider;
        private static SqlSchemaProvider DataBaseSchemaProvider
        {
            get
            {
                if (_DataBaseSchemaProvider == null)
                {
                    _DataBaseSchemaProvider = new SqlSchemaProvider(ConnectString, null);
                }
                return _DataBaseSchemaProvider;
            }
        }

        static DatabaseSchema _DatabaseSchema;
        private static DatabaseSchema DatabaseSchema
        {
            get
            {
                if (_DatabaseSchema == null)
                    _DatabaseSchema = new DatabaseSchema(DataBaseSchemaProvider, null);
                return _DatabaseSchema;
            }
        }

        #endregion


        public static void DoDataSet(DataTable _insertTable, DataTable _delTable, DataTable updateTable)
        {
            if (_insertTable != null)
            {
                foreach (DataRow row in _insertTable.Rows)
                    InsertRow(row);
            }
            if (updateTable != null)
            {
                foreach (DataRow row in updateTable.Rows)
                    UpdateRow(row);
            }
            if (_delTable != null)
            {
                foreach (DataRow row in _delTable.Rows)
                    DeleteRow(row);
            }
        }

        public static void DoDataSet(DataSet ds)
        {
            if (ds == null || ds.Tables.Count == 0)
                return;
            foreach (DataTable dt in ds.Tables)
            {

                DataTable insertTable = dt.GetChanges(DataRowState.Added);
                if (insertTable != null)
                {
                    foreach (DataRow row in insertTable.Rows)
                        InsertRow(row);
                }



                DataTable updateTable = dt.GetChanges(DataRowState.Modified);
                if (updateTable != null)
                {
                    foreach (DataRow row in updateTable.Rows)
                        UpdateRow(row);
                }
                DataTable deleteTable = dt.GetChanges(DataRowState.Deleted);

                if (deleteTable != null)
                {
                    deleteTable.RejectChanges();
                    foreach (DataRow row in deleteTable.Rows)
                        DeleteRow(row);
                }

                dt.AcceptChanges();
            }
        }


        private static void InsertRow(DataRow dr)
        {
            string fields = string.Empty;
            string values = string.Empty;
            int fieldNo = 0;
            DataTable dt = dr.Table;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                fields += (dt.Columns[i].ColumnName + ",");
                fieldNo++;
            }

            fields = fields.Remove(fields.Length - 1);

            for (int i = 0; i < fieldNo; i++)
            {
                values += string.Format("@{0},", dt.Columns[i].ColumnName);
            }
            values = values.Remove(values.Length - 1);

            string sql = string.Format(@"INSERT INTO {0} ({1}) values ({2})", dt.TableName, fields, values);

            DbCommand insertCommand = SysDataBase.CreateCommandByCommandType(CommandType.Text, sql);

            ColumnSchema[] css = DataBaseSchemaProvider.GetTableColumns(DatabaseSchema.Tables[dt.TableName]);
            foreach (DataColumn dc in dt.Columns)
            {
                foreach (ColumnSchema cs in css)
                {
                    if (cs.Name.ToLower() == dc.ColumnName.ToLower())
                    {
                        SysDataBase.AddInParameter(insertCommand, dc.ColumnName, cs.DataType, dr[dc.ColumnName]);
                        break;
                    }
                }
            }
            SysDataBase.ExecuteDataSet(insertCommand);

        }

        private static void DeleteRow(DataRow dr)
        {
            string condition = string.Empty;

            PrimaryKeySchema pks = DataBaseSchemaProvider.GetTablePrimaryKey(DatabaseSchema.Tables[dr.Table.TableName]);
            if (pks == null || pks.MemberColumns.Count == 0)
            {
                throw new Exception(dr.Table.TableName + "没有设置主键！");
            }

            foreach (MemberColumnSchema mcs in pks.MemberColumns)
            {
                condition += string.Format(@" {0}='{1}' AND", mcs.Name, dr[mcs.Name].ToString());
            }

            condition = condition.Remove(condition.Length - 3);

            string sql = string.Format(@"DELETE FROM {0} WHERE {1}", dr.Table.TableName, condition);
            DbCommand deleteCommand = SysDataBase.CreateCommandByCommandType(CommandType.Text, sql);
            SysDataBase.ExecuteDataSet(deleteCommand);
        }

        private static void UpdateRow(DataRow dr)
        {
            string setFields = string.Empty;
            string condition = string.Empty;
            DataTable dt = dr.Table;

            PrimaryKeySchema pks = DataBaseSchemaProvider.GetTablePrimaryKey(DatabaseSchema.Tables[dt.TableName]);
            if (pks == null || pks.MemberColumns.Count == 0)
            {
                throw new Exception(dt.TableName + "没有设置主键！");
            }
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                bool isPKColumn = false;
                foreach (MemberColumnSchema mcs in pks.MemberColumns)
                {
                    //Equals(dt.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase))
                    if (mcs.Name.Equals(dt.Columns[i].ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        isPKColumn = true;
                        break;
                    }
                }
                if (!isPKColumn)
                    setFields += (dt.Columns[i].ColumnName + "=@" + dt.Columns[i].ColumnName + ",");
            }
            setFields = setFields.Remove(setFields.Length - 1);


            foreach (MemberColumnSchema mcs in pks.MemberColumns)
            {
                condition += string.Format(@" {0}='{1}' AND", mcs.Name, dr[mcs.Name].ToString());
            }
            condition = condition.Remove(condition.Length - 3);


            string sql = string.Format(@"UPDATE {0} SET {1} WHERE {2}", dr.Table.TableName, setFields, condition);

            DbCommand updateCommand = SysDataBase.CreateCommandByCommandType(CommandType.Text, sql);
            ColumnSchema[] css = DataBaseSchemaProvider.GetTableColumns(DatabaseSchema.Tables[dt.TableName]);
            foreach (DataColumn dc in dt.Columns)
            {
                foreach (ColumnSchema cs in css)
                {
                    if (cs.Name.Equals(dc.ColumnName, StringComparison.OrdinalIgnoreCase))
                    {
                        SysDataBase.AddInParameter(updateCommand, dc.ColumnName, cs.DataType, dr[dc.ColumnName]);
                        break;
                    }

                }
            }

            SysDataBase.ExecuteDataSet(updateCommand);
        }




    }
}
