namespace SchemaExplorer
{
    using System;

    public interface IIDbSchemaProviderEnumerator
    {
        bool MoveNext();
        void Reset();

        IDbSchemaProvider Current { get; }
    }
}

