using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TimesheetGPT.WebAPI.Endpoints;

namespace TimesheetGPT.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddTimesheetGptApi(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication UseTimesheetGptApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.MapGetSubjects();

        return app;
    }
}
