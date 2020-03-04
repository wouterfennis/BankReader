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

namespace BankReader.Data.UnitTests.Json
{
    [TestClass]
    public class JsonReaderTests
    {
        private Mock<ITextStreamFactory> _textStreamFactoryMock;
        private JsonRuleReader _jsonReader;
        private JsonCategoryRulesBuilder _testdataBuilder;

        [TestInitialize]
        public void Initialize()
        {
            _testdataBuilder = new JsonCategoryRulesBuilder();

            _textStreamFactoryMock = new Mock<ITextStreamFactory>();
            _jsonReader = new JsonRuleReader(_textStreamFactoryMock.Object);
        }

        [TestMethod]
        public void ReadRules_WithValidJSON_ReturnsRules()
        {
            // Arrange
            string expectedPath = "D:/somePath";

            string json = _testdataBuilder.AddTaxRule()
                                         .Build();
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            // Act
            var result = _jsonReader.ReadRules(expectedPath);

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
            string expectedPath = "D:/somePath";

            string json = "invalidJSON";
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            // Act
            Action action = () => _jsonReader.ReadRules(expectedPath);

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}
