using AutoFixture;
using BankReader.ConsoleHost.Interfaces;
using BankReader.Shared.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankReader.ConsoleHost.UnitTest
{
    [TestClass]
    public class ApplicationTests
    {
        private Mock<IHouseholdService> _householdService;
        private Mock<IHouseholdBookWriter> _householdBookWriter;
        private Fixture _fixture;
        private Application _sut;

        [TestInitialize]
        public void Initalize()
        {
            _householdService = new Mock<IHouseholdService>();
            _householdBookWriter = new Mock<IHouseholdBookWriter>();
            _fixture = new Fixture();
            _sut = new Application(_householdService.Object, _householdBookWriter.Object);
        }

        [TestMethod]
        public void Run_WithHouseholdPosts_WritesHouseholdBook()
        {
            var expectedBook = _fixture.Create<HouseholdBook>();
            _householdService
                .Setup(x => x.CreateHouseholdBook())
                .Returns(expectedBook);

            _sut.Run();

            _householdBookWriter.Verify(x => x.WriteAsync(expectedBook), Times.Once);
        }
    }
}
