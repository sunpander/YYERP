namespace SchemaExplorer
{
    using System;

    public interface ISchemaObjectBaseEnumerator
    {
        bool MoveNext();
        void Reset();

        SchemaObjectBase Current { get; }
    }
}

