using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIGateway.Interfaces;
using APIGateway.Models;

namespace APIGateway.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
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
        public async Task<IActionResult> CreateUser([FromBody] UserTable User)
        {
            if (User == null)
            {
                return BadRequest("Proposal letter data is null.");
            }

            var createdUser = await _service.CreateUser(User);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // PUT: User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserTable User)
        {
            if (User == null || id != User.Id)
            {
                return BadRequest("Invalid proposal letter data.");
            }

            var updatedUser = await _service.UpdateUser(User);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok("Proposal letter updated successfully.");
        }

        // DELETE: User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _service.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok("Proposal letter deleted successfully.");
        }
    }
}