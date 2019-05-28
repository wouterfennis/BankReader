using BankReader.Data.Json;
using BankReader.Data.UnitTests.TestdataBuilders;
using BankReader.Data.Utilities;
using BankReader.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;

namespace BankReader.Data.UnitTests.Json
{
    [TestClass]
    public class JsonReaderTests
    {
        [TestMethod]
        public void ReadRules_WithValidJSON_ReturnsRules()
        {
            // Arrange
            string expectedPath = "path";

            var testdataBuilder = new JsonCategoryRulesBuilder();
            string json = testdataBuilder.AddTaxRule()
                                         .Build();
            var stringReader = new StringReader(json);
            var textStreamFactoryMock = new Mock<ITextStreamFactory>();

            textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            var jsonReader = new JsonReader(textStreamFactoryMock.Object);

            // Act
            var result = jsonReader.ReadRules(expectedPath);

            // Assert
            result.Should().HaveCount(1);
            var categoryRule = result.ElementAt(0);
            categoryRule.Category.Should().Be(Category.Tax);
            categoryRule.Descriptions.Should().HaveCount(1);
            var description = categoryRule.Descriptions.ElementAt(0);
            description.Should().Be("belasting");
        }
    }
}
