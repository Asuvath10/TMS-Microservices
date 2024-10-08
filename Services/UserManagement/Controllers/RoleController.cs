using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserManagement.Interfaces.service;
using TMS.Models;


namespace UserManagement.Controllers
{
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var Roles = await _service.GetRoles();
            return Ok(Roles);
        }

        [HttpGet("GetRoleByid")]
        public async Task<ActionResult> GetRoleById(int id)
        {
            var Roles = await _service.GetRoleById(id);
            return Ok(Roles);
        }

    }
}