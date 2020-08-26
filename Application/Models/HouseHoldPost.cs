using System.Collections.Generic;
using System.Linq;

namespace Bankreader.Application.Models
{
    public class HouseholdPost
    {
        public Category Category { get; }
        private IList<HouseholdTransaction> _transactions { get; }

        public HouseholdPost(Category category)
        {
            Category = category;
            _transactions = new List<HouseholdTransaction>();
        }

        public void AddTransaction(HouseholdTransaction householdTransaction)
        {
            var transaction = SearchHouseholdTransaction(householdTransaction.YearMonth, householdTransaction.TransactionDirection);
            if (transaction == null)
            {
                _transactions.Add(householdTransaction);
            }
            else
            {
                transaction.RaiseAmount(householdTransaction.Amount);
            }
        }

        private HouseholdTransaction SearchHouseholdTransaction(YearMonth yearMonth, TransactionDirection transactionDirection)
        {
            return _transactions
                .SingleOrDefault(transaction => transaction.YearMonth == yearMonth &&
                transaction.TransactionDirection == transactionDirection);
        }

        public decimal GetExpenses(YearMonth yearMonth)
        {
            return _transactions
                .Where(transaction => transaction.YearMonth == yearMonth)
                .Where(transaction => transaction.TransactionDirection == TransactionDirection.Af)
                .Sum(x => x.Amount);
        }

        public decimal GetIncome(YearMonth yearMonth)
        {
            return _transactions
                .Where(transaction => transaction.YearMonth == yearMonth)
                .Where(transaction => transaction.TransactionDirection == TransactionDirection.Bij)
                .Sum(x => x.Amount);
        }

    }
}