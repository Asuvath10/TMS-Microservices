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
        public PLService(IPLRepository repo)
        {
            _repo = repo;
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
        public async Task<IEnumerable<ProposalLetter>> GetAllPLsByPreparerId(int preparerId)
        {
            return await _repo.GetAllProposalLettersByPreparerId(preparerId);
        }
        public async Task<IEnumerable<ProposalLetter>> GetAllPLsByReviewerId(int reviewerId)
        {
            return await _repo.GetAllProposalLettersByReviewerId(reviewerId);
        }
        public async Task<IEnumerable<ProposalLetter>> GetAllPLsByApproverId(int approverId)
        {
            return await _repo.GetAllProposalLettersByApproverId(approverId);
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
    }
}