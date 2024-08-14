var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
services.AddSingleton<IPDFGenerationService>(provider =>
{
    ComponentInfo.SetLicense("Gembox:License");
    return new PdfGenerationService();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.Run();
