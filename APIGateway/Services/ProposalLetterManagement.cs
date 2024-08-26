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
            var proposalLetters = JsonConvert.DeserializeObject<ProposalLetter>(content);
            return proposalLetters;
        }
        public async Task<Plstatus> GetPLStatusById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/PLStatus/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetters = JsonConvert.DeserializeObject<Plstatus>(content);
            return proposalLetters;
        }
        public async Task<List<ProposalLetter>> GetProposalLettersByUserId(int userid)
        {
            var response = await _httpClient.GetAsync($"/api/PL/GetallPLsByUserId/{userid}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
            return proposalLetter;
        }
        public async Task<List<ProposalLetter>> GetProposalLettersByStatusId(int statusid)
        {
            var response = await _httpClient.GetAsync($"/api/PL/GetallPLsByStatusId/{statusid}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
            return proposalLetter;
        }
        public async Task<List<ProposalLetter>> GetProposalLettersByPreparerId(int preparerId)
        {
            var response = await _httpClient.GetAsync($"/api/PL/GetallPLsByPreparerId/{preparerId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
            return proposalLetter;
        }
        public async Task<List<ProposalLetter>> GetProposalLettersByReviewerId(int reviewerId)
        {
            var response = await _httpClient.GetAsync($"/api/PL/GetallPLsByReviewerId/{reviewerId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
            return proposalLetter;
        }
        public async Task<List<ProposalLetter>> GetProposalLettersByApproverId(int approverId)
        {
            var response = await _httpClient.GetAsync($"/api/PL/GetallPLsByApproverId/{approverId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var proposalLetter = JsonConvert.DeserializeObject<List<ProposalLetter>>(content);
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
                throw new InvalidOperationException($"Failed to update ProposalLetter. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new InvalidOperationException("Unexpected content type received from the server.");
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

        //Form calls 
        public async Task<List<Form>> GetAllForms()
        {
            var response = await _httpClient.GetAsync("/api/Form/Forms");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var forms = JsonConvert.DeserializeObject<List<Form>>(content);
            return forms;
        }
        public async Task<List<Form>> GetallFormsByPLId(int PLid)
        {
            var response = await _httpClient.GetAsync($"/api/Form/GetallFormsByPLId/{PLid}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var forms = JsonConvert.DeserializeObject<List<Form>>(content);
            return forms;
        }
        public async Task<Form> GetFormById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Form/GetForm/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var form = JsonConvert.DeserializeObject<Form>(content);
            return form;
        }

        public async Task<int> CreateForm(Form form)
        {
            var jsonContent = JsonConvert.SerializeObject(form);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/Form/CreateForm", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            int createdFormId = int.Parse(responseContent);
            return createdFormId;
        }

        public async Task<Form> UpdateForm(Form form)
        {
            var jsonContent = JsonConvert.SerializeObject(form);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/Form/UpdateForm/{form.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Failed to update ProposalLetter. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                throw new InvalidOperationException("Unexpected content type received from the server.");
            }

            var updatedform = JsonConvert.DeserializeObject<Form>(responseContent);

            return updatedform;
        }

        public async Task<bool> DeleteForm(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Form/DeleteForm/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}