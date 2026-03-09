using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.WebAPI.Contracts;

namespace TimesheetGPT.WebAPI.Endpoints;

public static class ChatEndpoints
{
    public static IEndpointRouteBuilder MapChat(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/chat")
            .WithTags("Chat")
            .RequireAuthorization();

        group.MapPost("/messages", SendMessage)
            .WithName("ChatWithData")
            .WithSummary("Ask a question over connected provider data.")
            .Produces<ChatResponse>()
            .ProducesValidationProblem();

        return app;
    }

    private static async Task<Results<Ok<ChatResponse>, ValidationProblem>> SendMessage(
        ChatRequest request,
        IAiService aiService)
    {
        if (string.IsNullOrWhiteSpace(request.Ask))
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.Ask)] = ["Ask is required."]
            });

        var response = await aiService.ChatWithGraphApi(request.Ask);
        return TypedResults.Ok(new ChatResponse(response));
    }
}
