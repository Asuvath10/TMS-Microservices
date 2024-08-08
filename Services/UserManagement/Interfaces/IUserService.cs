using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserTable>> GetAllUsers();
        Task<UserTable> GetUserById(int id);
        Task<UserTable> CreateUser(UserTable user);
        Task<UserTable> UpdateUser(UserTable user);
        Task<bool> DeleteUser(int id);
    }
}