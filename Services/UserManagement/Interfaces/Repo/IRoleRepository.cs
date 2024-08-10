using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Interfaces.Repo
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRoles();
    }
}