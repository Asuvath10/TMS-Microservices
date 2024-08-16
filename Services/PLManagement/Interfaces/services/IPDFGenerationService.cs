using System;
using System.Threading.Tasks;
using TMS.Models;

namespace PLManagement.Interfaces.services
{
    public interface IPDFGenerationService
    {
        Byte[] GeneratePdf(ProposalLetter proposalLetter, string password);
    }
}