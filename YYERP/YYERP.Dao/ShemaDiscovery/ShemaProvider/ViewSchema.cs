namespace SchemaExplorer
{ 
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing.Design;

    [Serializable]
    public class ViewSchema : TabularObjectBase
    {
        private ViewColumnSchemaCollection _columns;
        private DateTime _dateCreated;
        private ExtendedProperty[] _defaultExtendedProperties;
        private string _owner;
        private string _viewText;

        public ViewSchema(DatabaseSchema database, string name, string owner, DateTime dateCreated)
        {
            this._dateCreated = DateTime.MinValue;
            this._viewText = string.Empty;
            base._database = database;
            base._name = name;
            this._owner = owner;
            this._dateCreated = dateCreated;
        }

        public ViewSchema(DatabaseSchema database, string name, string owner, DateTime dateCreated, ExtendedProperty[] extendedProperties) : this(database, name, owner, dateCreated)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            // This item is obfuscated and can not be translated.
            ViewSchema schema = obj as ViewSchema;
            if (schema == null || !schema.Database.Equals(this.Database) || !(schema.Owner == this.Owner) || !(schema.Name == this.Name))
            {
                return false;
            }
            else
                return true; 
        }

        public static string FormatFullName(string owner, string name)
        {
            if (string.IsNullOrEmpty(owner))
            {
                return name;
            }
            return (owner + "." + name);
        }

        public override int GetHashCode()
        {
            // This item is obfuscated and can not be translated.
             
            return ((this.Database.GetHashCode() ^ this.Owner.GetHashCode()) ^ this.Name.GetHashCode());
        }

        public DataTable GetViewData()
        {
            return this.Database.Provider.GetViewData( this);
        }

        public override void Refresh()
        { 
            base.Refresh();
            this._columns = null;
            this._viewText = string.Empty;
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override string ToString()
        {
            return null;
            // This item is obfuscated and can not be translated.
        }

        [Browsable(false)]
        public ViewColumnSchemaCollection Columns
        {
            get
            {    
                if (this._columns == null)
                {
                    this.Database.ValidateProvider();
                    this._columns = new ViewColumnSchemaCollection(this.Database.Provider.GetViewColumns( this));
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
                    _dataTableColumns = ConvertViewColumnSchemaCollectionToDataTable(Columns);
                }
                return _dataTableColumns;
            }
        }

        private DataTable ConvertViewColumnSchemaCollectionToDataTable(ViewColumnSchemaCollection Columns)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("AllowDBNull", typeof(bool));
            dt.Columns.Add("DataType", typeof(DbType));
            dt.Columns.Add("NativeType", typeof(string));
            dt.Columns.Add("Precision", typeof(byte));
            dt.Columns.Add("Scale", typeof(int));
            dt.Columns.Add("Size", typeof(int));
            dt.Columns.Add("SystemType", typeof(Type));

            foreach (ViewColumnSchema columnschema in Columns)
            {
                DataRow row = dt.NewRow();
                row["Name"] = columnschema.Name;
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

        public override string FullName
        {
            get
            {
                return FormatFullName(this.Owner, this.Name);
            }
        }

        public string Owner
        {
            get
            {
                return this._owner;
            }
        }

        public string SortName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Owner))
                {
                    return string.Format("{0} ({1})", this.Name, this.Owner);
                   
                }
                return this.Name;
            }
        }

        [Browsable(false)]
        public string ViewText
        {
            get
            {    
                if (this._viewText == string.Empty)
                {
                    this.Database.ValidateProvider();
                    this._viewText = this.Database.Provider.GetViewText(this); 
                } 
                return this._viewText;
            }
        }
    }
}

