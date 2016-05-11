namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface ITableSchemaList : ITableSchemaCollection
    {
        int Add(TableSchema value);
        void Clear();
        bool Contains(TableSchema value);
        bool Contains(string name);
        bool Contains(string owner, string name);
        int IndexOf(TableSchema value);
        int IndexOf(string name);
        int IndexOf(string owner, string name);
        void Insert(int index, TableSchema value);
        void Remove(TableSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        TableSchema this[string owner, string name] { get; }

        TableSchema this[string name] { get; }

        TableSchema this[int index] { get; set; }
    }
}

