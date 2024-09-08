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
        var forms = _dbContext.Forms.AsNoTracking();
        return forms;
    }
    public async Task<IEnumerable<Form>> GetAllFormsByPLId(int PLid)
    {
        var forms = await _dbContext.Forms.AsNoTracking().Where(p => p.Plid == PLid).ToListAsync();
        return forms;
    }

    public async Task<Form> GetFormById(int id)
    {
        return await _dbContext.Forms.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> CreateForm(Form form)
    {
        if (form != null)
        {
            form.CreatedOn = DateTime.UtcNow;
        }
        _dbContext.Forms.Add(form);
        await _dbContext.SaveChangesAsync();
        return form.Id;
    }

    public async Task<int> UpdateForm(Form form)
    {
        if (form.Id == 0 || form.Id == null)
        {
            _dbContext.Forms.Add(form);
        }
        else
        {
            _dbContext.Forms.Update(form);
        }
        await _dbContext.SaveChangesAsync();
        return form.Id;
    }

    public async Task<bool> DeleteForm(int id)
    {
        var form = await _dbContext.Forms.FindAsync(id);
        if (form == null)
        {
            return false;
        }
        _dbContext.Forms.Remove(form);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
