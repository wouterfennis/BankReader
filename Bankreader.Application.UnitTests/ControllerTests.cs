using AutoFixture;
using Bankreader.Application;
using Bankreader.Application.Interfaces;
using Bankreader.Application.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankReader.ConsoleHost.UnitTest
{
    [TestClass]
    public class ControllerTests
    {
        private Mock<IHouseholdService> _householdServiceMock;
        private Mock<IHousekeepingBookWriter> _housekeepingBookWriterMock;
        private Fixture _fixture;
        private Controller _sut;

        [TestInitialize]
        public void Initalize()
        {
            _householdServiceMock = new Mock<IHouseholdService>();
            _housekeepingBookWriterMock = new Mock<IHousekeepingBookWriter>();
            _fixture = new Fixture();
            _sut = new Controller(_householdServiceMock.Object, _housekeepingBookWriterMock.Object);
        }

        [TestMethod]
        public void Run_WithHouseholdPosts_WritesHouseholdBook()
        {
            var expectedBook = _fixture.Create<HouseholdBook>();
            _householdServiceMock
                .Setup(x => x.CreateHouseholdBook())
                .Returns(expectedBook);

            _sut.Run();

            _housekeepingBookWriterMock.Verify(x => x.WriteAsync(expectedBook), Times.Once);
        }
    }
}
