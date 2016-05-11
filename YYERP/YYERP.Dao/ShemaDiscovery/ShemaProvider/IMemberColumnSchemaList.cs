namespace SchemaExplorer
{
    using System;
    using System.Reflection;

    public interface IMemberColumnSchemaList : IMemberColumnSchemaCollection
    {
        int Add(MemberColumnSchema value);
        void Clear();
        bool Contains(MemberColumnSchema value);
        bool Contains(string name);
        int IndexOf(MemberColumnSchema value);
        int IndexOf(string name);
        void Insert(int index, MemberColumnSchema value);
        void Remove(MemberColumnSchema value);
        void RemoveAt(int index);

        bool IsFixedSize { get; }

        bool IsReadOnly { get; }

        MemberColumnSchema this[string name] { get; }

        MemberColumnSchema this[int index] { get; set; }
    }
}

