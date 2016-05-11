namespace SchemaExplorer
{
     
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    [Serializable]
    public class ExtendedPropertyCollection : IExtendedPropertyList, IList, IList<ExtendedProperty>, ICloneable
    {
        private ExtendedProperty[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        public ExtendedPropertyCollection()
        {
            this._array = new ExtendedProperty[0x10];
        }

        public ExtendedPropertyCollection(ExtendedPropertyCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new ExtendedProperty[collection.Count];
            this.AddRange(collection);
        }

        private ExtendedPropertyCollection(Tag tag)
        {
        }

        public ExtendedPropertyCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new ExtendedProperty[capacity];
        }

        public ExtendedPropertyCollection(ExtendedProperty[] array)
        {
            if (array == null)
            {
                this._array = new ExtendedProperty[0x10];
            }
            else
            {
                this._array = new ExtendedProperty[array.Length];
                this.AddRange(array);
            }
        }

        public virtual int Add(ExtendedProperty value)
        {
            int num2 = 2;
        Label_000D:
            switch (num2)
            {
                case 0:
                    goto Label_005D;

                case 1:
                    this.EnsureCapacity(this._count + 1);
                    num2 = 0;
                    goto Label_000D;

                case 2:
                    break;

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

        public virtual void AddRange(ExtendedPropertyCollection collection)
        {
            // This item is obfuscated and can not be translated.
            int num = 4;
        Label_0018:
            switch (num)
            {
                case 0:
                    if ((this._count + collection.Count) <= this._array.Length)
                    {
                        goto Label_00D1;
                    }
                    num = 2;
                    goto Label_0018;

                case 1:
                    throw new ArgumentNullException("collection");

                case 2:
                    this.EnsureCapacity(this._count + collection.Count);
                    num = 6;
                    goto Label_0018;

                case 3:
                    if (collection.Count != 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_0018;

                case 4:
                    
                    break;

                case 5:
                    return;

                case 6:
                    goto Label_00D1;
            }
            if (collection != null)
            {
                num = 3;
            }
            else
            {
                num = 1;
            }
            goto Label_0018;
        Label_00D1:
            this._version++;
            Array.Copy(collection.InnerArray, 0, this._array, this._count, collection.Count);
            this._count += collection.Count;
        }

        public virtual void AddRange(ExtendedProperty[] array)
        {
            // This item is obfuscated and can not be translated.
            int num = 5;
        Label_0018:
            switch (num)
            {
                case 0:
                    goto Label_00C8;

                case 1:
                    this.EnsureCapacity(this._count + array.Length);
                    num = 0;
                    goto Label_0018;

                case 2:
                    throw new ArgumentNullException("array");

                case 3:
                    return;

                case 4:
                    if ((this._count + array.Length) <= this._array.Length)
                    {
                        goto Label_00C8;
                    }
                    num = 1;
                    goto Label_0018;

                case 5:
                    
                    break;

                case 6:
                    if (array.Length != 0)
                    {
                        num = 4;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_0018;
            }
            if (array != null)
            {
                num = 6;
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

        public virtual int BinarySearch(ExtendedProperty value)
        {
            return Array.BinarySearch<ExtendedProperty>(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    goto Label_0045;

                case 2:
                    break;

                case 3:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 2;
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 0;
                        goto Label_000D;
                    }
                    break;
            }
            throw new InvalidOperationException("Enumerator is not on a collection element.");
           
        Label_0045:
            num = 3;
            goto Label_000D;
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
                    throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");

                case 1:
                    throw new ArgumentException("Argument section must be large enough for collection.", "array");

                case 2:
                    throw new ArgumentNullException("array");

                case 3:
                    throw new ArgumentException("Argument cannot be multidimensional.", "array");

                case 4:
                    throw new ArgumentException("Argument must be less than array length.", "arrayIndex");

                case 5:
                    break;

                case 6:
                    if (array.Rank <= 1)
                    {
                        num = 7;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                case 7:
                    if (arrayIndex >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                case 8:
                    if (this._count <= (array.Length - arrayIndex))
                    {
                        return;
                    }
                    num = 1;
                    goto Label_000D;

                case 9:
                    if (arrayIndex < array.Length)
                    {
                        num = 8;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_000D;

                default:
                    break;
                   
            }
            if (array == null)
            {
                num = 2;
            }
            else
            {
                num = 6;
            }
            goto Label_000D;
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
           
            ExtendedPropertyCollection propertys = new ExtendedPropertyCollection(this._count);
            Array.Copy(this._array, 0, propertys._array, 0, this._count);
            propertys._count = this._count;
            propertys._version = this._version;
            return propertys;
        }

        public virtual bool Contains(ExtendedProperty value)
        {
            return (this.IndexOf(value) >= 0);
        }

        public bool Contains(string name)
        {
            return (this.IndexOf(name) >= 0);
        }

        public virtual void CopyTo(ExtendedProperty[] array)
        {
            // This item is obfuscated and can not be translated.
           
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(ExtendedProperty[] array, int arrayIndex)
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

        public virtual IExtendedPropertyEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int IndexOf(ExtendedProperty value)
        {
            return Array.IndexOf<ExtendedProperty>(this._array, value, 0, this._count);
        }

        public int IndexOf(string name)
        {
            // This item is obfuscated and can not be translated.
            int num;
        Label_0023:
            num = 0;
            int num2 = 1;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 1:
                    
                    num2 = 4;
                    goto Label_0002;

                case 2:
                    return -1;

                case 3:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 4:
                    if (num < this.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 2;
                    }
                    goto Label_0002;

                case 5:
                    return num;
            }
            goto Label_0023;
        }

        public virtual void Insert(int index, ExtendedProperty value)
        {
            int num = 7;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");

                case 1:
                    break;

                case 2:
                    goto Label_0153;

                case 3:
                    if (index >= this._count)
                    {
                        goto Label_0153;
                    }
                    num = 6;
                    goto Label_000D;

                case 4:
                    goto Label_0108;

                case 5:
                    if (this._count != this._array.Length)
                    {
                        goto Label_0108;
                    }
                    num = 1;
                    goto Label_000D;

                case 6:
                    Array.Copy(this._array, index, this._array, index + 1, this._count - index);
                    num = 2;
                    goto Label_000D;

                case 8:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 9:
                    if (index <= this._count)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                default:
                    if (index >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 8;
                    }
                    goto Label_000D;
                   
            }
            this.EnsureCapacity(this._count + 1);
            num = 4;
            goto Label_000D;
        Label_0108:
            this._version++;
            num = 3;
            goto Label_000D;
        Label_0153:
            this._array[index] = value;
            this._count++;
        }

        public static ExtendedPropertyCollection ReadOnly(ExtendedPropertyCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new ReadOnlyList(collection);
        }

        public virtual void Remove(ExtendedProperty value)
        {
            // This item is obfuscated and can not be translated.
            int num;
        Label_0017:
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
                    goto Label_0017;
            }
        }

        public virtual void RemoveAt(int index)
        {
            int num2;
            goto Label_0017;
        Label_0002:
            switch (num2)
            {
                case 0:
                    Array.Copy(this._array, index + 1, this._array, index, this._count - index);
                    num2 = 2;
                    goto Label_0002;

                case 1:
                    goto Label_003A;

                case 2:
                    goto Label_0088;
            }
        Label_0017:
            this.ValidateIndex(index);
            this._version++;
            num2 = 1;
            goto Label_0002;
           
        Label_003A:
            if (index < --this._count)
            {
                num2 = 0;
                goto Label_0002;
            }
        Label_0088:
            this._array[this._count] = null;
        }

        public virtual void RemoveRange(int index, int count)
        {
            // This item is obfuscated and can not be translated.
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    return;

                case 2:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 3:
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                    
                    num = 6;
                    goto Label_000D;

                case 4:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 5:
                    if (index >= this._count)
                    {
                        break;
                    }
                    num = 3;
                    goto Label_000D;

                case 6:
                    break;

                case 7:
                    if (count >= 0)
                    {
                        num = 9;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 8:
                    if (count != 0)
                    {
                        this._version++;
                        this._count -= count;
                        num = 5;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                case 9:
                    if ((index + count) <= this._count)
                    {
                        num = 8;
                    }
                    else
                    {
                        num = 4;
                    }
                    goto Label_000D;

                case 10:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                default:
                    if (index < 0)
                    {
                        num = 10;
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
            if (this._count <= 1)
            {
                return;
               
            }
            this._version++;
            Array.Reverse(this._array, 0, this._count);
        }

        public virtual void Reverse(int index, int count)
        {
            int num = 5;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (count >= 0)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 1:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 4;
                    goto Label_000D;

                case 2:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 3:
                    if ((index + count) <= this._count)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;

                case 4:
                    num = 7;
                    goto Label_000D;

                case 6:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 7:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Reverse(this._array, index, count);
                        return;
                    }
                    num = 8;
                    goto Label_000D;

                case 8:
                    return;

                case 9:
                    break;

                default:
                    if (index >= 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;
                   
            }
            throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
        }

        public virtual void Sort()
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
            Array.Sort<ExtendedProperty>(this._array, 0, this._count);
        }

        public virtual void Sort(IComparer comparer)
        {
            if (this._count > 1)
            {
                this._version++;
                Array.Sort(this._array, 0, this._count, comparer);
            }
            else
            {
                return;
               
            }
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            // This item is obfuscated and can not be translated.
            int num = 6;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 1:
                    if (count >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;

                case 2:
                    if ((index + count) <= this._count)
                    {
                         
                        num = 8;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                case 3:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 4:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 5:
                    num = 7;
                    goto Label_000D;

                case 7:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Sort(this._array, index, count, comparer);
                        return;
                    }
                    num = 9;
                    goto Label_000D;

                case 8:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 5;
                    goto Label_000D;

                case 9:
                    return;
            }
            if (index >= 0)
            {
                num = 1;
            }
            else
            {
                num = 4;
            }
            goto Label_000D;
        }

        public static ExtendedPropertyCollection Synchronized(ExtendedPropertyCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection<ExtendedProperty>.Add(ExtendedProperty item)
        {
            this.Add(item);
        }

        bool ICollection<ExtendedProperty>.Remove(ExtendedProperty item)
        {
            // This item is obfuscated and can not be translated.
            if (!this.Contains(item.Name))
            {
                return false;
            } 
            this.Remove(item);
            return true;
        }

        IEnumerator<ExtendedProperty> IEnumerable<ExtendedProperty>.GetEnumerator()
        {
            return new List<ExtendedProperty>(this.ToArray()).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((ExtendedProperty[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((ExtendedProperty) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((ExtendedProperty) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((ExtendedProperty) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (ExtendedProperty) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((ExtendedProperty) value);
        }

        public virtual ExtendedProperty[] ToArray()
        {
            // This item is obfuscated and can not be translated.
            
            ExtendedProperty[] destinationArray = new ExtendedProperty[this._count];
            Array.Copy(this._array, destinationArray, this._count);
            return destinationArray;
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
                    goto Label_003F;

                case 1:
                    builder.Append(", ");
                    num2 = 0;
                    goto Label_0002;

                case 2:
                    if (num >= (this.Count - 1))
                    {
                        goto Label_003F;
                    }
                    num2 = 1;
                    goto Label_0002;

                case 3:
                    return builder.ToString();

                case 4:
                    if (num < this.Count)
                    {
                        builder.AppendFormat("\"{0}\"", this[num].ToString().Replace("\"", "\"\""));
                        num2 = 2;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 5:
                case 6:
                    num2 = 4;
                    goto Label_0002;

                default:
                    goto Label_002A;
                   
            }
        Label_002A:
            builder = new StringBuilder();
            num = 0;
            num2 = 5;
            goto Label_0002;
        Label_003F:
            num++;
            num2 = 6;
            goto Label_0002;
        }

        public virtual void TrimToSize()
        {
            this.Capacity = this._count;
        }

        private void ValidateIndex(int index)
        {
            int num = 2;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 1:
                    if (index < this._count)
                    {
                        return;
                    }
                    break;

                case 3:
                    throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");

                default:
                    if (index < 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 1;
                    }
                    goto Label_000D;
                   
            }
            num = 3;
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
                int num = 1;
            Label_000D:
                switch (num)
                {
                    case 0:
                        return;

                    case 2:
                        if (value >= this._count)
                        {
                            num = 5;
                        }
                        else
                        {
                            num = 3;
                        }
                        goto Label_000D;

                    case 3:
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

                    case 4:
                        this._array = new ExtendedProperty[0x10];
                        return;

                    case 5:
                        if (value != 0)
                        {
                            ExtendedProperty[] destinationArray = new ExtendedProperty[value];
                            Array.Copy(this._array, destinationArray, this._count);
                            this._array = destinationArray;
                            return;
                        }
                        num = 4;
                        goto Label_000D;

                    default:
                        if (value != this._array.Length)
                        {
                            num = 2;
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

        protected virtual ExtendedProperty[] InnerArray
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

        public ExtendedProperty this[string name]
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
            set
            {
                int index = this.IndexOf(name);
                if (index < 0)
                {
                    this.Add(value);
                    return;
                }
                goto Label_000F;
               
            Label_000F:;
                this[index] = value;
            }
        }

        public virtual ExtendedProperty this[int index]
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
                this[index] = (ExtendedProperty) value;
            }
        }

        [Serializable]
        private sealed class Enumerator : IExtendedPropertyEnumerator, IEnumerator
        {
            private readonly ExtendedPropertyCollection _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(ExtendedPropertyCollection collection)
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

            public ExtendedProperty Current
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
        private sealed class ReadOnlyList : ExtendedPropertyCollection
        {
            private ExtendedPropertyCollection _collection;

            internal ReadOnlyList(ExtendedPropertyCollection collection) : base(ExtendedPropertyCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(ExtendedProperty value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(ExtendedPropertyCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(ExtendedProperty[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(ExtendedProperty value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new ExtendedPropertyCollection.ReadOnlyList((ExtendedPropertyCollection) this._collection.Clone());
            }

            public override bool Contains(ExtendedProperty value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(ExtendedProperty[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(ExtendedProperty[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override IExtendedPropertyEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(ExtendedProperty value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, ExtendedProperty value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(ExtendedProperty value)
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

            public override ExtendedProperty[] ToArray()
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

            protected override ExtendedProperty[] InnerArray
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

            public override ExtendedProperty this[int index]
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
        private sealed class SyncList : ExtendedPropertyCollection
        {
            private ExtendedPropertyCollection _collection;
            private object _root;

            internal SyncList(ExtendedPropertyCollection collection) : base(ExtendedPropertyCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(ExtendedProperty value)
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

            public override void AddRange(ExtendedPropertyCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
                return;
               
            }

            public override void AddRange(ExtendedProperty[] array)
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

            public override int BinarySearch(ExtendedProperty value)
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
                    return new ExtendedPropertyCollection.SyncList((ExtendedPropertyCollection) this._collection.Clone());
                }
               
                //return obj2;
            }

            public override bool Contains(ExtendedProperty value)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(ExtendedProperty[] array)
            {
                // This item is obfuscated and can not be translated.
                
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(ExtendedProperty[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override IExtendedPropertyEnumerator GetEnumerator()
            {
                IExtendedPropertyEnumerator enumerator;
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

            public override int IndexOf(ExtendedProperty value)
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

            public override void Insert(int index, ExtendedProperty value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
                return;
               
            }

            public override void Remove(ExtendedProperty value)
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

            public override ExtendedProperty[] ToArray()
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

            protected override ExtendedProperty[] InnerArray
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

            public override ExtendedProperty this[int index]
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

