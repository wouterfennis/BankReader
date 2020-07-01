using BankReader.Data.Models;
using System;

namespace BankReader.Data.UnitTests.Csv.TestData
{
    class CsvTransaction
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

        public override string ToString()
        {
            return $"\"{Date.ToString("yyyyMMdd")}\",\"{Description}\"," +
                   $"\"{Accountnumber}\",\"{ContraAccountnumber}\",\"{Code}\"," +
                   $"\"{TransactionDirection}\",\"{Amount}\",\"{MutationType}\",\"{Comments}\"\r\n";
        }
    }
}
