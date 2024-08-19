using System;
using System.Threading.Tasks;
using DocumentManagement.Models;

namespace DocumentManagement.Interfaces
{
    public interface IPDFGenerationService
    {
        Task<byte[]> GeneratePdf(int plId);

    }
}