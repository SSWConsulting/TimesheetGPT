using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using TimesheetGPT.WebAPI.Endpoints;

namespace TimesheetGPT.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddTimesheetGptApi(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddMicrosoftIdentityWebApiAuthentication(config, "AzureAd")
            .EnableTokenAcquisitionToCallDownstreamApi()
            .AddInMemoryTokenCaches();
        
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
        
        return services;
    }
    
    public static WebApplication UseTimesheetGptApi(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapGetSubjects();
        
        return app;
    }
}