using BankReader.Shared.Models;
using System;

namespace BankReader.Application.Models
{
    public class Banktransaction
    {
        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string Accountnumber { get; set; }

        public string ContraAccountnumber { get; set; }

        public string Code { get; set; }

        public TransactionDirection TransactionDirection { get; set; }

        public decimal Amount { get; set; }

        public string MutationType { get; set; }

        public string Comments { get; set; }
    }
}
