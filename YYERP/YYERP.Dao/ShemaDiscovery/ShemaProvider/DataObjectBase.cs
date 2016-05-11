namespace SchemaExplorer
{
    using System;
    using System.Data;

    [Serializable]
    public abstract class DataObjectBase : SchemaObjectBase
    {
        protected bool _allowDBNull;
        protected DbType _dataType;
        protected string _nativeType = string.Empty;
        protected byte _precision;
        protected int _scale;
        protected int _size;

        public virtual bool AllowDBNull
        {
            get
            {
                return this._allowDBNull;
            }
        }

        public virtual DbType DataType
        {
            get
            {
                return this._dataType;
            }
        }

        public virtual string NativeType
        {
            get
            {
                return this._nativeType;
            }
        }

        public virtual byte Precision
        {
            get
            {
                return this._precision;
            }
        }

        public virtual int Scale
        {
            get
            {
                return this._scale;
            }
        }

        public virtual int Size
        {
            get
            {
                return this._size;
            }
        }

        public virtual Type SystemType
        {
            get
            {
                return SchemaUtility.GetSystemType(this.DataType);
            }
        }
    }
}

