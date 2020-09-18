using Bankreader.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace Bankreader.Application.Models
{
    public class HouseholdPost
    {
        public Category Category { get; }
        public IList<Transaction> OriginalTransactions { get; }
        public IList<GroupedTransaction> TransactionsPerMonthYear { get; }

        public HouseholdPost(Category category)
        {
            Category = category;
            OriginalTransactions = new List<Transaction>();
            TransactionsPerMonthYear = new List<GroupedTransaction>();
        }

        public void AddTransaction(string description, decimal amount, YearMonth yearMonth, TransactionDirection transactionDirection)
        {
            OriginalTransactions.Add(new Transaction(description, amount, yearMonth, transactionDirection));
            var existingGroupedTransaction = SearchGroupedTransaction(yearMonth, transactionDirection);
            if (existingGroupedTransaction == null)
            {
                var groupedTransaction = new GroupedTransaction(amount, yearMonth, transactionDirection);
                TransactionsPerMonthYear.Add(groupedTransaction);
            }
            else
            {
                existingGroupedTransaction.RaiseAmount(amount);
            }
        }

        private GroupedTransaction SearchGroupedTransaction(YearMonth yearMonth, TransactionDirection transactionDirection)
        {
            return TransactionsPerMonthYear
                .SingleOrDefault(transaction => transaction.YearMonth == yearMonth &&
                transaction.TransactionDirection == transactionDirection);
        }

        public decimal GetExpenses(YearMonth yearMonth)
        {
            return TransactionsPerMonthYear
                .Where(transaction => transaction.YearMonth == yearMonth)
                .Where(transaction => transaction.TransactionDirection == TransactionDirection.Af)
                .Sum(x => x.Amount);
        }

        public decimal GetIncome(YearMonth yearMonth)
        {
            return TransactionsPerMonthYear
                .Where(transaction => transaction.YearMonth == yearMonth)
                .Where(transaction => transaction.TransactionDirection == TransactionDirection.Bij)
                .Sum(x => x.Amount);
        }
    }
}