using System;
using BankReader.Models;

namespace BankReader.Implementation.Models
{
    public class HouseholdTransaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public TransactionDirection TransactionDirection { get; }

        public HouseholdTransaction(decimal amount, DateTime date, TransactionDirection transactionDirection)
        {
            Amount = amount;
            Date = date;
            TransactionDirection = transactionDirection;
        }
    }
}
