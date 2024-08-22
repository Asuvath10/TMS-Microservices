using System.Security.AccessControl;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Models;


namespace PLManagement.Interfaces.Repos;

public interface IPLRepository
{
    Task<IEnumerable<ProposalLetter>> GetAllProposalLetter();
    Task<IEnumerable<ProposalLetter>> GetAllProposalLettersByUserId(int userId);
    Task<ProposalLetter> GetProposalLetterById(int id);
    Task<int> CreateProposalLetter(ProposalLetter proposalLetter);
    Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter);
    Task<bool> DeleteProposalLetter(int id);
    Task<IEnumerable<ProposalLetter>> GetAllProposalLettersByStatusId(int statusId);
}
