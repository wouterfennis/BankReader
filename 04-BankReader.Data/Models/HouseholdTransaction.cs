namespace BankReader.Data.Models
{
    public class HouseholdTransaction
    {
        public decimal Amount { get; private set; }
        internal YearMonth YearMonth { get; }
        public TransactionDirection TransactionDirection { get; }

        public HouseholdTransaction(decimal amount, YearMonth yearMonth, TransactionDirection transactionDirection)
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
