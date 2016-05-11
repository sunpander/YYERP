namespace SchemaExplorer
{
    using System;

    public interface IMemberColumnSchemaEnumerator
    {
        bool MoveNext();
        void Reset();

        MemberColumnSchema Current { get; }
    }
}

