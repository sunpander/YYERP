namespace SchemaExplorer
{
    using System;
    using System.Data;

    public interface IDbSchemaProvider
    {
        //ParameterSchema[] GetCommandParameters( CommandSchema command);
        //CommandResultSchema[] GetCommandResultSchemas( CommandSchema command);
        //CommandSchema[] GetCommands( DatabaseSchema database);
        //string GetCommandText( CommandSchema command);
       // string GetDatabaseName(string dbName,string userName);
        ExtendedProperty[] GetExtendedProperties( SchemaObjectBase schemaObject);
        ColumnSchema[] GetTableColumns( TableSchema table);
        DataTable GetTableData( TableSchema table);
        IndexSchema[] GetTableIndexes( TableSchema table);
        TableKeySchema[] GetTableKeys( TableSchema table);
        PrimaryKeySchema GetTablePrimaryKey( TableSchema table);
        TableSchema[] GetTables( DatabaseSchema database);
        ViewColumnSchema[] GetViewColumns( ViewSchema view);
        DataTable GetViewData( ViewSchema view);
        ViewSchema[] GetViews( DatabaseSchema database);
        string GetViewText(ViewSchema view);
        void SetExtendedProperties(SchemaObjectBase schemaObject);

        string Description { get; }

        string DbName { get; set; }
        string UserName { get; set; }
    }
}

