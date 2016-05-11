namespace SchemaExplorer
{
    using System;
    using System.ComponentModel;

    [Serializable]
    public class IndexSchema : SchemaObjectBase
    {
        private ExtendedProperty[] _defaultExtendedProperties;
        private bool _isClustered;
        private bool _isPrimaryKey;
        private bool _isUnique;
        private MemberColumnSchemaCollection _memberColumns;
        private TableSchema _table;

        public IndexSchema(TableSchema table, string name, bool isPrimaryKey, bool isUnique, bool isClustered)
        {
            base._database = table.Database;
            this._table = table;
            base._name = name;
            this._isPrimaryKey = isPrimaryKey;
            this._isUnique = isUnique;
            this._isClustered = isClustered;
            this._memberColumns = new MemberColumnSchemaCollection();
        }

        public IndexSchema(TableSchema table, string name, bool isPrimaryKey, bool isUnique, bool isClustered, ExtendedProperty[] extendedProperties) : this(table, name, isPrimaryKey, isUnique, isClustered)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public IndexSchema(TableSchema table, string name, bool isPrimaryKey, bool isUnique, bool isClustered, string[] memberColumns) : this(table, name, isPrimaryKey, isUnique, isClustered)
        {
            this._memberColumns = new MemberColumnSchemaCollection();
            string[] strArray = memberColumns;
            int index = 0;
            while (true)
            {
                if (index >= strArray.Length)
                {
                    break;
                }
                string str = strArray[index];
                this._memberColumns.Add(new MemberColumnSchema(this.Table.Columns[str]));
                index++;
            }
        }

        public IndexSchema(TableSchema table, string name, bool isPrimaryKey, bool isUnique, bool isClustered, string[] memberColumns, ExtendedProperty[] extendedProperties) : this(table, name, isPrimaryKey, isUnique, isClustered, memberColumns)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            IndexSchema schema;
            int num;
            goto Label_0023;
        Label_0002:
            switch (num)
            {
                case 0:
                    num = 2;
                    goto Label_0002;

                case 1:
                    num = 3;
                    goto Label_0002;

                case 2:
                    goto Label_0080;

                case 3:
                    if (!(schema.Name == this.Name))
                    {
                        goto Label_00A3;
                    }
                    num = 4;
                    goto Label_0002;

                case 4:
                    return true;
                   

                case 5:
                    if (schema == null)
                    {
                        goto Label_00A3;
                    }
                    num = 0;
                    goto Label_0002;
            }
        Label_0023:
            schema = obj as IndexSchema;
            num = 5;
            goto Label_0002;
        Label_0080:
            if (schema.Table.Equals(this.Table))
            {
                num = 1;
                goto Label_0002;
            }
        Label_00A3:
            return false;
        }

        public static string FormatFullName(string owner, string tableName, string indexName)
        {
            // This item is obfuscated and can not be translated.
            if (!string.IsNullOrEmpty(owner))
            {
                return string.Format("{0}.{1}.{2}", owner, tableName, indexName);
            } 
            return string.Format("{0}.{1}", tableName, indexName);
        }

        public override int GetHashCode()
        {
            return (this.Table.GetHashCode() ^ this.Name.GetHashCode());
        }

        public override void Refresh()
        {
            goto Label_0003;
           
        Label_0003:
            base.Refresh();
            this.Table.Refresh();
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public bool IsClustered
        {
            get
            {
                return this._isClustered;
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return this._isPrimaryKey;
            }
        }

        public bool IsUnique
        {
            get
            {
                return this._isUnique;
            }
        }

        [Browsable(false)]
        public MemberColumnSchemaCollection MemberColumns
        {
            get
            {
                return this._memberColumns;
            }
        }

        [Browsable(false)]
        public TableSchema Table
        {
            get
            {
                return this._table;
            }
        }
    }
}

