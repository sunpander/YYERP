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
    public class ViewSchemaCollection : IViewSchemaList, IList, IList<ViewSchema>, ICloneable
    {
        private ViewSchema[] _array;
        private int _count;
        private const int _defaultCapacity = 0x10;
        [NonSerialized]
        private int _version;

        public ViewSchemaCollection()
        {
            this._array = new ViewSchema[0x10];
        }

        public ViewSchemaCollection(ViewSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this._array = new ViewSchema[collection.Count];
            this.AddRange(collection);
        }

        private ViewSchemaCollection(Tag tag)
        {
        }

        public ViewSchemaCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");
            }
            this._array = new ViewSchema[capacity];
        }

        public ViewSchemaCollection(ViewSchema[] array)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            this._array = new ViewSchema[array.Length];
            this.AddRange(array);
        }

        public virtual int Add(ViewSchema value)
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

        public virtual void AddRange(ViewSchema[] array)
        {
            // This item is obfuscated and can not be translated.
            int num = 3;
        Label_0018:
            switch (num)
            {
                case 0:
                    if ((this._count + array.Length) <= this._array.Length)
                    {
                        goto Label_00C8;
                    }
                    num = 4;
                    goto Label_0018;

                case 1:
                    throw new ArgumentNullException("array");

                case 2:
                    if (array.Length != 0)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 5;
                    }
                    goto Label_0018;

                case 3:
                    
                    break;

                case 4:
                    this.EnsureCapacity(this._count + array.Length);
                    num = 6;
                    goto Label_0018;

                case 5:
                    return;

                case 6:
                    goto Label_00C8;
            }
            if (array != null)
            {
                num = 2;
            }
            else
            {
                num = 1;
            }
            goto Label_0018;
        Label_00C8:
            this._version++;
            Array.Copy(array, 0, this._array, this._count, array.Length);
            this._count += array.Length;
        }

        public virtual void AddRange(ViewSchemaCollection collection)
        {
            // This item is obfuscated and can not be translated.
            int num = 4;
        Label_0018:
            switch (num)
            {
                case 0:
                    return;

                case 1:
                    this.EnsureCapacity(this._count + collection.Count);
                    num = 2;
                    goto Label_0018;

                case 2:
                    goto Label_00D1;

                case 3:
                    if ((this._count + collection.Count) <= this._array.Length)
                    {
                        goto Label_00D1;
                    }
                    num = 1;
                    goto Label_0018;

                case 4:
                    
                    break;

                case 5:
                    throw new ArgumentNullException("collection");

                case 6:
                    if (collection.Count != 0)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_0018;
            }
            if (collection != null)
            {
                num = 6;
            }
            else
            {
                num = 5;
            }
            goto Label_0018;
        Label_00D1:
            this._version++;
            Array.Copy(collection.InnerArray, 0, this._array, this._count, collection.Count);
            this._count += collection.Count;
        }

        public virtual int BinarySearch(ViewSchema value)
        {
            return Array.BinarySearch<ViewSchema>(this._array, 0, this._count, value);
        }

        private void CheckEnumIndex(int index)
        {
            // This item is obfuscated and can not be translated.
        //    int num = 0;
        //Label_000D:
        //    switch (num)
        //    {
        //        case 1:
        //            num = 3;
        //            goto Label_000D;

        //        case 2:
        //            break;

        //        case 3:
        //            if (index < this._count)
        //            {
        //                return;
        //            } 
        //            num = 2;
        //            break;
        //        default:
        //            if (index >= 0)
        //            {
        //                num = 1; 
        //            }
        //            break;
        //    }
            if (index >= 0 && index < this._count)
            {
                return;
            }
            else
            {
                throw new InvalidOperationException("Enumerator is not on a collection element.");
            }
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
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            else
            {
                if (array.Rank <= 1)
                {
                    if (arrayIndex >= 0)
                    {
                        if (arrayIndex < array.Length)
                        {
                            if (this._count <= (array.Length - arrayIndex))
                            {
                                return;
                            }
                            else
                            {
                                throw new ArgumentException("Argument must be less than array length.", "arrayIndex");
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");
                    }
                }
                else
                {
                    throw new ArgumentException("Argument cannot be multidimensional.", "array");
                }

            }
        //    int num = 4;
        //Label_000D:
        //    switch (num)
        //    {
        //        case 0:
        //            throw new ArgumentException("Argument must be less than array length.", "arrayIndex");

        //        case 1:
        //            break;

        //        case 2:
        //            throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");

        //        case 3:
        //            if (array.Rank <= 1)
        //            {
        //                num = 6;
        //            }
        //            else
        //            {
        //                num = 9;
        //            }
        //            goto Label_000D;

        //        case 5:
        //            if (arrayIndex < array.Length)
        //            {
        //                num = 8;
        //            }
        //            else
        //            {
        //                num = 0;
        //            }
        //            goto Label_000D;

        //        case 6:
        //            if (arrayIndex >= 0)
        //            {
        //                num = 5;
        //            }
        //            else
        //            {
        //                num = 2;
        //            }
        //            goto Label_000D;

        //        case 7:
        //            throw new ArgumentException("Argument section must be large enough for collection.", "array");

        //        case 8:
        //            if (this._count <= (array.Length - arrayIndex))
        //            {
        //                return;
        //            }
        //            num = 7;
        //            goto Label_000D;

        //        case 9:
        //            throw new ArgumentException("Argument cannot be multidimensional.", "array");

        //        default:
        //            if (array == null)
        //            {
        //                num = 1;
        //                goto Label_000D;
                       
        //            }
        //            else
        //            {
        //                num = 3;
        //                goto Label_000D;
        //            }
        //            break;
        //    }
        //    throw new ArgumentNullException("array");
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
             
            ViewSchemaCollection schemas = new ViewSchemaCollection(this._count);
            Array.Copy(this._array, 0, schemas._array, 0, this._count);
            schemas._count = this._count;
            schemas._version = this._version;
            return schemas;
        }

        public virtual bool Contains(ViewSchema value)
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

        public virtual void CopyTo(ViewSchema[] array)
        {
            // This item is obfuscated and can not be translated.
           
            this.CheckTargetArray(array, 0);
            Array.Copy(this._array, array, this._count);
        }

        public virtual void CopyTo(ViewSchema[] array, int arrayIndex)
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

        public virtual IViewSchemaEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public virtual int IndexOf(ViewSchema value)
        {
            return Array.IndexOf<ViewSchema>(this._array, value, 0, this._count);
        }

        public int IndexOf(string name)
        {
            // This item is obfuscated and can not be translated.
            int num;
        Label_0023:
            num = 0;
            int num2 = 2;
        Label_0002:
            switch (num2)
            {
                case 0:
                case 2: 
                    num2 = 3;
                    goto Label_0002;

                case 1:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        num++;
                        num2 = 0;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 3:
                    if (num < this.Count)
                    {
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 5;
                    }
                    goto Label_0002;

                case 4:
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
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    if (string.Compare(this[num].Name, name, true) != 0)
                    {
                        goto Label_003D;
                    }
                    num2 = 7;
                    goto Label_0002;

                case 2:
                case 6:
                    goto Label_007D;

                case 3:
                    if (string.Compare(this[num].Owner, owner, true) != 0)
                    {
                        goto Label_003D;
                    }
                    num2 = 0;
                    goto Label_0002;

                case 4:
                    return -1;

                case 5:
                    if (num < this.Count)
                    {
                        num2 = 3;
                    }
                    else
                    {
                        num2 = 4;
                    }
                    goto Label_0002;

                case 7:
                    return num;
            }
        Label_002B:
            num = 0;
            num2 = 2;
            goto Label_0002;
           
        Label_003D:
            num++;
            num2 = 6;
            goto Label_0002;
        Label_007D:
            num2 = 5;
            goto Label_0002;
        }

        public virtual void Insert(int index, ViewSchema value)
        {
            // This item is obfuscated and can not be translated.
            int num = 1;
        Label_000D:
            switch (num)
            {
                case 0:
                    if (index <= this._count)
                    {
                        num = 7;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                case 2:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");

                case 3:
                    Array.Copy(this._array, index, this._array, index + 1, this._count - index);
                    num = 5;
                    goto Label_000D;

                case 4:
                    break;

                case 5:
                    goto Label_0161;

                case 6:
                    if (index >= this._count)
                    {
                        goto Label_0161;
                    }
                    
                    num = 3;
                    goto Label_000D;

                case 7:
                    if (this._count != this._array.Length)
                    {
                        break;
                    }
                    num = 8;
                    goto Label_000D;

                case 8:
                    this.EnsureCapacity(this._count + 1);
                    num = 4;
                    goto Label_000D;

                case 9:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

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
            this._version++;
            num = 6;
            goto Label_000D;
        Label_0161:
            this._array[index] = value;
            this._count++;
        }

        public static ViewSchemaCollection ReadOnly(ViewSchemaCollection collection)
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
            IViewSchemaEnumerator enumerator = this.GetEnumerator();
            try
            {
                
                int num = 4;
            Label_0021:
                switch (num)
                {
                    case 0:
                        if (enumerator.MoveNext())
                        {
                            break;
                        }
                        num = 2;
                        goto Label_0021;

                    case 1:
                        return;

                    case 2:
                        num = 1;
                        goto Label_0021;

                    default:
                        num = 0;
                        goto Label_0021;
                }
                enumerator.Current.Refresh();
                num = 3;
                goto Label_0021;
            }
            finally
            {
                IDisposable disposable;
                int num2;
                goto Label_009E;
            Label_0089:
                switch (num2)
                {
                    case 0:
                        disposable.Dispose();
                        num2 = 1;
                        goto Label_0089;

                    case 1:
                        goto Label_00D3;

                    case 2:
                        if (disposable == null)
                        {
                            goto Label_00D3;
                        }
                        num2 = 0;
                        goto Label_0089;
                }
            Label_009E:
                disposable = enumerator as IDisposable;
                num2 = 2;
                goto Label_0089;
            Label_00D3:;
            }
        }

        public virtual void Remove(ViewSchema value)
        {
            // This item is obfuscated and can not be translated.
            int num;
        Label_0022:
            num = this.IndexOf(value);
            int num2 = 2;
        Label_0002:
            
            switch (num2)
            {
                case 0:
                    this.RemoveAt(num);
                    num2 = 1;
                    goto Label_0002;

                case 1:
                    break;

                case 2:
                    if (num < 0)
                    {
                        break;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_0022;
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
            int num = 3;
        Label_000D:
            switch (num)
            {
                case 0:
                    break;

                case 1:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 2:
                    return;

                case 4:
                    Array.Copy(this._array, index + count, this._array, index, this._count - index);
                    num = 6;
                    goto Label_000D;

                case 5:
                    if (index >= this._count)
                    {
                        goto Label_0173;
                    }
                    num = 4;
                    goto Label_000D;

                case 6:
                    goto Label_0173;

                case 7:
                    if ((index + count) <= this._count)
                    {
                        num = 10;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;

                case 8:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 9:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 10:
                    if (count != 0)
                    {
                        this._version++;
                        this._count -= count;
                        num = 5;
                    }
                    else
                    {
                        num = 2;
                    }
                    goto Label_000D;

                default:
                    if (index < 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    goto Label_000D;
                   
            }
            if (count >= 0)
            {
                num = 7;
            }
            else
            {
                num = 8;
            }
            goto Label_000D;
        Label_0173:
            Array.Clear(this._array, this._count, count);
        }

        public virtual void Reverse()
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (0 <= (0 + 7))
                {
                    return;
                }
                
            }
            this._version++;
            Array.Reverse(this._array, 0, this._count);
        }

        public virtual void Reverse(int index, int count)
        {
            int num = 4;
        Label_000D:
            switch (num)
            {
                case 0:
                    num = 3;
                    goto Label_000D;

                case 1:
                    return;

                case 2:
                    if ((index + count) <= this._count)
                    {
                        num = 5;
                    }
                    else
                    {
                        num = 7;
                    }
                    goto Label_000D;

                case 3:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Reverse(this._array, index, count);
                        return;
                    }
                    num = 1;
                    goto Label_000D;

                case 5:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 0;
                    goto Label_000D;

                case 6:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 7:
                    throw new ArgumentException("Arguments denote invalid range of elements.");

                case 8:
                    if (count >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 6;
                    }
                    goto Label_000D;

                case 9:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
            if (index < 0)
            {
                num = 9;
                goto Label_000D;
               
            }
            num = 8;
            goto Label_000D;
        }

        public virtual void Sort()
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (6 >= (6 - 0))
                {
                    return;
                }
                
            }
            this._version++;
            Array.Sort<ViewSchema>(this._array, 0, this._count);
        }

        public virtual void Sort(IComparer comparer)
        {
            // This item is obfuscated and can not be translated.
            if (this._count <= 1)
            {
                if (0 <= (0 + 4))
                {
                    return;
                }
               
            }
            this._version++;
            Array.Sort(this._array, 0, this._count, comparer);
        }

        public virtual void Sort(int index, int count, IComparer comparer)
        {
            // This item is obfuscated and can not be translated.
            int num = 5;
        Label_000D:
           
            switch (num)
            {
                case 0:
                    if (count <= 1)
                    {
                        return;
                    }
                    num = 7;
                    goto Label_000D;

                case 1:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");

                case 2:
                    if ((index + count) <= this._count)
                    {
                        num = 0;
                    }
                    else
                    {
                        num = 9;
                    }
                    goto Label_000D;

                case 3:
                    throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");

                case 4:
                    return;

                case 6:
                    if (this._count > 1)
                    {
                        this._version++;
                        Array.Sort(this._array, index, count, comparer);
                        return;
                    }
                    num = 4;
                    goto Label_000D;

                case 7:
                    num = 6;
                    goto Label_000D;

                case 8:
                    if (count >= 0)
                    {
                        num = 2;
                    }
                    else
                    {
                        num = 3;
                    }
                    goto Label_000D;

                case 9:
                    throw new ArgumentException("Arguments denote invalid range of elements.");
            }
            if (index >= 0)
            {
                num = 8;
            }
            else
            {
                num = 1;
            }
            goto Label_000D;
        }

        public static ViewSchemaCollection Synchronized(ViewSchemaCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            return new SyncList(collection);
        }

        void ICollection<ViewSchema>.Add(ViewSchema item)
        {
            this.Add(item);
        }

        bool ICollection<ViewSchema>.Remove(ViewSchema item)
        {
            if (this.Contains(item.Name))
            {
                this.Remove(item);
                return true;
               
            }
            return false;
        }

        IEnumerator<ViewSchema> IEnumerable<ViewSchema>.GetEnumerator()
        {
            return new List<ViewSchema>(this.ToArray()).GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.CopyTo((ViewSchema[]) array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            return this.Add((ViewSchema) value);
        }

        bool IList.Contains(object value)
        {
            return this.Contains((ViewSchema) value);
        }

        int IList.IndexOf(object value)
        {
            return this.IndexOf((ViewSchema) value);
        }

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (ViewSchema) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((ViewSchema) value);
        }

        public virtual ViewSchema[] ToArray()
        {
            // This item is obfuscated and can not be translated.
            
            ViewSchema[] destinationArray = new ViewSchema[this._count];
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
                    builder.Append(", ");
                    num2 = 4;
                    goto Label_0002;

                case 1:
                case 5:
                    num2 = 2;
                    goto Label_0002;

                case 2:
                    if (num < this.Count)
                    {
                        builder.AppendFormat("\"{0}\"", this[num].ToString().Replace("\"", "\"\""));
                        num2 = 6;
                    }
                    else
                    {
                        num2 = 3;
                    }
                    goto Label_0002;

                case 3:
                    return builder.ToString();

                case 4:
                    goto Label_003F;

                case 6:
                    if (num >= (this.Count - 1))
                    {
                        goto Label_003F;
                    }
                    num2 = 0;
                    goto Label_0002;

                default:
                    goto Label_002A;
                   
            }
        Label_002A:
            builder = new StringBuilder();
            num = 0;
            num2 = 1;
            goto Label_0002;
        Label_003F:
            num++;
            num2 = 5;
            goto Label_0002;
        }

        public virtual void TrimToSize()
        {
            this.Capacity = this._count;
        }

        private void ValidateIndex(int index)
        {
            // This item is obfuscated and can not be translated.
            int num = 3;
        Label_000D:
            switch (num)
            {
                case 0:
                    throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");

                case 1:
                    if (index < this._count)
                    {
                        return;
                    }
                    num = 0;
                    goto Label_000D;

                case 2:
                    throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
            }
           
            if (index < 0)
            {
                num = 2;
            }
            else
            {
                num = 1;
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
                int num = 0;
            Label_000D:
                switch (num)
                {
                    case 1:
                        if (value >= this._count)
                        {
                            num = 5;
                        }
                        else
                        {
                            num = 4;
                        }
                        goto Label_000D;

                    case 2:
                        this._array = new ViewSchema[0x10];
                        return;

                    case 3:
                        return;

                    case 4:
                        throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

                    case 5:
                        if (value != 0)
                        {
                            ViewSchema[] destinationArray = new ViewSchema[value];
                            Array.Copy(this._array, destinationArray, this._count);
                            this._array = destinationArray;
                            return;
                        }
                        num = 2;
                        goto Label_000D;

                    default:
                        if (value != this._array.Length)
                        {
                            num = 1;
                            goto Label_000D;
                        }
                        break;
                       
                }
                num = 3;
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

        protected virtual ViewSchema[] InnerArray
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

        public ViewSchema this[string owner, string name]
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

        public ViewSchema this[string name]
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

        public virtual ViewSchema this[int index]
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
                this[index] = (ViewSchema) value;
            }
        }

        [Serializable]
        private sealed class Enumerator : IViewSchemaEnumerator, IEnumerator
        {
            private readonly ViewSchemaCollection _collection;
            private int _index;
            private readonly int _version;

            internal Enumerator(ViewSchemaCollection collection)
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

            public ViewSchema Current
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
        private sealed class ReadOnlyList : ViewSchemaCollection
        {
            private ViewSchemaCollection _collection;

            internal ReadOnlyList(ViewSchemaCollection collection) : base(ViewSchemaCollection.Tag.Default)
            {
                this._collection = collection;
            }

            public override int Add(ViewSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(ViewSchemaCollection collection)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void AddRange(ViewSchema[] array)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override int BinarySearch(ViewSchema value)
            {
                return this._collection.BinarySearch(value);
            }

            public override void Clear()
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override object Clone()
            {
                return new ViewSchemaCollection.ReadOnlyList((ViewSchemaCollection) this._collection.Clone());
            }

            public override bool Contains(ViewSchema value)
            {
                return this._collection.Contains(value);
            }

            public override void CopyTo(ViewSchema[] array)
            {
                this._collection.CopyTo(array);
            }

            public override void CopyTo(ViewSchema[] array, int arrayIndex)
            {
                this._collection.CopyTo(array, arrayIndex);
            }

            public override IViewSchemaEnumerator GetEnumerator()
            {
                return this._collection.GetEnumerator();
            }

            public override int IndexOf(ViewSchema value)
            {
                return this._collection.IndexOf(value);
            }

            public override void Insert(int index, ViewSchema value)
            {
                throw new NotSupportedException("Read-only collections cannot be modified.");
            }

            public override void Remove(ViewSchema value)
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

            public override ViewSchema[] ToArray()
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

            protected override ViewSchema[] InnerArray
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

            public override ViewSchema this[int index]
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
        private sealed class SyncList : ViewSchemaCollection
        {
            private ViewSchemaCollection _collection;
            private object _root;

            internal SyncList(ViewSchemaCollection collection) : base(ViewSchemaCollection.Tag.Default)
            {
                this._root = collection.SyncRoot;
                this._collection = collection;
            }

            public override int Add(ViewSchema value)
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

            public override void AddRange(ViewSchemaCollection collection)
            {
                lock (this._root)
                {
                    this._collection.AddRange(collection);
                }
                return;
               
            }

            public override void AddRange(ViewSchema[] array)
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

            public override int BinarySearch(ViewSchema value)
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
                    return new ViewSchemaCollection.SyncList((ViewSchemaCollection) this._collection.Clone());
                }
               
                //return obj2;
            }

            public override bool Contains(ViewSchema value)
            {
                // This item is obfuscated and can not be translated.
                 
                lock (this._root)
                {
                    return this._collection.Contains(value);
                }
            }

            public override void CopyTo(ViewSchema[] array)
            {
                // This item is obfuscated and can not be translated.
               
                lock (this._root)
                {
                    this._collection.CopyTo(array);
                }
            }

            public override void CopyTo(ViewSchema[] array, int arrayIndex)
            {
                lock (this._root)
                {
                    goto Label_0012;
                   
                Label_0012:
                    this._collection.CopyTo(array, arrayIndex);
                }
            }

            public override IViewSchemaEnumerator GetEnumerator()
            {
                IViewSchemaEnumerator enumerator;
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

            public override int IndexOf(ViewSchema value)
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

            public override void Insert(int index, ViewSchema value)
            {
                lock (this._root)
                {
                    this._collection.Insert(index, value);
                }
                return;
               
            }

            public override void Remove(ViewSchema value)
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

            public override ViewSchema[] ToArray()
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

            protected override ViewSchema[] InnerArray
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

            public override ViewSchema this[int index]
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

