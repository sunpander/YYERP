namespace SchemaExplorer
{
    using System;

    public interface ITableKeySchemaCollection
    {
        void CopyTo(TableKeySchema[] array, int arrayIndex);
        ITableKeySchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

