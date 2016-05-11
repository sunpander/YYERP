namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;

    internal static class SqlFactory
    {
        public static string GetAllCommandParameters(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetAllCommandParameters2005;
            }
            return SqlScripts.GetAllCommandParameters;
        }

        public static string GetAllTableColumns(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetAllTableColumns2005;
            }
            return SqlScripts.GetAllTableColumns;
        }

        public static string GetAllViewColumns(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetAllViewColumns2005;
            }
            return SqlScripts.GetAllViewColumns;
        }

        public static string GetColumnConstraints(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetColumnConstraints2005;
            }
            return SqlScripts.GetColumnConstraints;
        }

        public static string GetColumnConstraintsWhere(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return " AND SCHEMA_NAME([tbl].[uid]) = @SchemaName AND [tbl].[name] = @TableName AND [clmns].[name] = @ColumnName";
            }
            return " AND [stbl].[name] = @SchemaName AND [tbl].[name] = @TableName AND [clmns].[name] = @ColumnName";
        }

        public static string GetCommandParameters(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetCommandParameters2005;
            }
            return SqlScripts.GetCommandParameters;
        }

        public static string GetCommands(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetCommands2005;
            }
            return SqlScripts.GetCommands;
        }

        public static string GetExtendedData(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetExtendedData2005;
            }
            return SqlScripts.GetExtenedData;
        }

        public static string GetExtendedProperties(int majorVersion)
        {
            return SqlScripts.GetExtendedProperties;
        }

        public static string GetIndexes(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetIndexes2005;
            }
            return SqlScripts.GetIndexes;
        }

        public static string GetKeys(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetKeys2005;
            }
            return SqlScripts.GetKeys;
        }

        public static string GetTableColumns(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetTableColumns2005;
            }
            return SqlScripts.GetTableColumns;
        }

        public static string GetTables(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetTables2005;
            }
            //if (majorVersion == 8)
            //{
            //    return SqlScripts.GetTables2000;
            //}
            return SqlScripts.GetTables;
        }

        public static string GetViewColumns(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetViewColumns2005;
            }
            return SqlScripts.GetViewColumns;
        }

        public static string GetViews(int majorVersion)
        {
            if (majorVersion >= 9)
            {
                return SqlScripts.GetViews2005;
            }
            return SqlScripts.GetViews;
        }
    }
}

