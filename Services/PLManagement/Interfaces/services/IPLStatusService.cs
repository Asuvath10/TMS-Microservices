using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Models;

namespace PLManagement.Interfaces.services
{
    public interface IPLStatusService
    {
        Task<IEnumerable<Plstatus>> GetPLStatuses();
    }
}