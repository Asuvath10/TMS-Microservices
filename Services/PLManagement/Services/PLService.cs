using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Models;
using PLManagement.Interfaces;
using PLManagement.Repositories;

namespace PLManagement.Services
{
    public class PLService : IPLService
    {
        private readonly IPLRepository _repo;

        public PLService(IPLRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProposalLetter> GetAllPLservice()
        {
            return _repo.GetAllPL();
        }

    }
}