using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Interfaces.service
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
    }
}