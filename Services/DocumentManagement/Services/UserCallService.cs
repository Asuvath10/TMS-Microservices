using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using DocumentManagement.Interfaces;
using Newtonsoft.Json;
using TMS.Models;

namespace DocumentManagement.Services
{
    public class UserCallService : IUserCallService
    {
        private readonly HttpClient _httpClient;
        public UserCallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/User/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<User>(content);
            if (User == null)
            {
                throw new InvalidOperationException("It is not a valid UserId");
            }
            return User;
        }
    }
}