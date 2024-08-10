using UserManagement.Services;
using PLservice.Tests.Mockdata;
using Moq;
using UserManagement.Interfaces.Repo;

namespace PLservice.Tests.Services
{
    public class RoleServiceTest
    {
        private readonly RoleService _Service;
        private readonly Mock<IRoleRepository> _mockRepo;

        public RoleServiceTest()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _Service = new RoleService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetRoleById_ReturnsRoleName()
        {
            // Arrange
            var expectedRoles = RoleMockData.GetRoles();
            _mockRepo.Setup(repo => repo.GetRoles())
                     .ReturnsAsync(expectedRoles);

            // Act
            var result = await _Service.GetRoles();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRoles.Count, result.Count());
            Assert.Contains(result, u => u.Id == 1);
            Assert.Contains(result, u => u.Id == 2);
            Assert.Contains(result, u => u.Id == 3);
            Assert.Contains(result, u => u.Id == 4);
            _mockRepo.Verify(repo => repo.GetRoles(), Times.Once);
        }

    }
}