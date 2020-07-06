using AutoFixture;
using BankReader.Data.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankReader.Data.UnitTests.Models
{
    [TestClass]
    public class MonthTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Create_WithValidMaximalMonth_CreatesMonthType()
        {
            int validMonth = 12;

            Month Month = validMonth;

            Month.Value.Should().Be(validMonth);
        }

        [TestMethod]
        public void Create_WithValidMinimalMonth_CreatesMonthType()
        {
            int validMonth = 1;

            Month Month = validMonth;

            Month.Value.Should().Be(validMonth);
        }

        [TestMethod]
        public void Create_WithMonthUnderMinimal_ThrowsException()
        {
            int validMonth = 0;

            Action action = () => { Month.FromInt32(validMonth); };

            action.Should()
                .Throw<ArgumentException>();
        }

        [TestMethod]
        public void Create_WithMonthAboveMaximum_ThrowsException()
        {
            int validMonth = 13;

            Action action = () => { Month.FromInt32(validMonth); };

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}
