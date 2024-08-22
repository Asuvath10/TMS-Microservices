using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
using UserManagement.Interfaces.service;

namespace UserManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var Users = await _service.GetAllUsers();
            return Ok(Users);
        }
        [HttpGet("GetAllUsersByRoleId/{roleId}")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsersByRoleId(int roleId)
        {
            var Users = await _service.GetAllUsersByRoleId(roleId);
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var User = await _service.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<User>> GetUserByemail(string email)
        {
            var User = await _service.GetUserByEmail(email);
            if (User == null)
            {
                return Ok(null);
            }
            return Ok(User);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] User user)
        {
            if (user == null) { return BadRequest("Request is null"); }
            int CreateduserId = await _service.CreateUser(user);
            return Ok(CreateduserId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var updatedUser = await _service.UpdateUser(user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok("User Updated Successfully");
        }

        [HttpPut("{id}/DisableUser")]
        public async Task<IActionResult> DisableUser(int id)
        {
            var result = await _service.DisableUser(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("User Disabled Successfully");
        }
    }
}
