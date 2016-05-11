namespace SchemaExplorer
{
    using System;

    public interface IDataObjectBaseEnumerator
    {
        bool MoveNext();
        void Reset();

        DataObjectBase Current { get; }
    }
}

