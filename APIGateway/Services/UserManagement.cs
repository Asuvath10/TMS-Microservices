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
        public async Task<List<User>> GetAllUsers()
        {
            var response = await _httpClient.GetAsync("/api/User");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var Users = JsonConvert.DeserializeObject<List<User>>(content);
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
        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/User/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var User = JsonConvert.DeserializeObject<User>(content);
            return User;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            var response = await _httpClient.GetAsync($"/api/User/GetUserByEmail?email={email}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(content);
            return user;
        }

        public async Task<int> CreateUser(User User)
        {
            var jsonContent = JsonConvert.SerializeObject(User);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/User", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            int createdUserId = int.Parse(responseContent);
            return createdUserId;
        }

        public async Task<User> UpdateUser(User User)
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

            var updatedUser = JsonConvert.DeserializeObject<User>(responseContent);

            return updatedUser;
        }

        public async Task<bool> DisableUser(int id)
        {
            var response = await _httpClient.PutAsync($"/api/User/{id}/DisableUser", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<(bool IsValid, User? User)> ValidateUserCredentials(string email, string password)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                return (false, null);
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return (isValid, isValid ? user : null);
        }
        public async Task<(bool IsValid, User? User)> CheckEmailavailability(string email)
        {
            var user = await GetUserByEmail(email);

            if (user == null)
            {
                //email is taken (user exists)
                return (false, null);
            }
            bool isValid = true;
            return (isValid, isValid ? user : null);
        }
    }
}