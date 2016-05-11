namespace YYERP.Dao.SqlServerSchemaProvider
{

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Text;
    using System.Text.RegularExpressions;
    using SchemaExplorer;
    using YYERP.Dao;
    public class SqlSchemaProvider : IDbSchemaProvider
    {
        private string _databaseName = string.Empty;
        private int _sqlServerMajorVersion;
        private bool _sqlServerMajorVersionChecked;
        private string _sqlServerVersion;
        public static string parseParameterRegex = "\r\n      CREATE\\s+PROC(?:EDURE)?                               # find the start of the stored procedure\r\n      .*?                                                   # skip all content until we get to the name of the parameter that we are looking for\r\n      {0}                                                   # name of the parameter we are interested in\r\n      \\s+(?:AS\\s+)?[\\w\\.\\[\\]]+(?:[\\(\\)\\s\\w,]+)?             # parameter data type\r\n      (?:\\s*\\=\\s*(?<default>(?:N?'[^']*' | [\\w-\\.]+)))?     # parameter default value\r\n      (?:\\s*(?:OUTPUT)?\\s*,?\\s*--\\s*(?<comment>[^\\r\\n]*))?  # parameter comment\r\n      .*?^\\s*AS\\s*(?:--[^\\r\\n]*)?\\s*$                       # end of stored procedure definition";
        private const string SQL_GetDatabaseName = "SELECT db_name()";
        private const string SQL_GetObjectData = "SELECT * FROM [{0}].[{1}]";
        private const string SQL_GetObjectSource = "EXEC sp_helptext @objectname";
        private const string SQL_GetSqlServerVersion = "EXEC master.dbo.xp_msver ProductVersion";

        public SqlSchemaProvider()
        {
        }

        public SqlSchemaProvider(string dbName, string userName)
        {
            this._userName = userName;
            this._DbName = dbName;
        }


        private bool ContainsKey(TableSchema tablesSchema, TableKeySchema keySchema)
        {
            foreach (TableKeySchema schema in tablesSchema.Keys)
            {
                if (((schema.Name == keySchema.Name) && (schema.PrimaryKeyTable.FullName == keySchema.PrimaryKeyTable.FullName)) && (schema.ForeignKeyTable.FullName == keySchema.ForeignKeyTable.FullName))
                {
                    return true;
                }
            }
            return false;
        }

        private ExtendedPropertyCollection GetColumnExtendedProperties(ColumnSchema column)
        {
            ExtendedPropertyCollection propertys = new ExtendedPropertyCollection();
            propertys.AddRange(this.GetExtendedProperties(this.DbName, column.Table.Owner, "Table", column.Table.Name, "Column", column.Name));
            if (this.IsSQL2000OrNewer())
            {
                int sqlServerMajorVersion = this.GetSqlServerMajorVersion();
                string sql = SqlFactory.GetColumnConstraints(sqlServerMajorVersion) + SqlFactory.GetColumnConstraintsWhere(sqlServerMajorVersion);
                sql.Replace("@SchemaName", "'" + column.Table.Owner + "'");
                sql.Replace("@TableName", "'" + column.Table.Name + "'");
                sql.Replace("@ColumnName", "'" + column.Name + "'");
                DataTable dt = UserDataAccess.ExecuteDataSet(sql);

                foreach (DataRow row in dt.Rows)
                {

                    string str2 = row["ConstraintName"].ToString();
                    string str3 = row["ConstraintType"].ToString();
                    string str4 = row["ConstraintDef"].ToString();
                    propertys.Add(new ExtendedProperty(string.Format("CS_Constraint_{0}_Name", str2), str2, DbType.String));
                    propertys.Add(new ExtendedProperty(string.Format("CS_Constraint_{0}_Type", str2), str3, DbType.String));
                    propertys.Add(new ExtendedProperty(string.Format("CS_Constraint_{0}_Definition", str2), str4, DbType.String));
                }
            }
            return propertys;
        }

        private List<ColumnSchema> GetColumnsFromReader(TableSchema table, DataTable dt)
        {
            return this.GetColumnsFromReader(Utility.ToDictionary(table), dt, false);
        }

        private List<ColumnSchema> GetColumnsFromReader(IDictionary<string, TableSchema> tables, DataTable dt, bool populateTable)
        {
            List<ColumnSchema> list = new List<ColumnSchema>();
            List<ExtendedProperty> list2 = new List<ExtendedProperty>();
            foreach (DataRow row in dt.Rows)
            {
                TableSchema schema;
                string name = row["Name"].ToString();
                string str2 = row["DataType"].ToString();
                string nativeType = row["SystemType"].ToString();
                DbType dbType = Utility.GetDbType(nativeType);
                int size = int.Parse(row["Length"].ToString());
                byte @byte = byte.Parse(row["NumericPrecision"].ToString());
                int scale = int.Parse(row["NumericScale"].ToString());
                bool boolean = row["IsNullable"].ToString() == "1" ? true : false;
                string str4 = row["DefaultValue"].ToString();
                bool flag2 = int.Parse(row["Identity"].ToString()) == 1;
                bool flag3 = int.Parse(row["IsRowGuid"].ToString()) == 1;
                bool flag4 = int.Parse(row["IsComputed"].ToString()) == 1;
                bool flag5 = (row["IsDeterministic"] is DBNull) || int.Parse(row["IsDeterministic"].ToString()) == 1;
                string str5 = row["IdentitySeed"].ToString();
                string str6 = row["IdentityIncrement"].ToString();
                string str7 = row["ComputedDefinition"].ToString();
                string str8 = row["Collation"].ToString();
                int num4 = int.Parse(row["ObjectId"].ToString());
                string owner = row["SchemaName"].ToString();
                string str10 = row["TableName"].ToString();
                list2.Clear();
                list2.Add(new ExtendedProperty("CS_IsRowGuidCol", flag3, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IsIdentity", flag2, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IsComputed", flag4, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IsDeterministic", flag5, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IdentitySeed", str5, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IdentityIncrement", str6, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_Default", str4, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_ComputedDefinition", str7, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_Collation", str8, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_ObjectID", num4, DbType.Int32, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_SystemType", nativeType, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_UserType", str2, DbType.String, PropertyStateEnum.ReadOnly));
                ExtendedProperty[] extendedProperties = list2.ToArray();
                if (tables.TryGetValue(TableSchema.FormatFullName(owner, str10), out schema))
                {
                    ColumnSchema schema2 = new ColumnSchema(schema, name, dbType, string.IsNullOrEmpty(nativeType) ? str2 : nativeType, size, @byte, scale, boolean, extendedProperties);
                    if (populateTable)
                    {
                        schema.Columns.Add(schema2);
                    }
                    list.Add(schema2);
                }
            }
            return list;
        }



        private ExtendedPropertyCollection GetDatabaseExtendedProperties()
        {
            ExtendedPropertyCollection propertys = new ExtendedPropertyCollection();
            propertys.AddRange(this.GetExtendedProperties("", "", "", "", "", ""));
            propertys.Add(new ExtendedProperty("CS_DatabaseVersion", this.GetSqlServerVersion(), DbType.String));
            propertys.Add(new ExtendedProperty("CS_DatabaseMajorVersion", this.GetSqlServerMajorVersion(), DbType.Int32));
            return propertys;
        }

        //public string GetDatabaseName()
        //{
        //    if (this._databaseName == string.Empty)
        //    {
        //        SqlService service = new SqlService(this.DbName) {
        //            IsSingleRow = true
        //        };
        //        using (SqlDataReader reader = service.ExecuteSqlReader("SELECT db_name()"))
        //        {
        //            while (reader.Read())
        //            {
        //                this._databaseName = row[0);
        //            }
        //        }
        //    }
        //    return this._databaseName;
        //}

        public ExtendedProperty[] GetExtendedProperties(SchemaObjectBase schemaObject)
        {
            if (schemaObject is DatabaseSchema)
            {
                DatabaseSchema schema1 = (DatabaseSchema)schemaObject;
                return this.GetDatabaseExtendedProperties().ToArray();
            }
            if (schemaObject is TableSchema)
            {
                TableSchema schema = (TableSchema)schemaObject;
                return this.GetExtendedProperties(schema.Owner, "Table", schema.Name);
            }
            if (schemaObject is ColumnSchema)
            {
                ColumnSchema column = (ColumnSchema)schemaObject;
                return this.GetColumnExtendedProperties(column).ToArray();
            }
            if (schemaObject is ViewSchema)
            {
                ViewSchema schema3 = (ViewSchema)schemaObject;
                return this.GetExtendedProperties(schema3.Owner, "View", schema3.Name);
            }
            if (schemaObject is ViewColumnSchema)
            {
                ViewColumnSchema schema4 = (ViewColumnSchema)schemaObject;
                return this.GetExtendedProperties(this.DbName, schema4.View.Owner, "View", schema4.View.Name, "Column", schema4.Name);
            }
            if (schemaObject is IndexSchema)
            {
                IndexSchema schema5 = (IndexSchema)schemaObject;
                return this.GetExtendedProperties(this.DbName, schema5.Table.Owner, "Table", schema5.Table.Name, "Index", schema5.Name);
            }

            if (schemaObject is PrimaryKeySchema)
            {
                PrimaryKeySchema schema8 = (PrimaryKeySchema)schemaObject;
                return this.GetExtendedProperties(this.DbName, schema8.Table.Owner, "Table", schema8.Table.Name, "Constraint", schema8.Name);
            }
            if (schemaObject is TableKeySchema)
            {
                TableKeySchema schema9 = (TableKeySchema)schemaObject;
                return this.GetExtendedProperties(this.DbName, schema9.ForeignKeyTable.Owner, "Table", schema9.ForeignKeyTable.Name, "Constraint", schema9.Name);
            }
            return new ExtendedProperty[0];
        }

        private ExtendedProperty[] GetExtendedProperties(string owner, string level1type, string level1name)
        {
            return this.GetExtendedProperties(this.GetLevelZero(), owner, level1type, level1name, "", "");
        }

        private ExtendedProperty[] GetExtendedProperties(string owner, string level1type, string level1name, string level2type, string level2name)
        {
            return this.GetExtendedProperties(this.GetLevelZero(), owner, level1type, level1name, level2type, level2name);
        }

        private ExtendedProperty[] GetExtendedProperties(string level0type, string level0name, string level1type, string level1name, string level2type, string level2name)
        {
            if (!this.IsSQL2000OrNewer())
            {
                return new ExtendedProperty[0];
            }
            List<ExtendedProperty> list = new List<ExtendedProperty>();
            string str = string.Empty;
            string sql = SqlScripts.GetExtendedProperties;
            sql = sql.Replace("@level0type", "'" + level0type + "'");
            sql = sql.Replace("@level0name", "'" + level0name + "'");
            sql = sql.Replace("@level1type", "'" + level1type + "'");
            sql = sql.Replace("@level1name", "'" + level1name + "'");
            sql = sql.Replace("@level2type", "'" + level2type + "'");
            sql = sql.Replace("@level2name", "'" + level2name + "'");

            //DataTable dt = UserDataAccess.ExecuteDataSet(sql,this.DbName);
            //foreach(DataRow row in dt.Rows)
            //    {
            //        ExtendedProperty item = new ExtendedProperty(row[0].ToString(), row[1], row[2] is DBNull ? DbType.Object : Utility.GetDbType(row[2].ToString()));
            //        list.Add(item);
            //        if ((item.Name == "MS_Description") && (item.Value != null))
            //        {
            //            str = item.Value.ToString();
            //        }
            //    }
            //list.Add(new ExtendedProperty("CS_Description", str, DbType.String, PropertyStateEnum.ReadOnly));
            return list.ToArray();
        }

        private List<IndexSchema> GetIndexesFromReader(TableSchema table, DataTable dt)
        {
            return this.GetIndexesFromReader(Utility.ToDictionary(table), dt, false);
        }

        private List<IndexSchema> GetIndexesFromReader(IDictionary<string, TableSchema> tables, DataTable dt, bool populateTable)
        {
            Dictionary<string, IndexSchema> dictionary = new Dictionary<string, IndexSchema>();
            List<ExtendedProperty> list = new List<ExtendedProperty>();
            foreach (DataRow row in dt.Rows)
            {
                TableSchema schema;
                bool flag = false;
                string indexName = row["IndexName"].ToString();
                int.Parse(row["Status"].ToString());
                bool boolean = row["IsPrimary"].ToString() == "1" ? true : false;
                bool isUnique = row["IsUnique"].ToString() == "1" ? true : false;
                bool isClustered = row["IsClustered"].ToString() == "1" ? true : false;
                bool flag5 = row["IgnoreDupKey"].ToString() == "1" ? true : false;
                bool flag6 = row["IsHypothetical"].ToString() == "1" ? true : false;
                bool flag7 = row["IsPadIndex"].ToString() == "1" ? true : false;
                bool flag8 = row["IsUniqueConstraint"].ToString() == "1" ? true : false;
                bool flag9 = row["IsIndex"].ToString() == "1" ? true : false;
                bool flag10 = false;
                //  bool flag11 = bool.Parse(row["NoRecompute"].ToString());
                bool flag12 = row["IsFullTextKey"].ToString() == "1" ? true : false;
                bool flag13 = row["IsTable"].ToString() == "1" ? true : false;
                bool flag14 = row["IsStatistics"].ToString() == "1" ? true : false;
                bool flag15 = row["IsAutoStatistics"].ToString() == "1" ? true : false;

                //  bool flag16 = bool.Parse(row["IsUniqueConstraint"].ToString()); 
                Byte flag17 = byte.Parse(row["FillFactor"].ToString());
                bool flag18 = row["IsComputed"].ToString() == "1" ? true : false;
                bool flag19 = row["IsDescending"].ToString() == "1" ? true : false;
                string owner = row["SchemaName"].ToString();
                string tableName = row["ParentName"].ToString();
                string str4 = row["ColumnName"].ToString();
                string key = IndexSchema.FormatFullName(owner, tableName, indexName);
                if (!Utility.IsAnyNullOrEmpty(new string[] { indexName, tableName, str4 }) && tables.TryGetValue(TableSchema.FormatFullName(owner, tableName), out schema))
                {
                    IndexSchema schema2 = null;
                    if (dictionary.ContainsKey(key))
                    {
                        schema2 = dictionary[key];
                    }
                    if (schema2 == null)
                    {
                        list.Clear();
                        list.Add(new ExtendedProperty("CS_FileGroup", row["FileGroup"].ToString(), DbType.AnsiString, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IsFullTextKey", flag12, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IsTable", flag13, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IsStatistics", flag14, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IsAutoStatistics", flag15, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IsHypothetical", flag6, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_IgnoreDupKey", flag5, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_PadIndex", flag7, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_DRIPrimaryKey", boolean, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_DRIUniqueKey", flag8, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_DRIIndex", flag9, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_DropExist", flag10, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        //  list.Add(new ExtendedProperty("CS_NoRecompute", flag11, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        //  list.Add(new ExtendedProperty("CS_IsConstraint", flag16, DbType.Boolean, PropertyStateEnum.ReadOnly));
                        list.Add(new ExtendedProperty("CS_OrigFillFactor", flag17, DbType.Byte, PropertyStateEnum.ReadOnly));
                        schema2 = new IndexSchema(schema, indexName, boolean, isUnique, isClustered, list.ToArray());
                        dictionary.Add(key, schema2);
                        flag = true;
                    }
                    list.Clear();
                    list.Add(new ExtendedProperty("CS_IsDescending", flag19, DbType.Boolean, PropertyStateEnum.ReadOnly));
                    list.Add(new ExtendedProperty("CS_IsComputed", flag18, DbType.Boolean, PropertyStateEnum.ReadOnly));
                    MemberColumnSchema schema3 = new MemberColumnSchema(schema.Columns[str4], list.ToArray());
                    schema2.MemberColumns.Add(schema3);
                    if (populateTable)
                    {
                        if (!schema.Indexes.Contains(indexName))
                        {
                            schema.Indexes.Add(schema2);
                        }
                        if (boolean)
                        {
                            if (flag)
                            {
                                list.Clear();
                                list.Add(new ExtendedProperty("CS_FileGroup", row["FileGroup"].ToString(), DbType.AnsiString, PropertyStateEnum.ReadOnly));
                                list.Add(new ExtendedProperty("CS_IsClustered", isClustered, DbType.Boolean, PropertyStateEnum.ReadOnly));
                                list.Add(new ExtendedProperty("CS_OrigFillFactor", byte.Parse(row["FillFactor"].ToString()), DbType.Byte, PropertyStateEnum.ReadOnly));
                                PrimaryKeySchema schema4 = new PrimaryKeySchema(schema, indexName, list.ToArray());
                                schema.PrimaryKey = schema4;
                            }
                            schema.PrimaryKey.MemberColumns.Add(schema3);
                        }
                    }
                }
            }
            return new List<IndexSchema>(dictionary.Values);
        }

        private List<TableKeySchema> GetKeysFromReader(TableSchema table, DataTable dt)
        {
            return this.GetKeysFromReader(Utility.ToDictionary(table), dt, false);
        }

        private List<TableKeySchema> GetKeysFromReader(IDictionary<string, TableSchema> tables, DataTable dt, bool populateTable)
        {
            Dictionary<string, TableKeySchema> dictionary = new Dictionary<string, TableKeySchema>();
            List<ExtendedProperty> list = new List<ExtendedProperty>();
            DatabaseSchema database = null;
            foreach (TableSchema schema2 in tables.Values)
            {
                database = schema2.Database;
                break;
            }
            foreach (DataRow row in dt.Rows)
            {
                string keyName = row["ConstraintName"].ToString();
                string owner = row["PrimaryTableOwner"].ToString();
                string tableName = row["PrimaryTableName"].ToString();
                string str4 = row["PrimaryColumnName"].ToString();
                string str5 = row["ForeignTableOwner"].ToString();
                string name = row["ForeignTableName"].ToString();
                string str7 = row["ForeignColumnName"].ToString();
                bool boolean = bool.Parse(row["IsNotForReplication"].ToString());
                bool flag2 = byte.Parse(row["DeleteReferentialAction"].ToString()) == 1;
                bool flag3 = byte.Parse(row["UpdateReferentialAction"].ToString()) == 1;
                bool flag4 = bool.Parse(row["WithNoCheck"].ToString());
                if (!Utility.IsAnyNullOrEmpty(new string[] { keyName, tableName, str4, name, str7 }))
                {
                    TableSchema schema3;
                    TableSchema schema4;
                    string key = string.Format("{0}.{1}", TableKeySchema.FormatFullName(owner, tableName, keyName), TableSchema.FormatFullName(str5, name));
                    if (!tables.TryGetValue(TableSchema.FormatFullName(owner, tableName), out schema3))
                    {
                        schema3 = database.Tables[owner, tableName];
                    }
                    if (!tables.TryGetValue(TableSchema.FormatFullName(str5, name), out schema4))
                    {
                        schema4 = database.Tables[str5, name];
                    }
                    if ((schema3 != null) && (schema4 != null))
                    {
                        TableKeySchema schema5;
                        if (!dictionary.TryGetValue(key, out schema5))
                        {
                            list.Clear();
                            list.Add(new ExtendedProperty("CS_CascadeDelete", flag2, DbType.Boolean, PropertyStateEnum.ReadOnly));
                            list.Add(new ExtendedProperty("CS_CascadeUpdate", flag3, DbType.Boolean, PropertyStateEnum.ReadOnly));
                            list.Add(new ExtendedProperty("CS_IsNotForReplication", boolean, DbType.Boolean, PropertyStateEnum.ReadOnly));
                            list.Add(new ExtendedProperty("CS_WithNoCheck", flag4, DbType.Boolean, PropertyStateEnum.ReadOnly));
                            schema5 = new TableKeySchema(keyName, schema4, schema3, list.ToArray());
                            dictionary.Add(key, schema5);
                            if (populateTable)
                            {
                                if (!this.ContainsKey(schema3, schema5))
                                {
                                    schema3.Keys.Add(schema5);
                                }
                                if (!this.ContainsKey(schema4, schema5))
                                {
                                    schema4.Keys.Add(schema5);
                                }
                            }
                        }
                        MemberColumnSchema schema6 = new MemberColumnSchema(schema3.Columns[str4]);
                        schema5.PrimaryKeyMemberColumns.Add(schema6);
                        MemberColumnSchema schema7 = new MemberColumnSchema(schema4.Columns[str7]);
                        schema5.ForeignKeyMemberColumns.Add(schema7);
                    }
                }
            }
            return new List<TableKeySchema>(dictionary.Values);
        }

        private string GetLevelZero()
        {
            if (this.IsSQL2005OrNewer())
            {
                return "SCHEMA";
            }
            return "USER";
        }



        private int GetSqlServerMajorVersion()
        {
            if (!this._sqlServerMajorVersionChecked)
            {
                this._sqlServerMajorVersionChecked = true;
                string sqlServerVersion = this.GetSqlServerVersion();
                if (sqlServerVersion.Length > 0)
                {
                    int index = sqlServerVersion.IndexOf('.');
                    if (index > 0)
                    {
                        this._sqlServerMajorVersion = int.Parse(sqlServerVersion.Substring(0, index));
                    }
                }
            }
            return this._sqlServerMajorVersion;
        }

        private string GetSqlServerVersion()
        {
            if (this._sqlServerVersion == null)
            {
                this._sqlServerVersion = string.Empty;
                try
                {

                    DataTable table = UserDataAccess.ExecuteDataSet("EXEC master.dbo.xp_msver ProductVersion");
                    this._sqlServerVersion = (string)table.Rows[0][3];
                }
                catch
                {
                }
            }
            return this._sqlServerVersion;
        }

        public ColumnSchema[] GetTableColumns(TableSchema table)
        {
            List<ColumnSchema> columnsFromReader;
            string sql = SqlFactory.GetTableColumns(this.GetSqlServerMajorVersion());
            sql = sql.Replace("@SchemaName", "'" + table.Owner + "'");
            sql = sql.Replace("@TableName", "'" + table.Name + "'");
            DataTable dt = UserDataAccess.ExecuteDataSet(sql);

            columnsFromReader = this.GetColumnsFromReader(table, dt);
            return columnsFromReader.ToArray();
        }

        public DataTable GetTableData(TableSchema table)
        {
            string sql = string.Format("SELECT * FROM [{0}].[{1}]", table.Owner, table.Name);
            return UserDataAccess.ExecuteDataSet(sql);
        }

        public IndexSchema[] GetTableIndexes(TableSchema table)
        {
            List<IndexSchema> indexesFromReader;
            string getTableIndexes;
            if (this.IsSQL2005OrNewer())
            {
                getTableIndexes = SqlScripts.GetTableIndexes2005;
            }
            else
            {
                getTableIndexes = SqlScripts.GetTableIndexes;
            }
            getTableIndexes = getTableIndexes.Replace("@tableName", "'" + table.Name + "'");
            getTableIndexes = getTableIndexes.Replace("@schemaName", "'" + table.Owner + "'");

            DataTable dt = UserDataAccess.ExecuteDataSet(getTableIndexes);

            indexesFromReader = this.GetIndexesFromReader(table, dt);
            return indexesFromReader.ToArray();
        }

        public TableKeySchema[] GetTableKeys(TableSchema table)
        {
            string getTableKeys;
            if (this.IsSQL2005OrNewer())
            {
                getTableKeys = SqlScripts.GetTableKeys2005;
            }
            else
            {
                getTableKeys = SqlScripts.GetTableKeys;
            }
            List<TableKeySchema> keysFromReader = new List<TableKeySchema>();
            getTableKeys = getTableKeys.Replace("@tableName", "'" + table.Name + "'");
            getTableKeys = getTableKeys.Replace("@schemaName", "'" + table.Owner + "'");
            DataTable dt = UserDataAccess.ExecuteDataSet(getTableKeys);

            keysFromReader = this.GetKeysFromReader(table, dt);
            return keysFromReader.ToArray();
        }

        public PrimaryKeySchema GetTablePrimaryKey(TableSchema table)
        {
            PrimaryKeySchema schema = null;
            foreach (IndexSchema schema2 in table.Indexes)
            {
                if (schema2.IsPrimaryKey)
                {
                    if (schema == null)
                    {
                        schema = new PrimaryKeySchema(table, schema2.Name);
                    }
                    foreach (MemberColumnSchema schema3 in schema2.MemberColumns)
                    {
                        schema.MemberColumns.Add(schema3);
                    }
                    //if (schema2.ExtendedProperties.Contains("CS_FileGroup"))
                    //{
                    //    schema.ExtendedProperties.Add(schema2.ExtendedProperties["CS_FileGroup"]);
                    //}
                    //if (schema2.ExtendedProperties.Contains("CS_OrigFillFactor"))
                    //{
                    //    schema.ExtendedProperties.Add(schema2.ExtendedProperties["CS_OrigFillFactor"]);
                    //}
                    //schema.ExtendedProperties.Add(new ExtendedProperty("CS_IsClustered", schema2.IsClustered, DbType.Boolean, PropertyStateEnum.ReadOnly));
                    return schema;
                }
            }
            return schema;
        }

        public TableSchema[] GetTables(DatabaseSchema database)
        {
            SqlBuilder builder = new SqlBuilder();
            List<TableSchema> tables = new List<TableSchema>();
            List<ExtendedProperty> list2 = new List<ExtendedProperty>();
            int sqlServerMajorVersion = this.GetSqlServerMajorVersion();
            builder.AppendStatement(SqlFactory.GetTables(sqlServerMajorVersion));
            if (database.DeepLoad)
            {
                builder.AppendStatement(SqlFactory.GetAllTableColumns(sqlServerMajorVersion));
                builder.AppendStatement(SqlFactory.GetIndexes(sqlServerMajorVersion));
                builder.AppendStatement(SqlFactory.GetKeys(sqlServerMajorVersion));
                builder.AppendStatement(SqlFactory.GetColumnConstraints(sqlServerMajorVersion));
                builder.AppendStatement(SqlFactory.GetExtendedData(sqlServerMajorVersion));
            }
            DataTable dt = UserDataAccess.ExecuteDataSet((string)builder);

            foreach (DataRow row in dt.Rows)
            {
                list2.Clear();
                list2.Add(new ExtendedProperty("CS_FileGroup", row[4].ToString(), DbType.AnsiString, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_ObjectID", int.Parse(row[5].ToString()), DbType.Int32, PropertyStateEnum.ReadOnly));
                TableSchema item = new TableSchema(database, row[0].ToString(), row[1].ToString(), DateTime.Parse(row[3].ToString()), list2.ToArray());
                tables.Add(item);
            }
            if (database.DeepLoad)
            {
                if (dt.Rows.Count > 0)
                {
                    this.PopulateTableColumns(dt, tables);
                }
                if (dt.Rows.Count > 0)
                {
                    this.PopulateTableIndexes(dt, tables);
                }
                if (dt.Rows.Count > 0)
                {
                    this.PopulateTableKeys(dt, tables);
                }
                if (dt.Rows.Count > 0)
                {
                    this.PopulateTableColumnConstraints(dt, tables);
                }
                if (dt.Rows.Count > 0)
                {
                    this.PopulateTableExtendedData(dt, tables);
                }
                this.PopulateTableDescriptions(tables);
            }
            return tables.ToArray();
        }
        private void PopulateTableIndexes(DataTable dt, IList<TableSchema> tables)
        {
            if ((tables != null) && (tables.Count >= 1))
            {
                Dictionary<string, TableSchema> dictionary = new Dictionary<string, TableSchema>();
                foreach (TableSchema schema in tables)
                {
                    dictionary.Add(schema.FullName, schema);
                    schema.Indexes = new IndexSchemaCollection();
                }
                this.GetIndexesFromReader(dictionary, dt, true);
            }
        }
        private void PopulateTableKeys(DataTable dt, IList<TableSchema> tables)
        {
            if ((tables != null) && (tables.Count >= 1))
            {
                Dictionary<string, TableSchema> dictionary = new Dictionary<string, TableSchema>();
                foreach (TableSchema schema in tables)
                {
                    dictionary.Add(schema.FullName, schema);
                    schema.Keys = new TableKeySchemaCollection();
                }
                this.GetKeysFromReader(dictionary, dt, true);
            }
        }

        public ViewColumnSchema[] GetViewColumns(ViewSchema view)
        {
            List<ViewColumnSchema> viewColumnsFromReader;
            string viewColumns = SqlFactory.GetViewColumns(this.GetSqlServerMajorVersion());
            viewColumns = viewColumns.Replace("@SchemaName", "'" + view.Owner + "'");
            viewColumns = viewColumns.Replace("@ViewName", "'" + view.Name + "'");
            DataTable dt = UserDataAccess.ExecuteDataSet(viewColumns);
            viewColumnsFromReader = this.GetViewColumnsFromReader(view, dt);
            return viewColumnsFromReader.ToArray();
        }

        private List<ViewColumnSchema> GetViewColumnsFromReader(ViewSchema view, DataTable dt)
        {
            return this.GetViewColumnsFromReader(Utility.ToDictionary(view), dt, false);
        }

        private List<ViewColumnSchema> GetViewColumnsFromReader(IDictionary<string, ViewSchema> views, DataTable dt, bool populateView)
        {
            List<ViewColumnSchema> list = new List<ViewColumnSchema>();
            List<ExtendedProperty> list2 = new List<ExtendedProperty>();
            foreach (DataRow row in dt.Rows)
            {
                string name = row["Name"].ToString();
                string str2 = row["DataType"].ToString();
                string nativeType = row["SystemType"].ToString();
                DbType dbType = Utility.GetDbType(nativeType);
                int size = int.Parse(row["Length"].ToString());
                byte @byte = byte.Parse(row["NumericPrecision"].ToString());
                int scale = int.Parse(row["NumericScale"].ToString());
                bool boolean = row["IsNullable"].ToString() == "1" ? true : false;
                string str4 = row["DefaultValue"].ToString();
                bool flag2 = int.Parse(row["IsComputed"].ToString()) == 1;
                bool flag3 = row["IsDeterministic"] is DBNull || (int.Parse(row["IsDeterministic"].ToString()) == 1);
                string str5 = row["ComputedDefinition"].ToString();
                string str6 = row["Collation"].ToString();
                int num4 = int.Parse(row["ObjectId"].ToString());
                string owner = row["SchemaName"].ToString();
                string str8 = row["ViewName"].ToString();
                list2.Clear();
                list2.Add(new ExtendedProperty("CS_IsComputed", flag2, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_IsDeterministic", flag3, DbType.Boolean, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_Default", str4, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_ComputedDefinition", str5, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_Collation", str6, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_ObjectID", num4, DbType.Int32, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_SystemType", nativeType, DbType.String, PropertyStateEnum.ReadOnly));
                list2.Add(new ExtendedProperty("CS_UserType", str2, DbType.String, PropertyStateEnum.ReadOnly));
                ViewSchema schema = null;
                if (views.TryGetValue(ViewSchema.FormatFullName(owner, str8), out schema))
                {
                    ViewColumnSchema schema2 = new ViewColumnSchema(schema, name, dbType, string.IsNullOrEmpty(nativeType) ? str2 : nativeType, size, @byte, scale, boolean, list2.ToArray());
                    if (populateView)
                    {
                        schema.Columns.Add(schema2);
                    }
                    list.Add(schema2);
                }
            }
            return list;
        }

        public DataTable GetViewData(ViewSchema view)
        {
            string sql = string.Format("SELECT * FROM [{0}].[{1}]", view.Owner, view.Name);
            return UserDataAccess.ExecuteDataSet(sql);
        }

        public ViewSchema[] GetViews(DatabaseSchema database)
        {
            SqlBuilder builder = new SqlBuilder();
            List<ViewSchema> views = new List<ViewSchema>();
            List<ExtendedProperty> list2 = new List<ExtendedProperty>();
            int sqlServerMajorVersion = this.GetSqlServerMajorVersion();
            builder.AppendStatement(SqlFactory.GetViews(sqlServerMajorVersion));
            if (database.DeepLoad)
            {
                builder.AppendStatement(SqlFactory.GetAllViewColumns(sqlServerMajorVersion));
                builder.AppendStatement(SqlFactory.GetExtendedData(sqlServerMajorVersion));
            }
            DataTable dt = UserDataAccess.ExecuteDataSet(builder);


            foreach (DataRow row in dt.Rows)
            {
                list2.Clear();
                list2.Add(new ExtendedProperty("CS_ObjectID", int.Parse(row[4].ToString()), DbType.Int32, PropertyStateEnum.ReadOnly));
                ViewSchema item = new ViewSchema(database, row[0].ToString(), row[1].ToString(), DateTime.Parse(row[3].ToString()), list2.ToArray());
                views.Add(item);
            }
            if (database.DeepLoad)
            {
                if (dt.Rows.Count > 0)
                {
                    this.PopulateViewColumns(dt, views);
                }
                if (dt.Rows.Count > 0)
                {
                    this.PopulateViewExtendedData(dt, views);
                }
                this.PopulateViewDescriptions(views);
            }
            return views.ToArray();
        }
        private void PopulateViewColumns(DataTable dt, IList<ViewSchema> views)
        {
            if ((views != null) && (views.Count >= 1))
            {
                Dictionary<string, ViewSchema> dictionary = new Dictionary<string, ViewSchema>();
                foreach (ViewSchema schema in views)
                {
                    dictionary.Add(schema.FullName, schema);
                    schema.Columns = new ViewColumnSchemaCollection();
                }
                this.GetViewColumnsFromReader(dictionary, dt, true);
            }
        }
        public string GetViewText(ViewSchema view)
        {
            StringBuilder builder = new StringBuilder();
            string sql = "EXEC sp_helptext " + string.Format("'[{0}].[{1}]'", view.Owner, view.Name);
            // sql.Replace("@objectname",string.Format("[{0}].[{1}]", view.Owner, view.Name.Replace("'", "''")));
            DataTable dt = UserDataAccess.ExecuteDataSet(sql);
            foreach (DataRow row in dt.Rows)
            {
                builder.Append(row[0].ToString());
            }

            return builder.ToString();
        }

        private bool IsSQL2000OrNewer()
        {
            return (this.GetSqlServerMajorVersion() >= 8);
        }

        private bool IsSQL2005OrNewer()
        {
            return (this.GetSqlServerMajorVersion() >= 9);
        }


        private void PopulateTableColumnConstraints(DataTable dt, IList<TableSchema> tables)
        {
            if ((tables != null) && (tables.Count >= 1))
            {
                Dictionary<string, TableSchema> dictionary = Utility.ToDictionary(tables);
                int num = 1;
                foreach (DataRow row in dt.Rows)
                {
                    TableSchema schema;
                    string name = row["TableName"].ToString();
                    string owner = row["SchemaName"].ToString();
                    string str3 = row["ColumnName"].ToString();
                    string str4 = row["ConstraintName"].ToString();
                    string str5 = row["ConstraintType"].ToString();
                    string str6 = row["ConstraintDef"].ToString();
                    if (!Utility.IsAnyNullOrEmpty(new string[] { name, owner, str3 }) && dictionary.TryGetValue(TableSchema.FormatFullName(owner, name), out schema))
                    {
                        ColumnSchema schema2 = schema.Columns[str3];
                        if (schema2 != null)
                        {
                            schema2.MarkLoaded();
                            schema2.ExtendedProperties.Add(new ExtendedProperty("CS_Constraint_" + str4 + "_Name", str4, DbType.String));
                            schema2.ExtendedProperties.Add(new ExtendedProperty("CS_Constraint_" + str4 + "_Type", str5, DbType.String));
                            schema2.ExtendedProperties.Add(new ExtendedProperty("CS_Constraint_" + str4 + "_Definition", str6, DbType.String));
                            num++;
                        }
                    }
                }
            }
        }

        private void PopulateTableColumns(DataTable dt, IList<TableSchema> tables)
        {
            if ((tables != null) && (tables.Count >= 1))
            {
                Dictionary<string, TableSchema> dictionary = new Dictionary<string, TableSchema>();
                foreach (TableSchema schema in tables)
                {
                    dictionary.Add(schema.FullName, schema);
                    schema.Columns = new ColumnSchemaCollection();
                }
                this.GetColumnsFromReader(dictionary, dt, true);
            }
        }

        private void PopulateTableDescriptions(IList<TableSchema> tables)
        {
            foreach (TableSchema schema in tables)
            {
                this.SyncDescription(schema);
                if (schema.HasPrimaryKey)
                {
                    this.SyncDescription(schema.PrimaryKey);
                }
                foreach (ColumnSchema schema2 in schema.Columns)
                {
                    this.SyncDescription(schema2);
                }
                foreach (IndexSchema schema3 in schema.Indexes)
                {
                    this.SyncDescription(schema3);
                    foreach (MemberColumnSchema schema4 in schema3.MemberColumns)
                    {
                        this.SyncDescription(schema4);
                    }
                }
                foreach (TableKeySchema schema5 in schema.Keys)
                {
                    this.SyncDescription(schema5);
                    foreach (MemberColumnSchema schema6 in schema5.ForeignKeyMemberColumns)
                    {
                        this.SyncDescription(schema6);
                    }
                    foreach (MemberColumnSchema schema7 in schema5.PrimaryKeyMemberColumns)
                    {
                        this.SyncDescription(schema7);
                    }
                }
            }
        }

        private void PopulateTableExtendedData(DataTable dt, IList<TableSchema> tables)
        {
            if ((tables != null) && (tables.Count >= 1))
            {
                Dictionary<string, TableSchema> dictionary = Utility.ToDictionary(tables);
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["PropertyName"].ToString();
                    object obj2 = row["PropertyValue"];
                    string str2 = row["PropertyBaseType"].ToString();
                    DbType dataType = string.IsNullOrEmpty(str2) ? DbType.Object : Utility.GetDbType(str2);
                    string str3 = row["ObjectName"].ToString();
                    string owner = row["ObjectOwner"].ToString();
                    string str5 = row["ObjectType"].ToString().Trim();
                    string str6 = row["ParentName"].ToString();
                    string str7 = row["ParentOwner"].ToString();
                    row["ParentType"].ToString();
                    string str8 = row["FieldName"].ToString();
                    string str9 = row["IndexName"].ToString();
                    int num = Convert.ToInt32(byte.Parse(row["Type"].ToString()));
                    int num2 = int.Parse(row["Minor"].ToString());
                    if ((name == null) || !name.StartsWith("microsoft_database_tools", StringComparison.Ordinal))
                    {
                        TableSchema schema;
                        ExtendedProperty property = new ExtendedProperty(name, obj2, dataType);
                        switch (str5)
                        {
                            case "PK":
                            case "K":
                                {
                                    dictionary.TryGetValue(TableSchema.FormatFullName(str7, str6), out schema);
                                    if ((schema != null) && schema.HasPrimaryKey)
                                    {
                                        schema.PrimaryKey.MarkLoaded();
                                        schema.PrimaryKey.ExtendedProperties.Add(property);
                                    }
                                    continue;
                                }
                            case "U":
                                {
                                    dictionary.TryGetValue(TableSchema.FormatFullName(owner, str3), out schema);
                                    if (schema != null)
                                    {
                                        if (num == 1)
                                        {
                                            if ((num2 > 0) && !string.IsNullOrEmpty(str8))
                                            {
                                                ColumnSchema schema2 = schema.Columns[str8];
                                                schema2.MarkLoaded();
                                                schema2.ExtendedProperties.Add(property);
                                            }
                                            else
                                            {
                                                schema.MarkLoaded();
                                                schema.ExtendedProperties.Add(property);
                                            }
                                        }
                                        else if ((num == 7) && !string.IsNullOrEmpty(str9))
                                        {
                                            IndexSchema schema3 = schema.Indexes[str9];
                                            schema3.MarkLoaded();
                                            schema3.ExtendedProperties.Add(property);
                                        }
                                    }
                                    continue;
                                }
                        }
                        if (str5 == "F")
                        {
                            dictionary.TryGetValue(TableSchema.FormatFullName(str7, str6), out schema);
                            if (schema != null)
                            {
                                TableKeySchema schema4 = schema.Keys[str3];
                                schema4.MarkLoaded();
                                schema4.ExtendedProperties.Add(property);
                            }
                        }
                    }
                }
            }
        }

        private void PopulateViewDescriptions(IList<ViewSchema> views)
        {
            foreach (ViewSchema schema in views)
            {
                this.SyncDescription(schema);
                foreach (ViewColumnSchema schema2 in schema.Columns)
                {
                    this.SyncDescription(schema2);
                }
            }
        }

        private void PopulateViewExtendedData(DataTable dt, IList<ViewSchema> views)
        {
            if ((views != null) && (views.Count >= 1))
            {
                Dictionary<string, ViewSchema> dictionary = Utility.ToDictionary(views);
                foreach (DataRow row in dt.Rows)
                {
                    string name = row["PropertyName"].ToString();
                    object obj2 = row["PropertyValue"].ToString();
                    string str2 = row["PropertyBaseType"].ToString();
                    DbType dataType = string.IsNullOrEmpty(str2) ? DbType.Object : Utility.GetDbType(str2);
                    string str3 = row["ObjectName"].ToString();
                    string owner = row["ObjectOwner"].ToString();
                    string str5 = row["ObjectType"].ToString().Trim();
                    string str6 = row["FieldName"].ToString();
                    //Convert.ToInt32(.GetByte("Type"));
                    int num = int.Parse(row["Minor"].ToString());
                    if ((name == null) || !name.StartsWith("microsoft_database_tools", StringComparison.Ordinal))
                    {
                        ExtendedProperty property = new ExtendedProperty(name, obj2, dataType);
                        if (str5 == "V")
                        {
                            ViewSchema schema;
                            dictionary.TryGetValue(ViewSchema.FormatFullName(owner, str3), out schema);
                            if ((num > 0) && !string.IsNullOrEmpty(str6))
                            {
                                ViewColumnSchema schema2 = schema.Columns[str6];
                                schema2.MarkLoaded();
                                schema2.ExtendedProperties.Add(property);
                            }
                            else
                            {
                                schema.MarkLoaded();
                                schema.ExtendedProperties.Add(property);
                            }
                        }
                    }
                }
            }
        }

        public void SetExtendedProperties(SchemaObjectBase schemaObject)
        {
            if (schemaObject is DatabaseSchema)
            {
                this.SetExtendedProperties(schemaObject, "", "", "", "", "", "");
            }
            else if (schemaObject is TableSchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((TableSchema)schemaObject).Owner, "Table", ((TableSchema)schemaObject).Name, "", "");
            }
            else if (schemaObject is ColumnSchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((ColumnSchema)schemaObject).Table.Owner, "Table", ((ColumnSchema)schemaObject).Table.Name, "Column", ((ColumnSchema)schemaObject).Name);
            }
            else if (schemaObject is ViewSchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((ViewSchema)schemaObject).Owner, "View", ((ViewSchema)schemaObject).Name, "", "");
            }
            else if (schemaObject is ViewColumnSchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((ViewColumnSchema)schemaObject).View.Owner, "View", ((ViewColumnSchema)schemaObject).View.Name, "Column", ((ViewColumnSchema)schemaObject).Name);
            }
            else if (schemaObject is IndexSchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((IndexSchema)schemaObject).Table.Owner, "Table", ((IndexSchema)schemaObject).Table.Name, "Index", ((IndexSchema)schemaObject).Name);
            }
            else if (schemaObject is PrimaryKeySchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((PrimaryKeySchema)schemaObject).Table.Owner, "Table", ((PrimaryKeySchema)schemaObject).Table.Name, "Constraint", ((PrimaryKeySchema)schemaObject).Name);
            }
            else if (schemaObject is TableKeySchema)
            {
                this.SetExtendedProperties(schemaObject, this.GetLevelZero(), ((TableKeySchema)schemaObject).ForeignKeyTable.Owner, "Table", ((TableKeySchema)schemaObject).ForeignKeyTable.Name, "Constraint", ((TableKeySchema)schemaObject).Name);
            }
        }

        private void SetExtendedProperties(SchemaObjectBase schemaObject, string level0type, string level0name, string level1type, string level1name, string level2type, string level2name)
        {
            try
            {
                bool flag = false;
                foreach (ExtendedProperty property in schemaObject.ExtendedProperties)
                {
                    //string name = property.Name;
                    //if (name == "CS_Description")
                    //{
                    //    name = "MS_Description";
                    //    if (!schemaObject.ExtendedProperties.Contains(name))
                    //    {
                    //        property.PropertyState = PropertyStateEnum.New;
                    //    }
                    //}
                    //if (property.PropertyState == PropertyStateEnum.New)
                    //{
                    //    service.Reset();
                    //    service.AddParameter("@name", SqlDbType.VarChar, name, true);
                    //    service.AddParameter("@value", property.DataType, property.Value, true);
                    //    service.AddParameter("@level0type", SqlDbType.VarChar, level0type, true);
                    //    service.AddParameter("@level0name", SqlDbType.VarChar, level0name, true);
                    //    service.AddParameter("@level1type", SqlDbType.VarChar, level1type, true);
                    //    service.AddParameter("@level1name", SqlDbType.VarChar, level1name, true);
                    //    service.AddParameter("@level2type", SqlDbType.VarChar, level2type, true);
                    //    service.AddParameter("@level2name", SqlDbType.VarChar, level2name, true);
                    //    service.ExecuteSP("sp_addextendedproperty");
                    //    flag = true;
                    //}
                    //else
                    //{
                    //    if (property.PropertyState == PropertyStateEnum.Dirty)
                    //    {
                    //        service.Reset();
                    //        service.AddParameter("@name", SqlDbType.VarChar, name, true);
                    //        service.AddParameter("@value", property.DataType, property.Value, true);
                    //        service.AddParameter("@level0type", SqlDbType.VarChar, level0type, true);
                    //        service.AddParameter("@level0name", SqlDbType.VarChar, level0name, true);
                    //        service.AddParameter("@level1type", SqlDbType.VarChar, level1type, true);
                    //        service.AddParameter("@level1name", SqlDbType.VarChar, level1name, true);
                    //        service.AddParameter("@level2type", SqlDbType.VarChar, level2type, true);
                    //        service.AddParameter("@level2name", SqlDbType.VarChar, level2name, true);
                    //        service.ExecuteSP("sp_updateextendedproperty");
                    //        flag = true;
                    //        continue;
                    //    }
                    //    if (property.PropertyState == PropertyStateEnum.Deleted)
                    //    {
                    //        service.Reset();
                    //        service.AddParameter("@name", SqlDbType.VarChar, name, true);
                    //        service.AddParameter("@level0type", SqlDbType.VarChar, level0type, true);
                    //        service.AddParameter("@level0name", SqlDbType.VarChar, level0name, true);
                    //        service.AddParameter("@level1type", SqlDbType.VarChar, level1type, true);
                    //        service.AddParameter("@level1name", SqlDbType.VarChar, level1name, true);
                    //        service.AddParameter("@level2type", SqlDbType.VarChar, level2type, true);
                    //        service.AddParameter("@level2name", SqlDbType.VarChar, level2name, true);
                    //        service.ExecuteSP("sp_dropextendedproperty");
                    //        flag = true;
                    //    }
                    //}
                }
                if (flag)
                {
                    schemaObject.Refresh();
                }
            }
            finally
            {
                //if (service != null)
                //{
                //    service.Disconnect();
                //}
            }
        }



        private void SyncDescription(SchemaObjectBase schema)
        {
            schema.MarkLoaded();
            if (schema is MemberColumnSchema)
            {
                ((MemberColumnSchema)schema).Column.MarkLoaded();
            }
            string str = string.Empty;
            if (schema.ExtendedProperties.Contains("MS_Description"))
            {
                str = schema.ExtendedProperties["MS_Description"].Value as string;
            }
            schema.ExtendedProperties["CS_Description"] = new ExtendedProperty("CS_Description", str ?? string.Empty, DbType.String, PropertyStateEnum.ReadOnly);
        }



        public string Description
        {
            get
            {
                return "SQL Server Schema Provider";
            }
        }

        public bool EditorAvailable
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "SqlSchemaProvider";
            }
        }


        /// <summary>
        /// Return the Constant NAME of this SchemaProvider
        /// </summary>
        private string _DbName;

        public string DbName
        {
            get { return _DbName; }
            set { _DbName = value; }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                this._userName = value;
            }

        }
    }
}

