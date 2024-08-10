using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;

namespace APIGateway.Interfaces
{
    public interface IUserManagement
    {
        Task<List<UserTable>> GetAllUsers();
        Task<List<Role>> GetAllRoles();
        Task<UserTable> GetUserById(int id);
        Task<UserTable> CreateUser(UserTable User);
        Task<UserTable> UpdateUser(UserTable User);
        Task<bool> DeleteUser(int id);
    }
}