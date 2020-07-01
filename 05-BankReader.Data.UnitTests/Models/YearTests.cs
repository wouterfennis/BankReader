using AutoFixture;
using BankReader.Data.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankReader.Data.UnitTests.Models
{
    [TestClass]
    public class YearTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Create_WithValidMaximumYear_CreatesYearType()
        {
            int validYear = 2100;

            Year year = validYear;

            year.Value.Should().Be(validYear);
        }

        [TestMethod]
        public void Create_WithValidMinimumYear_CreatesYearType()
        {
            int validYear = 2000;

            Year year = validYear;

            year.Value.Should().Be(validYear);
        }

        [TestMethod]
        public void Create_WithYearUnderMinimum_ThrowsException()
        {
            int validYear = 1999;

            Action action = () => { Year year = validYear; };

            action.Should()
                .Throw<ArgumentException>();
        }

        [TestMethod]
        public void Create_WithYearAboveMaximum_ThrowsException()
        {
            int validYear = 2101;

            Action action = () => { Year year = validYear; };

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}
