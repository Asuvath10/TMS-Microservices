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
    public class ProposalLetterManagement : IProposalLetterManagement
    {
        private readonly HttpClient _httpClient;
        public ProposalLetterManagement(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ProposalLetter>> GetAllProposals()
        {
            var response = await _httpClient.GetAsync("/api/PL");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetters = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
            return proposalLetters;
        }

        //Get ALL PLStatuses
        public async Task<List<Plstatus>> GetAllStatuses()
        {
            var response = await _httpClient.GetAsync("/api/PLStatus");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var plstatuses = JsonConvert.DeserializeObject<List<Plstatus>>(content);
            return plstatuses;
        }
        public async Task<ProposalLetter> GetProposalLetterById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/PL/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<ProposalLetter>(content);
            return proposalLetter;
        }

        public async Task<int> CreateProposalLetter(ProposalLetter proposalLetter)
        {
            var jsonContent = JsonConvert.SerializeObject(proposalLetter);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/PL", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            int createdProposalLetterId = int.Parse(responseContent);
            return createdProposalLetterId;
        }

        public async Task<ProposalLetter> UpdateProposalLetter(ProposalLetter proposalLetter)
        {
            var jsonContent = JsonConvert.SerializeObject(proposalLetter);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/PL/{proposalLetter.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update ProposalLetter. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new Exception("Unexpected content type received from the server.");
            }

            var updatedProposalLetter = JsonConvert.DeserializeObject<ProposalLetter>(responseContent);

            return updatedProposalLetter;
        }

        public async Task<bool> DeleteProposalLetter(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/PL/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<ProposalLetter> AddSignatureAsync(int proposalLetterId, byte[] signature)
        {
            // Call to the PLManagement service to add signature
            var content = new ByteArrayContent(signature);
            var response = await _httpClient.PutAsync($"/api/PL/{proposalLetterId}/signature", content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<ProposalLetter>(responseContent);
            return proposalLetter;
        }

        public async Task<ProposalLetter> GeneratePdf(int proposalLetterId)
        {
            // Call to the PLManagement service to generate PDF
            var response = await _httpClient.PutAsync($"/api/PL/{proposalLetterId}/generate-pdfurl", null);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<ProposalLetter>(responseContent);
            return proposalLetter;
        }
    }
}