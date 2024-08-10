using PLManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PLManagement.Interfaces.Repos;

public interface IPLStatusRepository
{
    Task<IEnumerable<Plstatus>> GetPLStatuses();
}
