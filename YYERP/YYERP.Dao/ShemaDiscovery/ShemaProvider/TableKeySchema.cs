namespace SchemaExplorer
{
    using System;
    using System.ComponentModel;

    [Serializable]
    public class TableKeySchema : SchemaObjectBase
    {
        private ExtendedProperty[] _defaultExtendedProperties;
        private MemberColumnSchemaCollection _foreignKeyMemberColumns;
        private TableSchema _foreignKeyTable;
        private MemberColumnSchemaCollection _primaryKeyMemberColumns;
        private TableSchema _primaryKeyTable;

        public TableKeySchema(string name, TableSchema foreignKeyTable, TableSchema primaryKeyTable, ExtendedProperty[] extendedProperties)
        {
            base._name = name;
            base._database = foreignKeyTable.Database;
            this._foreignKeyTable = foreignKeyTable;
            this._primaryKeyTable = primaryKeyTable;
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
            this._foreignKeyMemberColumns = new MemberColumnSchemaCollection();
            this._primaryKeyMemberColumns = new MemberColumnSchemaCollection();
        }

        public TableKeySchema(DatabaseSchema database, string name, string[] foreignKeyMemberColumns, string foreignKeyTable, string[] primaryKeyMemberColumns, string primaryKeyTable) : this(database, name, foreignKeyMemberColumns, string.Empty, foreignKeyTable, primaryKeyMemberColumns, string.Empty, primaryKeyTable)
        {
        }

        public TableKeySchema(DatabaseSchema database, string name, string[] foreignKeyMemberColumns, string foreignKeyTable, string[] primaryKeyMemberColumns, string primaryKeyTable, ExtendedProperty[] extendedProperties) : this(database, name, foreignKeyMemberColumns, string.Empty, foreignKeyTable, primaryKeyMemberColumns, string.Empty, primaryKeyTable)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public TableKeySchema(DatabaseSchema database, string name, string[] foreignKeyMemberColumns, string foreignKeyTableOwner, string foreignKeyTable, string[] primaryKeyMemberColumns, string primaryKeyTableOwner, string primaryKeyTable)
        {
            base._database = database;
            base._name = name;
            if (foreignKeyTableOwner.Length <= 0)
            {
                this._foreignKeyTable = this.Database.Tables[foreignKeyTable];
            }
            else
            {
                this._foreignKeyTable = this.Database.Tables[foreignKeyTableOwner, foreignKeyTable];
            }
            this._foreignKeyMemberColumns = new MemberColumnSchemaCollection();
            foreach (string str in foreignKeyMemberColumns)
            {
                this._foreignKeyMemberColumns.Add(new MemberColumnSchema(this._foreignKeyTable.Columns[str]));
            }
            if (primaryKeyTableOwner.Length <= 0)
            {
                this._primaryKeyTable = this.Database.Tables[primaryKeyTable];
            }
            else
            {
                this._primaryKeyTable = this.Database.Tables[primaryKeyTableOwner, primaryKeyTable];
            }
            this._primaryKeyMemberColumns = new MemberColumnSchemaCollection();
            foreach (string str2 in primaryKeyMemberColumns)
            {
                this._primaryKeyMemberColumns.Add(new MemberColumnSchema(this._primaryKeyTable.Columns[str2]));
            }
        }

        public TableKeySchema(DatabaseSchema database, string name, string[] foreignKeyMemberColumns, string foreignKeyTableOwner, string foreignKeyTable, string[] primaryKeyMemberColumns, string primaryKeyTableOwner, string primaryKeyTable, ExtendedProperty[] extendedProperties) : this(database, name, foreignKeyMemberColumns, foreignKeyTableOwner, foreignKeyTable, primaryKeyMemberColumns, primaryKeyTableOwner, primaryKeyTable)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            TableKeySchema schema;
            int num;
            goto Label_0023;
        Label_0002:
            switch (num)
            {
                case 0:
                    num = 4;
                    goto Label_0002;

                case 1:
                    return true;

                case 2:
                    goto Label_0080;

                case 3:
                    if (schema == null)
                    {
                        goto Label_00A3;
                    }
                    num = 5;
                    goto Label_0002;

                case 4:
                    if (!(schema.Name == this.Name))
                    {
                        goto Label_00A3;
                    }
                    num = 1;
                    goto Label_0002;

                case 5:
                    num = 2;
                    goto Label_0002;
            }
        Label_0023:
            schema = obj as TableKeySchema;
            num = 3;
            goto Label_0002;
           
        Label_0080:
            if (schema.PrimaryKeyTable.Equals(this.PrimaryKeyTable))
            {
                num = 0;
                goto Label_0002;
            }
        Label_00A3:
            return false;
        }

        public static string FormatFullName(string owner, string tableName, string keyName)
        {
            // This item is obfuscated and can not be translated.
            if (!string.IsNullOrEmpty(owner))
            {
                return string.Format("{0}.{1}.{2}", owner, tableName, keyName);
            }
            
            return string.Format("{0}.{1}", tableName, keyName);
        }

        public override int GetHashCode()
        {
            return (this.PrimaryKeyTable.GetHashCode() ^ this.Name.GetHashCode());
        }

        public override void Refresh()
        {
            goto Label_0003;
           
        Label_0003:
            base.Refresh();
            this.Database.Refresh();
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        [Browsable(false)]
        public MemberColumnSchemaCollection ForeignKeyMemberColumns
        {
            get
            {
                return this._foreignKeyMemberColumns;
            }
        }

        [Browsable(false)]
        public TableSchema ForeignKeyTable
        {
            get
            {
                return this._foreignKeyTable;
            }
        }

        [Browsable(false)]
        public PrimaryKeySchema PrimaryKey
        {
            get
            {
                return this.PrimaryKeyTable.PrimaryKey;
            }
        }

        [Browsable(false)]
        public MemberColumnSchemaCollection PrimaryKeyMemberColumns
        {
            get
            {
                return this._primaryKeyMemberColumns;
            }
        }

        [Browsable(false)]
        public TableSchema PrimaryKeyTable
        {
            get
            {
                return this._primaryKeyTable;
            }
        }
    }
}

