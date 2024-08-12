using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;

namespace APIGateway.Interfaces
{
    public interface IUserManagement
    {
        Task<List<User>> GetAllUsers();
        Task<List<Role>> GetAllRoles();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<int> CreateUser(User User);
        Task<User> UpdateUser(User User);
        Task<bool> DisableUser(int id);
    }
}