namespace YYERP.Dao.SqlServerSchemaProvider
{
    using System;
    using System.Text;

    internal class SqlBuilder
    {
        private StringBuilder sqlStatements = new StringBuilder();

        public void AppendStatement(string sql)
        {
            string str = sql.Trim();
            this.sqlStatements.Append(str);
            if (!str.EndsWith(";", StringComparison.OrdinalIgnoreCase))
            {
                this.sqlStatements.Append(';');
            }
            this.sqlStatements.AppendLine();
        }

        public static implicit operator string(SqlBuilder builder)
        {
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.sqlStatements.ToString();
        }
    }
}

