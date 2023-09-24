using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TimesheetGPT.Application.Interfaces;
using TimesheetGPT.Application.Services;

namespace TimesheetGPT.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAIService, SemKerAIService>();
        // services.AddScoped<IAIService, LangChainAIService>(); //TODO: Try langchain out

        return services;
    }
}
