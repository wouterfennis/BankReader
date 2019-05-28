using AutoFixture;
using BankReader.Data.Csv.Converters;
using BankReader.Models;
using CsvHelper;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BankReader.Data.UnitTests.Csv.Converters
{
    [TestClass]
    public class DirectionConverterTests
    {
        private TransactionDirectionConverter _transactionDirectionConverter;
        private Fixture _fixture;

        [TestInitialize]
        public void Initalize()
        {
            _fixture = new Fixture();
            _transactionDirectionConverter = new TransactionDirectionConverter();
        }

        [TestMethod]
        [DataRow("Bij", TransactionDirection.Bij)]
        [DataRow("Af", TransactionDirection.Af)]
        public void ConvertFromString_WithValidValue_ReturnsCorrectConvertion(string input, TransactionDirection expectedResult)
        {
            // Arrange
            var readerRow = new Mock<IReaderRow>();

            // Act
            var result = _transactionDirectionConverter.ConvertFromString(input, readerRow.Object, null);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void ConvertFromString_WithUnknownValue_ThrowsException()
        {
            // Arrange
            var readerRow = new Mock<IReaderRow>();
            string unknownValue = "UnknownValue";

            // Act
            Action action = () => _transactionDirectionConverter.ConvertFromString(unknownValue, readerRow.Object, null);

            // Assert
            action.Should().Throw<ArgumentException>().WithMessage($"For value: '{unknownValue}' no transaction direction has been defined");
        }

        [TestMethod]
        [DataRow(TransactionDirection.Bij, "Bij")]
        [DataRow(TransactionDirection.Af, "Af")]
        public void ConvertToString_WithValidValue_ReturnsCorrectConvertion(TransactionDirection input, string expectedResult)
        {
            // Arrange
            var writerRow = new Mock<IWriterRow>();

            // Act
            var result = _transactionDirectionConverter.ConvertToString(input, writerRow.Object, null);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
