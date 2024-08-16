using DocumentManagement.Interfaces;
using DocumentManagement.Services;
using GemBox.Document;
using Google.Cloud.Storage.V1;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
           {
               c.SwaggerDoc("v1", new OpenApiInfo { Title = "DocumentManagement", Version = "v1" });
           });
builder.Services.AddCors((setup) =>
{
    setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
//Firebase Configuration Settings.
builder.Services.AddSingleton<IFirebaseStorageService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    string bucketName = configuration["Firebase:BucketName"];
    string credentialsPath = configuration["Firebase:CredentialsFilePath"];
    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialsPath);
    var storageClient = StorageClient.Create();
    return new FirebaseStorageService(bucketName, storageClient);
});

builder.Services.AddSingleton<IPDFGenerationService>(provider =>
{
    var FirebaseStorageService = provider.GetRequiredService<IFirebaseStorageService>();
    ComponentInfo.SetLicense("Gembox:License");
    return new PdfGenerationService(FirebaseStorageService);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentManagement v1"));
}
app.UseCors("default");
app.UseHttpsRedirection();

app.Run();
