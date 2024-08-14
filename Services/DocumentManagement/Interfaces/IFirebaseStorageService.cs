using System.Threading.Tasks;

namespace DocumentManagement.Interfaces
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadFileAsync(string folderPath, byte[] fileContent, string contentType);
    }
}