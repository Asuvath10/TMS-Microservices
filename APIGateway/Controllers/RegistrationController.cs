using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using APIGateway.Interfaces;
using TMS.Models;
using APIGateway.Models;

namespace APIGateway.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IUserManagement _userService;
        public RegistrationController(IUserManagement userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] Register register)
        {
            if (register == null)
            {
                return BadRequest("Enter all the values");
            }
            try
            {
                bool emailAvailable = await _userService.CheckEmailavailability(register.Email);
                if (!emailAvailable)
                {
                    return BadRequest("Email is already taken");
                }
                var newuser = new User
                {
                    Name = register.Name,
                    Address = register.Address,
                    Pan = register.Pan,
                    //encrypting the password
                    Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                    RoleId = 2,
                    Email = register.Email,
                    FullName = register.FullName,
                    Disable = false,
                    CreatedOn = DateTime.Now,
                    CreatedBy = 2
                };
                int CreateUser = await _userService.CreateUser(newuser);
                return Ok(CreateUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}