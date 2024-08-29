using System.Net;
using DocumentManagement.Interfaces;
using DocumentManagement.Services;
using GemBox.Document;
using GlobalException;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Http;
using Microsoft.OpenApi.Models;
using policy;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddHttpClient();
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

builder.Services.AddHttpClient<IPLCallService, PLCallService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["PLService:BaseUrl"]);
            })
            //Set lifetime to five minutes
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(RetryPolicy.GetRetryPolicy()));

builder.Services.AddHttpClient<IUserCallService, UserCallService>(client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["UserService:BaseUrl"]);
            })
            //Set lifetime to five minutes
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            // Added retrypolicy for overcome socket exception.
            .AddHttpMessageHandler(() => new PolicyHttpMessageHandler(RetryPolicy.GetRetryPolicy()));

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
    var plService = provider.GetRequiredService<IPLCallService>();
    var userService = provider.GetRequiredService<IUserCallService>();
    ComponentInfo.SetLicense("FREE-LIMITED-KEY");
    return new PdfGenerationService(FirebaseStorageService, plService, userService);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocumentManagement v1"));
}
app.UseCors("default");
app.UseRouting();
// app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();
