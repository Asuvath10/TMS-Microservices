using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Models;

namespace APIGateway.Interfaces
{
    public interface IDocumentManagement
    {
        Task<Byte[]> DownloadFile(string fileUrl);
        Task<string> UploadFile(Byte[] file);
        Task<Byte[]> GeneratePDF(int plId);
    }
}