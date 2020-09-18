using AutoFixture;
using Bankreader.Application.Models;
using Bankreader.Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Bankreader.Application.UnitTests.Models
{
    [TestClass]
    public class HouseholdPostTests
    {
        private Fixture _fixture;
        private HouseholdPost _sut;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _sut = new HouseholdPost(_fixture.Create<Category>());
        }

        [TestMethod]
        public void AddTransaction_NoPredecessor_AddsTransactionToOriginalTransactions()
        {
            var expectedDescription = _fixture.Create<string>();
            var expectedAmount = _fixture.Create<decimal>();
            var expectedYearMonth = YearMonth.FromDateTime(_fixture.Create<DateTime>());
            var expectedTransactionDirection = _fixture.Create<TransactionDirection>();

            _sut.AddTransaction(expectedDescription, expectedAmount, expectedYearMonth, expectedTransactionDirection);

            _sut.OriginalTransactions.Should().ContainSingle();
            var transaction = _sut.OriginalTransactions.Single();
            transaction.Description.Should().Be(expectedDescription);
            transaction.Amount.Should().Be(expectedAmount);
            transaction.YearMonth.Should().Be(expectedYearMonth);
            transaction.TransactionDirection.Should().Be(expectedTransactionDirection);
        }

        [TestMethod]
        public void AddTransaction_NoCategoryPredecessor_AddsTransactionToTransactionsPerMonthYear()
        {
            var expectedAmount = _fixture.Create<decimal>();
            var expectedYearMonth = YearMonth.FromDateTime(_fixture.Create<DateTime>());
            var expectedTransactionDirection = _fixture.Create<TransactionDirection>();

            _sut.AddTransaction(_fixture.Create<string>(), expectedAmount, expectedYearMonth, expectedTransactionDirection);

            _sut.TransactionsPerMonthYear.Should().ContainSingle();
            var transaction = _sut.TransactionsPerMonthYear.Single();
            transaction.Amount.Should().Be(expectedAmount);
            transaction.YearMonth.Should().Be(expectedYearMonth);
            transaction.TransactionDirection.Should().Be(expectedTransactionDirection);
        }

        [TestMethod]
        public void AddTransaction_CategoryPredecessor_AddsAmountToExistingTransactionPerMonthYear()
        {
            var expectedAmount = _fixture.Create<decimal>();
            var expectedYearMonth = YearMonth.FromDateTime(_fixture.Create<DateTime>());
            var expectedTransactionDirection = _fixture.Create<TransactionDirection>();

            _sut.AddTransaction(_fixture.Create<string>(), expectedAmount, expectedYearMonth, expectedTransactionDirection);
            _sut.AddTransaction(_fixture.Create<string>(), expectedAmount, expectedYearMonth, expectedTransactionDirection);

            _sut.TransactionsPerMonthYear.Should().ContainSingle();
            var transaction = _sut.TransactionsPerMonthYear.Single();
            transaction.Amount.Should().Be(expectedAmount + expectedAmount);
        }

        [TestMethod]
        public void AddTransaction_CategoryPredecessor_AddsSecondTransactionToOriginalTransactions()
        {
            var firstEntryDescription = _fixture.Create<string>();
            var firstEntryAmount = _fixture.Create<decimal>();
            var firstEntryYearMonth = YearMonth.FromDateTime(_fixture.Create<DateTime>());
            var firstEntryTransactionDirection = _fixture.Create<TransactionDirection>();

            _sut.AddTransaction(firstEntryDescription, firstEntryAmount, firstEntryYearMonth, firstEntryTransactionDirection);

            var secondEntryDescription = _fixture.Create<string>();
            var secondEntryAmount = _fixture.Create<decimal>();
            var secondEntryYearMonth = YearMonth.FromDateTime(_fixture.Create<DateTime>());
            var secondEntryTransactionDirection = _fixture.Create<TransactionDirection>();

            _sut.AddTransaction(secondEntryDescription, secondEntryAmount, secondEntryYearMonth, secondEntryTransactionDirection);

            _sut.OriginalTransactions.Should().HaveCount(2);
            _sut.OriginalTransactions.Should().Contain(x => 
            x.Description == firstEntryDescription 
            && x.Amount == firstEntryAmount
            && x.TransactionDirection == firstEntryTransactionDirection 
            && x.YearMonth == firstEntryYearMonth);

            _sut.OriginalTransactions.Should().Contain(x =>
            x.Description == secondEntryDescription
            && x.Amount == secondEntryAmount
            && x.TransactionDirection == secondEntryTransactionDirection
            && x.YearMonth == secondEntryYearMonth);
        }
    }
}
