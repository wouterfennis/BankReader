using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.Banktransactions.Mapping;
using Bankreader.Infrastructure.Files;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankReader.Data.Csv
{
    public class CsvTransactionReader : ITransactionProvider
    {
        private readonly IFileLocationProvider _transactionsLocationProvider;
        private readonly ITextStreamFactory _textStreamFactory;

        public CsvTransactionReader(IFileLocationProvider transactionsLocationProvider, ITextStreamFactory textStreamFactory)
        {
            _transactionsLocationProvider = transactionsLocationProvider;
            _textStreamFactory = textStreamFactory;
        }

        public IList<Banktransaction> ProvideTransactions()
        {
            var filePath = _transactionsLocationProvider.GetTransactionsLocation();
            using (var reader = _textStreamFactory.Create(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("nl-NL")))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TransactionMapping>();
                csv.Configuration.Delimiter = ";";
                return csv.GetRecords<Banktransaction>().ToList();
            }
        }
    }
}
