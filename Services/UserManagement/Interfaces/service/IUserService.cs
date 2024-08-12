using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Interfaces.service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<int> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DisableUser(int id);
    }
}