namespace SchemaExplorer
{
    using System;

    public interface IExtendedPropertyEnumerator
    {
        bool MoveNext();
        void Reset();

        ExtendedProperty Current { get; }
    }
}

