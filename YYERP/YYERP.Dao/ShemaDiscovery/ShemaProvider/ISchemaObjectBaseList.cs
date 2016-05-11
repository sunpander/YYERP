namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface ISchemaObjectBaseList : ISchemaObjectBaseCollection
    {
        int Add(SchemaObjectBase value);
        void Clear();
        bool Contains(SchemaObjectBase value);
        bool Contains(string name);
        int IndexOf(SchemaObjectBase value);
        int IndexOf(string name);
        void Insert(int index, SchemaObjectBase value);
        void Remove(SchemaObjectBase value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        SchemaObjectBase this[string name] { get; }

        SchemaObjectBase this[int index] { get; set; }
    }
}

