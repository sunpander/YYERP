namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IExtendedPropertyList : IExtendedPropertyCollection
    {
        int Add(ExtendedProperty value);
        void Clear();
        bool Contains(ExtendedProperty value);
        bool Contains(string name);
        int IndexOf(ExtendedProperty value);
        int IndexOf(string name);
        void Insert(int index, ExtendedProperty value);
        void Remove(ExtendedProperty value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        ExtendedProperty this[string name] { get; }

        ExtendedProperty this[int index] { get; set; }
    }
}

