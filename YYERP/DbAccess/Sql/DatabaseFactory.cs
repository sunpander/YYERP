using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbAccess.Sql
{
    /// <summary>
    /// 数据库管理类
    /// 1.先对ConnectionString赋值
    /// 2.调用 using (IDatabase db = DatabaseFactory.GetDatabase())
    /// {
    ///             string strSql2 = " select * from TTTTT where NAME =@NAME  ";
    ///             Dictionary<string, object> parameter2 = new Dictionary<string, object>();
    ///             parameter2.Add("NAME", name);  //设置参数
    ///             DataTable dt = db.ExecuteQuery(strSql2, parameter2); //查询
    /// }
    /// </summary>
    public class DatabaseFactory : IDisposable
    {
        public static string GetConnectionString( )
        {
            string strConnection = "";
            string dataSource = "yyerp";
            string userName = "root";
            string password = "";
            //对密码进行解密
            //password = Decode(password);
            string baseType = "MySql";
            strConnection = "Data Source=" + dataSource + ";Persist Security Info=True;User ID =" + userName + ";Password=" + password;// +";Unicode=True";

            if (baseType.Equals("DB2"))
            {
                strConnection = "Provider= IBMDADB2.DB2CLIENT9;" + strConnection;
                //strConnection = "Provider=IBMDADB2.DB2COPY1;" + strConnection;
            }
            else if (baseType.Equals("Oracle"))
            {
                strConnection = "Provider= OraOLEDB.Oracle.1;" + strConnection;
            }
            else if (baseType.Equals("SqlServer"))
            {
                strConnection = "Provider= SQLNCLI.1;" + strConnection + ""; ;
            }
            else if (baseType.Equals("MySql"))
            {
                strConnection = "server=127.0.0.1;port=3306;database=yyerp;user id=root;characterset=latin1"; ;
            }
            else
            {
                throw new Exception("数据库类型错误.");
            }
            return strConnection;
        }
        private static DatabaseType type;

        public static IDatabase CreateDatabase()
        {
            type = DatabaseType.Default;
            if (ConnectionString.Contains("OraOLEDB.Oracle.1"))
            {
                type = DatabaseType.Oracle;
            }
            else if (ConnectionString.Contains("IBMDADB2.DB2CLIENT9"))
            {
                type = DatabaseType.DB2;
            }
            //SQLNCLI11
            else if (ConnectionString.Contains("SQLNCLI") || ConnectionString.Contains("SQLOLEDB"))
            {
                type = DatabaseType.MSSqlServer;
            }
            else if (ConnectionString.Contains("server") || ConnectionString.Contains("port"))
            {
                type = DatabaseType.MySql;
            }
            else
            {
                type = DatabaseType.SqlLite;
            }
            return CreateDatabase(type);
        }
        private static string _conStr = "";
        public static string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_conStr))
                {
                    _conStr = GetConnectionString();
                }
                return _conStr;
            }
            set
            {
                _conStr = value;

            }
        }
        public static DatabaseType GetDatabaseType()
        {
            if (type != null)
            {
                return type;
            }
            return DatabaseType.Oracle;
        }

        private static IDatabase CreateDatabase(DatabaseType baseType)
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new ApplicationException("要先对DatabaseManager的ConnectionString赋值");
            }
            IDatabase Database = null;
            switch (baseType)
            {
                case DatabaseType.SqlLite:
                    Database = new SQLiteDatabase(ConnectionString);
                    break;
                case DatabaseType.MSSqlServer:
                case DatabaseType.Oracle:
                case DatabaseType.DB2:
                case DatabaseType.Default:
                    Database = new OleDbDatabase(ConnectionString);
                    break;
                case DatabaseType.MySql:
                    Database = new MySQLDatabase(ConnectionString);
                    break;
                    
            }
            return Database;
        }

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
