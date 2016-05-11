namespace SchemaExplorer
{
     
     
    //using SchemaExplorer.Serialization;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing.Design;

    [Serializable]
    public class TableSchema : TabularObjectBase
    {
        private ColumnSchemaCollection _columns;
        private DateTime _dateCreated;
        private ExtendedProperty[] _defaultExtendedProperties;
        private ColumnSchemaCollection _foreignKeyColumns;
        private TableKeySchemaCollection _foreignKeys;
        private IndexSchemaCollection _indexes;
        private TableKeySchemaCollection _keys;
        private ColumnSchemaCollection _nonForeignKeyColumns;
        private ColumnSchemaCollection _nonKeyColumns;
        private ColumnSchemaCollection _nonPrimaryKeyColumns;
        private string _owner;
        private PrimaryKeySchema _primaryKey;
        private bool _primaryKeyChecked;
        private TableKeySchemaCollection _primaryKeys;

        public TableSchema(DatabaseSchema database, string name, string owner, DateTime dateCreated)
        {
            this._dateCreated = DateTime.MinValue;
            base._database = database;
            base._name = name;
            this._owner = owner;
            this._dateCreated = dateCreated;
        }

        public TableSchema(DatabaseSchema database, string name, string owner, DateTime dateCreated, ExtendedProperty[] extendedProperties) : this(database, name, owner, dateCreated)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        private void CheckPrimaryKey()
        {
            // This item is obfuscated and can not be translated.
            PrimaryKeySchema tablePrimaryKey;
            this.Database.ValidateProvider();
            tablePrimaryKey = this.Database.Provider.GetTablePrimaryKey(this); 
            this._primaryKey = tablePrimaryKey; 
            if (this._primaryKeyChecked)
            {
                return;
            } 
        } 
        public override bool Equals(object obj)
        { 
            TableSchema schema = obj as TableSchema;

            if (schema == null)
                return false;
            else
            {
                if (!(schema.Owner == this.Owner) || !(schema.Name == this.Name) ||
                    !schema.Database.Equals(this.Database))
                    return false;
            }

            return true;
        }

        public static string FormatFullName(string owner, string name)
        {
            if (string.IsNullOrEmpty(owner))
            {
                return name;
            }
            return string.Format("{0}.{1}", owner, name);
        }

        public override int GetHashCode()
        {
            return ((this.Database.GetHashCode() ^ this.Owner.GetHashCode()) ^ this.Name.GetHashCode());
        }

        public DataTable GetTableData()
        {
            return this.Database.Provider.GetTableData(this);
        }

        public bool IsDependantOf(TableSchema table)
        {
            TableSchemaCollection checkedTables = new TableSchemaCollection();
            return this.IsDependantOfInternal(table, checkedTables);
        }

        private bool IsDependantOfInternal(TableSchema table, TableSchemaCollection checkedTables)
        {

           
           
            //        for(int i;i < table.ForeignKeys.Count;i++)
            //        {
            //            TableSchema primaryKeyTable = table.ForeignKeys[i].PrimaryKeyTable;
            //            if (!primaryKeyTable.Equals(this))
            //            {
            //                if (!checkedTables.Contains(primaryKeyTable))
            //                {
            //                    checkedTables.Add(primaryKeyTable);
            //                }
            //            }
            //        }
                   
                    
          
            //if (this.IsDependantOfInternal(primaryKeyTable, checkedTables))
            //{
                
            //}

            return false;
       
        }

        public override void Refresh()
        {
            
            base.Refresh();
            this._primaryKey = null;
            this._keys = null;
            this._indexes = null;
            this._columns = null;
            this._nonPrimaryKeyColumns = null;
            this._nonKeyColumns = null;
            this._foreignKeys = null;
            this._primaryKeys = null;
            this._primaryKeyChecked = false;
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override string ToString()
        {
            return FormatFullName(this.Owner, this.Name);
        }

        [Browsable(false)]
        public ColumnSchemaCollection Columns
        {
            get
            {
                if (_columns == null)
                {
                    this.Database.ValidateProvider();
                    this._columns = new ColumnSchemaCollection(this.Database.Provider.GetTableColumns(this));
                }
                return this._columns;
            }
            set
            {
                this._columns = value;
            }
        }
        private DataTable _dataTableColumns;
        public DataTable DataTableColumns
        {
            get 
            {
                if (_dataTableColumns == null)
                {
                    _dataTableColumns=ConvertColumnSchemaCollectionToDataTable(Columns);

                }
                return _dataTableColumns;
            }
        }
        /// <summary>
        /// 得到表中所有的列
        /// </summary>
        /// <param name="Columns"></param>
        /// <returns></returns>
        private DataTable ConvertColumnSchemaCollectionToDataTable(ColumnSchemaCollection Columns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IsPrimayColumns",typeof(bool));
            dt.Columns.Add("AllowDBNull", typeof(bool));
            dt.Columns.Add("DataType", typeof(DbType));
            dt.Columns.Add("NativeType", typeof(string));
            dt.Columns.Add("Precision", typeof(byte));
            dt.Columns.Add("Scale", typeof(int));
            dt.Columns.Add("Size", typeof(int));
            dt.Columns.Add("SystemType", typeof(Type));

            foreach (ColumnSchema columnschema in Columns)
            {
                DataRow row = dt.NewRow();
                if (columnschema.IsPrimaryKeyMember)
                    row["IsPrimayColumns"] = true;
                else
                    row["IsPrimayColumns"] = false;
                row["AllowDBNull"] = columnschema.AllowDBNull;
                row["DataType"] = columnschema.DataType;
                row["NativeType"] = columnschema.NativeType;
                row["Precision"] = columnschema.Precision;
                row["Scale"] = columnschema.Scale;
                row["Size"] = columnschema.Size;
                row["SystemType"] = columnschema.SystemType;
                dt.Rows.Add(row);
            }
            return dt;
        }

        public DateTime DateCreated
        {
            get
            {
                return this._dateCreated;
            }
        }

        [Browsable(false)]
        public ColumnSchemaCollection ForeignKeyColumns
        {
            get
            {   
                
                if (this._foreignKeyColumns == null)
                {
                    this._foreignKeyColumns = new ColumnSchemaCollection();
                    IColumnSchemaEnumerator enumerator = this.Columns.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        ColumnSchema schema = enumerator.Current;                        

                        if (!schema.IsForeignKeyMember)
                        {
                            this._foreignKeyColumns.Add(schema);
                        }
                    }
                } 
                return this._foreignKeyColumns;
            }
        }

        [Browsable(false)]
        public TableKeySchemaCollection ForeignKeys
        {
            get
            {       
                if (this._foreignKeys == null)
                {
                    this._foreignKeys = new TableKeySchemaCollection();
                    ITableKeySchemaEnumerator enumerator = this.Keys.GetEnumerator();

                    if (enumerator.MoveNext())
                    {
                        TableKeySchema schema = enumerator.Current;
                        if (schema.ForeignKeyTable != this)
                        {
                            this._foreignKeys.Add(schema);
                        }
                    }
                }
                
                 return this._foreignKeys;
            }
        }

        public override string FullName
        {
            get
            {
                return FormatFullName(this.Owner, this.Name);
            }
        }

        [Browsable(false)]
        public bool HasPrimaryKey
        {
            get
            {
                this.CheckPrimaryKey();
                return (this._primaryKey != null);
            }
        }

        [Browsable(false)]
        public IndexSchemaCollection Indexes
        {
            get
            { 
                if (this._indexes == null)
                {
                    this.Database.ValidateProvider();
                    this._indexes = new IndexSchemaCollection(this.Database.Provider.GetTableIndexes(this));
                }
                return this._indexes;
            }
            set
            {
                this._indexes = value;
            }
        }

        private DataTable _dataTableIndex;
        public DataTable DataTableIndex
        {
            get 
            {
                if (_dataTableIndex == null)
                {
                    _dataTableIndex = ConverIndexSchemaCollectionToDataTable(Indexes);
                }
                return _dataTableIndex;
            }
        }
        /// <summary>
        /// 得到表中索引数据表
        /// </summary>
        /// <param name="Indexes"></param>
        /// <returns></returns>
        private DataTable ConverIndexSchemaCollectionToDataTable(IndexSchemaCollection Indexes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IsPrimayColumns", typeof(bool));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("IsUnique", typeof(bool));
            dt.Columns.Add("IsClustered", typeof(bool));

            if (Indexes != null)
            {
                foreach (IndexSchema indexSchema in Indexes)
                {
                    DataRow row = dt.NewRow();
                    if (indexSchema.IsPrimaryKey)
                        row["IsPrimayColumns"] = true;
                    else
                        row["IsPrimayColumns"] = false;
                    row["Name"] = indexSchema.Name;
                    row["IsUnique"] = indexSchema.IsUnique;
                    row["IsClustered"] = indexSchema.IsClustered;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        [Browsable(false)]
        public TableKeySchemaCollection Keys
        {
            get
            {
               if(this.Keys == null)
               {
                        this.Database.ValidateProvider();
                        this._keys = new TableKeySchemaCollection(this.Database.Provider.GetTableKeys(this));
                       
                }
                return this._keys;
            }
            set
            {
                this._keys = value;
            }
        }

        [Browsable(false)]
        public ColumnSchemaCollection NonForeignKeyColumns
        {
            get
            {
                if (_nonForeignKeyColumns == null)
                {
                    this._nonForeignKeyColumns = new ColumnSchemaCollection();
                    IColumnSchemaEnumerator enumerator = this.Columns.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        ColumnSchema schema = enumerator.Current;
                        if (schema.IsForeignKeyMember)
                        {
                            this._nonForeignKeyColumns.Add(schema);
                        }
                    }
                }
              
               
                return this._nonForeignKeyColumns;
            }
        }

        [Browsable(false)]
        public ColumnSchemaCollection NonKeyColumns
        {
            get
            {

                if (this._nonKeyColumns == null)
                {
                    this._nonKeyColumns = new ColumnSchemaCollection();
                    IColumnSchemaEnumerator enumerator = this.Columns.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        ColumnSchema schema = enumerator.Current;

                        if (!schema.IsPrimaryKeyMember && !schema.IsForeignKeyMember)
                        {
                            this._nonKeyColumns.Add(schema);
                        }      
                    }
                }   
               
                return this._nonKeyColumns;
            }
        }

        [Browsable(false)]
        public ColumnSchemaCollection NonPrimaryKeyColumns
        {
            get
            {
                if (this._nonPrimaryKeyColumns != null)
                {
                    this._nonPrimaryKeyColumns = new ColumnSchemaCollection();
                    IColumnSchemaEnumerator enumerator = this.Columns.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        ColumnSchema schema = enumerator.Current;

                        if (!schema.IsPrimaryKeyMember)
                        {
                            this._nonPrimaryKeyColumns.Add(schema);
                        }
                    }
                }
                
               
                return this._nonPrimaryKeyColumns;
            }
        }

        public string Owner
        {
            get
            {
                return this._owner;
            }
        }

        [Browsable(false)]
        public PrimaryKeySchema PrimaryKey
        {
            get
            {
               
                    this.CheckPrimaryKey();
                    if (this._primaryKey == null)
                    {
                        throw new ApplicationException(string.Format("Table \"{0}\" does not have a primary key.  Please use the HasPrimaryKey property to check for the existence of a primary key before accessing the PrimaryKey property.", this.Name));
                    }
                    return this._primaryKey;
               
            }
            set
            {
                this._primaryKey = value;
            }
        }

        private DataTable _dataTablePrimaryKey;
        public DataTable DataTablePrimaryKey
        {
            get
            {
                if (this._dataTablePrimaryKey == null)
                {
                    _dataTablePrimaryKey = ConvertMemberColumnSchemaCollectionToDataTable(PrimaryKey);
                }
                return _dataTablePrimaryKey;
            }
        }
        /// <summary>
        /// 得到表中的主键数据表
        /// </summary>
        /// <param name="PrimaryKey"></param>
        /// <returns></returns>
        private DataTable ConvertMemberColumnSchemaCollectionToDataTable(PrimaryKeySchema PrimaryKey)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IsPrimayColumns", typeof(bool));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("AllowDBNull", typeof(bool));
            dt.Columns.Add("DataType", typeof(DbType));
            dt.Columns.Add("NativeType", typeof(string));
            dt.Columns.Add("Precision", typeof(byte));
            dt.Columns.Add("Scale", typeof(int));
            dt.Columns.Add("Size", typeof(int));
            dt.Columns.Add("SystemType", typeof(Type));

            if (PrimaryKey != null)
            {
                foreach (MemberColumnSchema memberColumnSchema in PrimaryKey.MemberColumns)
                {
                    DataRow row = dt.NewRow();
                    if (memberColumnSchema.IsPrimaryKeyMember)
                        row["IsPrimayColumns"] = true;
                    else
                        row["IsPrimayColumns"] = false;
                    row["Name"] = memberColumnSchema.Name;
                    row["AllowDBNull"] = memberColumnSchema.AllowDBNull;
                    row["DataType"] = memberColumnSchema.DataType;
                    row["NativeType"] = memberColumnSchema.NativeType;
                    row["Precision"] = memberColumnSchema.Precision;
                    row["Scale"] = memberColumnSchema.Scale;
                    row["Size"] = memberColumnSchema.Size;
                    row["SystemType"] = memberColumnSchema.SystemType;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        [Browsable(false)]
        public TableKeySchemaCollection PrimaryKeys
        {
            get
            {
                if (this._primaryKeys == null)
                {
                    this._primaryKeys = new TableKeySchemaCollection();
                    ITableKeySchemaEnumerator enumerator = this.Keys.GetEnumerator();
                    if (enumerator.MoveNext())
                    {
                        TableKeySchema schema = enumerator.Current;
                        if (schema.PrimaryKeyTable == this)
                        {
                            this._primaryKeys.Add(schema);
                        }
                    }
                }
                return this._primaryKeys;
            }
        }

        public string SortName
        {
            get
            {
                // This item is obfuscated and can not be translated.
                if (!string.IsNullOrEmpty(this.Owner))
                {
                    return string.Format("{0} ({1})", this.Name, this.Owner);
                }
                
                return this.Name;
            }
        }
    }
}

