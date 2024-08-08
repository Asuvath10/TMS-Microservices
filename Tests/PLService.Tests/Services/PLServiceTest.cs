using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using PLservice.Tests.MockData;
using PLManagement.Services;
using PLManagement.Interfaces;
using PLManagement.Models;
using Moq;

namespace PLservice.Tests.Services
{
    public class PLServiceTest
    {
        private readonly PLService _service;
        private readonly Mock<IPLRepository> _mockRepo;

        public PLServiceTest()
        {
            _mockRepo = new Mock<IPLRepository>();
            _service = new PLService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllProposalLetters_ReturnsAllProposalLetters()
        {
            // Arrange
            var expectedProposalLetters = PLMockData.GetProposalLetters();
            _mockRepo.Setup(repo => repo.GetAllProposalLetter())
                     .ReturnsAsync(expectedProposalLetters);

            // Act
            var result = await _service.GetAllPLservice();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProposalLetters.Count, result.Count());
            Assert.Contains(result, pl => pl.Id == 1);
            Assert.Contains(result, pl => pl.Id == 2);
            _mockRepo.Verify(repo => repo.GetAllProposalLetter(), Times.Once);
        }

        [Fact]
        public async Task GetProposalLetterById_ReturnsProposalLetter()
        {
            // Arrange
            var expectedProposalLetter = PLMockData.GetSingleProposalLetter();
            _mockRepo.Setup(repo => repo.GetProposalLetterById(expectedProposalLetter.Id))
                     .ReturnsAsync(expectedProposalLetter);

            // Act
            var result = await _service.GetPLById(expectedProposalLetter.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProposalLetter.Id, result.Id);
            Assert.Equal(expectedProposalLetter.UserId, result.UserId);
            _mockRepo.Verify(repo => repo.GetProposalLetterById(expectedProposalLetter.Id), Times.Once);
        }

        [Fact]
        public async Task CanCreateProposalLetter()
        {
            //Arrange
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            _mockRepo.Setup(repo => repo.CreateProposalLetter(proposalLetter)).ReturnsAsync(proposalLetter);

            //ACT
            var result = await _service.CreateProposalLetter(proposalLetter);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(proposalLetter.Id, result.Id);
            Assert.Equal(proposalLetter.UserId, result.UserId);
            Assert.Equal(proposalLetter.AssessmentYear, result.AssessmentYear);
            Assert.Equal(proposalLetter.PlstatusId, result.PlstatusId);
            Assert.Equal(proposalLetter.Content, result.Content);
        }

        [Fact]
        public async Task CanUpdateProposalLetter()
        {
            // Arrange
            var existingProposalLetter = PLMockData.GetSingleProposalLetter();

            _mockRepo.Setup(repo => repo.GetProposalLetterById(existingProposalLetter.Id))
                     .ReturnsAsync(existingProposalLetter);

            // Act
            await _service.UpdateProposalLetter(existingProposalLetter);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateProposalLetter(existingProposalLetter), Times.Once);
        }

        [Fact]
        public async Task CanDeleteProposalLetter_ReturnsTrue_WhenProposalLetterExists()
        {
            // Arrange
            var existingProposalLetterId = 1;

            _mockRepo.Setup(repo => repo.DeleteProposalLetter(existingProposalLetterId))
                     .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteProposalLetter(existingProposalLetterId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(repo => repo.DeleteProposalLetter(existingProposalLetterId), Times.Once);
        }

        [Fact]
        public async Task DeleteProposalLetterAsync_ReturnsFalse_WhenProposalLetterDoesNotExist()
        {
            // Arrange
            var nonExistingProposalLetterId = 99;

            _mockRepo.Setup(repo => repo.DeleteProposalLetter(nonExistingProposalLetterId))
                     .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteProposalLetter(nonExistingProposalLetterId);

            // Assert
            Assert.False(result);
            _mockRepo.Verify(repo => repo.DeleteProposalLetter(nonExistingProposalLetterId), Times.Once);
        }
    }
}