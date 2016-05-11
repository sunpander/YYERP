namespace SchemaExplorer
{
    using System;

    public interface IDbConnectionStringEditor
    {
        bool ShowEditor(string currentConnectionString);

        string ConnectionString { get; }

        bool EditorAvailable { get; }
    }
}

