using UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Interfaces.Repo;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<int> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task<bool> DisableUser(int id);

}
