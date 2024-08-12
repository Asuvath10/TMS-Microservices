using System.Threading.Tasks;

namespace PLManagement.Interfaces.services
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(string folderPath, byte[] fileContent, string contentType);
    }
}