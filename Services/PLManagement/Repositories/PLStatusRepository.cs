using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLManagement.Interfaces.Repos;
using TMS.Models;
using PLManagement.Models;


namespace PLManagement.Repositories
{
    public class PLStatusRepository : IPLStatusRepository
    {
        private readonly PLManagementContext _dbContext;
        public PLStatusRepository(PLManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Plstatus>> GetPLStatuses()
        {
            return await _dbContext.Plstatuses.AsNoTracking().ToListAsync();
        }
        public async Task<Plstatus> GetPLStatusById(int id)
        {
            return await _dbContext.Plstatuses.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}