using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Data.Utilities;
using CsvHelper;

namespace BankReader.Data.Csv
{
    public class TransactionReader : ITransactionReader
    {
        private readonly ITextStreamFactory _textStreamFactory;

        public TransactionReader(ITextStreamFactory textStreamFactory)
        {
            _textStreamFactory = textStreamFactory;
        }

        public IList<Transaction> ReadCsv(string filePath)
        {
            using (var reader = _textStreamFactory.Create(filePath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TransactionMapping>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.CultureInfo = CultureInfo.GetCultureInfo("nl-NL");
                return csv.GetRecords<Transaction>().ToList();
            }
        }
    }
}
