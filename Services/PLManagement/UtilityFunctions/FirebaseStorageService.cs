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
 
    public FirebaseStorageService(IConfiguration configuration)
    {
        _bucketName = configuration["Firebase:BucketName"]; 
        string credentialsPath = configuration["Firebase:CredentialsFilePath"];
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
        _storageClient = StorageClient.Create();
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