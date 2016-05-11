namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface ITableKeySchemaList : ITableKeySchemaCollection
    {
        int Add(TableKeySchema value);
        void Clear();
        bool Contains(TableKeySchema value);
        bool Contains(string name);
        int IndexOf(TableKeySchema value);
        int IndexOf(string name);
        void Insert(int index, TableKeySchema value);
        void Remove(TableKeySchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        TableKeySchema this[string name] { get; }

        TableKeySchema this[int index] { get; set; }
    }
}

