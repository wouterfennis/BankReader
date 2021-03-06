﻿using Bankreader.Application.Models;
using Bankreader.Banktransactions.Converters;
using CsvHelper.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Bankreader.Banktransactions.Mapping
{
    [ExcludeFromCodeCoverage]
    public class TransactionMapping : ClassMap<Banktransaction>
    {
        public TransactionMapping()
        {
            Map(m => m.Date).TypeConverterOption.Format("yyyyMMdd").Name("Datum");
            Map(m => m.Description).Name("Naam / Omschrijving");
            Map(m => m.Accountnumber).Name("Rekening");
            Map(m => m.ContraAccountnumber).Name("Tegenrekening");
            Map(m => m.Code).Name("Code");
            Map(m => m.TransactionDirection).Name("Af Bij").TypeConverter<TransactionDirectionConverter>();
            Map(m => m.Amount).Name("Bedrag (EUR)");
            Map(m => m.MutationType).Name("Mutatiesoort");
            Map(m => m.Comments).Name("Mededelingen");
            Map(m => m.BalanceAfterMutation).Name("Saldo na mutatie");
            Map(m => m.Tag).Name("Tag");
        }
    }
}
