using UserManagement.Models;
using UserManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _dbContext;
    public UserRepository(UserManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserTable>> GetAllUsers()
    {
        return _dbContext.UserTables;
    }

    public async Task<UserTable> GetUserById(int id)
    {
        return await _dbContext.UserTables.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<UserTable> CreateUser(UserTable user)
    {
        _dbContext.UserTables.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<UserTable> UpdateUser(UserTable user)
    {
        _dbContext.UserTables.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var User = await _dbContext.UserTables.FindAsync(id);
        if (User == null)
        {
            return false;
        }
        _dbContext.UserTables.Remove(User);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
