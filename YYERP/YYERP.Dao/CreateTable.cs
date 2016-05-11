using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YYERP.Dao
{
    public class CreateTable
    {
        #region 将数据库类型转化为 .Net类型 (各个Provider已经提供了实现，所以此处先隐藏)

        /// <summary>
        /// SqlServer类型转化为.net数据类型
        /// </summary>
        /// <param name="nativeType"></param>
        /// <returns></returns>
        public static DbType GetSqlServerDbType(string nativeType)
        {
            switch (nativeType.Trim().ToLower())
            {
                case "bigint":
                    return DbType.Int64;

                case "binary":
                    return DbType.Binary;

                case "bit":
                    return DbType.Boolean;

                case "char":
                    return DbType.AnsiStringFixedLength;

                case "datetime":
                    return DbType.DateTime;

                case "decimal":
                    return DbType.Decimal;

                case "float":
                    return DbType.Double;

                case "image":
                    return DbType.Binary;

                case "int":
                    return DbType.Int32;

                case "money":
                    return DbType.Currency;

                case "nchar":
                    return DbType.StringFixedLength;

                case "ntext":
                    return DbType.String;

                case "numeric":
                    return DbType.Decimal;

                case "nvarchar":
                    return DbType.String;

                case "real":
                    return DbType.Single;

                case "smalldatetime":
                    return DbType.DateTime;

                case "smallint":
                    return DbType.Int16;

                case "smallmoney":
                    return DbType.Currency;

                case "sql_variant":
                    return DbType.Object;

                case "sysname":
                    return DbType.StringFixedLength;

                case "text":
                    return DbType.AnsiString;

                case "timestamp":
                    return DbType.Binary;

                case "tinyint":
                    return DbType.Byte;

                case "uniqueidentifier":
                    return DbType.Guid;

                case "varbinary":
                    return DbType.Binary;

                case "varchar":
                    return DbType.AnsiString;

                case "xml":
                    return DbType.Xml;

                case "datetime2":
                    return DbType.DateTime2;

                case "time":
                    return DbType.Time;

                case "date":
                    return DbType.Date;

                case "datetimeoffset":
                    return DbType.DateTimeOffset;
            }
            return DbType.Object;
        }

        /// <summary>
        /// DB2类型转化为.net数据类型
        /// </summary>
        /// <param name="nativeType"></param>
        /// <returns></returns>
        private static DbType GetDB2DbType(string nativeType)
        {
            DbType result = DbType.Object;
            // Convert native DB2 types to appropriate DbType enumerated values
            switch (nativeType.ToUpper())
            {
                case "DECIMAL":
                    result = DbType.Decimal;
                    break;

                case "SMALLINT":
                    result = DbType.Int16;
                    break;

                case "INTEGER":
                    result = DbType.Int32;
                    break;

                case "BIGINT":
                    result = DbType.Int64;
                    break;

                case "REAL":
                    result = DbType.Single;
                    break;

                case "DOUBLE":
                    result = DbType.Double;
                    break;
                case "DATE":
                case "TIMESTAMP":
                    result = DbType.DateTime;
                    break;

                case "TIME":
                    result = DbType.Time;
                    break;

                case "VARCHAR":
                    result = DbType.String;
                    break;

                case "CHAR":
                case "CHARACTER":
                    result = DbType.StringFixedLength;
                    break;
                case "CLOB":
                case "GRAPHIC":
                case "VARGRAPHIC":
                case "DBCLOB":
                case "BLOB":
                case "DATALINK":
                    result = DbType.Object;
                    break;

                default:
                    result = DbType.Object;
                    break;
            }
            return result;
        }

        /// <summary>
        /// Oracle类型转化为.net数据类型
        /// </summary>
        /// <param name="nativeType"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static DbType GetOracleDbType(string nativeType, int precision, int scale)
        {
            switch (nativeType.Trim().ToUpper())
            {
                case "BFILE":
                    return DbType.Object;

                case "BLOB":
                    return DbType.Object;

                case "CHAR":
                    return DbType.AnsiStringFixedLength;

                case "CLOB":
                    return DbType.Object;

                case "DATE":
                    return DbType.DateTime;

                case "FLOAT":
                    return DbType.Decimal;

                case "INTEGER":
                    return DbType.Decimal;

                case "UNSIGNED INTEGER":
                    return DbType.Decimal;

                case "INTERVAL YEAR TO MONTH":
                    return DbType.Int32;

                case "INTERVAL DAY TO SECOND":
                    return DbType.Object;

                case "LONG":
                    return DbType.AnsiString;

                case "LONG RAW":
                    return DbType.Binary;

                case "NCHAR":
                    return DbType.StringFixedLength;

                case "NCLOB":
                    return DbType.Object;

                case "NUMBER":
                    if ((precision >= 3) || (scale != 0))
                    {
                        if ((precision < 5) && (scale == 0))
                        {
                            return DbType.Int16;
                        }
                        if ((precision < 10) && (scale == 0))
                        {
                            return DbType.Int32;
                        }
                        if ((precision < 0x13) && (scale == 0))
                        {
                            return DbType.Int64;
                        }
                        if ((precision < 0x13) || (scale != 0))
                        {
                            if (precision < 8)
                            {
                                return DbType.Single;
                            }
                            if (precision < 0x10)
                            {
                                return DbType.Double;
                            }
                        }
                        return DbType.Decimal;
                    }
                    return DbType.Byte;

                case "NVARCHAR2":
                    return DbType.String;

                case "RAW":
                    return DbType.Binary;

                case "REF CURSOR":
                    return DbType.Object;

                case "ROWID":
                    return DbType.AnsiString;

                case "TIMESTAMP":
                    return DbType.DateTime;

                case "TIMESTAMP WITH LOCAL TIME ZONE":
                    return DbType.DateTime;

                case "TIMESTAMP WITH TIME ZONE":
                    return DbType.DateTime;

                case "VARCHAR2":
                    return DbType.AnsiString;

                case "BINARY_DOUBLE":
                    return DbType.Decimal;

                case "BINARY_FLOAT":
                    return DbType.Decimal;

                case "BINARY_INTEGER":
                    return DbType.Decimal;

                case "PLS_INTEGER":
                    return DbType.Decimal;

                case "Collection":
                    return DbType.String;

                case "UROWID":
                    return DbType.String;

                case "XMLType":
                    return DbType.String;
            }
            return DbType.Object;
        }
        #endregion

        /// <summary>
        ///  创建sql语句块
        /// </summary>
        /// <param name="dataType">数据类型</param>
        /// <param name="columnName">列名称</param>
        /// <param name="percision">长度</param>
        /// <param name="scale">精度</param>
        /// <param name="columnsLength">列最大长度</param>
        /// <param name="allowNull">是否能为空</param>
        /// <returns></returns>
        public static string CreateSqlStatementBlock(string dataType, string columnName, int percision, int scale, int columnsLength, string allowNull)
        {
            switch (dataType.ToLower())
            {
                case "nvarchar":
                    return columnName + " " + dataType + " " + "(" + columnsLength + ")" + " " + allowNull;
                case "bigint":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "binary":
                    return columnName + " " + dataType + " " + "(1)" + " " + allowNull;
                case "bit":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "datetime":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "decimal":
                    return columnName + " " + dataType + " " + "(" + percision + "," + scale + ")" + " " + allowNull;
                case "float":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "int":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "ntext":
                    return columnName + " " + dataType + " " + allowNull;
                case "real":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "uniqueidentifier":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "tinyint":
                    return columnName + " " + dataType + " " + "" + " " + allowNull;
                case "variant":
                    return columnName + " " + dataType + " " + "(" + columnsLength + ")" + " " + allowNull;
                default:
                    return columnName + " " + dataType + " " + allowNull;
            }
        }
 
        /// <summary>
        /// .Net DbType数据类型 转换为 对应的 SqlServer 数据类型
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns> 
        public static string DotNetDataTypeToSqlServerDataType(DbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case  DbType.Int64:
                    return "bigint";

                case  DbType.Binary:
                    return "binary";

                case DbType.Boolean:
                    return "bit";

                case DbType.AnsiStringFixedLength:
                    return "char";

                case DbType.DateTime:
                    return "datetime";

                case DbType.Decimal:
                    return "decimal";

                case DbType.Double:
                    return "float";

                //case DbType.Binary:
                //    return "image";

                case DbType.Int32:
                    return "int";

                case DbType.Currency:
                    return "money";

                case DbType.StringFixedLength:
                    return "nchar";

                case DbType.String:
                    return "ntext";

                //case DbType.String:
                //    return "nvarchar";

                case DbType.Single:
                    return "real";

                case DbType.Guid:
                    return "uniqueidentifier";

                //case DbType.DateTime:
                //    return "smalldatetime";

                case DbType.Int16:
                    return "smallint";

                //case DbType.Currency:
                //    return "smallmoney";

                //case DbType.AnsiString:
                //    return "text";

                //case DbType.Timestamp:
                //    return "timestamp";

                case DbType.Byte:
                    return "tinyint";

                //case DbType.VarBinary:
                //    return "varbinary";

                case DbType.AnsiString:
                    return "varchar";

                case DbType.Object:
                    return "sql_variant";

                case DbType.Xml:
                    return "xml";

                case DbType.Date:
                    return "date";

                case DbType.Time:
                    return "time";

                case DbType.DateTime2:
                    return "datetime2";

                case DbType.DateTimeOffset:
                    return "datetimeoffset";
            }
            return "varchar";
        } 

        /// <summary>
        /// 返回创建 DB2 Sql语句块
        /// </summary>
        /// <param name="dataType">字段数据类型</param>
        /// <param name="columnName">字段名</param>
        /// <returns>返回创建Sql语句块</returns>
        public static string CreateDB2SqlStatementBlock(string dataType, string columnName, int percision, int scale, int columnsLength, string allowNull)
        {
            if (columnName.StartsWith("VALUE"))
            {
                return columnName + " " + dataType + " " + "(" + columnsLength + ")";
            }
            switch (dataType)
            {
                case "varchar":
                    return columnName + " " + dataType + " " + "(" + columnsLength + ")";
                case "bigint":
                    return columnName + " " + dataType + " " + allowNull;
                case "binary":
                    return columnName + " " + dataType + " " + "(1)" + " " + allowNull; ;
                case "bit":
                    return columnName + " " + dataType + " " + allowNull;
                case "date":
                    return columnName + " " + dataType + " " + allowNull;
                case "decimal":
                    return columnName + " " + dataType + " " + "(" + percision + "," + scale + ")" + " " + allowNull;
                case "float":
                    return columnName + " " + dataType + " " + " " + allowNull;
                case "double":
                    return columnName + " " + dataType +" " + allowNull;
                case "integer":
                    return columnName + " " + dataType + " " + allowNull;
                case "ntext":
                    return columnName + " " + dataType + " " + allowNull;
                case "real":
                    return columnName + " " + dataType + " " + allowNull;
                default:
                    return columnName + " " + dataType;
            }
        }

      
        /// <summary>
        /// .Net DbType数据类型 转换为 对应的 DB2 数据类型
        /// </summary>
        /// <param name="DotNetDataType"></param>
        /// <returns></returns>
        public static string DotNetDataTypeToDB2DataType(DbType DotNetDataType)
        {
            switch (DotNetDataType)
            {
                case DbType.Int64: 
                    return "bigint"; 
                case DbType.String:
                    return "varchar";
                case DbType.DateTime:
                    return "date";
                case DbType.Decimal: 
                    return "decimal";
                case DbType.Double:
                    return "double";
                case DbType.Time:
                    return "time"; 
                case DbType.Int32: 
                    return "integer";
                case DbType.Single: 
                    return "real"; 
                case DbType.Int16:
                    return "smallint";
                case DbType.StringFixedLength:
                    return "char";  
                default:
                    return "varchar";
            }
        }

        /// <summary>
        /// 返回创建 Oracle sql语句块
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="columnName"></param>
        /// <param name="columnsLength"></param>
        /// <returns></returns>
        public static string CreateOracleSqlStatementBlock(string dataType, string columnName, int percision, int scale, int columnsLength, string allowNull)
        {
            switch (dataType)
            {
                case "varchar":
                    return columnName + " " + dataType + " " + "(" + columnsLength + ")";
                case "bigint":
                    return columnName + " " + dataType + " " + allowNull;
                case "binary":
                    return columnName + " " + dataType + " " + "(1)" + " " + allowNull;
                case "bit":
                    return columnName + " " + dataType + " " + allowNull;
                case "datetime":
                    return columnName + " " + dataType + " " + allowNull;
                case "Decimal":
                    return columnName + " " + dataType + " " + "(" + percision + "," + scale + ")" + " " + allowNull;
                case "decimal":
                    return columnName + " " + dataType + " " + "(" + percision + "," + scale + ")" + " " + allowNull;
                case "float":
                    return columnName + " " + dataType + " " + allowNull;
                case "double":
                    return columnName + " " + dataType + " " + allowNull;
                case "integer":
                    return columnName + " " + dataType + " " + allowNull;
                case "ntext":
                    return columnName + " " + dataType + " " + allowNull;
                case "real":
                    return columnName + " " + dataType + " " + allowNull;
                case "uniqueidentifier":
                    return columnName + " " + dataType + " " + allowNull;
                //case "tinyint":
                //    return columnName + " " + dataType + " " + allowNull;
                //case "variant":
                //    return columnName + " " + dataType + " " + "(" + columnsLength + ")";
                default:
                    return columnName + " " + dataType + " " + allowNull; 
            }
        }

        /// <summary>
        /// .Net DbType数据类型 转换为 对应的Oracle 数据类型
        /// </summary>
        /// <param name="DotNetDataType"></param>
        /// <returns></returns>
        public static string DotNetDataTypeToOracleDataType(DbType DotNetDataType)
        {
            switch (DotNetDataType)
            {
                case DbType.Object:
                    return "CLOB";
                case DbType.AnsiStringFixedLength:
                    return "CHAR";
                case DbType.DateTime:
                    return "DATE";
                case DbType.Decimal:
                    return "FLOAT";
                case DbType.Int16:
                    return "INTEGER";
                case DbType.Int32:
                    return "INTERVAL YEAR TO MONTH";
                case DbType.Int64:
                    return "INTEGER";
                case DbType.AnsiString:
                    return "LONG";
                case DbType.Binary:
                    return "LONG RAW";
                case DbType.StringFixedLength:
                    return "NCHAR";
                case DbType.Single:
                    return "FLOAT";
                case DbType.String:
                    return "NVARCHAR2"; 
                case DbType.VarNumeric:
                    return "NUMBER"; 
            }
            return "VarChar";
        }

       
        /// <summary>
        /// 判断在SqlServer数据库中是否存在表
        /// </summary> 
        /// <param name="tbname">表名</param>
        /// <param name="dbname">所在库的DCDS连接名称</param>
        /// <returns></returns>
        private static bool IsSqlServerTableExist(string tbname, string dbname)
        {
            try
            {
                StringBuilder tableNameisExistSql = new StringBuilder();
                tableNameisExistSql.Append("SELECT name FROM sys.objects ");
                tableNameisExistSql.Append("WHERE object_id = OBJECT_ID(N'[dbo].[" + tbname.ToUpper() + " ]')");
                tableNameisExistSql.Append("AND type in (N'U')");
                DataTable dt = UserDataAccess.ExecuteDataSet(tableNameisExistSql.ToString());
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 判断在DB2数据库中是否存在表
        /// </summary>
        /// <param name="tbname">表名</param>
        /// <param name="dbname">所在库的DCDS连接名称</param>
        /// <returns></returns>
        private static bool IsDb2TableExist(string tbName,string dbName)
        {
            StringBuilder tableNameisExistSql = new StringBuilder();
            //SELECT * FROM SYSIBM.SYSTABLES WHERE TID <> 0 AND Name = 'T_IQV_COMMON_DATA' 
            tableNameisExistSql.Append(" SELECT * FROM SYSIBM.TABLES ");
            tableNameisExistSql.Append(" WHERE TABLE_NAME = '" + tbName.ToUpper() + "'");
            DataTable dt = UserDataAccess.ExecuteDataSet(tableNameisExistSql.ToString());
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 判断在Oracle数据库中是否存在表
        /// </summary>
        /// <param name="tbname">表名</param>
        /// <param name="dbname">所在库的DCDS连接名称</param>
        /// <returns></returns>
        private static bool IsOracleTableExist(string tbName, string dbName)
        {
            StringBuilder tableNameisExistSql = new StringBuilder();
            tableNameisExistSql.Append("SELECT COUNT（*） FROM USER_OBJECTS");
            tableNameisExistSql.Append("WHERE OBJECT_NAME = UPPER （'" + tbName + "'");
            DataTable dt = UserDataAccess.ExecuteDataSet(tableNameisExistSql.ToString());
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        ///// <summary>
        ///// 向SqlServer数据库中 创建表
        ///// </summary>
        ///// <param name="database">目标数据库</param>
        ///// <param name="table">要创建的表</param>
        //public static void CreateTableToSqlServer(string dbName, TableSchema table)
        //{
        //    try
        //    {
        //        if (!IsSqlServerTableExist(table.Name, dbName))
        //        {
        //            //表示创建一个表结构
        //            StringBuilder createTableSqlScript = new StringBuilder();
        //            //int columnsLength = 0;
        //            createTableSqlScript.Append("CREATE TABLE " + table.Name + "( ");
        //            ColumnSchemaCollection columns = table.Columns;
        //            int i = 0;
        //            foreach (ColumnSchema cs in columns)
        //            {
        //                string sysType = DotNetDataTypeToSqlServerDataType(cs.DataType);
        //                string sqlBlock =CreateSqlStatementBlock(sysType, cs.Name, cs.Precision, cs.Scale, cs.Size, cs.AllowDBNull ? "NULL" : "NOT NULL");
        //                createTableSqlScript.Append(sqlBlock.ToUpper());
        //                if (i < columns.Count - 1)
        //                {
        //                    createTableSqlScript.Append(",");
        //                }
        //                else
        //                {
        //                    createTableSqlScript.Append(" )");
        //                }
        //                i++;
        //            }
        //            UserDataAccess.ExecuteScalar(createTableSqlScript.ToString(), dbName);
        //         }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        ///// <summary>
        ///// 向DB2数据库中 创建表
        ///// </summary>
        ///// <param name="database">目标数据库</param>
        ///// <param name="table">要创建的表</param>
        //public static void CreateTableToDB2(string dbName, TableSchema table)
        //{
        //    if (!IsDb2TableExist(table.Name, dbName))
        //    {
        //        //表示创建一个表结构
        //        StringBuilder createTableSqlScript = new StringBuilder();
        //        //int columnsLength = 0;
        //        createTableSqlScript.Append("CREATE TABLE " + table.Name + "( ");
        //        ColumnSchemaCollection columns = table.Columns;
        //        int i = 0;
        //        foreach (ColumnSchema cs in columns)
        //        {
        //            string sysType =DotNetDataTypeToDB2DataType(cs.DataType);
        //            string sqlBlock = CreateDB2SqlStatementBlock(sysType, cs.Name, cs.Precision, cs.Scale, cs.Size, cs.AllowDBNull ? "NULL" : "NOT NULL");
        //            createTableSqlScript.Append(sqlBlock.ToUpper());
        //            if (i < columns.Count - 1)
        //            {
        //                createTableSqlScript.Append(",");
        //            }
        //            else
        //            {
        //                createTableSqlScript.Append(" )");
        //            }
        //            i++;
        //        }
        //        UserDataAccess.ExecuteScalar(createTableSqlScript.ToString(), dbName);

        //    }
        //}


        ///// <summary>
        ///// 向Oracle数据库中 创建表 (还未测试)
        ///// </summary>
        ///// <param name="database">目标数据库</param>
        ///// <param name="table">要创建的表</param>
        //public static void CreateTableToOracle(string dbName, TableSchema table)
        //{
        //    if (!IsOracleTableExist(table.Name, dbName))
        //    {
        //        //表示创建一个表结构
        //        StringBuilder createTableSqlScript = new StringBuilder();
        //        //int columnsLength = 0;
        //        createTableSqlScript.Append("CREATE TABLE " + table.Name + "( ");
        //        ColumnSchemaCollection columns = table.Columns;
        //        int i = 0;
        //        foreach (ColumnSchema cs in columns)
        //        {
        //            string sysType =DotNetDataTypeToOracleDataType(cs.DataType);
        //            string sqlBlock =CreateOracleSqlStatementBlock(sysType, cs.Name,cs.Precision,cs.Scale, cs.Size,cs.AllowDBNull?"NULL":"NOT NULL");
        //            createTableSqlScript.Append(sqlBlock.ToUpper());
        //            if (i < columns.Count - 1)
        //            {
        //                createTableSqlScript.Append(",");
        //            }
        //            else
        //            {
        //                createTableSqlScript.Append(" )");
        //            }
        //            i++;
        //        }
        //        UserDataAccess.ExecuteScalar(createTableSqlScript.ToString(), dbName);
        //    }
        //}
    }
}
