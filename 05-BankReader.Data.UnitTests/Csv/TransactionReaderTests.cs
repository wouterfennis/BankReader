using AutoFixture;
using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Data.UnitTests.Csv.TestData;
using BankReader.Data.Utilities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankReader.Data.UnitTests.Csv
{
    [TestClass]
    public class TransactionReaderTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Read_WithValidCSV_ReturnsCorrectTransaction()
        {
            // Arrange
            var expectedPath = "path";
            var transaction = _fixture.Create<CsvTransaction>();
            var csv = getCsvHeader() + transaction;

            var textStreamFactoryMock = new Mock<ITextStreamFactory>();

            var textStreamMock = new StringReader(csv);
            textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(textStreamMock);

            var transactionReader = new CsvTransactionReader(textStreamFactoryMock.Object);

            // Act
            IEnumerable<Banktransaction> result = transactionReader.ReadTransactions(expectedPath);

            // Assert
            result.Should().HaveCount(1);
            var actualTransaction = result.ElementAt(0);
            actualTransaction.Date.Date.Should().Be(transaction.Date.Date);
        }

        private string getCsvHeader()
        {
            return
                "\"Datum\",\"Naam / Omschrijving\",\"Rekening\"," +
                "\"Tegenrekening\",\"Code\",\"Af Bij\"," +
                "\"Bedrag (EUR)\",\"MutatieSoort\",\"Mededelingen\"\r\n";
        }
    }
}
