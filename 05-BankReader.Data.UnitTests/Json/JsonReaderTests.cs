using BankReader.Data.Json;
using BankReader.Data.UnitTests.TestdataBuilders;
using BankReader.Data.Utilities;
using BankReader.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;
using BankReader.Data.Models;
using AutoFixture;
using BankReader.Data.Providers;

namespace BankReader.Data.UnitTests.Json
{
    [TestClass]
    public class JsonReaderTests
    {
        private Mock<ITextStreamFactory> _textStreamFactoryMock;
        private Mock<ICategoryRulesLocationProvider> _categoryRulesLocationProvider;
        private JsonCategoryRuleProvider _sut;
        private Fixture _fixture;
        private JsonCategoryRulesBuilder _testdataBuilder;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testdataBuilder = new JsonCategoryRulesBuilder();

            _textStreamFactoryMock = new Mock<ITextStreamFactory>();
            _categoryRulesLocationProvider = new Mock<ICategoryRulesLocationProvider>();
            _sut = new JsonCategoryRuleProvider(_categoryRulesLocationProvider.Object, _textStreamFactoryMock.Object);
        }

        [TestMethod]
        public void ProvideRules_WithValidJSON_ReturnsRules()
        {
            // Arrange
            string expectedPath = _fixture.Create<string>();
            _categoryRulesLocationProvider
                .Setup(x => x.GetCategoryRulesLocation())
                .Returns(expectedPath);
            string json = _testdataBuilder.AddTaxRule()
                                         .Build();
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            // Act
            var result = _sut.ProvideRules();

            // Assert
            result.Should().HaveCount(1);
            var categoryRule = result.ElementAt(0);
            categoryRule.Category.Should().Be(Category.Tax);
            categoryRule.Descriptions.Should().HaveCount(1);
            var description = categoryRule.Descriptions.ElementAt(0);
            description.Should().Be("belasting");
        }

        [TestMethod]
        public void ReadRules_WithInvalidJSON_ThrowsException()
        {
            // Arrange
            string expectedPath = _fixture.Create<string>();
            _categoryRulesLocationProvider
                .Setup(x => x.GetCategoryRulesLocation())
                .Returns(expectedPath);

            string json = "invalidJSON";
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            // Act
            Action action = () => _sut.ProvideRules();

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}
