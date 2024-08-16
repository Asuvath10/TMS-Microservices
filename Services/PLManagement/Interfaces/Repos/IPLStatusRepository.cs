using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Models;


namespace PLManagement.Interfaces.Repos;

public interface IPLStatusRepository
{
    Task<IEnumerable<Plstatus>> GetPLStatuses();
}
