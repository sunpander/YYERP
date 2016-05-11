using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SchemaExplorer;
using System.Data.Common;
using System.Data;

namespace YYERP.Dao
{
    public abstract partial class Database
    {
        public string Name
        {
            get;
            set;
        }

        public virtual string DataBaseType
        {
            get
            {
                return "SqlServer";
            }
        }

        public string UserName
        {
            get;
            set;
        }

        public virtual DatabaseSchema Schema
        {
            get { return DbschemaFactory.Get(this.DataBaseType, Name, null); }
        }


        
    }
}
