using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Interfaces;

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
        public async Task<ActionResult<IEnumerable<UserTable>>> GetAllUsers()
        {
            var Users = await _service.GetAllUsers();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserTable>> GetUserById(int id)
        {
            var User = await _service.GetUserById(id);
            if (User == null)
            {
                return NotFound();
            }
            return Ok(User);
        }

        [HttpPost]
        public async Task<ActionResult<UserTable>> Post([FromBody] UserTable user)
        {
            if (user == null) { return BadRequest("Request is null"); }
            var Createduser = await _service.CreateUser(user);
            return Ok(Createduser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProposalLetter(int id,UserTable user)
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposalLetter(int id)
        {
            var result = await _service.DeleteUser(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("User Deleted Successfully");
        }
    }
}
