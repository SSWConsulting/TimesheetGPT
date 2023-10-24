using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace TimesheetGPT.WebAPI.Endpoints;

public static class EmailEndpoints
{
    public static void MapChat(this IEndpointRouteBuilder app) =>
        app.MapGet("/api/chat", async (string ask) =>
            {

            })
            .WithName("GetEmailSubjects")
            .WithOpenApi();

}