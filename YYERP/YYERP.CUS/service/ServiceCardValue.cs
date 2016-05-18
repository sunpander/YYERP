using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YYERP.CUS.service
{
    public  class ServiceCardValue
    {
        private static String tableName = "cus_value";
        public String brandId = "";
        public String cardId = "";
        public String cardName = "";
        public String hideInSales = "";
        public String companyId = AppContanst.CompanyId;

        public DataTable getEmptyTable()
        {
            return DbAccess.ServiceDB.ExecuteSql("select * from   cus_card where 1=2   limit 1");
        }
     
        internal static int update(string brandId, string brandName)
        {
            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add("brandName", brandName);
            parm.Add("brandId", brandId);
            string sqlInsert = " update cus_brand  set brandName=@brandName where brandId=@brandId  ";
            return   DbAccess.ServiceDB.ExecuteNonSql(sqlInsert, parm);
         
        }
        public static DataTable queryByCol(String colName,String colValue)
        {

            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add("companyId", AppContanst.CompanyId);
            parm.Add(colName, colValue);
            string sqlSel = "select * from " + tableName + " where "+colName+"=@"+colName;
            DataTable dt = DbAccess.ServiceDB.ExecuteSql(sqlSel,parm);
            return dt;
        }
        internal static DataTable queryAll()
        {
            DataTable dt = DbAccess.ServiceDB.ExecuteSql("select * from " + tableName);
            return dt;
        }

        internal static int insert(string brandId, string cardName,string hideInSales)
        {
            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add("brandId", brandId);
            parm.Add("cardName", cardName);
            parm.Add("companyId", AppContanst.CompanyId);
            string sqlInsert = " insert into cus_card(companyId,cardName,brandId,hideInSales) values(@companyId,@cardName,@brandId,@hideInSales)  ";
            DbAccess.ServiceDB.ExecuteSql(sqlInsert, parm);
            return 1;
        }
    }
}
