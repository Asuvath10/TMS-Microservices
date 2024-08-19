using System.Security.AccessControl;
using PLManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLManagement.Interfaces.Repos;
using System;
using TMS.Models;
using PLManagement.Models;
using System.Linq;

namespace PLManagement.Repositories;

public class PLRepository : IPLRepository
{
    private readonly PLManagementContext _dbContext;
    public PLRepository(PLManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ProposalLetter>> GetAllProposalLetter()
    {
        var proposal = _dbContext.ProposalLetters;
        return proposal;
    }
    public async Task<IEnumerable<ProposalLetter>> GetAllProposalLettersByUserId(int userId)
    {
        var proposal = _dbContext.ProposalLetters.Where(p => p.UserId == userId).ToList();
        return proposal;
    }

    public async Task<ProposalLetter> GetProposalLetterById(int id)
    {
        return await _dbContext.ProposalLetters.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> CreateProposalLetter(ProposalLetter proposalLetter)
    {
        _dbContext.ProposalLetters.Add(proposalLetter);
        await _dbContext.SaveChangesAsync();
        return proposalLetter.Id;
    }

    public async Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter)
    {
        _dbContext.ProposalLetters.Update(proposalLetter);
        await _dbContext.SaveChangesAsync();
        return proposalLetter;
    }

    public async Task<bool> DeleteProposalLetter(int id)
    {
        var proposalLetter = await _dbContext.ProposalLetters.FindAsync(id);
        if (proposalLetter == null)
        {
            return false;
        }
        _dbContext.ProposalLetters.Remove(proposalLetter);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
