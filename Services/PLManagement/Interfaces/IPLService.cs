using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Models;

namespace PLManagement.Interfaces
{
    public interface IPLService
    {
        Task<IEnumerable<ProposalLetter>> GetAllPLservice();
        Task<ProposalLetter> GetPLById(int id);
        Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter);
        Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
        Task<bool> DeleteProposalLetter(int id);
    }
}