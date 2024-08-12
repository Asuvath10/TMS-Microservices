using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Interfaces.service;
using UserManagement.Interfaces.Repo;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _repo.GetAllUsers();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _repo.GetUserById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _repo.GetUserByemail(email);
        }

        public async Task<int> CreateUser(User user)
        {
            int UserId = await _repo.CreateUser(user);
            return UserId;
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _repo.UpdateUser(user);
        }

        public async Task<bool> DisableUser(int id)
        {
            return await _repo.DisableUser(id);
        }

    }
}