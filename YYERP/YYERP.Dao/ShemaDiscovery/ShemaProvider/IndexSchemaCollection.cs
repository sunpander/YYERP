namespace SchemaExplorer
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    [Serializable]
    public class IndexSchemaCollection : IIndexSchemaList, IList, IList<IndexSchema>, ICloneable
    {
        private IndexSchema[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        public IndexSchemaCollection()
        {
            this._array = new IndexSchema[0x10];
        }

        public IndexSchemaCollection(IndexSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new IndexSchema[collection.Count];
            this.AddRange(collection);
        }

        private IndexSchemaCollection(Tag tag)
        {
        }

        public IndexSchemaCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new IndexSchema[capacity];
        }

        public IndexSchemaCollection(IndexSchema[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this._array = new IndexSchema[array.Length];
            this.AddRange(array);
        }

        public virtual int Add(IndexSchema value)
        {
            int num2 = 0;
        Label_000D:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    goto Label_005D;

                case 2:
                    this.EnsureCapacity(this._count + 1);
                    num2 = 1;
                    goto Label_000D;

                default:
                    break;
                   
            }
            if (this._count == this._array.Length)
            {
                num2 = 2;
                goto Label_000D;
            }
        Label_005D:
            this._version++;
            this._array[this._count] = value;
            return this._count++;
        }

        public virtual void AddRange(IndexSchemaCollection collection)
        {
            int num = 6;
        Label_000D:
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
                    goto Label_000D;

                case 1:
                    if ((this._count + collection.Count) <= this._array.Length)
                    {
                        goto Label_00C9;
                    }
                    num = 2;
                    goto Label_000D;

                case 2:
                    this.EnsureCapacity(this._count + collection.Count);
                    num = 5;
                    goto Label_000D;

                case 3:
                    return;

                case 4:
                    break;

                case 5:
                    goto Label_00C9;

                default:
                    if (collection != null)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentNullException("collection");
        Label_00C9:
            this._version++;
            Array.Copy(collection.InnerArray, 0, this._array, this._count, collection.Count);
            this._count += collection.Count;
        }

        public virtual void AddRange(IndexSchema[] array)
        {
            // This item is obfuscated and can not be translated.
            int num = 3;
        Label_0018:
            switch (num)
            {
                case 0:
                    throw new ArgumentNullException("array");

                case 1:
                    this.EnsureCapacity(this._count + array.Length);
                    num = 2;
                    goto Label_0018;

                case 2:
                    goto Label_00C8;

                case 3:
                    
                    break;

                case 4:
                    return;

                case 5:
                    if ((this._count + array.Length) <= this._array.Length)
                    {
                        goto Label_00C8;
                    }
                    num = 1;
                    goto Label_0018;

                case 6:
                    if (array.Length != 0)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_0018;
            }
            if (array != null)
            {
                num = 6;
            }
            else
            {
                num = 0;
            }
            goto Label_0018;
        Label_00C8:
            this._version++;
            Array.Copy(array, 0, this._array, this._count, array.Length);
            this._count += array.Length;
        }

        public virtual int BinarySearch(IndexSchema value)
        {
            return Array.BinarySearch<IndexSchema>(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            // This item is obfuscated and can not be translated.
            int num = 0;
        Label_000D:
            switch (num)
            {
                case 1:
                    
                    break;

                case 2:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 1;
                    goto Label_000D;

                case 3:
                    num = 2;
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 3;
                        goto Label_000D;
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
            int num = 0;
        Label_000D:
            switch (num)
            {
                case 1:
                    throw new ArgumentException("Argument must be less than array length.", "arrayIndex");

                case 2:
                    throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");

                case 3:
                    if (this._count <= (array.Length - arrayIndex))
                    {
                        return;
                    }
                    num = 9;
                    goto Label_000D;

                case 4:
                    break;

                case 5:
                    throw new ArgumentException("Argument cannot be multidimensional.", "array");

                case 6:
                    if (array.Rank <= 1)
                    {
                        num = 8;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;

                case 7:
                    if (arrayIndex < array.Length)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;

                case 8:
                    if (arrayIndex >= 0)
                    {
                        num = 7;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 9:
                    throw new ArgumentException("Argument section must be large enough for collection.", "array");

                default:
                    if (array == null)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentNullException("array");
        }

        public virtual void Clear()
        {
            goto Label_0003;
           
        Label_0003:
            if (this._count == 0)
            {
                return;
            }
            this._version++;
            Array.Clear(this._array, 0, this._count);
            this._count = 0;
        }

        public virtual object Clone()
        {
            IndexSchemaCollection schemas;
            goto Label_0003;
           
        Label_0003:
            schemas = new IndexSchemaCollection(this._count);
            Array.Copy(this._array, 0, schemas._array, 0, this._count);
            schemas._count = this._count;
            schemas._version = this._version;
            return schemas;
        }

        public virtual bool Contains(IndexSchema value)
        {
            return (this.IndexOf(value) >= 0);
        }

        public bool Contains(string name)
        {
            return (this.IndexOf(name) >= 0);
        }

        public virtual void CopyTo(IndexSchema[] array)
        {
            
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(IndexSchema[] array, int arrayIndex)
        {
            // This item is obfuscated and can not be translated.
            
            this.CheckTargetArray(array, arrayIndex);
            Array.Copy(this._array, 0, array, arrayIndex, this._count);
        }

        private void EnsureCapacity(int minimum)
        {
            // This item is obfuscated and can not be translated.
        }

        public virtual IIndexSchemaEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int IndexOf(IndexSchema value)
        {
            return Array.IndexOf<IndexSchema>(this._array, value, 0, this._count);
        }

        public int IndexOf(string name)
        {
            int num;
            goto Label_0026;
           
        Label_0026:
            num = 0;
            int num2 = 0;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 3:
                    num2 = 4;
                    goto Label_0002;

                case 1:
                    return num;

                case 2:
                    return -1;

                case 4:
                    if (num < this.Count)
                    {
                        num2 = 5;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 5:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        num++;
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 1;
                    }
                    goto Label_0002;
            }
            goto Label_0026;
        }

        public virtual void Insert(int index, IndexSchema value)
        {
            // This item is obfuscated and can not be translated.
            int num = 7;
        Label_000D:
            switch (num)
            {
                case 0:
                    this.EnsureCapacity(this._count + 1);
                    num = 9;
                    goto Label_000D;

                case 1:
                    if (index <= this._count)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;

                case 2:
                    Array.Copy(this._array, index, this._array, index + 1, this._count - index);
                    num = 4;
                    goto Label_000D;

                case 3:
                    if (index >= this._count)
                    {
                        goto Label_0161;
                    }
                    num = 2;
                    goto Label_000D;

                case 4:
                    goto Label_0161;

                case 5:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 6:
                    if (this._count != this._array.Length)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_000D;

                case 8:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");

                case 9:
                    break;

                default:
                    if (index >= 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;
            }
            
            this._version++;
            num = 3;
            goto Label_000D;
        Label_0161:
            this._array[index] = value;
            this._count++;
        }

        public static IndexSchemaCollection ReadOnly(IndexSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new ReadOnlyList(collection);
        }

        public virtual void Remove(IndexSchema value)
        {
            int num;
            goto Label_001A;
           
        Label_001A:
            num = this.IndexOf(value);
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                    break;

                case 1:
                    if (num < 0)
                    {
                        break;
                    }
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    this.RemoveAt(num);
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_001A;
            }
        }

        public virtual void RemoveAt(int index)
        {
            // This item is obfuscated and can not be translated.
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
            int num = 4;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (count != 0)
                    {
                        break;
                    }
                    num = 7;
                    goto Label_000D;

                case 1:
                    if (index >= this._count)
                    {
                        goto Label_0173;
                    }
                    num = 6;
                    goto Label_000D;

                case 2:
                    if (count >= 0)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_000D;

                case 3:
                    if ((index + count) <= this._count)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;

                case 5:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 6:
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                    num = 9;
                    goto Label_000D;

                case 7:
                    return;

                case 8:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 9:
                    goto Label_0173;

                case 10:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                default:
                    if (index < 0)
                    {
                        num = 10;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;
                   
            }
            this._version++;
            this._count -= count;
            num = 1;
            goto Label_000D;
        Label_0173:
            Array.Clear(this._array, this._count, count);
        }

        public virtual void Reverse()
        {
            // This item is obfuscated and can not be translated.
            if (this._count > 1)
            {
                
                this._version++;
                Array.Reverse(this._array, 0, this._count);
            }
        }

        public virtual void Reverse(int index, int count)
        {
            int num = 6;
        Label_000D:
            switch (num)
            {
                case 0:
                    return;

                case 1:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 2:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 9;
                    goto Label_000D;

                case 3:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 4:
                    if (count >= 0)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;

                case 5:
                    if ((index + count) <= this._count)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                case 7:
                    if (this._count > 1)
                    {
                        break;
                    }
                    num = 0;
                    goto Label_000D;

                case 8:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 9:
                    num = 7;
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;
                   
            }
            this._version++;
            Array.Reverse(this._array, index, count);
        }

        public virtual void Sort()
        {
            goto Label_0003;
           
        Label_0003:
            if (this._count <= 1)
            {
                return;
            }
            this._version++;
            Array.Sort<IndexSchema>(this._array, 0, this._count);
        }

        public virtual void Sort(IComparer comparer)
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
            Array.Sort(this._array, 0, this._count, comparer);
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            int num = 8;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 1:
                    if ((index + count) <= this._count)
                    {
                        num = 6;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;

                case 2:
                    break;

                case 3:
                    num = 9;
                    goto Label_000D;

                case 4:
                    return;

                case 5:
                    if (count >= 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 6:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 3;
                    goto Label_000D;

                case 7:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 9:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Sort(this._array, index, count, comparer);
                        return;
                    }
                    num = 4;
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
        }

        public static IndexSchemaCollection Synchronized(IndexSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection<IndexSchema>.Add(IndexSchema item)
        {
            this.Add(item);
        }

        bool ICollection<IndexSchema>.Remove(IndexSchema item)
        {
            if (this.Contains(item.Name))
            {
                this.Remove(item);
                return true;
               
            }
            return false;
        }

        IEnumerator<IndexSchema> IEnumerable<IndexSchema>.GetEnumerator()
        {
            return new List<IndexSchema>(this.ToArray()).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((IndexSchema[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((IndexSchema) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((IndexSchema) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((IndexSchema) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (IndexSchema) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((IndexSchema) value);
        }

        public virtual IndexSchema[] ToArray()
        {
            
            IndexSchema[] destinationArray = new IndexSchema[this._count];
            Array.Copy(this._array, destinationArray, this._count);
            return destinationArray;
        }

        public override string ToString()
        {
            // This item is obfuscated and can not be translated.
            StringBuilder builder;
            int num;
            int num2;
            goto Label_0032;
        Label_0002:
            
            switch (num2)
            {
                case 0:
                    if (num >= (this.Count - 1))
                    {
                        goto Label_0047;
                    }
                    num2 = 4;
                    goto Label_0002;

                case 1:
                case 3:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (num < this.Count)
                    {
                        builder.AppendFormat("\"{0}\"", this[num].ToString().Replace("\"", "\"\""));
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 6;
                    }
                    goto Label_0002;

                case 4:
                    builder.Append(", ");
                    num2 = 5;
                    goto Label_0002;

                case 5:
                    goto Label_0047;

                case 6:
                    return builder.ToString();
            }
        Label_0032:
            builder = new StringBuilder();
            num = 0;
            num2 = 3;
            goto Label_0002;
        Label_0047:
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
            int num = 0;
        Label_0018:
            switch (num)
            {
                case 0:
                   
                    break;

                case 1:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 3;
                    goto Label_0018;

                case 2:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 3:
                    throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");
            }
            if (index < 0)
            {
                num = 2;
            }
            else
            {
                num = 1;
            }
            goto Label_0018;
        }

        public virtual int Capacity
        {
            get
            {
                return this._array.Length;
            }
            set
            {
                int num = 0;
            Label_000D:
                switch (num)
                {
                    case 1:
                        return;

                    case 2:
                        this._array = new IndexSchema[0x10];
                        return;

                    case 3:
                        if (value >= this._count)
                        {
                            num = 4;
                        }
                        else
                        {
                            num = 5;
                        }
                        goto Label_000D;

                    case 4:
                        if (value != 0)
                        {
                            IndexSchema[] destinationArray = new IndexSchema[value];
                            Array.Copy(this._array, destinationArray, this._count);
                            this._array = destinationArray;
                            return;
                        }
                        num = 2;
                        goto Label_000D;

                    case 5:
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

                    default:
                        if (value != this._array.Length)
                        {
                            num = 3;
                            goto Label_000D;
                        }
                        break;
                       
                }
                num = 1;
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

        protected virtual IndexSchema[] InnerArray
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

        public IndexSchema this[string name]
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

        public virtual IndexSchema this[int index]
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
                this[index] = (IndexSchema) value;
            }
        }

        [Serializable]
        private sealed class Enumerator : IIndexSchemaEnumerator, IEnumerator
        {
            private readonly IndexSchemaCollection _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(IndexSchemaCollection collection)
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

            public IndexSchema Current
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
        private sealed class ReadOnlyList : IndexSchemaCollection
        {
            private IndexSchemaCollection _collection;

            internal ReadOnlyList(IndexSchemaCollection collection) : base(IndexSchemaCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(IndexSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(IndexSchemaCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(IndexSchema[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(IndexSchema value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new IndexSchemaCollection.ReadOnlyList((IndexSchemaCollection) this._collection.Clone());
            }

            public override bool Contains(IndexSchema value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(IndexSchema[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(IndexSchema[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override IIndexSchemaEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(IndexSchema value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, IndexSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(IndexSchema value)
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

            public override IndexSchema[] ToArray()
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

            protected override IndexSchema[] InnerArray
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

            public override IndexSchema this[int index]
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
        private sealed class SyncList : IndexSchemaCollection
        {
            private IndexSchemaCollection _collection;
            private object _root;

            internal SyncList(IndexSchemaCollection collection) : base(IndexSchemaCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(IndexSchema value)
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

            public override void AddRange(IndexSchemaCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
                return;
               
            }

            public override void AddRange(IndexSchema[] array)
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

            public override int BinarySearch(IndexSchema value)
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
                    return new IndexSchemaCollection.SyncList((IndexSchemaCollection) this._collection.Clone());
                }
               
                //return obj2;
            }

            public override bool Contains(IndexSchema value)
            {
               
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(IndexSchema[] array)
            {
                
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(IndexSchema[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override IIndexSchemaEnumerator GetEnumerator()
            {
                IIndexSchemaEnumerator enumerator;
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

            public override int IndexOf(IndexSchema value)
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

            public override void Insert(int index, IndexSchema value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
                return;
               
            }

            public override void Remove(IndexSchema value)
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

            public override IndexSchema[] ToArray()
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

            protected override IndexSchema[] InnerArray
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

            public override IndexSchema this[int index]
            {
                get
                {
                    
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

