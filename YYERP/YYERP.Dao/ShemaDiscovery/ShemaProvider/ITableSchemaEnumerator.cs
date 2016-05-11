namespace SchemaExplorer
{
    using System;

    public interface ITableSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        TableSchema Current { get; }
    }
}

