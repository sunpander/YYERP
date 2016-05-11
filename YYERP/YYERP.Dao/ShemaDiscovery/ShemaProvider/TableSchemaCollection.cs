namespace SchemaExplorer
{
     
     
    //using SchemaExplorer.Serialization;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    [Serializable]
    public class TableSchemaCollection : ITableSchemaList, IList, IList<TableSchema>, ICloneable
    {
        private TableSchema[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        public TableSchemaCollection()
        {
            this._array = new TableSchema[0x10];
        }

        public TableSchemaCollection(TableSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new TableSchema[collection.Count];
            this.AddRange(collection);
        }

        private TableSchemaCollection(Tag tag)
        {
        }

        public TableSchemaCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new TableSchema[capacity];
        }

        public TableSchemaCollection(TableSchema[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this._array = new TableSchema[array.Length];
            this.AddRange(array);
        }

        public virtual int Add(TableSchema value)
        {
            int num2 = 0;
        Label_000D:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    this.EnsureCapacity(this._count + 1);
                    num2 = 2;
                    goto Label_000D;

                case 2:
                    goto Label_005D;

                default:
                    break;
                   
            }
            if (this._count == this._array.Length)
            {
                num2 = 1;
                goto Label_000D;
            }
        Label_005D:
            this._version++;
            this._array[this._count] = value;
            return this._count++;
        }

        public virtual void AddRange(TableSchema[] array)
        {
            // This item is obfuscated and can not be translated.
            int num = 4;
        Label_0018:
            switch (num)
            {
                case 0:
                    if (array.Length != 0)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_0018;

                case 1:
                    return;

                case 2:
                    throw new ArgumentNullException("array");

                case 3:
                    goto Label_00C8;

                case 4:
                    
                    break;

                case 5:
                    if ((this._count + array.Length) <= this._array.Length)
                    {
                        goto Label_00C8;
                    }
                    num = 6;
                    goto Label_0018;

                case 6:
                    this.EnsureCapacity(this._count + array.Length);
                    num = 3;
                    goto Label_0018;
            }
            if (array != null)
            {
                num = 0;
            }
            else
            {
                num = 2;
            }
            goto Label_0018;
        Label_00C8:
            this._version++;
            Array.Copy(array, 0, this._array, this._count, array.Length);
            this._count += array.Length;
        }

        public virtual void AddRange(TableSchemaCollection collection)
        {
            // This item is obfuscated and can not be translated.
            int num = 6;
        Label_0018:
            switch (num)
            {
                case 0:
                    if (collection.Count != 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_0018;

                case 1:
                    if ((this._count + collection.Count) <= this._array.Length)
                    {
                        goto Label_00D1;
                    }
                    num = 2;
                    goto Label_0018;

                case 2:
                    this.EnsureCapacity(this._count + collection.Count);
                    num = 5;
                    goto Label_0018;

                case 3:
                    return;

                case 4:
                    throw new ArgumentNullException("collection");

                case 5:
                    goto Label_00D1;

                case 6:
                  
                    break;
            }
            if (collection != null)
            {
                num = 0;
            }
            else
            {
                num = 4;
            }
            goto Label_0018;
        Label_00D1:
            this._version++;
            Array.Copy(collection.InnerArray, 0, this._array, this._count, collection.Count);
            this._count += collection.Count;
        }

        public virtual int BinarySearch(TableSchema value)
        {
            return Array.BinarySearch<TableSchema>(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            // This item is obfuscated and can not be translated.
           
            int num = 3;
        Label_0018:
            switch (num)
            {
                case 0:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 2;
                    goto Label_0018;

                case 1:
                    num = 0;
                    goto Label_0018;

                case 2:
                    break;

                default:
                    if (index >= 0)
                    {
                        num = 1;
                        goto Label_0018;
                    }
                    break;
            }
            throw new InvalidOperationException("Enumerator is not on a collection element.");
        }

        private void CheckEnumVersion(int version)
        {
            if (version != this._version)
            {
                throw new InvalidOperationException("Enumerator invalidated by modification to collection.");
            }
        }

        private void CheckTargetArray(Array array, int arrayIndex)
        {
            int num = 5;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (arrayIndex >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;

                case 1:
                    throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");

                case 2:
                    throw new ArgumentException("Argument must be less than array length.", "arrayIndex");

                case 3:
                    throw new ArgumentNullException("array");

                case 4:
                    if (this._count <= (array.Length - arrayIndex))
                    {
                        return;
                    }
                    num = 8;
                    goto Label_000D;

                case 6:
                    throw new ArgumentException("Argument cannot be multidimensional.", "array");

                case 7:
                    if (array.Rank <= 1)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;

                case 8:
                    break;

                case 9:
                    if (arrayIndex < array.Length)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                default:
                    if (array == null)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentException("Argument section must be large enough for collection.", "array");
        }

        public virtual void Clear()
        {
            if (this._count != 0)
            {
                this._version++;
                Array.Clear(this._array, 0, this._count);
                this._count = 0;
            }
            else
            {
                return;
               
            }
        }

        public virtual object Clone()
        {
            // This item is obfuscated and can not be translated.
            
            TableSchemaCollection schemas = new TableSchemaCollection(this._count);
            Array.Copy(this._array, 0, schemas._array, 0, this._count);
            schemas._count = this._count;
            schemas._version = this._version;
            return schemas;
        }

        public virtual bool Contains(TableSchema value)
        {
            return (this.IndexOf(value) >= 0);
        }

        public bool Contains(string name)
        {
            return (this.IndexOf(name) >= 0);
        }

        public bool Contains(string owner, string name)
        {
            return (this.IndexOf(owner, name) >= 0);
        }

        public virtual void CopyTo(TableSchema[] array)
        {
            // This item is obfuscated and can not be translated.
           
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(TableSchema[] array, int arrayIndex)
        {
            goto Label_0003;
           
        Label_0003:
            this.CheckTargetArray(array, arrayIndex);
            Array.Copy(this._array, 0, array, arrayIndex, this._count);
        }

        private void EnsureCapacity(int minimum)
        {
            // This item is obfuscated and can not be translated.
        }

        public virtual ITableSchemaEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int IndexOf(TableSchema value)
        {
            return Array.IndexOf<TableSchema>(this._array, value, 0, this._count);
        }

        public int IndexOf(string name)
        {
            // This item is obfuscated and can not be translated.
            int num;
        Label_0023:
            num = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 4:
                    
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (num < this.Count)
                    {
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 2:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        num++;
                        num2 = 4;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    return num;

                case 5:
                    return -1;
            }
            goto Label_0023;
        }

        public int IndexOf(string owner, string name)
        {
            int num;
            int num2;
            goto Label_002B;
        Label_0002:
            switch (num2)
            {
                case 0:
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    return num;

                case 2:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        goto Label_003D;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 3:
                    if (string.Compare(this[num].Owner, owner, true) != 0)
                    {
                        goto Label_003D;
                    }
                    num2 = 0;
                    goto Label_0002;

                case 4:
                    if (num < this.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_0002;

                case 5:
                case 7:
                    goto Label_007D;

                case 6:
                    return -1;
            }
        Label_002B:
            num = 0;
            num2 = 5;
            goto Label_0002;
           
        Label_003D:
            num++;
            num2 = 7;
            goto Label_0002;
        Label_007D:
            num2 = 4;
            goto Label_0002;
        }

        public virtual void Insert(int index, TableSchema value)
        {
            // This item is obfuscated and can not be translated.
            int num = 9;
        Label_000D:
            switch (num)
            {
                case 0:
                    Array.Copy(this._array, index, this._array, index + 1, this._count - index);
                    num = 5;
                    goto Label_000D;

                case 1:
                    if (index <= this._count)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 2:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");

                case 3:
                    break;

                case 4:
                    this.EnsureCapacity(this._count + 1);
                    num = 3;
                    goto Label_000D;

                case 5:
                    goto Label_0161;

                case 6:
                    if (this._count != this._array.Length)
                    {
                        break;
                    }
                    num = 4;
                    goto Label_000D;

                case 7:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 8:
                    if (index >= this._count)
                    {
                        goto Label_0161;
                    }
                   
                    num = 0;
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;
            }
            this._version++;
            num = 8;
            goto Label_000D;
        Label_0161:
            this._array[index] = value;
            this._count++;
        }

        public static TableSchemaCollection ReadOnly(TableSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new ReadOnlyList(collection);
        }

        public void Refresh()
        {
            // This item is obfuscated and can not be translated.
            ITableSchemaEnumerator enumerator = this.GetEnumerator();
            try
            {
                int num = 3;
            Label_0016:
                switch (num)
                {
                    case 0:
                        if (enumerator.MoveNext())
                        {
                            break;
                        }
                        num = 2;
                        goto Label_0016;

                    case 2:
                        num = 4;
                        goto Label_0016;

                    case 4:
                        

                    default:
                        num = 0;
                        goto Label_0016;
                }
                enumerator.Current.Refresh();
                num = 1;
                goto Label_0016;
            }
            finally
            {
                IDisposable disposable;
                int num2;
                goto Label_0093;
            Label_007E:
                switch (num2)
                {
                    case 0:
                        disposable.Dispose();
                        num2 = 1;
                        goto Label_007E;

                    case 1:
                        goto Label_00C8;

                    case 2:
                        if (disposable == null)
                        {
                            goto Label_00C8;
                        }
                        num2 = 0;
                        goto Label_007E;
                }
            Label_0093:
                disposable = enumerator as IDisposable;
                num2 = 2;
                goto Label_007E;
            Label_00C8:;
            } 
        }

        public virtual void Remove(TableSchema value)
        {
            int num;
            int num2;
            goto Label_0017;
        Label_0002:
            switch (num2)
            {
                case 0:
                    this.RemoveAt(num);
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    return;

                case 2:
                    goto Label_002D;
            }
        Label_0017:
            num = this.IndexOf(value);
            num2 = 2;
            goto Label_0002;
           
        Label_002D:
            if (num >= 0)
            {
                num2 = 0;
                goto Label_0002;
            }
        }

        public virtual void RemoveAt(int index)
        {
        Label_0017:
            this.ValidateIndex(index);
            this._version++;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                    Array.Copy(this._array, index + 1, this._array, index, this._count - index);
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (index >= --this._count)
                    {
                        break;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0017;
                   
            }
            this._array[this._count] = null;
        }

        public virtual void RemoveRange(int index, int count)
        {
            int num = 8;
        Label_000D:
            switch (num)
            {
                case 0:
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                    num = 5;
                    goto Label_000D;

                case 1:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 2:
                    if (count >= 0)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;

                case 3:
                    return;

                case 4:
                    if (index >= this._count)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_000D;

                case 5:
                    break;

                case 6:
                    if ((index + count) <= this._count)
                    {
                        num = 10;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;

                case 7:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 9:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 10:
                    if (count != 0)
                    {
                        this._version++;
                        this._count -= count;
                        num = 4;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;
                   
            }
            Array.Clear(this._array, this._count, count);
        }

        public virtual void Reverse()
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (0 <= (0 + 2))
                {
                    return;
                } 
            }
            this._version++;
            Array.Reverse(this._array, 0, this._count);
        }

        public virtual void Reverse(int index, int count)
        {
            int num = 8;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (count >= 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;

                case 1:
                    if ((index + count) <= this._count)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_000D;

                case 2:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Reverse(this._array, index, count);
                        return;
                    }
                    num = 5;
                    goto Label_000D;

                case 3:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 4:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 5:
                    return;

                case 6:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 7;
                    goto Label_000D;

                case 7:
                    num = 2;
                    goto Label_000D;

                case 9:
                    break;

                default:
                    if (index >= 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
        }

        public virtual void Sort()
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (1 <= (1 + 4))
                {
                    return;
                }
                
            }
            this._version++;
            Array.Sort<TableSchema>(this._array, 0, this._count);
        }

        public virtual void Sort(IComparer comparer)
        {
            // This item is obfuscated and can not be translated.
            if (this._count > 1)
            {
                
                this._version++;
                Array.Sort(this._array, 0, this._count, comparer);
            }
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            int num = 3;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 1:
                    num = 8;
                    goto Label_000D;

                case 2:
                    return;

                case 4:
                    break;

                case 5:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 6:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 1;
                    goto Label_000D;

                case 7:
                    if (count >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;

                case 8:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Sort(this._array, index, count, comparer);
                        return;
                    }
                    num = 2;
                    goto Label_000D;

                case 9:
                    if ((index + count) <= this._count)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 7;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
        }

        public static TableSchemaCollection Synchronized(TableSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection<TableSchema>.Add(TableSchema item)
        {
            this.Add(item);
        }

        bool ICollection<TableSchema>.Remove(TableSchema item)
        {
            goto Label_0003;
           
        Label_0003:
            if (!this.Contains(item.Name))
            {
                return false;
            }
            this.Remove(item);
            return true;
        }

        IEnumerator<TableSchema> IEnumerable<TableSchema>.GetEnumerator()
        {
            return new List<TableSchema>(this.ToArray()).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((TableSchema[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((TableSchema) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((TableSchema) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((TableSchema) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (TableSchema) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((TableSchema) value);
        }

        public virtual TableSchema[] ToArray()
        {
            TableSchema[] schemaArray;
            goto Label_0003;
           
        Label_0003:
            schemaArray = new TableSchema[this._count];
            Array.Copy(this._array, schemaArray, this._count);
            return schemaArray;
        }

        public override string ToString()
        {
            StringBuilder builder;
            int num;
            int num2;
            goto Label_002A;
        Label_0002:
            switch (num2)
            {
                case 0:
                    if (num >= (this.Count - 1))
                    {
                        goto Label_003F;
                    }
                    num2 = 5;
                    goto Label_0002;

                case 1:
                case 2:
                    num2 = 4;
                    goto Label_0002;

                case 3:
                    return builder.ToString();

                case 4:
                    if (num < this.Count)
                    {
                        builder.AppendFormat("\"{0}\"", this[num].ToString().Replace("\"", "\"\""));
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 5:
                    builder.Append(", ");
                    num2 = 6;
                    goto Label_0002;

                case 6:
                    goto Label_003F;

                default:
                    goto Label_002A;
                   
            }
        Label_002A:
            builder = new StringBuilder();
            num = 0;
            num2 = 2;
            goto Label_0002;
        Label_003F:
            num++;
            num2 = 1;
            goto Label_0002;
        }

        public virtual void TrimToSize()
        {
            this.Capacity = this._count;
        }

        private void ValidateIndex(int index)
        {
            // This item is obfuscated and can not be translated.
            int num = 2;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 1:
                    throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");

                case 3:
                    if (index < this._count)
                    {
                        return;
                    }
                    
                    num = 1;
                    goto Label_000D;
            }
            if (index < 0)
            {
                num = 0;
            }
            else
            {
                num = 3;
            }
            goto Label_000D;
        }

        public virtual int Capacity
        {
            get
            {
                return this._array.Length;
            }
            set
            {
                int num = 2;
            Label_000D:
                switch (num)
                {
                    case 0:
                        return;

                    case 1:
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

                    case 3:
                        if (value >= this._count)
                        {
                            num = 4;
                        }
                        else
                        {
                            num = 1;
                        }
                        goto Label_000D;

                    case 4:
                        if (value != 0)
                        {
                            TableSchema[] destinationArray = new TableSchema[value];
                            Array.Copy(this._array, destinationArray, this._count);
                            this._array = destinationArray;
                            return;
                        }
                        num = 5;
                        goto Label_000D;

                    case 5:
                        this._array = new TableSchema[0x10];
                        return;

                    default:
                        if (value != this._array.Length)
                        {
                            num = 3;
                            goto Label_000D;
                        }
                        break;
                       
                }
                num = 0;
                goto Label_000D;
            }
        }

        public virtual int Count
        {
            get
            {
                return this._count;
            }
        }

        protected virtual TableSchema[] InnerArray
        {
            get
            {
                return this._array;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public TableSchema this[string owner, string name]
        {
            get
            {
                int index = this.IndexOf(owner, name);
                if (index < 0)
                {
                    return null;
                }
                goto Label_0010;
               
            Label_0010:;
                return this[index];
            }
        }

        public TableSchema this[string name]
        {
            get
            {
                int index = this.IndexOf(name);
                if (index >= 0)
                {
                    return this[index];
                   
                }
                return null;
            }
        }

        public virtual TableSchema this[int index]
        {
            get
            {
                this.ValidateIndex(index);
                return this._array[index];
            }
            set
            {
                // This item is obfuscated and can not be translated.
                
                this.ValidateIndex(index);
                this._version++;
                this._array[index] = value;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return this;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (TableSchema) value;
            }
        }

        [Serializable]
        private sealed class Enumerator : ITableSchemaEnumerator, IEnumerator
        {
            private readonly TableSchemaCollection _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(TableSchemaCollection collection)
            {
                this._collection = collection;
                this._version = collection._version;
                this._index = -1;
            }

            public bool MoveNext()
            {
                // This item is obfuscated and can not be translated.
                 
                this._collection.CheckEnumVersion(this._version);
                return (++this._index < this._collection.Count);
            }

            public void Reset()
            {
                this._collection.CheckEnumVersion(this._version);
                this._index = -1;
            }

            public TableSchema Current
            {
                get
                {
                    goto Label_0003;
                   
                Label_0003:
                    this._collection.CheckEnumIndex(this._index);
                    this._collection.CheckEnumVersion(this._version);
                    return this._collection[this._index];
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }
        }

        [Serializable]
        private sealed class ReadOnlyList : TableSchemaCollection
        {
            private TableSchemaCollection _collection;

            internal ReadOnlyList(TableSchemaCollection collection) : base(TableSchemaCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(TableSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(TableSchemaCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(TableSchema[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(TableSchema value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new TableSchemaCollection.ReadOnlyList((TableSchemaCollection) this._collection.Clone());
            }

            public override bool Contains(TableSchema value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(TableSchema[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(TableSchema[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override ITableSchemaEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(TableSchema value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, TableSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(TableSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveAt(int index)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void RemoveRange(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Reverse(int index, int count)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override TableSchema[] ToArray()
            {
                return this._collection.ToArray();
            }

            public override void TrimToSize()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int Capacity
            {
                get
                {
                    return this._collection.Capacity;
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override int Count
            {
                get
                {
                    return this._collection.Count;
                }
            }

            protected override TableSchema[] InnerArray
            {
                get
                {
                    return this._collection.InnerArray;
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return true;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return true;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return this._collection.IsSynchronized;
                }
            }

            public override TableSchema this[int index]
            {
                get
                {
                    return this._collection[index];
                }
                set
                {
                    throw new NotSupportedException("Read-only collections cannot be modified.");
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._collection.SyncRoot;
                }
            }
        }

        [Serializable]
        private sealed class SyncList : TableSchemaCollection
        {
            private TableSchemaCollection _collection;
            private object _root;

            internal SyncList(TableSchemaCollection collection) : base(TableSchemaCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(TableSchema value)
            {
                // This item is obfuscated and can not be translated.
                int num;
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    num = this._collection.Add(value);
                }
                finally
                {
                   
                    Monitor.Exit(obj2);
                }
                return num;
            }

            public override void AddRange(TableSchemaCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
                return;
               
            }

            public override void AddRange(TableSchema[] array)
            {
                // This item is obfuscated and can not be translated.
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.AddRange(array);
                }
                finally
                {
                     
                    Monitor.Exit(obj2);
                }
            }

            public override int BinarySearch(TableSchema value)
            {
                // This item is obfuscated and can not be translated.
                int num;
                lock (this._root)
                {
                    num = this._collection.BinarySearch(value);
                }
                
                return num;
            }

            public override void Clear()
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                    
                    this._collection.Clear();
                }
            }

            public override object Clone()
            {
                //object obj2;
                lock (this._root)
                {
                    return new TableSchemaCollection.SyncList((TableSchemaCollection) this._collection.Clone());
                }
               
                //return obj2;
            }

            public override bool Contains(TableSchema value)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(TableSchema[] array)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(TableSchema[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override ITableSchemaEnumerator GetEnumerator()
            {
                ITableSchemaEnumerator enumerator;
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    enumerator = this._collection.GetEnumerator();
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
                return enumerator;
            }

            public override int IndexOf(TableSchema value)
            {
                int index;
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    index = this._collection.IndexOf(value);
                }
                finally
                {
                    goto Label_0021;
                   
                Label_0021:
                    Monitor.Exit(obj2);
                }
                return index;
            }

            public override void Insert(int index, TableSchema value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
                return;
               
            }

            public override void Remove(TableSchema value)
            {
                lock (this._root)
                {
                    this._collection.Remove(value);
                }
                return;
               
            }

            public override void RemoveAt(int index)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    this._collection.RemoveAt(index);
                }
            }

            public override void RemoveRange(int index, int count)
            {
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.RemoveRange(index, count);
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
            }

            public override void Reverse()
            {
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Reverse();
                }
                finally
                {
                    goto Label_001F;
                   
                Label_001F:
                    Monitor.Exit(obj2);
                }
            }

            public override void Reverse(int index, int count)
            {
                object obj2;
                goto Label_0003;
               
            Label_0003:
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Reverse(index, count);
                }
                finally
                {
                    Monitor.Exit(obj2);
                }
            }

            public override void Sort()
            {
                // This item is obfuscated and can not be translated.
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.Sort();
                }
                finally
                {
                    
                    Monitor.Exit(obj2);
                }
            }

            public override void Sort(IComparer comparer)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.Sort(comparer);
                }
            }

            public override void Sort(int index, int count, IComparer comparer)
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                    this._collection.Sort(index, count, comparer);
                }
                if (7 <= (7 + 5))
                {
                    return;
                }
               
            }

            public override TableSchema[] ToArray()
            {
                // This item is obfuscated and can not be translated.
                lock (this._root)
                {
                   
                    return this._collection.ToArray();
                }
            }

            public override void TrimToSize()
            {
                object obj2;
                Monitor.Enter(obj2 = this._root);
                try
                {
                    this._collection.TrimToSize();
                }
                finally
                {
                    goto Label_001F;
                   
                Label_001F:
                    Monitor.Exit(obj2);
                }
            }

            public override int Capacity
            {
                get
                {
                    int num;
                    lock (this._root)
                    {
                        goto Label_0012;
                       
                    Label_0012:
                        num = this._collection.Capacity;
                    }
                    return num;
                }
                set
                {
                    lock (this._root)
                    {
                        goto Label_0012;
                       
                    Label_0012:
                        this._collection.Capacity = value;
                    }
                }
            }

            public override int Count
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    lock (this._root)
                    {
                       
                        return this._collection.Count;
                    }
                }
            }

            protected override TableSchema[] InnerArray
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    lock (this._root)
                    {
                       
                        return this._collection.InnerArray;
                    }
                }
            }

            public override bool IsFixedSize
            {
                get
                {
                    return this._collection.IsFixedSize;
                }
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this._collection.IsReadOnly;
                }
            }

            public override bool IsSynchronized
            {
                get
                {
                    return true;
                }
            }

            public override TableSchema this[int index]
            {
                get
                {
                    // This item is obfuscated and can not be translated.
                    
                    lock (this._root)
                    {
                        return this._collection[index];
                    }
                }
                set
                {
                    object obj2;
                    goto Label_0003;
                   
                Label_0003:
                    Monitor.Enter(obj2 = this._root);
                    try
                    {
                        this._collection[index] = value;
                    }
                    finally
                    {
                        Monitor.Exit(obj2);
                    }
                }
            }

            public override object SyncRoot
            {
                get
                {
                    return this._root;
                }
            }
        }

        private enum Tag
        {
            Default
        }
    }
}

