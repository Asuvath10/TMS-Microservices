using System.Security.AccessControl;
using PLManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLManagement.Interfaces.Repos;

public interface IPLRepository
{
    Task<IEnumerable<ProposalLetter>> GetAllProposalLetter();
    Task<ProposalLetter> GetProposalLetterById(int id);
    Task<int> CreateProposalLetter(ProposalLetter proposalLetter);
    Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
    Task<bool> DeleteProposalLetter(int id);
}
