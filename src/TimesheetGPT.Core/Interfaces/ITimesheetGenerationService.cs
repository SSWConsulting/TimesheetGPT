using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Models.Generation;

namespace TimesheetGPT.Core.Interfaces;

public interface ITimesheetGenerationService
{
    Task<Summary> GenerateSummaryAsync(
        DateTime date,
        string extraPrompts = "",
        string additionalNotes = "",
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Summary>> GenerateRangeAsync(
        DateTime start,
        DateTime end,
        string extraPrompts = "",
        string additionalNotes = "",
        IProgress<GenerationProgressUpdate>? progress = null,
        CancellationToken cancellationToken = default);
}
