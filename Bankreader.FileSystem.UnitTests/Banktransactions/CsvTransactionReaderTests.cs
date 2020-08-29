using AutoFixture;
using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.FileSystem.UnitTests.Banktransactions.TestData;
using Bankreader.Infrastructure.Files;
using BankReader.Data.Csv;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bankreader.FileSystem.UnitTests.Banktransactions
{
    [TestClass]
    public class CsvTransactionReaderTests
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
        public void ProvideTransactions_WithValidCsv_ReturnsCorrectTransaction()
        {
            var expectedPath = new Mock<IFileInfoWrapper>().Object;
            _fileLocationProvider.Setup(x => x.GetTransactionsLocation())
                .Returns(expectedPath);
            var transaction = _fixture.Create<CsvTransaction>();
            var csv = transaction.ToString();

            var textStreamMock = new StringReader(csv);
            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(textStreamMock);

            IEnumerable<Banktransaction> result = _sut.ProvideTransactions();

            result.Should().HaveCount(1);
            var actualTransaction = result.ElementAt(0);
            actualTransaction.Date.Date.Should().Be(transaction.Date.Date);
        }
    }
}
