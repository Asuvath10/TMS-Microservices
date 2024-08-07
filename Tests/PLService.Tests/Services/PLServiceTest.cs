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
        public async Task CanCreateProposalLetter()
        {
            //Arrange
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            _mockRepo.Setup(repo => repo.CreateProposalLetter(proposalLetter)).ReturnsAsync(proposalLetter);

            //ACT
            var result =await _service.CreateProposalLetter(proposalLetter);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(proposalLetter.Id, result.Id);
            Assert.Equal(proposalLetter.UserId, result.UserId);
            Assert.Equal(proposalLetter.AssessmentYear, result.AssessmentYear);
            Assert.Equal(proposalLetter.PlstatusId, result.PlstatusId);
            Assert.Equal(proposalLetter.Content, result.Content);
        }
    }
}