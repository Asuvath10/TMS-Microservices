using UserManagement.Models;
using UserManagement.Interfaces;
using System.Collections.Generic;

namespace UserManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _dbContext;
    public UserRepository(UserManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<UserTable> GetAllUser()
    {
        return _dbContext.UserTables;
    }
}
