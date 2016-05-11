namespace SchemaExplorer
{
    using System;

    public interface IExtendedPropertyCollection
    {
        void CopyTo(ExtendedProperty[] array, int arrayIndex);
        IExtendedPropertyEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

