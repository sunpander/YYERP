namespace SchemaExplorer
{
    using System;

    public interface ITableSchemaCollection
    {
        void CopyTo(TableSchema[] array, int arrayIndex);
        ITableSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

