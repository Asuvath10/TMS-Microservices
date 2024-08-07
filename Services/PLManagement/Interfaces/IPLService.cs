using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PLManagement.Models;

namespace PLManagement.Interfaces
{
    public interface IPLService
    {
        IEnumerable<ProposalLetter> GetAllPLservice();
        Task<ProposalLetter> CreateProposalLetter(ProposalLetter proposalLetter);
    }
}