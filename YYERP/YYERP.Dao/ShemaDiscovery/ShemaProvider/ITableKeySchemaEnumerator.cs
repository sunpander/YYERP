namespace SchemaExplorer
{
    using System;

    public interface ITableKeySchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        TableKeySchema Current { get; }
    }
}

