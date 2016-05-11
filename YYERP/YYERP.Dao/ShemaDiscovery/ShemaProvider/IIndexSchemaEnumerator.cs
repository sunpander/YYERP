namespace SchemaExplorer
{
    using System;

    public interface IIndexSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        IndexSchema Current { get; }
    }
}

