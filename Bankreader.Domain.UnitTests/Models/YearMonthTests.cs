using AutoFixture;
using Bankreader.Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankReader.Shared.UnitTests.Models
{
    [TestClass]
    public class YearMonthTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Create_WithValidYearAndMonth_CreatesYearMonthType()
        {
            int validYear = 2100;
            int validMonth = 12;

            YearMonth yearMonth = new YearMonth(validYear, validMonth);

            yearMonth.Year.Value.Should().Be(validYear);
            yearMonth.Month.Value.Should().Be(validMonth);
        }

        [TestMethod]
        public void Create_WithInValidYear_ThrowsException()
        {
            int invalidYear = 2101;
            int month = 12;

            Action action = () => { new YearMonth(invalidYear, month); };

            action.Should()
                .Throw<ArgumentException>();
        }

        [TestMethod]
        public void Create_WithInValidMonth_ThrowsException()
        {
            int year = 2100;
            int invalidMonth = 13;

            Action action = () => { new YearMonth(year, invalidMonth); };

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}
