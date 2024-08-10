using System.Runtime.Serialization.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Interfaces;
using APIGateway.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace APIGateway.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly HttpClient _httpClient;
        public UserManagement(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<UserTable>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<UserTable>>(content);
            return Users;
        }
        public async Task<List<Role>> GetAllRoles()
        {
            var response = await _httpClient.GetAsync("/Role");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var Roles = JsonConvert.DeserializeObject<List<Role>>(content);
            return Roles;
        }
        public async Task<UserTable> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/User/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<UserTable>(content);
            return User;
        }

        public async Task<UserTable> CreateUser(UserTable User)
        {
            var jsonContent = JsonConvert.SerializeObject(User);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/User", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var createdUser = JsonConvert.DeserializeObject<UserTable>(responseContent);
            return createdUser;
        }

        public async Task<UserTable> UpdateUser(UserTable User)
        {
            var jsonContent = JsonConvert.SerializeObject(User);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/User/{User.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update User. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new Exception("Unexpected content type received from the server.");
            }

            var updatedUser = JsonConvert.DeserializeObject<UserTable>(responseContent);

            return updatedUser;
        }

        public async Task<bool> DeleteUser(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/User/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}