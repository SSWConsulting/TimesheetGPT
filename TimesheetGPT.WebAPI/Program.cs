using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Development.json")
    .Build();

builder.Services.AddMicrosoftIdentityWebApiAuthentication(configuration, "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Get tenantId, clientId and clientSecret from appsettings.json
// and pass them to GraphService
//
// var tenantId = configuration["AzureAd:TenantId"];
// var clientId = configuration["AzureAd:ClientId"];
// var clientSecret = configuration["AzureAd:ClientSecret"];
//
// // ensure the above values are set
// if (string.IsNullOrEmpty(tenantId) ||
//     string.IsNullOrEmpty(clientId) ||
//     string.IsNullOrEmpty(clientSecret))
// {
//     throw new Exception("TenantId, ClientId and ClientSecret must be set in appsettings.json");
// }


app.MapGet("/get-subjects", async () =>
    {
        throw new NotImplementedException();
        // Initialize GraphServiceClient
        // var graphClient = new GraphServiceClient(); //TODO: Get a token from the request and use it to initialize the client

        // Fetch user info
        
        // var result = await graphService.GetEmailSubjects(DateTime.Now -  TimeSpan.FromDays(1));
        // return result;
    })
    .WithName("GetEmailSubjects")
    .WithOpenApi();

app.Run();