using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Interfaces;
using APIGateway.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IUserManagement _userService;

        public AuthenticationController(IAuthenticationService authService, IUserManagement userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null) return BadRequest("Invalid client request");
            var (isValid, user) = await _userService.ValidateUserCredentials(login.Email, login.Password);

            if (!isValid || user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var token = _authService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}