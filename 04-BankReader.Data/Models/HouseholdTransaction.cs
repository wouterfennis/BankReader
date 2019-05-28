using System;
using BankReader.Models;

namespace BankReader.Implementation.Models
{
    public class HouseholdTransaction
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TransactionDirection TransactionDirection { get; set; }
    }
}
