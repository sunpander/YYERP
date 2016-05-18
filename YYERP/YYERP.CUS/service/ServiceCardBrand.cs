using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace YYERP.CUS.service
{
    public  class ServiceCardBrand
    {
        private static String tableName = "cus_brand";
        public String brandId = "";
        public String brandName = "";
        public String companyId = AppContanst.CompanyId;

        public DataTable getEmptyTable()
        {
            return DbAccess.ServiceDB.ExecuteSql("select * from   cus_brand where 1=2   limit 1");
        }

        public DataTable ConvertToTable()
        {
            DataTable dt = getEmptyTable();
           

            return dt;
        }

  
        public static int insert(string brandName)
        {
            Dictionary<String,Object> parm = new  Dictionary<String,Object>();
            parm.Add("brandName",brandName);
            parm.Add("companyId", AppContanst.CompanyId);
            string sqlInsert = " insert into cus_brand(companyId,brandName) values(@companyId,@brandName)  ";
            DbAccess.ServiceDB.ExecuteSql(sqlInsert, parm);
            return 1;
        }
        public int update()
        {
            return 1;
        }
        public int delete()
        {
            return 1;
        }

        internal static int update(string brandId, string brandName)
        {
            Dictionary<String, Object> parm = new Dictionary<String, Object>();
            parm.Add("brandName", brandName);
            parm.Add("brandId", brandId);
            string sqlInsert = " update cus_brand  set brandName=@brandName where brandId=@brandId  ";
            return   DbAccess.ServiceDB.ExecuteNonSql(sqlInsert, parm);
         
        }

        internal static DataTable queryAll()
        {
            DataTable dt = DbAccess.ServiceDB.ExecuteSql("select * from " + tableName);
            return dt;
        }
    }
}
