namespace SchemaExplorer
{
    using System;
    using System.ComponentModel;
    using System.Data;

    [Serializable]
    public class MemberColumnSchema : ColumnSchema
    {
        private bool _areExtendedPropertiesMerged;
        private ExtendedProperty[] _defaultExtendedProperties;
        private ColumnSchema _memberColumn;

        public MemberColumnSchema(ColumnSchema memberColumn)
        {
            this._memberColumn = memberColumn;
            base._extendedProperties = new ExtendedPropertyCollection();
        }

        public MemberColumnSchema(ColumnSchema memberColumn, ExtendedProperty[] extendedProperties)
        {
            this._memberColumn = memberColumn;
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            return this.Column.Equals(obj);
        }

        public override int GetHashCode()
        {
            return (this.Column.Table.GetHashCode() ^ this.Column.Name.GetHashCode());
        }

        public override void Refresh()
        {
            goto Label_0003;
           
        Label_0003:
            base.Refresh();
            this.Column.Table.Refresh();
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override bool AllowDBNull
        {
            get
            {
                return this.Column.AllowDBNull;
            }
        }

        [Browsable(false)]
        public ColumnSchema Column
        {
            get
            {
                return this._memberColumn;
            }
        }

        [Browsable(false)]
        public ExtendedPropertyCollection ColumnExtendedProperties
        {
            get
            {
                return this.Column.ExtendedProperties;
            }
        }

        [Browsable(false)]
        public override DatabaseSchema Database
        {
            get
            {
                return this.Column.Database;
            }
        }

        public override DbType DataType
        {
            get
            {
                return this.Column.DataType;
            }
        }

        public override string Description
        {
            get
            {
                return this.Column.Description;
            }
        }

        [Description("Used to store any additional information about the schema object. The meta data can be set if the provider supports it.")]
        public override ExtendedPropertyCollection ExtendedProperties
        {
            get
            {
                // This item is obfuscated and can not be translated.
                int num = 1;
            Label_000D:
                switch (num)
                {
                    case 0:
                        
                        break;

                    case 2:
                        base.ExtendedProperties = SchemaUtility.MergeExtendedProperties(base.ExtendedProperties, this.Column.ExtendedProperties);
                        this._areExtendedPropertiesMerged = true;
                        num = 0;
                        goto Label_000D;

                    default:
                        if (!this._areExtendedPropertiesMerged)
                        {
                            num = 2;
                            goto Label_000D;
                        }
                        break;
                }
                return base.ExtendedProperties;
            }
            set
            {
                base.ExtendedProperties = value;
            }
        }

        public override bool IsForeignKeyMember
        {
            get
            {
                return this.Column.IsForeignKeyMember;
            }
        }

        public override bool IsPrimaryKeyMember
        {
            get
            {
                return this.Column.IsPrimaryKeyMember;
            }
        }

        public override bool IsUnique
        {
            get
            {
                return this.Column.IsUnique;
            }
        }

        public override string Name
        {
            get
            {
                return this.Column.Name;
            }
        }

        public override string NativeType
        {
            get
            {
                return this.Column.NativeType;
            }
        }

        public override byte Precision
        {
            get
            {
                return this.Column.Precision;
            }
        }

        public override int Scale
        {
            get
            {
                return this.Column.Scale;
            }
        }

        public override int Size
        {
            get
            {
                return this.Column.Size;
            }
        }

        public override Type SystemType
        {
            get
            {
                return this.Column.SystemType;
            }
        }

        [Browsable(false)]
        public override TableSchema Table
        {
            get
            {
                return this.Column.Table;
            }
        }
    }
}

