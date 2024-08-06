using PLManagement.Models;
using PLManagement.Interfaces;
using System.Collections.Generic;

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
}
