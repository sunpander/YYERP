namespace SchemaExplorer
{
    using System;

    public interface IViewColumnSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        ViewColumnSchema Current { get; }
    }
}

