namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using SchemaExplorer;

    internal static class Utility
    {
       

        internal static DbType GetDbType(string nativeType)
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

        internal static string GetNativeType(SqlDbType sqlDbType)
        {
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    return "bigint";

                case SqlDbType.Binary:
                    return "binary";

                case SqlDbType.Bit:
                    return "bit";

                case SqlDbType.Char:
                    return "char";

                case SqlDbType.DateTime:
                    return "datetime";

                case SqlDbType.Decimal:
                    return "decimal";

                case SqlDbType.Float:
                    return "float";

                case SqlDbType.Image:
                    return "image";

                case SqlDbType.Int:
                    return "int";

                case SqlDbType.Money:
                    return "money";

                case SqlDbType.NChar:
                    return "nchar";

                case SqlDbType.NText:
                    return "ntext";

                case SqlDbType.NVarChar:
                    return "nvarchar";

                case SqlDbType.Real:
                    return "real";

                case SqlDbType.UniqueIdentifier:
                    return "uniqueidentifier";

                case SqlDbType.SmallDateTime:
                    return "smalldatetime";

                case SqlDbType.SmallInt:
                    return "smallint";

                case SqlDbType.SmallMoney:
                    return "smallmoney";

                case SqlDbType.Text:
                    return "text";

                case SqlDbType.Timestamp:
                    return "timestamp";

                case SqlDbType.TinyInt:
                    return "tinyint";

                case SqlDbType.VarBinary:
                    return "varbinary";

                case SqlDbType.VarChar:
                    return "varchar";

                case SqlDbType.Variant:
                    return "sql_variant";

                case SqlDbType.Xml:
                    return "xml";

                case SqlDbType.Date:
                    return "date";

                case SqlDbType.Time:
                    return "time";

                case SqlDbType.DateTime2:
                    return "datetime2";

                case SqlDbType.DateTimeOffset:
                    return "datetimeoffset";
            }
            return "sql_variant";
        }

        internal static ParameterDirection GetParameterDirection(short parameterType)
        {
            switch (parameterType)
            {
                case 1:
                    return ParameterDirection.Input;

                case 2:
                    return ParameterDirection.InputOutput;

                case 3:
                    return ParameterDirection.Output;

                case 4:
                    return ParameterDirection.ReturnValue;
            }
            return ParameterDirection.Input;
        }

        internal static SqlDbType GetSqlDbType(string nativeType)
        {
            switch (nativeType.Trim().ToLower())
            {
                case "bigint":
                    return SqlDbType.BigInt;

                case "binary":
                    return SqlDbType.Binary;

                case "bit":
                    return SqlDbType.Bit;

                case "char":
                    return SqlDbType.Char;

                case "datetime":
                    return SqlDbType.DateTime;

                case "decimal":
                    return SqlDbType.Decimal;

                case "float":
                    return SqlDbType.Float;

                case "image":
                    return SqlDbType.Image;

                case "int":
                    return SqlDbType.Int;

                case "money":
                    return SqlDbType.Money;

                case "nchar":
                    return SqlDbType.NChar;

                case "ntext":
                    return SqlDbType.NText;

                case "numeric":
                    return SqlDbType.Decimal;

                case "nvarchar":
                    return SqlDbType.NVarChar;

                case "real":
                    return SqlDbType.Real;

                case "smalldatetime":
                    return SqlDbType.SmallDateTime;

                case "smallint":
                    return SqlDbType.SmallInt;

                case "smallmoney":
                    return SqlDbType.SmallMoney;

                case "sql_variant":
                    return SqlDbType.Variant;

                case "sysname":
                    return SqlDbType.NChar;

                case "text":
                    return SqlDbType.Text;

                case "timestamp":
                    return SqlDbType.Timestamp;

                case "tinyint":
                    return SqlDbType.TinyInt;

                case "uniqueidentifier":
                    return SqlDbType.UniqueIdentifier;

                case "varbinary":
                    return SqlDbType.VarBinary;

                case "varchar":
                    return SqlDbType.VarChar;

                case "xml":
                    return SqlDbType.Xml;

                case "datetime2":
                    return SqlDbType.DateTime2;

                case "time":
                    return SqlDbType.Time;

                case "date":
                    return SqlDbType.Date;

                case "datetimeoffset":
                    return SqlDbType.DateTimeOffset;
            }
            return SqlDbType.Variant;
        }

        internal static bool IsAnyNullOrEmpty(params string[] values)
        {
            if ((values != null) && (values.Length != 0))
            {
                for (int i = 0; i < values.Length; i++)
                {
                    if (string.IsNullOrEmpty(values[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static bool ParseBooleanExtendedProperty(SchemaObjectBase schemaObjectBase, string extendedProperty)
        {
            bool flag;
            if ((schemaObjectBase == null) || !schemaObjectBase.ExtendedProperties.Contains(extendedProperty))
            {
                return false;
            }
            bool.TryParse(schemaObjectBase.ExtendedProperties[extendedProperty].Value.ToString(), out flag);
            return flag;
        }


        internal static Dictionary<string, TableSchema> ToDictionary(IList<TableSchema> tables)
        {
            Dictionary<string, TableSchema> dictionary = new Dictionary<string, TableSchema>();
            foreach (TableSchema schema in tables)
            {
                dictionary.Add(schema.FullName, schema);
            }
            return dictionary;
        }

        internal static Dictionary<string, ViewSchema> ToDictionary(IList<ViewSchema> views)
        {
            Dictionary<string, ViewSchema> dictionary = new Dictionary<string, ViewSchema>();
            foreach (ViewSchema schema in views)
            {
                dictionary.Add(schema.FullName, schema);
            }
            return dictionary;
        }


        internal static Dictionary<string, TableSchema> ToDictionary(TableSchema table)
        {
            Dictionary<string, TableSchema> dictionary = new Dictionary<string, TableSchema>();
            dictionary.Add(table.FullName, table);
            return dictionary;
        }

        internal static Dictionary<string, ViewSchema> ToDictionary(ViewSchema view)
        {
            Dictionary<string, ViewSchema> dictionary = new Dictionary<string, ViewSchema>();
            dictionary.Add(view.FullName, view);
            return dictionary;
        }
    }
}

