using System.Collections.Generic;
using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Implementation.Wrappers;

namespace BankReader.Implementation.Transactions
{
    public class CsvTransactionService : ITransactionService
    {
        private readonly IConsoleScreen _consoleScreen;
        private readonly ITransactionReader _transactionReader;

        public CsvTransactionService(IConsoleScreen consoleScreen, ITransactionReader transactionReader)
        {
            _transactionReader = transactionReader;
            _consoleScreen = consoleScreen;
        }

        public IList<Transaction> RetrieveTransactions()
        {
            var filePath = @"C:\Git\BankReader\test.csv";//_consoleScreen.AskForPath("CSV transaction");

            IList<Transaction> transactions = _transactionReader.ReadCsv(filePath);
            _consoleScreen.WriteLine($"There are {transactions.Count} transactions in this CSV");

            return transactions;
        }
    }
}