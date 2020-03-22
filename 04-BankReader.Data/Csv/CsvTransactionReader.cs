﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BankReader.Data.Csv.Models;
using BankReader.Data.Providers;
using BankReader.Data.Utilities;
using CsvHelper;

namespace BankReader.Data.Csv
{
    public class CsvTransactionReader : ITransactionProvider
    {
        private readonly ITransactionsLocationProvider _transactionsLocationProvider;
        private readonly ITextStreamFactory _textStreamFactory;

        public CsvTransactionReader(ITransactionsLocationProvider transactionsLocationProvider, ITextStreamFactory textStreamFactory)
        {
            _transactionsLocationProvider = transactionsLocationProvider;
            _textStreamFactory = textStreamFactory;
        }

        public IList<Banktransaction> ProvideTransactions()
        {
            var filePath = _transactionsLocationProvider.GetTransactionsLocation();
            using (var reader = _textStreamFactory.Create(filePath))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<TransactionMapping>();
                csv.Configuration.Delimiter = ",";
                csv.Configuration.CultureInfo = CultureInfo.GetCultureInfo("nl-NL");
                return csv.GetRecords<Banktransaction>().ToList();
            }
        }
    }
}
