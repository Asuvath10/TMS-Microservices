using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Models;
using PLManagement.Interfaces.Repos;
using PLManagement.Interfaces.services;
using System;
using System.IO;
using System.Linq;

namespace PLManagement.Services
{
    public class PLService : IPLService
    {
        private readonly IPLRepository _repo;

        private readonly IFirebaseStorageService _storageService;
        private readonly IPDFGenerationService _pdfGenerationService;
        public PLService(IPLRepository repo, IFirebaseStorageService storageService, IPDFGenerationService pdfGenerationService)
        {
            _repo = repo;
            _storageService = storageService;
            _pdfGenerationService = pdfGenerationService;
        }

        public async Task<IEnumerable<ProposalLetter>> GetAllPLservice()
        {
            return await _repo.GetAllProposalLetter();
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
        public async Task<ProposalLetter> AddSignatureAsync(int proposalLetterId)
        {
            var proposalLetter = await _repo.GetProposalLetterById(proposalLetterId);
            // Statusid 4 is Pending Approval
            if (proposalLetter == null || proposalLetter.PlstatusId != 4)
            {
                throw new InvalidOperationException("Proposal letter not found or status is not pending approval.");
            }
            var image = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "sign.png");
            var signature = await File.ReadAllBytesAsync(image);
            var signatureUrl = await _storageService.UploadFileAsync("signatures", signature, "image/png");
            proposalLetter.ProposalApprovals.Add(new ProposalApproval
            {
                ApproverSign = signatureUrl,
                ApprovedOn = DateTime.UtcNow
            });

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

            // Generate Password-Protected PDF using the user's password
            var pdfData = _pdfGenerationService.GeneratePdf(proposalLetter, "userName", "abcd", proposalLetter.ProposalApprovals.FirstOrDefault()?.ApproverSign);

            // Upload PDF to Firebase
            var pdfUrl = await _storageService.UploadFileAsync("pdfs", pdfData, "application/pdf");

            // Check for already it is approved
            var Approval = proposalLetter.ProposalApprovals.FirstOrDefault();
            if (Approval != null)
            {
                Approval.Pdf = pdfUrl;
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