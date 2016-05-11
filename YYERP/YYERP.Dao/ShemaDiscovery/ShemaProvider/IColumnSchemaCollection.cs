namespace SchemaExplorer
{
    using System;

    public interface IColumnSchemaCollection
    {
        void CopyTo(ColumnSchema[] array, int arrayIndex);
        IColumnSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

