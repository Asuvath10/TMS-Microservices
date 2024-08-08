using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Interfaces;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserTable>> GetAllUsers()
        {
            return await _repo.GetAllUsers();
        }

        public async Task<UserTable> GetUserById(int id)
        {
            return await _repo.GetUserById(id);
        }

        public async Task<UserTable> CreateUser(UserTable user)
        {
            return await _repo.CreateUser(user);
        }

        public async Task<UserTable> UpdateUser(UserTable user)
        {
            return await _repo.UpdateUser(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _repo.DeleteUser(id);
        }

    }
}