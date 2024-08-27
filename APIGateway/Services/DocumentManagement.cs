using System.Runtime.Serialization.Json;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Interfaces;
using TMS.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.IO;
using System.Net.Mime;

namespace APIGateway.Services
{
    public class DocumentManagement : IDocumentManagement
    {
        private readonly HttpClient _httpClient;
        public DocumentManagement(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Byte[]> DownloadFile(string fileUrl)
        {
            var response = await _httpClient.GetAsync($"/api/Document/download?signatureUrl={fileUrl}");
            response.EnsureSuccessStatusCode();
            byte[] file = await response.Content.ReadAsByteArrayAsync();
            return file;
        }

        public async Task<string> UploadFile(Byte[] file)
        {
            using (var memoryStream = new MemoryStream(file))
            {
                // Create the content with the memory stream
                var content = new StreamContent(memoryStream);

                // Set the content type header
                content.Headers.ContentType = new MediaTypeHeaderValue("application/image");

                var response = await _httpClient.PutAsync("/api/Document/upload", content);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }

        public async Task<Byte[]> GeneratePDF(int plId)
        {
            var response = await _httpClient.GetAsync($"/api/Document/generate?plId={plId}");
            response.EnsureSuccessStatusCode();
            byte[] file = await response.Content.ReadAsByteArrayAsync();
            return file;
        }
    }
}