using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;
using TMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIGateway.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagement _service;

        public UserController(IUserManagement service)
        {
            _service = service;
        }

        // Get: Users
        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _service.GetAllUsers();
            return Ok(Users);
        }

        // Get: Users
        [HttpGet("Roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var Users = await _service.GetAllRoles();
            return Ok(Users);
        }

        // GET: User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var User = await _service.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        // POST: User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User User)
        {
            if (User == null)
            {
                return BadRequest("User data is null.");
            }

            int createdUserId = await _service.CreateUser(User);
            return Ok(createdUserId);
        }

        // PUT: User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User User)
        {
            if (User == null || id != User.Id)
            {
                return BadRequest("Invalid User data.");
            }

            var updatedUser = await _service.UpdateUser(User);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok("User updated successfully.");
        }

        // DELETE: User/{id}
        [HttpPut("{id}/DisableUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _service.DisableUser(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok("User Disabled successfully.");
        }
    }
}