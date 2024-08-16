using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Models;

namespace PLManagement.Interfaces.services
{
    public interface IPLStatusService
    {
        Task<IEnumerable<Plstatus>> GetPLStatuses();
    }
}