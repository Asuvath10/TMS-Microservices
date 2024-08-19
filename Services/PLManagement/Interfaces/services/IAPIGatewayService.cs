using System;
using System.Threading.Tasks;
using TMS.Models;

namespace PLManagement.Interfaces
{
    public interface IApiGatewayService
    {
        Task<string> UploadFile(string folderpath, Byte[] file, string contentType);
    }
}