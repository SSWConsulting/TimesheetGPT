using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.Models.ExternalConnectors;
using System.Reflection;
using TimesheetGPT.Application.Interfaces;
using TimesheetGPT.Application.Services;

namespace TimesheetGPT.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAiService, SemKerAiService>();
        // services.AddScoped<IAIService, LangChainAIService>(); //TODO: Try langchain out
        
        return services;
    }
}