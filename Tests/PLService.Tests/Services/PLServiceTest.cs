using PLservice.Tests.MockData;
using PLManagement.Services;
using Moq;
using PLManagement.Interfaces.Repos;
using TMS.Models;
using PLManagement.Interfaces;

namespace PLservice.Tests.Services
{
    public class PLServiceTest
    {
        private readonly PLService _service;
        private readonly Mock<IPLRepository> _mockRepo;
        private readonly Mock<IApiGatewayService> _mockapigatewayservice;

        public PLServiceTest()
        {
            _mockRepo = new Mock<IPLRepository>();
            _mockapigatewayservice = new Mock<IApiGatewayService>();

            _service = new PLService(_mockRepo.Object, _mockapigatewayservice.Object);
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
            _mockRepo.Setup(repo => repo.CreateProposalLetter(proposalLetter)).ReturnsAsync(proposalLetter.Id);

            //ACT
            var result = await _service.CreateProposalLetter(proposalLetter);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(proposalLetter.Id, result);
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
        [Fact]
        public async Task AddSignatureAsync_Success()
        {
            // Arrange
            var proposalLetterId = 1;
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            proposalLetter.PlstatusId = 4;
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync(proposalLetter);
            var signature = new Byte[] { 1, 2, 3, 4, 5 };
            var signatureUrl = "https://storage.googleapis.com/fake-bucket/signatures/some-signature";
            _mockapigatewayservice.Setup(s => s.UploadFile(It.IsAny<string>(), It.IsAny<byte[]>(), "image/png"))
                .ReturnsAsync(signatureUrl);

            // Act
            var result = await _service.AddSignatureAsync(proposalLetterId, signature);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ApproverSignUrl);
            Assert.Equal(5, result.PlstatusId);
            _mockRepo.Verify(repo => repo.UpdateProposalLetter(result), Times.Once);
        }

        [Fact]
        public async Task AddSignatureAsync_Failure_WhenProposalLetterNotFound()
        {
            // Arrange
            var proposalLetterId = 1;
            var signature = new Byte[] { 1, 2, 3, };
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync((ProposalLetter)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddSignatureAsync(proposalLetterId, signature));
        }

        [Fact]
        public async Task AddSignatureAsync_Failure_WhenProposalLetterStatusisNotinPendingApproval()
        {
            // Arrange
            var proposalLetterId = 1;
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            proposalLetter.PlstatusId = 3; // Status is not pending approval
            var signature = new Byte[] { 1, 2, 3 };
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync(proposalLetter);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddSignatureAsync(proposalLetterId, signature));
        }

        [Fact]
        public async Task AddPdf_Success()
        {
            // Arrange
            var proposalLetterId = 1;
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            proposalLetter.PlstatusId = 5; // Ensure status is approved
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync(proposalLetter);

            Byte[] pdfData = new byte[] { 1, 2, 3, 4 };
            _mockapigatewayservice.Setup(ps => ps.GeneratePDF(It.IsAny<int>()))
                .ReturnsAsync(pdfData);

            var pdfUrl = "https://storage.googleapis.com/fake-bucket/pdfs/some-pdf";
            //Sample url for approver sign
            proposalLetter.ApproverSignUrl = pdfUrl;
            _mockapigatewayservice.Setup(s => s.UploadFile("pdfs", pdfData, "application/pdf"))
                .ReturnsAsync(pdfUrl);

            // Act
            var result = await _service.AddPdf(proposalLetterId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pdfUrl, result.PdfUrl);
            _mockRepo.Verify(repo => repo.UpdateProposalLetter(result), Times.Once);
        }

        [Fact]
        public async Task AddPdf_Failure_WhenProposalLetterNotFound()
        {
            // Arrange
            var proposalLetterId = 1;
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync((ProposalLetter)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddPdf(proposalLetterId));
        }

        [Fact]
        public async Task AddPdf_Failure_WhenStatusNotApproved()
        {
            // Arrange
            var proposalLetterId = 1;
            var proposalLetter = PLMockData.GetSingleProposalLetter();
            proposalLetter.PlstatusId = 4; // Status is not approved
            _mockRepo.Setup(repo => repo.GetProposalLetterById(proposalLetterId))
                .ReturnsAsync(proposalLetter);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.AddPdf(proposalLetterId));
        }
    }
}