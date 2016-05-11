namespace SchemaExplorer
{
    using System;

    public interface IViewSchemaCollection
    {
        void CopyTo(ViewSchema[] array, int arrayIndex);
        IViewSchemaEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

