using UserManagement.Models;
using System.Collections.Generic;

namespace UserManagement.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserTable> GetAllUser();
}
