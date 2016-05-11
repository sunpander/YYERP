namespace SchemaExplorer
{
    using System;
    using System.ComponentModel;
using System.Data;

    [Serializable]
    public class PrimaryKeySchema : SchemaObjectBase
    {
        private ExtendedProperty[] _defaultExtendedProperties;
        private MemberColumnSchemaCollection _memberColumns;
        private TableSchema _table;

        public PrimaryKeySchema(TableSchema table, string name)
        {
            base._database = table.Database;
            this._table = table;
            base._name = name;
            this._memberColumns = new MemberColumnSchemaCollection();
        }

        public PrimaryKeySchema(TableSchema table, string name, ExtendedProperty[] extendedProperties) : this(table, name)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public PrimaryKeySchema(TableSchema table, string name, string[] memberColumns) : this(table, name)
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

        public PrimaryKeySchema(TableSchema table, string name, string[] memberColumns, ExtendedProperty[] extendedProperties) : this(table, name, memberColumns)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            // This item is obfuscated and can not be translated.
            PrimaryKeySchema schema;
        Label_0023:
            schema = obj as PrimaryKeySchema;
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    num = 2;
                    goto Label_0002;

                case 1:
                    if (schema == null)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_0002;

                case 2:
                   
                    if (!schema.Table.Equals(this.Table))
                    {
                        break;
                    }
                    num = 4;
                    goto Label_0002;

                case 3:
                    return true;

                case 4:
                    num = 5;
                    goto Label_0002;

                case 5:
                    if (!(schema.Name == this.Name))
                    {
                        break;
                    }
                    num = 3;
                    goto Label_0002;

                default:
                    goto Label_0023;
            }
            return false;
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

