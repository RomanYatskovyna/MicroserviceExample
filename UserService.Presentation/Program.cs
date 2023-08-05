using Grpc.Net.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using System.IO.Compression;
using System.Reflection;
using UserService.Presentation.GrpcCompressionProviders;

var builder = WebApplication.CreateBuilder(args);
// Configure the HTTP request pipeline.
builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
    options.MaxReceiveMessageSize = 6291456; // 6 MB
    options.MaxSendMessageSize = 6291456; // 6 MB
    options.CompressionProviders = new List<ICompressionProvider>()
    {
        new BrotliCompressionProvider(CompressionLevel.Optimal) //br
	};
    // grpcaccept-encoding, and must match the compression provider declared in CompressionProviders collection 
    options.ResponseCompressionAlgorithm = "br";
    // compression level used if not set on the provider
    options.ResponseCompressionLevel = CompressionLevel.Optimal;
});
builder.Services.AddGrpcReflection();

//builder.Services.AddGrpcHttpApi();
var versions = new List<ApiVersionDescription>()
{
    new(new ApiVersion(1,0),"v1",false),
	//new(new ApiVersion(2,0),"v2",false)

};
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(options =>
{
    var swaggerSection = builder.Configuration.GetSection("Swagger");
    var licenseSection = swaggerSection.GetSection("License");
    foreach (var description in versions)
    {
        options.SwaggerDoc(
            description.GroupName,
            new OpenApiInfo
            {
                Title = swaggerSection["Title"],
                Description = swaggerSection["Description"] +
                              (description.IsDeprecated ? " [DEPRECATED]" : string.Empty),
                Version = description.ApiVersion.ToString(),
                TermsOfService = string.IsNullOrEmpty(swaggerSection["TermsOfServiceUrl"])
                    ? null
                    : new Uri(swaggerSection["TermsOfServiceUrl"]!),
                License = licenseSection is null
                    ? null
                    : new OpenApiLicense { Name = licenseSection["Name"], Url = new Uri(licenseSection["Url"]!) }
            });
    }
    options.EnableAnnotations();

    var currentAssembly = Assembly.GetExecutingAssembly();
    var xmlDocs = currentAssembly.GetReferencedAssemblies()
        .Union(new[] { currentAssembly.GetName() })
        .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location)!,
            $"{a.Name}.xml"))
        .Where(File.Exists).ToArray();
    Array.ForEach(xmlDocs, d =>
    {
        options.IncludeXmlComments(d);
        options.IncludeGrpcXmlComments(d, true);

    });
});
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    foreach (var groupName in versions.Select(item => item.GroupName))
    {
        options.SwaggerEndpoint($"../swagger/{groupName}/swagger.json",
            groupName.ToUpperInvariant());
    }
});
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcReflectionService().EnableGrpcWeb();
app.MapGrpcService<UserService.Presentation.Services.v1.AuthService>();
app.MapGrpcService<UserService.Presentation.Services.v2.AuthService>();
await app.RunAsync();
