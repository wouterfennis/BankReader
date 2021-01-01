using BankReader.Shared.Models;
using System;

namespace Bankreader.FileSystem.UnitTests.Banktransactions.TestData
{
    internal class CsvTransaction
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

        public decimal SaldoAfterMutation { get; set; }

        public string Tag { get; set; }


        private string GetCsvHeader()
        {
            return
                "\"Datum\";\"Naam / Omschrijving\";\"Rekening\";" +
                "\"Tegenrekening\";\"Code\";\"Af Bij\";" +
                "\"Bedrag (EUR)\";\"Mutatiesoort\";\"Mededelingen\";\"Saldo na mutatie\";\"Tag\"";
        }

        public override string ToString()
        {
            var transaction = $"\"{Date.ToString("yyyyMMdd")}\";\"{Description}\";" +
                   $"\"{Accountnumber}\";\"{ContraAccountnumber}\";\"{Code}\";" +
                   $"\"{TransactionDirection}\";\"{Amount}\";\"{MutationType}\";" +
                   $"\"{Comments}\";\"{SaldoAfterMutation}\";\"{Tag}\"\r\n";

            return $"{GetCsvHeader()}\r\n{transaction}";
        }
    }
}
