using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;


namespace APIGateway.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateJwtToken(User user);
    }
}