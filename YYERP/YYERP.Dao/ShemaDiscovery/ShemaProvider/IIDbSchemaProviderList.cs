namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IIDbSchemaProviderList : IIDbSchemaProviderCollection
    {
        int Add(IDbSchemaProvider value);
        void Clear();
        bool Contains(IDbSchemaProvider value);
        bool Contains(string name);
        int IndexOf(IDbSchemaProvider value);
        int IndexOf(string name);
        void Insert(int index, IDbSchemaProvider value);
        void Remove(IDbSchemaProvider value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        IDbSchemaProvider this[string name] { get; }

        IDbSchemaProvider this[int index] { get; set; }
    }
}

