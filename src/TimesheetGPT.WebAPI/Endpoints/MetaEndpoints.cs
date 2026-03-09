using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using TimesheetGPT.Core;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.WebAPI.Contracts;

namespace TimesheetGPT.WebAPI.Endpoints;

public static class MetaEndpoints
{
    public static IEndpointRouteBuilder MapMeta(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api")
            .WithTags("Meta")
            .RequireAuthorization();

        group.MapGet("/me", GetCurrentUser)
            .WithName("GetCurrentUser")
            .WithSummary("Get current user session information.")
            .Produces<UserProfileResponse>();

        group.MapGet("/providers", GetProviders)
            .WithName("GetProviders")
            .WithSummary("Get configured provider status.")
            .Produces<ProviderStatusResponse>();

        return app;
    }

    private static async Task<UserProfileResponse> GetCurrentUser(HttpContext httpContext, IUserProfileService userProfileService)
    {
        var displayName = await userProfileService.GetDisplayNameAsync();
        var authProvider = httpContext.User.FindFirstValue(AuthProviderNames.AuthProviderClaimType) ?? "Unknown";

        return new UserProfileResponse(displayName ?? "Unknown", authProvider);
    }

    private static ProviderStatusResponse GetProviders(IConfiguration configuration)
    {
        var microsoftEnabled = !string.IsNullOrWhiteSpace(configuration["AzureAd:ClientId"]);
        var googleEnabled = !string.IsNullOrWhiteSpace(configuration["Google:ClientId"]);

        var defaultProvider = microsoftEnabled
            ? AuthProviderNames.Microsoft
            : googleEnabled
                ? AuthProviderNames.Google
                : "None";

        return new ProviderStatusResponse(microsoftEnabled, googleEnabled, defaultProvider);
    }
}
