namespace SchemaExplorer
{
     
   
     
    //using SchemaExplorer.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing.Design;
    using System.IO;
    using System.Reflection;
    using System.Text;

    [Serializable]
    public class DatabaseSchema : SchemaObjectBase
    {
        [NonSerialized]
        //private IDbSchemaProvider _cachedProvider;
       // private CommandSchemaCollection _commands;
        private string _connectionString;
        private string _databaseName;
        //private bool _gotDatabaseName = false;
        [NonSerialized]
        private IDbSchemaProvider _provider;
        //private StringCollection _providerSearchDirectories;
        //private Type _providerType;
        private TableSchemaCollection _tables;
        private ViewSchemaCollection _views;

        static DatabaseSchema()
        {
           
        }

        public DatabaseSchema()
        {
           
            this._connectionString = string.Empty;
            this._databaseName = string.Empty;
           
            base._database = this;
        }

        public DatabaseSchema(IDbSchemaProvider provider, string dbName)
        {
            this._databaseName = dbName; 
            base._database = this;
            this._provider = provider;
        }

       

        private void ClearSchema()
        {
            this._tables = null;
            //this._gotDatabaseName = false;
        }

        public override bool Equals(object obj)
        {
            return false;
        }

        public override int GetHashCode()
        {
            int num=-1;
            //ManagedAssemblyResolver useManagedAssemblyResolver = AssemblyResolver.Current.UseManagedAssemblyResolver;
            //try
            //{
            //    goto Label_0010;
               
            //Label_0010:
            //    num = this.ConnectionString.GetHashCode() ^ this.Provider.GetType().FullName.GetHashCode();
            //}
            //finally
            //{
            //    int num2 = 0;
            //Label_0041:
            //    switch (num2)
            //    {
            //        case 1:
            //            break;

            //        case 2:
            //            useManagedAssemblyResolver.Dispose();
            //            num2 = 1;
            //            goto Label_0041;

            //        default:
            //            if (useManagedAssemblyResolver != null)
            //            {
            //                num2 = 2;
            //                goto Label_0041;
            //            }
            //            break;
            //    }
            //}
            return num;
        }


        public override void Refresh()
        {
            base.Refresh();
            this.ClearSchema();
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void ValidateProvider()
        {
            
        }
        public override string Name
        {
            get
            {             
                return this._databaseName;
            }
        }

        [Browsable(false)]
        public IDbSchemaProvider Provider
        {
            get
            {                
                return this._provider;
            }
            set
            {
                this._provider = value;
            }
        }

       

        [Browsable(false)]
        public TableSchemaCollection Tables
        {
            get
            {
                if (this._tables == null)
                {
                    _tables = new TableSchemaCollection(this.Provider.GetTables(this));
                }
                    
                return this._tables;
            }
        }

        [Browsable(false)]
        public ViewSchemaCollection Views
        {
            get
            {
                if(_views == null)
                    this._views = new ViewSchemaCollection(this.Provider.GetViews(this));
                       
                return this._views;
            }
        }
    }
}

