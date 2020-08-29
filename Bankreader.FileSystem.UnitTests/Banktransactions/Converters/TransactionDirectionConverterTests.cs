using Bankreader.Banktransactions.Converters;
using Bankreader.Domain.Models;
using CsvHelper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Bankreader.FileSystem.UnitTests.Banktransactions.Converters
{
    [TestClass]
    public class TransactionDirectionConverterTests
    {
        private TransactionDirectionConverter _transactionDirectionConverter;

        [TestInitialize]
        public void Initialize()
        {
            _transactionDirectionConverter = new TransactionDirectionConverter();
        }

        [TestMethod]
        [DataRow("Bij", TransactionDirection.Bij)]
        [DataRow("Af", TransactionDirection.Af)]
        public void ConvertFromString_WithValidValue_ReturnsCorrectConvertion(string input, TransactionDirection expectedResult)
        {
            var readerRow = new Mock<IReaderRow>();

            var result = _transactionDirectionConverter.ConvertFromString(input, readerRow.Object, null);

            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void ConvertFromString_WithUnknownValue_ThrowsException()
        {
            var readerRow = new Mock<IReaderRow>();
            string unknownValue = "UnknownValue";

            Action action = () => _transactionDirectionConverter.ConvertFromString(unknownValue, readerRow.Object, null);

            action.Should().Throw<ArgumentException>().WithMessage($"For value: '{unknownValue}' no transaction direction has been defined");
        }

        [TestMethod]
        [DataRow(TransactionDirection.Bij, "Bij")]
        [DataRow(TransactionDirection.Af, "Af")]
        public void ConvertToString_WithValidValue_ReturnsCorrectConvertion(TransactionDirection input, string expectedResult)
        {
            var writerRow = new Mock<IWriterRow>();

            var result = _transactionDirectionConverter.ConvertToString(input, writerRow.Object, null);

            result.Should().Be(expectedResult);
        }
    }
}
