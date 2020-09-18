using Bankreader.Domain.Models;

namespace Bankreader.Application.Models
{
    public class Transaction
    {
        public string Description { get; }
        public decimal Amount { get; }
        public YearMonth YearMonth { get; }
        public TransactionDirection TransactionDirection { get; }

        public Transaction(string description, decimal amount, YearMonth yearMonth, TransactionDirection transactionDirection)
        {
            Description = description;
            Amount = amount;
            YearMonth = yearMonth;
            TransactionDirection = transactionDirection;
        }
    }
}
