namespace SchemaExplorer
{
    using System;

    public interface ISchemaObjectBaseCollection
    {
        void CopyTo(SchemaObjectBase[] array, int arrayIndex);
        ISchemaObjectBaseEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

