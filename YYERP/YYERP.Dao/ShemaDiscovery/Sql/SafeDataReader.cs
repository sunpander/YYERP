namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;
    using System.Data;
    using System.Reflection;

    public class SafeDataReader : IDataReader, IDisposable, IDataRecord
    {
        private IDataReader _dataReader;
        private bool _disposedValue;

        public SafeDataReader(IDataReader dataReader)
        {
            this._dataReader = dataReader;
        }

        public void Close()
        {
            this._dataReader.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposedValue && disposing)
            {
                this._dataReader.Dispose();
            }
            this._disposedValue = true;
        }

        ~SafeDataReader()
        {
            this.Dispose(false);
        }

        public virtual bool GetBoolean(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return false;
            }
            return this._dataReader.GetBoolean(i);
        }

        public bool GetBoolean(string name)
        {
            return this.GetBoolean(this._dataReader.GetOrdinal(name));
        }

        public virtual byte GetByte(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetByte(i);
        }

        public byte GetByte(string name)
        {
            return this.GetByte(this._dataReader.GetOrdinal(name));
        }

        public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetBytes(i, fieldOffset, buffer, bufferOffset, length);
        }

        public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.GetBytes(this._dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        public virtual char GetChar(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return '\0';
            }
            char[] buffer = new char[1];
            this._dataReader.GetChars(i, 0, buffer, 0, 1);
            return buffer[0];
        }

        public char GetChar(string name)
        {
            return this.GetChar(this._dataReader.GetOrdinal(name));
        }

        public virtual long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetChars(i, fieldOffset, buffer, bufferOffset, length);
        }

        public long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.GetChars(this._dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
        }

        public virtual IDataReader GetData(int i)
        {
            return this._dataReader.GetData(i);
        }

        public IDataReader GetData(string name)
        {
            return this.GetData(this._dataReader.GetOrdinal(name));
        }

        public virtual string GetDataTypeName(int i)
        {
            return this._dataReader.GetDataTypeName(i);
        }

        public string GetDataTypeName(string name)
        {
            return this.GetDataTypeName(this._dataReader.GetOrdinal(name));
        }

        public virtual DateTime GetDateTime(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return DateTime.MinValue;
            }
            return this._dataReader.GetDateTime(i);
        }

        public virtual DateTime GetDateTime(string name)
        {
            return this.GetDateTime(this._dataReader.GetOrdinal(name));
        }

        public virtual decimal GetDecimal(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0M;
            }
            return this._dataReader.GetDecimal(i);
        }

        public decimal GetDecimal(string name)
        {
            return this.GetDecimal(this._dataReader.GetOrdinal(name));
        }

        public virtual double GetDouble(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0.0;
            }
            return this._dataReader.GetDouble(i);
        }

        public double GetDouble(string name)
        {
            return this.GetDouble(this._dataReader.GetOrdinal(name));
        }

        public virtual Type GetFieldType(int i)
        {
            return this._dataReader.GetFieldType(i);
        }

        public Type GetFieldType(string name)
        {
            return this.GetFieldType(this._dataReader.GetOrdinal(name));
        }

        public virtual float GetFloat(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0f;
            }
            return this._dataReader.GetFloat(i);
        }

        public float GetFloat(string name)
        {
            return this.GetFloat(this._dataReader.GetOrdinal(name));
        }

        public virtual Guid GetGuid(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return Guid.Empty;
            }
            return this._dataReader.GetGuid(i);
        }

        public Guid GetGuid(string name)
        {
            return this.GetGuid(this._dataReader.GetOrdinal(name));
        }

        public virtual short GetInt16(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetInt16(i);
        }

        public short GetInt16(string name)
        {
            return this.GetInt16(this._dataReader.GetOrdinal(name));
        }

        public virtual int GetInt32(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetInt32(i);
        }

        public int GetInt32(string name)
        {
            return this.GetInt32(this._dataReader.GetOrdinal(name));
        }

        public virtual long GetInt64(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return 0;
            }
            return this._dataReader.GetInt64(i);
        }

        public long GetInt64(string name)
        {
            return this.GetInt64(this._dataReader.GetOrdinal(name));
        }

        public virtual string GetName(int i)
        {
            return this._dataReader.GetName(i);
        }

        public int GetOrdinal(string name)
        {
            return this._dataReader.GetOrdinal(name);
        }

        public DataTable GetSchemaTable()
        {
            return this._dataReader.GetSchemaTable();
        }

        public virtual string GetString(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return string.Empty;
            }
            return this._dataReader.GetString(i);
        }

        public string GetString(string name)
        {
            return this.GetString(this._dataReader.GetOrdinal(name));
        }

        public virtual object GetValue(int i)
        {
            if (this._dataReader.IsDBNull(i))
            {
                return null;
            }
            return this._dataReader.GetValue(i);
        }

        public object GetValue(string name)
        {
            return this.GetValue(this._dataReader.GetOrdinal(name));
        }

        public int GetValues(object[] values)
        {
            return this._dataReader.GetValues(values);
        }

        public virtual bool IsDBNull(int i)
        {
            return this._dataReader.IsDBNull(i);
        }

        public virtual bool IsDBNull(string name)
        {
            return this.IsDBNull(this._dataReader.GetOrdinal(name));
        }

        public bool NextResult()
        {
            return this._dataReader.NextResult();
        }

        public bool Read()
        {
            return this._dataReader.Read();
        }

        protected IDataReader DataReader
        {
            get
            {
                return this._dataReader;
            }
        }

        public int Depth
        {
            get
            {
                return this._dataReader.Depth;
            }
        }

        public int FieldCount
        {
            get
            {
                return this._dataReader.FieldCount;
            }
        }

        public bool IsClosed
        {
            get
            {
                return this._dataReader.IsClosed;
            }
        }

        public virtual object this[int i]
        {
            get
            {
                if (this._dataReader.IsDBNull(i))
                {
                    return null;
                }
                return this._dataReader[i];
            }
        }

        public object this[string name]
        {
            get
            {
                object obj2 = this._dataReader[name];
                if (DBNull.Value.Equals(obj2))
                {
                    return null;
                }
                return obj2;
            }
        }

        public int RecordsAffected
        {
            get
            {
                return this._dataReader.RecordsAffected;
            }
        }
    }
}

