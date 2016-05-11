using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using SchemaExplorer;
 

namespace YYERP.Dao
{
    public class DbschemaFactory
    {
        static Dictionary<string, DatabaseSchema> Schemas = new Dictionary<string, DatabaseSchema>();
        public static DatabaseSchema Get(string databaseType, string dbName, string userName)
        {
            if (Schemas.ContainsKey(dbName))
                return Schemas[dbName];
            else
            {
                IDbSchemaProvider provider = GetProvider(databaseType, dbName, userName);
                DatabaseSchema dbSchema = new DatabaseSchema(provider, dbName);
                Schemas.Add(dbName, dbSchema);
                return dbSchema;
            }
        }
        static Dictionary<string, IDbSchemaProvider> provider = new Dictionary<string, IDbSchemaProvider>();
        private static IDbSchemaProvider GetProvider(string databaseType,string dbName,string userName)
        {
            if (provider.ContainsKey(dbName))
                return provider[dbName];
            else
            {
                if (ProviderConfig.ContainsKey(databaseType))
                {
                    Type objType = Type.GetType(ProviderConfig[databaseType], true);

                    // create object
                    object obj = Activator.CreateInstance(objType, new object[] { dbName, userName });
                    provider.Add(dbName, obj as IDbSchemaProvider);
                    return obj as IDbSchemaProvider;
                }
                else
                {
                    throw new Exception(string.Format("不支持数据库类型{0}", databaseType));
                }
            }

        }

        public void Flush()
        {
            _ProviderConfig = null;
        }
        static Dictionary<string, string> _ProviderConfig;

        private static Dictionary<string, string> ProviderConfig
        {
            get
            {
                if (_ProviderConfig == null)
                {
                    _ProviderConfig = new Dictionary<string, string>();
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Configuration\SchemaProvider.xml"; ;
                    DataSet ds = new DataSet();
                    ds.ReadXml(fileName, XmlReadMode.ReadSchema);
                    foreach (DataRow row in ds.Tables["SchemaProvider"].Rows)
                    {
                        _ProviderConfig.Add(row[0].ToString(), row[1].ToString());
                    }

                }
                return _ProviderConfig;
            }
        }
    }
}
