using AutoFixture;
using Bankreader.Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BankReader.Shared.UnitTests.Models
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

            Action action = () => { Year.FromInt32(validYear); };

            action.Should()
                .Throw<ArgumentException>();
        }

        [TestMethod]
        public void Create_WithYearAboveMaximum_ThrowsException()
        {
            int validYear = 3001;

            Action action = () => { Year.FromInt32(validYear); };

            action.Should()
                .Throw<ArgumentException>();
        }
    }
}
