using System.Runtime.Serialization.Json;
using System;
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
    }
}