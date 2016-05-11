namespace SchemaExplorer
{
    using System;

    public interface IIDbSchemaProviderCollection
    {
        void CopyTo(IDbSchemaProvider[] array, int arrayIndex);
        IIDbSchemaProviderEnumerator GetEnumerator();

        int Count { get; }

        bool IsSynchronized { get; }

        object SyncRoot { get; }
    }
}

