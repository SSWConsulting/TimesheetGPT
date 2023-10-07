using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace TimesheetGPT.WebAPI.Endpoints;

public static class EmailEndpoints
{
    public static void MapGetSubjects(this IEndpointRouteBuilder app) =>
        app.MapGet("/get-subjects", async () =>
            {
                throw new NotImplementedException();
                // Initialize GraphServiceClient
                // var graphClient = new GraphServiceClient(); //TODO: Get a token from the request and use it to initialize the client

                // Fetch user info
        
                // var result = await graphService.GetEmailSubjects(DateTime.Now -  TimeSpan.FromDays(1));
                // return result;
            })
            .WithName("GetEmailSubjects")
            .WithOpenApi();

}