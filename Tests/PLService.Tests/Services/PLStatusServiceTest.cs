using PLservice.Tests.Mockdata;
using Moq;
using PLManagement.Services;
using PLManagement.Interfaces.Repos;

namespace PLservice.Tests.Services
{
    public class PLStatusServiceTest
    {
        private readonly PLStatusService _Service;
        private readonly Mock<IPLStatusRepository> _mockRepo;

        public PLStatusServiceTest()
        {
            _mockRepo = new Mock<IPLStatusRepository>();
            _Service = new PLStatusService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetPLStatuses_ReturnsALLStatuses()
        {
            // Arrange
            var expectedPLStatus = PLStatusMockData.GetPlstatuses();
            _mockRepo.Setup(repo => repo.GetPLStatuses())
                     .ReturnsAsync(expectedPLStatus);

            // Act
            var result = await _Service.GetPLStatuses();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPLStatus.Count, result.Count());
            Assert.Contains(result, u => u.Id == 1);
            Assert.Contains(result, u => u.Id == 2);
            Assert.Contains(result, u => u.Id == 3);
            Assert.Contains(result, u => u.Id == 4);
            Assert.Contains(result, u => u.Id == 5);
            _mockRepo.Verify(repo => repo.GetPLStatuses(), Times.Once);
        }

    }
}