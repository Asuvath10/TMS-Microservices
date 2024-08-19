using System.Security.AccessControl;
using PLManagement.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PLManagement.Interfaces.Repos;
using System;
using TMS.Models;
using PLManagement.Models;
using System.Linq;

namespace PLManagement.Repositories;

public class FormRepository : IFormRepository
{
    private readonly PLManagementContext _dbContext;
    public FormRepository(PLManagementContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Form>> GetAllForm()
    {
        var forms = _dbContext.Forms;
        return forms;
    }
    public async Task<IEnumerable<Form>> GetAllFormsByPLId(int PLId)
    {
        var forms = _dbContext.Forms.Where(p => p.Id == PLId).ToList();
        return forms;
    }

    public async Task<Form> GetFormById(int id)
    {
        return await _dbContext.Forms.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> CreateForm(Form Form)
    {
        _dbContext.Forms.Add(Form);
        await _dbContext.SaveChangesAsync();
        return Form.Id;
    }

    public async Task<Form> UpdateForm(Form Form)
    {
        _dbContext.Forms.Update(Form);
        await _dbContext.SaveChangesAsync();
        return Form;
    }

    public async Task<bool> DeleteForm(int id)
    {
        var Form = await _dbContext.Forms.FindAsync(id);
        if (Form == null)
        {
            return false;
        }
        _dbContext.Forms.Remove(Form);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
