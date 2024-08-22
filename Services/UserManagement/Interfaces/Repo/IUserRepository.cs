using TMS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Interfaces.Repo;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<IEnumerable<User>> GetAllUsersByRoleId(int roleid);
    Task<User> GetUserById(int id);
    Task<User> GetUserByemail(string email);
    Task<int> CreateUser(User user);
    Task<User> UpdateUser(User user);
    Task<bool> DisableUser(int id);

}
