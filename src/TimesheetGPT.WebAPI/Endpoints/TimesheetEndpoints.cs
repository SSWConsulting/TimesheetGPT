using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Models.Generation;
using TimesheetGPT.WebAPI.Contracts;

namespace TimesheetGPT.WebAPI.Endpoints;

public static class TimesheetEndpoints
{
    public static IEndpointRouteBuilder MapTimesheets(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/timesheets")
            .WithTags("Timesheets")
            .RequireAuthorization();

        group.MapPost("/generate", Generate)
            .WithName("GenerateTimesheet")
            .WithSummary("Generate a timesheet summary for a single day.")
            .Produces<TimesheetSummaryResponse>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        group.MapPost("/generate-range", GenerateRange)
            .WithName("GenerateTimesheetRange")
            .WithSummary("Generate timesheet summaries for a date range and return progress events.")
            .Produces<GenerateTimesheetRangeResponse>()
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return app;
    }

    private static async Task<Results<Ok<TimesheetSummaryResponse>, ValidationProblem>> Generate(
        GenerateTimesheetRequest request,
        ITimesheetGenerationService generationService,
        CancellationToken cancellationToken)
    {
        if (request.Date == default)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.Date)] = ["Date is required."]
            });

        var summary = await generationService.GenerateSummaryAsync(
            request.Date,
            request.ExtraPrompts ?? string.Empty,
            request.AdditionalNotes ?? string.Empty,
            cancellationToken);

        return TypedResults.Ok(Map(summary));
    }

    private static async Task<Results<Ok<GenerateTimesheetRangeResponse>, ValidationProblem>> GenerateRange(
        GenerateTimesheetRangeRequest request,
        ITimesheetGenerationService generationService,
        CancellationToken cancellationToken)
    {
        if (request.StartDate == default || request.EndDate == default)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.StartDate)] = ["Start and end dates are required."]
            });

        if (request.EndDate.Date < request.StartDate.Date)
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
                [nameof(request.EndDate)] = ["End date must be on or after the start date."]
            });

        var progressUpdates = new List<GenerationProgressUpdate>();
        var progress = new Progress<GenerationProgressUpdate>(update => progressUpdates.Add(update));

        var summaries = await generationService.GenerateRangeAsync(
            request.StartDate,
            request.EndDate,
            request.ExtraPrompts ?? string.Empty,
            request.AdditionalNotes ?? string.Empty,
            progress,
            cancellationToken);

        return TypedResults.Ok(new GenerateTimesheetRangeResponse(
            summaries.Select(Map).ToList(),
            progressUpdates));
    }

    private static TimesheetSummaryResponse Map(Summary summary) => new(
        summary.Date,
        summary.Text,
        summary.ModelUsed,
        summary.Emails,
        summary.Meetings);
}
