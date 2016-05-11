namespace SchemaExplorer
{
     
     
    //using SchemaExplorer.Serialization;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing.Design;

    [Serializable]
    public class ColumnSchema : DataObjectBase
    {
        private ExtendedProperty[] _defaultExtendedProperties;
        private TableSchema _table;

        internal ColumnSchema()
        {
        }

        public ColumnSchema(TableSchema table, string name, DbType dataType, string nativeType, int size, byte precision, int scale, bool allowDBNull)
        {
            base._database = table.Database;
            this._table = table;
            base._name = name;
            base._dataType = dataType;
            base._nativeType = nativeType;
            base._size = size;
            base._precision = precision;
            base._scale = scale;
            base._allowDBNull = allowDBNull;
        }

        public ColumnSchema(TableSchema table, string name, DbType dataType, string nativeType, int size, byte precision, int scale, bool allowDBNull, ExtendedProperty[] extendedProperties) : this(table, name, dataType, nativeType, size, precision, scale, allowDBNull)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override bool Equals(object obj)
        {
            // This item is obfuscated and can not be translated.
            ColumnSchema schema = obj as ColumnSchema;  
            if (!schema.Table.Equals(this.Table)||schema == null||!(schema.Name == this.Name))
            {
               return false;
            }
            else
               return false;
        }

        public override int GetHashCode()
        {
            return (this.Table.GetHashCode() ^ this.Name.GetHashCode());
        }

        public override void Refresh()
        { 
            base.Refresh();
            this.Table.Refresh();
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Table, this.Name);
        }

        public virtual bool IsForeignKeyMember
        {
            get
            {
                for (int num = 0; num < this.Table.ForeignKeys.Count; num++)
                {
                    if (this.Table.ForeignKeys[num].ForeignKeyMemberColumns.Contains(this))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public virtual bool IsPrimaryKeyMember
        {
            get
            {
                if (this.Table.HasPrimaryKey)
                {
                    return this.Table.PrimaryKey.MemberColumns.Contains(this.Name);
                   
                }
                return false;
            }
        }

        public virtual bool IsUnique
        {
            get
            {
                int num;
                int num2;
                goto Label_0033;
            Label_0002:
                switch (num2)
                {
                    case 0:
                        if (this.Table.Indexes[num].MemberColumns.Count != 1)
                        {
                            goto Label_0045;
                        }
                        num2 = 8;
                        goto Label_0002;

                    case 1:
                        num2 = 0;
                        goto Label_0002;

                    case 2:
                        return false;

                    case 3:
                        return true;

                    case 4:
                        if (num < this.Table.Indexes.Count)
                        {
                            num2 = 7;
                        }
                        else
                        {
                            num2 = 2;
                        }
                        goto Label_0002;

                    case 5:
                    case 6:
                        goto Label_00CB;

                    case 7:
                        if (!this.Table.Indexes[num].IsUnique)
                        {
                            goto Label_0045;
                        }
                        num2 = 1;
                        goto Label_0002;

                    case 8:
                        num2 = 9;
                        goto Label_0002;

                    case 9:
                        if (!this.Table.Indexes[num].MemberColumns.Contains(this))
                        {
                            goto Label_0045;
                        }
                        num2 = 3;
                        goto Label_0002;
                }
            Label_0033:
                num = 0;
                num2 = 6;
                goto Label_0002;
            Label_0045:
                num++;
                num2 = 5;
                goto Label_0002;
               
            Label_00CB:
                num2 = 4;
                goto Label_0002;
            }
        }

        [Browsable(false)]
        public virtual TableSchema Table
        {
            get
            {
                return this._table;
            }
        }
    }
}

