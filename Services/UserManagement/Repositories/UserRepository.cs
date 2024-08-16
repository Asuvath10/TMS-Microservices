using UserManagement.Models;
using UserManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Interfaces.Repo;
using TMS.Models;

namespace UserManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManagementContext _dbContext;
    public UserRepository(UserManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return _dbContext.Users;
    }

    public async Task<User> GetUserById(int id)
    {
        return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetUserByemail(string email)
    {
        return await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<int> CreateUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<User> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DisableUser(int id)
    {
        var User = await _dbContext.Users.FindAsync(id);
        if (User == null)
        {
            return false;
        }
        User.Disable = true;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
