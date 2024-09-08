using UserManagement.Services;
using PLservice.Tests.Mockdata;
using Moq;
using UserManagement.Interfaces.Repo;

namespace PLservice.Tests.Services
{
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _mockRepo;

        public UserServiceTest()
        {
            _mockRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var expectedUsers = UserMockData.GetUsers();
            _mockRepo.Setup(repo => repo.GetAllUsers())
                     .ReturnsAsync(expectedUsers);

            // Act
            var result = await _userService.GetAllUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers.Count, result.Count());
            Assert.Contains(result, u => u.Id == 1);
            Assert.Contains(result, u => u.Id == 2);
            _mockRepo.Verify(repo => repo.GetAllUsers(), Times.Once);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser()
        {
            // Arrange
            var expectedUser = UserMockData.GetSingleUser();
            _mockRepo.Setup(repo => repo.GetUserById(expectedUser.Id))
                     .ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserById(expectedUser.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUser.Id, result.Id);
            Assert.Equal(expectedUser.Name, result.Name);
            _mockRepo.Verify(repo => repo.GetUserById(expectedUser.Id), Times.Once);
        }

        [Fact]
        public async Task CanCreateUser()
        {
            //Arrange
            var NewUser = UserMockData.GetSingleUser();
            _mockRepo.Setup(repo => repo.CreateUser(NewUser)).ReturnsAsync(NewUser.Id);

            //ACT
            var result = await _userService.CreateUser(NewUser);

            //Assert
            Assert.Equal(NewUser.Id, result);
        }

        [Fact]
        public async Task CanUpdateUser()
        {
            // Arrange
            var existingUser = UserMockData.GetSingleUser();

            _mockRepo.Setup(repo => repo.GetUserById(existingUser.Id))
                     .ReturnsAsync(existingUser);

            // Act
            await _userService.UpdateUser(existingUser);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateUser(existingUser), Times.Once);
        }

        [Fact]
        public async Task CanDisableUser_ReturnsTrue_WhenUserExists()
        {
            // Arrange
            var existingUserId = 1;
            _mockRepo.Setup(repo => repo.DisableUser(existingUserId))
                     .ReturnsAsync(true);

            // Act
            var result = await _userService.DisableUser(existingUserId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(repo => repo.DisableUser(existingUserId), Times.Once);
        }

        [Fact]
        public async Task DisableUser_ReturnsFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var nonExistingUserId = 99;

            _mockRepo.Setup(repo => repo.DisableUser(nonExistingUserId))
                     .ReturnsAsync(false);

            // Act
            var result = await _userService.DisableUser(nonExistingUserId);

            // Assert
            Assert.False(result);
            _mockRepo.Verify(repo => repo.DisableUser(nonExistingUserId), Times.Once);
        }
    }
}