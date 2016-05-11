namespace SchemaExplorer
{
    using System;

    public interface IDataObjectBaseCollection
    {
        void CopyTo(DataObjectBase[] array, int arrayIndex);
        IDataObjectBaseEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

