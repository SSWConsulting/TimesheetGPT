using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Services;

namespace TimesheetGPT.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var msEnabled = !string.IsNullOrEmpty(configuration["AzureAd:ClientId"]);
        var googleEnabled = !string.IsNullOrEmpty(configuration["Google:ClientId"]);

        services.AddScoped<IAiService, SemKerAiService>();
        services.AddScoped<ITimesheetGenerationService, TimesheetGenerationService>();
        services.AddScoped<McpClientService>();

        if (msEnabled)
        {
            services.AddScoped<GraphService>();
            services.AddScoped<MicrosoftUserProfileService>();
        }

        if (googleEnabled)
        {
            services.AddScoped<GoogleWorkspaceService>();
            services.AddScoped<GoogleUserProfileService>();
        }

        // Factory that resolves the right IGraphService based on auth provider
        services.AddScoped<IGraphService>(sp =>
        {
            var httpContext = sp.GetService<IHttpContextAccessor>()?.HttpContext;
            if (googleEnabled && (IsGoogleUser(httpContext) || !msEnabled))
                return sp.GetRequiredService<GoogleWorkspaceService>();
            if (msEnabled)
                return sp.GetRequiredService<GraphService>();

            throw new InvalidOperationException("No auth provider configured. Set AzureAd:ClientId or Google:ClientId in appsettings.");
        });

        // Factory that resolves the right IUserProfileService based on auth provider
        services.AddScoped<IUserProfileService>(sp =>
        {
            var httpContext = sp.GetService<IHttpContextAccessor>()?.HttpContext;
            if (googleEnabled && (IsGoogleUser(httpContext) || !msEnabled))
                return sp.GetRequiredService<GoogleUserProfileService>();
            if (msEnabled)
                return sp.GetRequiredService<MicrosoftUserProfileService>();

            throw new InvalidOperationException("No auth provider configured. Set AzureAd:ClientId or Google:ClientId in appsettings.");
        });

        return services;
    }

    private static bool IsGoogleUser(HttpContext? httpContext)
    {
        return httpContext?.User.FindFirst(AuthProviderNames.AuthProviderClaimType)?.Value
               == AuthProviderNames.Google;
    }
}
