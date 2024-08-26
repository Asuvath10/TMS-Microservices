using System.Security.AccessControl;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Models;


namespace PLManagement.Interfaces.Repos;

public interface IFormRepository
{
    Task<IEnumerable<Form>> GetAllForm();
    Task<IEnumerable<Form>> GetAllFormsByPLId(int PLid);
    Task<Form> GetFormById(int id);
    Task<int> CreateForm(Form form);
    Task<int> UpdateForm(Form form);
    Task<bool> DeleteForm(int id);
}
