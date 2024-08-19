using System;
using System.Threading.Tasks;

namespace DocumentManagement.Interfaces
{
    public interface IPDFGenerationService
    {
        Task<byte[]> GeneratePdf(int plId);

    }
}