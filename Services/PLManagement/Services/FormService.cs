using System.Collections.Generic;
using System.Threading.Tasks;
using PLManagement.Interfaces.Repos;
using PLManagement.Interfaces.services;
using System;
using System.IO;
using System.Linq;
using TMS.Models;

namespace PLManagement.Services
{
    public class FormService : IFormService
    {
        private readonly IFormRepository _repo;
        public FormService(IFormRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Form>> GetAllForms()
        {
            return await _repo.GetAllForm();
        }

        public async Task<IEnumerable<Form>> GetAllFormsByPLId(int PLId)
        {
            return await _repo.GetAllFormsByPLId(PLId);
        }

        public async Task<Form> GetFormById(int PLId)
        {
            return await _repo.GetFormById(PLId);
        }

        public async Task<int> CreateForm(Form Form)
        {
            return await _repo.CreateForm(Form);
        }

        public async Task<Form> UpdateForm(Form Form)
        {
            return await _repo.UpdateForm(Form);
        }

        public async Task<bool> DeleteForm(int id)
        {
            return await _repo.DeleteForm(id);
        }

    }
}