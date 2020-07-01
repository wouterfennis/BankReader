using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace BankReader.Implementation.Services
{
    public class HouseholdService : IHouseholdService
    {
        private readonly ITransactionCategorizer _transactionCategorizer;
        private readonly ITransactionProvider _transactionProvider;

        public HouseholdService(ITransactionCategorizer transactionCategorizer, ITransactionProvider transactionProvider)
        {
            _transactionCategorizer = transactionCategorizer;
            _transactionProvider = transactionProvider;
        }

        public HouseholdBook CreateHouseholdBook()
        {
            var transactions = _transactionProvider.ProvideTransactions();

            var householdBook = new HouseholdBook();

            foreach (Banktransaction transaction in transactions.ToList())
            {
                var category = _transactionCategorizer.DetermineCategory(transaction.Description);
                var householdPost = householdBook.RetrieveHouseholdPost(category);
                var householdTransaction = new HouseholdTransaction(transaction.Amount, YearMonth.FromDateTime(transaction.Date), transaction.TransactionDirection);
                householdPost.AddTransaction(householdTransaction);
                transactions.Remove(transaction);
            }

            return householdBook;
        }
    }
}
