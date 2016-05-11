namespace SchemaExplorer
{
     
     
    //using SchemaExplorer.Serialization;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing.Design;

    [Serializable]
    public class ViewColumnSchema : DataObjectBase
    {
        private ExtendedProperty[] _defaultExtendedProperties;
        private ViewSchema _view;

        internal ViewColumnSchema()
        {
        }

        [Obsolete("Please use the constructors that contain the allowDBNull parameter.", false)]
        public ViewColumnSchema(ViewSchema view, string name, DbType dataType, string nativeType, int size, byte precision, int scale)
        {
            base._database = view.Database;
            this._view = view;
            base._name = name;
            base._dataType = dataType;
            base._nativeType = nativeType;
            base._size = size;
            base._precision = precision;
            base._scale = scale;
        }

        public ViewColumnSchema(ViewSchema view, string name, DbType dataType, string nativeType, int size, byte precision, int scale, bool allowDBNull)
        {
            base._database = view.Database;
            this._view = view;
            base._name = name;
            base._dataType = dataType;
            base._nativeType = nativeType;
            base._size = size;
            base._precision = precision;
            base._scale = scale;
            base._allowDBNull = allowDBNull;
        }

        [Obsolete("Please use the constructors that contain the allowDBNull parameter.", false)]
        public ViewColumnSchema(ViewSchema view, string name, DbType dataType, string nativeType, int size, byte precision, int scale, ExtendedProperty[] extendedProperties) : this(view, name, dataType, nativeType, size, precision, scale)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public ViewColumnSchema(ViewSchema view, string name, DbType dataType, string nativeType, int size, byte precision, int scale, bool allowDBNull, ExtendedProperty[] extendedProperties) : this(view, name, dataType, nativeType, size, precision, scale, allowDBNull)
        {
            this._defaultExtendedProperties = extendedProperties;
            base._extendedProperties = new ExtendedPropertyCollection(extendedProperties);
        }

        public override bool Equals(object obj)
        {
            // This item is obfuscated and can not be translated.
            ViewColumnSchema schema;
        Label_0023:
            schema = obj as ViewColumnSchema;
            int num = 1;
        Label_0002:
            switch (num)
            {
                case 0:
                    if (!(schema.Name == this.Name))
                    {
                        break;
                    }
                    num = 4;
                    goto Label_0002;

                case 1:
                    if (schema == null)
                    {
                        break;
                    }
                    num = 3;
                    goto Label_0002;

                case 2:
                    
                    if (!schema.View.Equals(this.View))
                    {
                        break;
                    }
                    num = 5;
                    goto Label_0002;

                case 3:
                    num = 2;
                    goto Label_0002;

                case 4:
                    return true;

                case 5:
                    num = 0;
                    goto Label_0002;

                default:
                    goto Label_0023;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (this.View.GetHashCode() ^ this.Name.GetHashCode());
        }

        public override void Refresh()
        {
            goto Label_0003;
           
        Label_0003:
            base.Refresh();
            this.View.Refresh();
            base._extendedProperties = new ExtendedPropertyCollection(this._defaultExtendedProperties);
        }

        public override string ToString()
        {
            return string.Format("{0}.{1}", this.View, this.Name);
        }

        [Browsable(false)]
        public virtual ViewSchema View
        {
            get
            {
                return this._view;
            }
        }
    }
}

