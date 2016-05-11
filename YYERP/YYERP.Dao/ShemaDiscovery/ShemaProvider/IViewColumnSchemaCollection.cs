namespace SchemaExplorer
{
    using System;

    public interface IViewColumnSchemaCollection
    {
        void CopyTo(ViewColumnSchema[] array, int arrayIndex);
        IViewColumnSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

