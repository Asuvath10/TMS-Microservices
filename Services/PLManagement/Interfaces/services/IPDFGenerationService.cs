using System;
using System.Threading.Tasks;
using PLManagement.Models;

namespace PLManagement.Interfaces.services
{
    public interface IPDFGenerationService
    {
        Byte[] GeneratePdf(ProposalLetter proposalLetter, string userName, string password, string signatureUrl = null);
    }
}