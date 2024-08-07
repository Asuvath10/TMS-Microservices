using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Models;
using PLManagement.Interfaces;
using PLManagement.Repositories;

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

        public async Task<ProposalLetter> GetPLById(int id)
        {
            return await _repo.GetProposalLetterById(id);
        }

        public async Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter)
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