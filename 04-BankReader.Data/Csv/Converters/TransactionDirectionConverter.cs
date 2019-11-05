﻿using System;
using BankReader.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace BankReader.Data.Csv.Converters
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