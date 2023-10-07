using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models.ExternalConnectors;
using System.Reflection;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Services;

namespace TimesheetGPT.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAiService, SemKerAiService>();
        // services.AddScoped<IAIService, LangChainAIService>(); //TODO: Try langchain out
        
        return services;
    }
}