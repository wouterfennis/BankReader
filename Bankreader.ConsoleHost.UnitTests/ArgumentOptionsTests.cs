using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bankreader.ConsoleHost.UnitTests
{
    [TestClass]
    public class ArgumentOptionsTests
    {
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Constructor_WithTooFewArguments_ThrowsException()
        {
            var arguments = _fixture.CreateMany<string>(2);

            Action action = () => new ArgumentOptions(arguments);

            action.Should().Throw<ArgumentException>().WithMessage("There aren't exactly 3 arguments passed*");
        }

        [TestMethod]
        public void Constructor_WithTooManyArguments_ThrowsException()
        {
            var arguments = _fixture.CreateMany<string>(4);

            Action action = () => new ArgumentOptions(arguments);

            action.Should().Throw<ArgumentException>().WithMessage("There aren't exactly 3 arguments passed*");
        }

        [TestMethod]
        public void Constructor_WithEnoughArguments_MakesArgumentsAvailableThroughFields()
        {
            var expectedTransactionsLocation = _fixture.Create<string>();
            var expectedCategoryRulesLocation = _fixture.Create<string>();
            var expectedWorkbookLocation = _fixture.Create<string>();

            var result = new ArgumentOptions(new[] {
                expectedTransactionsLocation,
                expectedCategoryRulesLocation,
                expectedWorkbookLocation });

            result.TransactionsLocation.Value.Should().Be(expectedTransactionsLocation);
            result.CategoryRulesLocation.Value.Should().Be(expectedCategoryRulesLocation);
            result.WorkbookLocation.Value.Should().Be(expectedWorkbookLocation);
        }
    }
}
