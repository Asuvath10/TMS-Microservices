using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;

namespace APIGateway.Interfaces
{
    public interface IProposalLetterManagement
    {
        Task<List<ProposalLetter>> GetAllProposals();
        Task<List<Plstatus>> GetAllStatuses();
        Task<ProposalLetter> GetProposalLetterById(int id);
        Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter);
        Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
        Task<bool> DeleteProposalLetter(int id);
    }
}