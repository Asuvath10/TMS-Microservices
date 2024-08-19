using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMS.Models;
using System.Net.Http;
using PLManagement.Interfaces;

namespace PLManagement.Services
{
    public class ApiGatewayService : IApiGatewayService
    {
        private readonly HttpClient _httpClient;
        public ApiGatewayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> UploadFile(string folderpath, Byte[] file, string contentType)
        {
            var content = new ByteArrayContent(file);
            var response = await _httpClient.PutAsync($"/api/Document/upload?foldername={folderpath}&contentType={contentType}", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}