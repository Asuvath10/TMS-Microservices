using System.Security.AccessControl;
using PLManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLManagement.Interfaces;

public interface IPLRepository
{
    IEnumerable<ProposalLetter> GetAllPL();
    Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter);
}
