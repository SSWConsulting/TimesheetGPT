using Microsoft.Extensions.DependencyInjection;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Services;

namespace TimesheetGPT.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAiService, SemKerAiService>();
        services.AddScoped<IGraphService, GraphService>();
        
        return services;
    }
}