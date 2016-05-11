using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using DbAccess.Sql;

namespace DbAccess
{
    public class ServiceDB
    {
        public static void InitDBInfo(string server)
        {
            if (String.IsNullOrEmpty(DatabaseFactory.ConnectionString))
            {
                int sp = server.IndexOf(";");
                string type = server.Substring(5, sp - 5);
                int sp1 = server.IndexOf(";", sp + 1);
                string datasource = server.Substring(sp + 10, sp1 - sp - 10);
                int sp2 = server.IndexOf(";", sp1 + 1);
                string username = server.Substring(sp1 + 10, sp2 - sp1 - 10);
                string password = server.Substring(sp2 + 10);

                DatabaseFactory.ConnectionString = Utiltity.GetConnectionString(type, datasource, username, password);
            }
        }
        public static DataTable ExecuteSql(string strSql )
        {
            if (String.IsNullOrEmpty(DatabaseFactory.ConnectionString))
            {
                DatabaseFactory.ConnectionString=DatabaseFactory.GetConnectionString();
                if (String.IsNullOrEmpty(DatabaseFactory.ConnectionString))
                {
                    throw new Exception("没指定数据库连接串");
                }
            }

            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                return db.ExecuteQuery(strSql); //查询
            }
        }


        public static string InsertRow(DataRow dr)
        {
            string fields = string.Empty;
            string values = string.Empty;
            int fieldNo = 0;
            DataTable dt = dr.Table;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                fields += (dt.Columns[i].ColumnName + ",");
                fieldNo++;
            }
            fields = fields.Remove(fields.Length - 1);
            for (int i = 0; i < fieldNo; i++)
            {
                values += string.Format("'{0}',", dr[dt.Columns[i].ColumnName]);
            }
            values = values.Remove(values.Length - 1);
            string sql = string.Format(@"INSERT INTO {0} ({1}) values ({2})", dt.TableName, fields, values);
            return sql;
        }
        public static int InsertRow(DataTable dt )
        {
            List<String> listSql = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listSql.Add(InsertRow(dt.Rows[i] ));
            }
            return ExecuteNonSql(listSql);

        }

        public static int ExecuteNonSql(List<string> strSql)
        {
            int rowCount = 0;
            for (int i = 0; i < strSql.Count; i++)
            {
                using (IDatabase db = DatabaseFactory.CreateDatabase())
                {
                    rowCount += db.ExecuteNonQuery(strSql[i]) ; //查询
                }
            }
            return rowCount;
        }

        public static int DeleteRow(DataTable dt, string idColName)
        {
            List<String> listSql = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listSql.Add( DeleteRow(dt.Rows[i], idColName));
            }
            return ExecuteNonSql(listSql);
             
        }

        public static string DeleteRow(DataRow dr, string idColName)
        {
            if (String.IsNullOrEmpty(dr.Table.TableName))
            {
                throw new Exception("表名称不能为空.");
            }
            string condition = string.Format(@" {0}='{1}' ", idColName, dr[idColName].ToString());
            string sql = string.Format(@"DELETE FROM {0} WHERE {1}", dr.Table.TableName, condition);
            return sql;
        }
        public static int UpdateRow(DataTable dt, string idColName)
        {
            List<String> listSql = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listSql.Add(UpdateRow(dt.Rows[i], idColName));
            }
            return ExecuteNonSql(listSql);

        }
        public static string UpdateRow(DataRow dr, String idColName)
        {
            string setFields = string.Empty;
            string condition = string.Empty;
            condition += (idColName + "='" + dr[idColName].ToString() + "'");
            foreach (DataColumn mcs in dr.Table.Columns)
            {
                setFields += string.Format(@" {0}='{1}' ,", mcs.ColumnName, dr[mcs.ColumnName].ToString());
            }
            setFields = setFields.Remove(setFields.Length - 1);
            string sql = string.Format(@"UPDATE {0} SET {1} WHERE {2}", dr.Table.TableName, setFields, condition);
            return sql;
        }


        public static DataTable ExecuteSql(string strSql,Dictionary<string,object> parameters)
        {
            if (String.IsNullOrEmpty(DatabaseFactory.ConnectionString))
            {
                throw new Exception("没指定server");
            }
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                return db.ExecuteQuery(strSql, parameters); //查询
            }
        }

        public static DataTable ExecuteSql(string strSql, string server)
        {
            InitDBInfo(server);
            using (IDatabase db = DatabaseFactory.CreateDatabase())
            {
                return db.ExecuteQuery(strSql); //查询
            }
        }

        //public static DataSet ExecuteSql(List<string> strSql, string server)
        //{
        //    if (String.IsNullOrEmpty(DatabaseFactory.ConnectionString))
        //    {
        //        int sp = server.IndexOf(";");
        //        string type = server.Substring(5, sp - 5);
        //        int sp1 = server.IndexOf(";", sp + 1);
        //        string datasource = server.Substring(sp + 10, sp1 - sp - 10);
        //        int sp2 = server.IndexOf(";", sp1 + 1);
        //        string username = server.Substring(sp1 + 10, sp2 - sp1 - 10);
        //        string password = server.Substring(sp2 + 10);
        //        DatabaseFactory.ConnectionString = Utiltity.GetConnectionString(type, datasource, username, password);
        //    }
        //    DataSet ds = new DataSet();
        //    for (int i = 0; i < strSql.Count; i++)
        //    {
        //        using (IDatabase db = DatabaseFactory.CreateDatabase())
        //        {
        //            ds.Tables.Add(db.ExecuteQuery(strSql[i])); //查询
        //        }
        //    }
        //    return ds;
        //}
    }
}
