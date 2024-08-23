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
    public class PLCallService : IPLCallService
    {
        private readonly HttpClient _httpClient;
        public PLCallService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProposalLetter> GetPLbyAPIGateway(int plId)
        {
            //Get PL from the API Gateway
            var response = await _httpClient.GetAsync($"/api/PL/{plId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<ProposalLetter>(content);
            if (proposalLetter == null)
            {
                throw new InvalidOperationException("ProposalLetter is null");
            }
            return proposalLetter;
        }
        public async Task<List<Form>> GetallFormsByPLId(int PLid)
        {
            var response = await _httpClient.GetAsync($"/api/Form/GetallFormsByPLId/{PLid}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var forms = JsonConvert.DeserializeObject<List<Form>>(content);
            if (forms == null)
            {
                throw new InvalidOperationException("form is null");
            }
            return forms;
        }
    }
}