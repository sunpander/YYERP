namespace SchemaExplorer
{
    using System;

    public interface IIndexSchemaCollection
    {
        void CopyTo(IndexSchema[] array, int arrayIndex);
        IIndexSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

