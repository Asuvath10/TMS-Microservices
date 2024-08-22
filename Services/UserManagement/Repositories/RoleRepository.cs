using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Interfaces.Repo;
using UserManagement.Models;
using TMS.Models;
using System.Linq;

namespace UserManagement.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManagementContext _dbContext;
        public RoleRepository(UserManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return _dbContext.Roles.AsNoTracking();
        }
        public async Task<Role> GetRolebyId(int id)
        {
            return await _dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(r=> r.Id == id);
        }
    }
}