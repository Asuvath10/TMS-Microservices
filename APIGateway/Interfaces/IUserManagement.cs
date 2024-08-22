using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace APIGateway.Interfaces
{
    public interface IUserManagement
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetAllUsersByRoleId(int roleId);
        Task<List<Role>> GetAllRoles();
        Task<Role> GetRoleById(int id);
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<int> CreateUser(User User);
        Task<User> UpdateUser(User User);
        Task<bool> DisableUser(int id);
        Task<(bool IsValid, User? User)> ValidateUserCredentials(string email, string password);
        Task<bool> CheckEmailavailability(string email);
    }
}