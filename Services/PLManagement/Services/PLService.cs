using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Interfaces.Repos;
using PLManagement.Interfaces.services;
using System;
using System.IO;
using System.Linq;
using TMS.Models;
using PLManagement.Interfaces;

namespace PLManagement.Services
{
    public class PLService : IPLService
    {
        private readonly IPLRepository _repo;
        private readonly IApiGatewayService _apigatewayService;
        public PLService(IPLRepository repo, IApiGatewayService apiGatewayService)
        {
            _repo = repo;
            _apigatewayService = apiGatewayService;
        }

        public async Task<IEnumerable<ProposalLetter>> GetAllPLservice()
        {
            return await _repo.GetAllProposalLetter();
        }

        public async Task<IEnumerable<ProposalLetter>> GetAllPLsByUserId(int userId)
        {
            return await _repo.GetAllProposalLettersByUserId(userId);
        }
        public async Task<IEnumerable<ProposalLetter>> GetAllPLsByStatusId(int statusId)
        {
            return await _repo.GetAllProposalLettersByStatusId(statusId);
        }

        public async Task<ProposalLetter> GetPLById(int id)
        {
            return await _repo.GetProposalLetterById(id);
        }

        public async Task<int> CreateProposalLetter(ProposalLetter proposalLetter)
        {
            return await _repo.CreateProposalLetter(proposalLetter);
        }

        public async Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter)
        {
            return await _repo.UpdateProposalLetter(proposalLetter);
        }

        public async Task<bool> DeleteProposalLetter(int id)
        {
            return await _repo.DeleteProposalLetter(id);
        }
        public async Task<ProposalLetter> AddSignatureAsync(int proposalLetterId, Byte[] signature)
        {
            var proposalLetter = await _repo.GetProposalLetterById(proposalLetterId);
            // Statusid 4 is Pending Approval
            if (proposalLetter == null || proposalLetter.PlstatusId != 4)
            {
                throw new InvalidOperationException("Proposal letter not found or status is not pending approval.");
            }
            // var image = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "sign.png");
            // var signatured = await File.ReadAllBytesAsync(image);

            var signatureUrl = await _apigatewayService.UploadFile("signatures", signature, "image/png");
            proposalLetter.ApproverSignUrl = signatureUrl;

            proposalLetter.PlstatusId = 5;

            await _repo.UpdateProposalLetter(proposalLetter);

            return proposalLetter;
        }

        public async Task<ProposalLetter> AddPdf(int proposalLetterId)
        {
            var proposalLetter = await _repo.GetProposalLetterById(proposalLetterId);


            if (proposalLetter == null || proposalLetter.PlstatusId != 5)
            {
                throw new InvalidOperationException("Proposal letter not found or status is not approved.");
            }

            // // Generate Password-Protected PDF using the user's password
            // var pdfData = await _apigatewayService.GeneratePDF(proposalLetterId);

            // Upload PDF to Firebase
            // var pdfUrl = await _apigatewayService.UploadFile("pdfs", pdfData, "application/pdf");

            // Check for already it is approved
            if (proposalLetter.ApproverSignUrl != null)
            {
                // proposalLetter.PdfUrl = pdfUrl;
                //Save the PDF URL.
                await _repo.UpdateProposalLetter(proposalLetter);
            }
            else
            {
                throw new InvalidOperationException($"No Proposal approval found for the Proposal Letter ID- {proposalLetterId}");
            }

            return proposalLetter;
        }
    }
}