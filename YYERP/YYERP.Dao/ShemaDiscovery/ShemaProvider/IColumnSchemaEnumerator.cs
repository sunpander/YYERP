namespace SchemaExplorer
{
    using System;

    public interface IColumnSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        ColumnSchema Current { get; }
    }
}

