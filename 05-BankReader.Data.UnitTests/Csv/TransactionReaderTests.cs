using AutoFixture;
using BankReader.Data.Csv;
using BankReader.Data.Csv.Models;
using BankReader.Data.Providers;
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
        private Mock<ITextStreamFactory> _textStreamFactoryMock;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private CsvTransactionReader _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _textStreamFactoryMock = new Mock<ITextStreamFactory>();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _sut = new CsvTransactionReader(_fileLocationProvider.Object, _textStreamFactoryMock.Object);
        }

        [TestMethod]
        public void ProvideTransactions_WithValidCSV_ReturnsCorrectTransaction()
        {
            // Arrange
            var expectedPath = new Mock<IFileInfoWrapper>().Object;
            _fileLocationProvider.Setup(x => x.GetTransactionsLocation())
                .Returns(expectedPath);
            var transaction = _fixture.Create<CsvTransaction>();
            var csv = $"{GetCsvHeader()}\r\n{transaction}";

            var textStreamMock = new StringReader(csv);
            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(textStreamMock);

            // Act
            IEnumerable<Banktransaction> result = _sut.ProvideTransactions();

            // Assert
            result.Should().HaveCount(1);
            var actualTransaction = result.ElementAt(0);
            actualTransaction.Date.Date.Should().Be(transaction.Date.Date);
        }

        private string GetCsvHeader()
        {
            return
                "\"Datum\",\"Naam / Omschrijving\",\"Rekening\"," +
                "\"Tegenrekening\",\"Code\",\"Af Bij\"," +
                "\"Bedrag (EUR)\",\"MutatieSoort\",\"Mededelingen\"";
        }
    }
}
