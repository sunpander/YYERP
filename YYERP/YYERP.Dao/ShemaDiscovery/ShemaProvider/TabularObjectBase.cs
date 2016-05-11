namespace SchemaExplorer
{
     
     
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;

    [Serializable]
    public abstract class TabularObjectBase : SchemaObjectBase
    {
        public abstract string FullName { get; }
    }
}

