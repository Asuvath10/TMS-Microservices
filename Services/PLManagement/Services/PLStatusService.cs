using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Models;
using PLManagement.Interfaces.Repos;
using PLManagement.Interfaces.services;

namespace PLManagement.Services
{
    public class PLStatusService : IPLStatusService
    {
        private readonly IPLStatusRepository _repo;

        public PLStatusService(IPLStatusRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Plstatus>> GetPLStatuses()
        {
            return await _repo.GetPLStatuses();
        }
    }
}