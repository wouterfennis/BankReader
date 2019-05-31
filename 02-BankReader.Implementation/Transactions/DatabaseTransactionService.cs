using System.Collections.Generic;
using BankReader.Data.Csv.Models;

namespace BankReader.Implementation.Transactions
{
    public class DatabaseTransactionService : ITransactionService
    {
        public IList<Transaction> RetrieveTransactions()
        {
            throw new System.NotImplementedException();
        }
    }
}