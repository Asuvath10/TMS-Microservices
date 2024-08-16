using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Models;

namespace UserManagement.Interfaces.service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<int> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task<bool> DisableUser(int id);
    }
}