using System.Security.AccessControl;
using PLManagement.Models;
using PLManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLManagement.Repositories;

public class PLRepository : IPLRepository
{
    private readonly PLManagementContext _dbContext;
    public PLRepository(PLManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<ProposalLetter> GetAllPL()
    {
        return _dbContext.ProposalLetters;
    }

    public async Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter){
        _dbContext.ProposalLetters.Add(proposalLetter);
        await _dbContext.SaveChangesAsync();
        return proposalLetter;
    }
}
