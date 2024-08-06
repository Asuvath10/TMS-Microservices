using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserTable> GetAllUserservice();
    }
}