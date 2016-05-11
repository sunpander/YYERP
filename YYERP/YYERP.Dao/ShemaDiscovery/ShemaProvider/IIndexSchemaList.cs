namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IIndexSchemaList : IIndexSchemaCollection
    {
        int Add(IndexSchema value);
        void Clear();
        bool Contains(IndexSchema value);
        bool Contains(string name);
        int IndexOf(IndexSchema value);
        int IndexOf(string name);
        void Insert(int index, IndexSchema value);
        void Remove(IndexSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        IndexSchema this[string name] { get; }

        IndexSchema this[int index] { get; set; }
    }
}

