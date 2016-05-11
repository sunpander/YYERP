namespace SchemaExplorer
{
    using System;

    public interface IMemberColumnSchemaCollection
    {
        void CopyTo(MemberColumnSchema[] array, int arrayIndex);
        IMemberColumnSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

