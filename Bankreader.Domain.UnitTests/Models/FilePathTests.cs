using Bankreader.Domain.Models;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Bankreader.Domain.UnitTests.Models
{
    [TestClass]
    public class FilePathTests
    {
        [TestInitialize]
        public void Initialize()
        {

        }

        [TestMethod]
        public void Create_WithEmptyString_ThrowsException()
        {
            Action action = () => FilePath.FromString(string.Empty);

            action.Should().Throw<ArgumentException>().WithMessage("The value was not a valid file path*");
        }

        [TestMethod]
        public void Create_WithNull_ThrowsException()
        {
            Action action = () => FilePath.FromString(null);

            action.Should().Throw<ArgumentException>().WithMessage("The value was not a valid file path*");
        }

        [TestMethod]
        public void Create_WithValidPath_ThrowsException()
        {
            var expectedPath = "/path/";
            var result = FilePath.FromString(expectedPath);

            result.Value.Should().Be(expectedPath);
        }
    }
}
