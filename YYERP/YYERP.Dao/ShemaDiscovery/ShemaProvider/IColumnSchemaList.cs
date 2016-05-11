namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IColumnSchemaList : IColumnSchemaCollection
    {
        int Add(ColumnSchema value);
        void Clear();
        bool Contains(ColumnSchema value);
        bool Contains(string name);
        int IndexOf(ColumnSchema value);
        int IndexOf(string name);
        void Insert(int index, ColumnSchema value);
        void Remove(ColumnSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        ColumnSchema this[string name] { get; }

        ColumnSchema this[int index] { get; set; }
    }
}

