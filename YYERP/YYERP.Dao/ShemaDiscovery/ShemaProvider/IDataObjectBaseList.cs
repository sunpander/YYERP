namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IDataObjectBaseList : IDataObjectBaseCollection
    {
        int Add(DataObjectBase value);
        void Clear();
        bool Contains(DataObjectBase value);
        bool Contains(string name);
        int IndexOf(DataObjectBase value);
        int IndexOf(string name);
        void Insert(int index, DataObjectBase value);
        void Remove(DataObjectBase value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        DataObjectBase this[string name] { get; }

        DataObjectBase this[int index] { get; set; }
    }
}

