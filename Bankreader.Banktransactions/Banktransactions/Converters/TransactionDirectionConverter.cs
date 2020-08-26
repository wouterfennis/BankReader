using BankReader.Shared.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace Bankreader.Banktransactions.Converters
{
    public class TransactionDirectionConverter : ITypeConverter
    {
        private const string WithdrawnValue = "Af";
        private const string DepositValue = "Bij";

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            switch (text)
            {
                case WithdrawnValue:
                    return TransactionDirection.Af;
                case DepositValue:
                    return TransactionDirection.Bij;
                default:
                    throw new ArgumentException($"For value: '{text}' no transaction direction has been defined");
            }
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            return value.ToString();
        }
    }
}
