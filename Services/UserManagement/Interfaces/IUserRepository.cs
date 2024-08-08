using UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserTable>> GetAllUsers();
    Task<UserTable> GetUserById(int id);
    Task<UserTable> CreateUser(UserTable user);
    Task<UserTable> UpdateUser(UserTable user);
    Task<bool> DeleteUser(int id);

}
