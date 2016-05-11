namespace SchemaExplorer
{
    using System;

    public interface IViewSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        ViewSchema Current { get; }
    }
}

