using Bankreader.Domain.Models;

namespace Bankreader.Application.Models
{
    public class GroupedTransaction
    {
        public decimal Amount { get; private set; }
        public  YearMonth YearMonth { get; }
        public TransactionDirection TransactionDirection { get; }

        public GroupedTransaction(decimal amount, YearMonth yearMonth, TransactionDirection transactionDirection)
        {
            Amount = amount;
            YearMonth = yearMonth;
            TransactionDirection = transactionDirection;
        }

        public void RaiseAmount(decimal extraAmount)
        {
            Amount = Amount + extraAmount;
        }
    }
}
