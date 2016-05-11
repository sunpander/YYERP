namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IViewColumnSchemaList : IViewColumnSchemaCollection
    {
        int Add(ViewColumnSchema value);
        void Clear();
        bool Contains(ViewColumnSchema value);
        bool Contains(string name);
        int IndexOf(ViewColumnSchema value);
        int IndexOf(string name);
        void Insert(int index, ViewColumnSchema value);
        void Remove(ViewColumnSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        ViewColumnSchema this[string name] { get; }

        ViewColumnSchema this[int index] { get; set; }
    }
}

