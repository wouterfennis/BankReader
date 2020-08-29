using AutoFixture;
using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Bankreader.FileSystem.Json;
using Bankreader.Infrastructure.Files;
using BankReader.Data.UnitTests.TestdataBuilders;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Linq;

namespace Bankreader.FileSystem.UnitTests.Json
{
    [TestClass]
    public class JsonCategoryRuleReaderTests
    {
        private Mock<ITextStreamFactory> _textStreamFactoryMock;
        private Mock<IFileLocationProvider> _fileLocationProvider;
        private JsonCategoryRuleReader _sut;
        private Fixture _fixture;
        private JsonCategoryRulesBuilder _testdataBuilder;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _testdataBuilder = new JsonCategoryRulesBuilder();

            _textStreamFactoryMock = new Mock<ITextStreamFactory>();
            _fileLocationProvider = new Mock<IFileLocationProvider>();
            _sut = new JsonCategoryRuleReader(_fileLocationProvider.Object, _textStreamFactoryMock.Object);
        }

        [TestMethod]
        public void ProvideRules_WithValidJson_ReturnsRules()
        {
            var expectedPath = new Mock<IFileInfoWrapper>().Object;
            _fileLocationProvider
                .Setup(x => x.GetCategoryRulesLocation())
                .Returns(expectedPath);
            string json = _testdataBuilder
                .AddTaxRule()
                .Build();
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            var result = _sut.ProvideRules();

            result.Should().ContainSingle();
            var categoryRule = result.Single();
            categoryRule.Category.Should().Be(Category.Tax);
            categoryRule.DescriptionMatches.Should().ContainSingle();
            var description = categoryRule.DescriptionMatches.Single();
            description.Should().Be("belasting");
        }

        [TestMethod]
        public void ReadRules_WithInvalidJson_ThrowsException()
        {
            var expectedPath = new Mock<IFileInfoWrapper>().Object;
            _fileLocationProvider
                .Setup(x => x.GetCategoryRulesLocation())
                .Returns(expectedPath);

            string json = "invalidJson";
            var stringReader = new StringReader(json);

            _textStreamFactoryMock
                .Setup(mock => mock.Create(expectedPath))
                .Returns(stringReader);

            Action action = () => _sut.ProvideRules();

            action.Should().Throw<Exception>();
        }
    }
}
