namespace YYERP.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Transactions;

    public class TransactionScopeConnections
    {
        private static Dictionary<Transaction, Dictionary<string, DbConnection>> transactionConnections = new Dictionary<Transaction, Dictionary<string, DbConnection>>();

        public static DbConnection GetConnection(Database db)
        {
            Dictionary<string, DbConnection> dictionary;
            DbConnection newOpenConnection;
            Transaction current = Transaction.Current;
            if (current == null)
            {
                return null;
            }
            transactionConnections.TryGetValue(current, out dictionary);
            if (dictionary != null)
            {
                dictionary.TryGetValue(db.ConnectionString.ToString(), out newOpenConnection);
                if (newOpenConnection != null)
                {
                    return newOpenConnection;
                }
            }
            else
            {
                dictionary = new Dictionary<string, DbConnection>();
                lock (transactionConnections)
                {
                    transactionConnections.Add(current, dictionary);
                }
            }
            if (dictionary.ContainsKey(db.ConnectionString))
            {
                return dictionary[db.ConnectionString];
            }
            newOpenConnection = db.GetNewOpenConnection();
            current.TransactionCompleted += new TransactionCompletedEventHandler(TransactionScopeConnections.OnTransactionCompleted);
            dictionary.Add(db.ConnectionString, newOpenConnection);
            return newOpenConnection;
        }

        private static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DbConnection> dictionary;
            transactionConnections.TryGetValue(e.Transaction, out dictionary);
            if (dictionary != null)
            {
                lock (transactionConnections)
                {
                    transactionConnections.Remove(e.Transaction);
                }
                foreach (DbConnection connection in dictionary.Values)
                {
                    connection.Dispose();
                }
            }
        }
    }
}

