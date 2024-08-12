using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using PLManagement.Interfaces;
using PLManagement.Interfaces.services;
using System;
using System.IO;
using System.Threading.Tasks;

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
            var objectName = $"{folderPath}/{Guid.NewGuid()}"; // Unique file name
            var storageObject = await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, stream);
            return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
        }
    }
}