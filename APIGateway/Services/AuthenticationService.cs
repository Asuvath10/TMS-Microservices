using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APIGateway.Interfaces;
using APIGateway.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TMS.Models;

public class AuthenticationService : IAuthenticationService
{
    private readonly IConfiguration _configuration;

    public AuthenticationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim("Role", user.Role!.RoleName!),
            new Claim("RoleId", user.RoleId.ToString()),
            new Claim("UserId", user.Id.ToString()),
            new Claim("FullName", user.FullName!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityTokenHandler().CreateJwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            new ClaimsIdentity(claims),
            DateTime.Now,
            DateTime.Now.AddDays(1),
            DateTime.Now,
            signingCredentials: creds);
        var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenstring;
    }
}