using System.Data;
using System;

namespace DbAccess.Sql
{
    public class Utiltity
    {

        public static string getGuidId()
        {
            return Guid.NewGuid().ToString().Substring(0,11);
        }
        /***
         * 合并表数据.
         * 前提:表结构完全一样
         ****/
        public static void combineTable(DataTable from,DataTable to)
        {
            if (from == null)
                return;
            for (int i = 0; i < from.Rows.Count; i++)
            {
                to.Rows.Add(from.Rows[i].ItemArray);
            }
        }

        public static string GetConnectionString(string baseType, string dataSource, string userName, string password)
        {
            string strConnection = "";
            //string dataSource = "BFR_120";
            //string userName = "EPRP";
            //string password = "EPRP";
            ////对密码进行解密
            ////password = Decode(password);
            //string baseType = "Oracle";
            strConnection = "Data Source=" + dataSource + ";Persist Security Info=True;User ID =" + userName + ";Password=" + password;// +";Unicode=True";

            if (baseType.ToLower().Equals("db2"))
            {
                strConnection = "Provider= IBMDADB2.DB2CLIENT9;" + strConnection;
                //strConnection = "Provider=IBMDADB2.DB2COPY1;" + strConnection;
            }
            else if (baseType.ToLower().Equals("oracle"))
            {
                strConnection = "Provider= OraOLEDB.Oracle.1;" + strConnection;
            }
            else if (baseType.ToLower().Equals("sqlserver"))
            {
                strConnection = "Provider= SQLNCLI.1;" + strConnection + ""; ;
            }
            else if (baseType.ToLower().Equals("sqllite"))
            {
                strConnection = "Data Source=" + dataSource;
            }
            else if (baseType.ToLower().Equals("mysql"))
            {
                //server=127.0.0.1;port=3306;database=yyerp;user id=root;characterset=latin1
                strConnection = "server=127.0.0.1;port=3306;database="+dataSource+";user id=" + dataSource;
            }
            else
            {
                throw new Exception("数据库类型错误.");
            }
            return strConnection;
        }
    }
}
