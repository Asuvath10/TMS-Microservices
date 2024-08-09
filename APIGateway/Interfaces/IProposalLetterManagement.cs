using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Models;

namespace APIGateway.Interfaces
{
    public interface IProposalLetterManagement
    {
        Task<List<ProposalLetter>> GetAllProposals();
    }
}