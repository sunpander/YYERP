namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IViewSchemaList : IViewSchemaCollection
    {
        int Add(ViewSchema value);
        void Clear();
        bool Contains(ViewSchema value);
        bool Contains(string name);
        bool Contains(string owner, string name);
        int IndexOf(ViewSchema value);
        int IndexOf(string name);
        int IndexOf(string owner, string name);
        void Insert(int index, ViewSchema value);
        void Remove(ViewSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        ViewSchema this[string owner, string name] { get; }

        ViewSchema this[string name] { get; }

        ViewSchema this[int index] { get; set; }
    }
}

