using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;
namespace PLManagement.Interfaces.services
{
    public interface IFormService
    {
        Task<IEnumerable<Form>> GetAllForms();
        Task<IEnumerable<Form>> GetAllFormsByPLId(int PLId);
        Task<Form> GetFormById(int id);
        Task<int> CreateForm(Form Form);
        Task<Form> UpdateForm(Form Form);
        Task<bool> DeleteForm(int id);
    }
}