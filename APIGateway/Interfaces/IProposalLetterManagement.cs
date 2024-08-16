using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace APIGateway.Interfaces
{
    public interface IProposalLetterManagement
    {
        Task<List<ProposalLetter>> GetAllProposals();
        Task<List<Plstatus>> GetAllStatuses();
        Task<ProposalLetter> GetProposalLetterById(int id);
        Task<int> CreateProposalLetter(ProposalLetter proposalLetter);
        Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
        Task<bool> DeleteProposalLetter(int id);
        Task<ProposalLetter> GeneratePdf(int proposalLetterId);
        Task<ProposalLetter> AddSignatureAsync(int proposalLetterId, byte[] signature);
    }
}