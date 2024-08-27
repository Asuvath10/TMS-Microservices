using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using DocumentManagement.Interfaces;

namespace DocumentManagement.Services
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public FirebaseStorageService(string bucketName, StorageClient storageClient)
        {
            _bucketName = bucketName;
            _storageClient = storageClient;
        }

        public async Task<string> UploadFileAsync(string folderPath, byte[] fileContent, string contentType)
        {
            using (var stream = new MemoryStream(fileContent))
            {
                var objectName = $"{folderPath}/{Guid.NewGuid()}";
                var storageObject = await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, stream);
                return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
            }
        }

        public async Task<byte[]> DownloadFileAsync(string fileurl)
        {
            string trimmed = fileurl.Substring(fileurl.LastIndexOf('/') + 1);
            var objectName = $"signatures/{trimmed}";
            using (var stream = new MemoryStream())
            {
                await _storageClient.DownloadObjectAsync(_bucketName, objectName, stream);
                return stream.ToArray();
            }
        }
    }
}