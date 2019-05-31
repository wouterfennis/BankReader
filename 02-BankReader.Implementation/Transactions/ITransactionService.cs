using System.Collections.Generic;
using BankReader.Data.Csv.Models;

namespace BankReader.Implementation.Transactions
{
    public interface ITransactionService
    {
        IList<Transaction> RetrieveTransactions();
    }
}