using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Models.Generation;

namespace TimesheetGPT.Core.Services;

public class TimesheetGenerationService(IAiService aiService, IGraphService graphService) : ITimesheetGenerationService
{
    public async Task<Summary> GenerateSummaryAsync(
        DateTime date,
        string extraPrompts = "",
        string additionalNotes = "",
        CancellationToken cancellationToken = default)
    {
        var emails = await graphService.GetSentEmails(date, cancellationToken);
        var meetings = await graphService.GetMeetings(date, cancellationToken);
        var summaryText = await aiService.GetSummaryBoring(emails, meetings, extraPrompts, cancellationToken, additionalNotes);

        return new Summary
        {
            Date = date,
            Emails = emails,
            Meetings = meetings,
            Text = summaryText,
            ModelUsed = "gpt-4.1-mini"
        };
    }

    public async Task<IReadOnlyList<Summary>> GenerateRangeAsync(
        DateTime start,
        DateTime end,
        string extraPrompts = "",
        string additionalNotes = "",
        IProgress<GenerationProgressUpdate>? progress = null,
        CancellationToken cancellationToken = default)
    {
        if (end < start)
            throw new ArgumentException("End date must be on or after the start date.", nameof(end));

        var dates = EachDay(start.Date, end.Date).ToList();
        var results = new List<Summary>(dates.Count);

        for (var index = 0; index < dates.Count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var date = dates[index];
            progress?.Report(new GenerationProgressUpdate(date, "loading", $"Loading activity for {date:dddd, d MMM}", index, dates.Count));

            var summary = await GenerateSummaryAsync(date, extraPrompts, additionalNotes, cancellationToken);
            results.Add(summary);

            progress?.Report(new GenerationProgressUpdate(date, "completed", $"Generated summary for {date:dddd, d MMM}", index + 1, dates.Count));
        }

        return results;
    }

    private static IEnumerable<DateTime> EachDay(DateTime start, DateTime end)
    {
        for (var day = start; day <= end; day = day.AddDays(1))
            yield return day;
    }
}
