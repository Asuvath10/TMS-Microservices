using PLManagement.Services;
using Moq;
using PLManagement.Interfaces.Repos;
using TMS.Models;
using PLManagement.Interfaces;
using PLManagement.Interfaces.services;
using PLservice.Tests.MockData;

namespace Formservice.Tests.Services
{
    public class FormServiceTest
    {
        private readonly IFormService _service;
        private readonly Mock<IFormRepository> _mockFormRepo;

        public FormServiceTest()
        {
            _mockFormRepo = new Mock<IFormRepository>();
            _service = new FormService(_mockFormRepo.Object);
        }

        [Fact]
        public async Task GetAllFormsByPLId_ReturnsAllFormsforthePLId()
        {
            // Arrange
            int PLId = 17;
            var expectedForms = PLMockData.GetallFormsByPLId();
            _mockFormRepo.Setup(repo => repo.GetAllFormsByPLId(PLId))
                     .ReturnsAsync(expectedForms);

            // Act
            var result = await _service.GetAllFormsByPLId(PLId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedForms.Count, result.Count());
            _mockFormRepo.Verify(repo => repo.GetAllFormsByPLId(PLId), Times.Once);
        }
        [Fact]
        public async Task GetFormById_ReturnsForm()
        {
            // Arrange
            var expectedForm = PLMockData.GetFormById();
            _mockFormRepo.Setup(repo => repo.GetFormById(expectedForm.Id))
                     .ReturnsAsync(expectedForm);

            // Act
            var result = await _service.GetFormById(expectedForm.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedForm.Id, result.Id);
            _mockFormRepo.Verify(repo => repo.GetFormById(expectedForm.Id), Times.Once);
        }

        [Fact]
        public async Task CanCreateForm()
        {
            //Arrange
            var Form = PLMockData.GetFormById();
            _mockFormRepo.Setup(repo => repo.CreateForm(Form)).ReturnsAsync(Form.Id);

            //ACT
            var result = await _service.CreateForm(Form);

            //Assert
            Assert.Equal(Form.Id, result);
        }

        [Fact]
        public async Task CanUpdateForm()
        {
            // Arrange
            var existingForm = PLMockData.GetFormById();

            _mockFormRepo.Setup(repo => repo.GetFormById(existingForm.Id))
                     .ReturnsAsync(existingForm);

            // Act
            await _service.UpdateForm(existingForm);

            // Assert
            _mockFormRepo.Verify(repo => repo.UpdateForm(existingForm), Times.Once);
        }

        [Fact]
        public async Task CanDeleteForm_ReturnsTrue_WhenFormExists()
        {
            // Arrange
            var existingFormId = 1;

            _mockFormRepo.Setup(repo => repo.DeleteForm(existingFormId))
                     .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteForm(existingFormId);

            // Assert
            Assert.True(result);
            _mockFormRepo.Verify(repo => repo.DeleteForm(existingFormId), Times.Once);
        }

        [Fact]
        public async Task DeleteFormAsync_ReturnsFalse_WhenFormDoesNotExist()
        {
            // Arrange
            var nonExistingFormId = 99;

            _mockFormRepo.Setup(repo => repo.DeleteForm(nonExistingFormId))
                     .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteForm(nonExistingFormId);

            // Assert
            Assert.False(result);
            _mockFormRepo.Verify(repo => repo.DeleteForm(nonExistingFormId), Times.Once);
        }
    }
}