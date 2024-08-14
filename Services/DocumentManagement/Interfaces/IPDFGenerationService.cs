using System;
using System.Threading.Tasks;
using PLManagement.Models;

namespace DocumentManagement.Interfaces
{
    public interface IPDFGenerationService
    {
        Byte[] GeneratePdf(ProposalLetter proposalLetter, string userName, string password, string signatureUrl = null);
    }
}