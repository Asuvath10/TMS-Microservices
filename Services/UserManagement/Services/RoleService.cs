using System.Threading.Tasks;
using TMS.Models;
using UserManagement.Interfaces.service;
using UserManagement.Interfaces.Repo;
using System.Collections;
using System.Collections.Generic;

namespace UserManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;

        public RoleService(IRoleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _repo.GetRoles();
        }
        public async Task<Role> GetRoleById(int id)
        {
            return await _repo.GetRolebyId(id);
        }
    }
}