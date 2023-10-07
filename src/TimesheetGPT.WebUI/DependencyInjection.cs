using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor;
using MudBlazor.Services;
using TimesheetGPT.Core;
using TimesheetGPT.Core.Services;
using TimesheetGPT.WebUI.Services;

namespace TimesheetGPT.WebUI;

public static class DependencyInjection
{
    public static IServiceCollection AddTimesheetGptUi(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container.
        var initialScopes = config["DownstreamApi:Scopes"]?.Split(' ');

        services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(config.GetSection("AzureAd"))
            .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
            .AddMicrosoftGraph(config.GetSection("DownstreamApi"))
            .AddInMemoryTokenCaches();
        services.AddControllersWithViews()
            .AddMicrosoftIdentityUI();

        services.AddAuthorization(options =>
        {
            // By default, all incoming requests will be authorized according to the default policy
            options.FallbackPolicy = options.DefaultPolicy;
        });

        services.AddRazorPages();
        services.AddServerSideBlazor()
            .AddMicrosoftIdentityConsentHandler();
        services.AddMudServices();
        services.AddMudMarkdownServices();

        services.AddApplication();

        services.AddScoped<ClipboardService>();
        services.AddScoped<TimesheetService>();
        
        return services;
    }
    
    public static WebApplication UseTimesheetGptUi(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        
        return app;
    }
}