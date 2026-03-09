using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Models.Generation;

namespace TimesheetGPT.Core.Services;

public class TimesheetService(IAiService aiService, ITimesheetGenerationService generationService)
{
    public Task<Summary> GenerateSummary(DateTime date, string extraPrompts = "", string additionalNotes = "", CancellationToken ct = default)
        => generationService.GenerateSummaryAsync(date, extraPrompts, additionalNotes, ct);

    public Task<IReadOnlyList<Summary>> GenerateRange(
        DateTime start,
        DateTime end,
        string extraPrompts = "",
        string additionalNotes = "",
        IProgress<GenerationProgressUpdate>? progress = null,
        CancellationToken ct = default)
        => generationService.GenerateRangeAsync(start, end, extraPrompts, additionalNotes, progress, ct);

    public async Task<string?> ChatWithGraph(string ask)
    {
        return await aiService.ChatWithGraphApi(ask);
    }

    public async Task<string?> SubmitTimesheet(string summaryText, DateTime date, CancellationToken ct = default)
    {
        return await aiService.SubmitTimesheetViaMcp(summaryText, date, ct);
    }
}
