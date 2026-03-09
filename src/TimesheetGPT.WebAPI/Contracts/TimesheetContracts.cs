using System.ComponentModel.DataAnnotations;
using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Models.Generation;

namespace TimesheetGPT.WebAPI.Contracts;

public sealed record GenerateTimesheetRequest(
    [property: Required] DateTime Date,
    string? ExtraPrompts,
    string? AdditionalNotes);

public sealed record GenerateTimesheetRangeRequest(
    [property: Required] DateTime StartDate,
    [property: Required] DateTime EndDate,
    string? ExtraPrompts,
    string? AdditionalNotes);

public sealed record TimesheetSummaryResponse(
    DateTime Date,
    string? SummaryText,
    string? ModelUsed,
    IReadOnlyList<Email> Emails,
    IReadOnlyList<Meeting> Meetings);

public sealed record GenerateTimesheetRangeResponse(
    IReadOnlyList<TimesheetSummaryResponse> Results,
    IReadOnlyList<GenerationProgressUpdate> Progress);

public sealed record ChatRequest([property: Required] string Ask);

public sealed record ChatResponse(string? Response);

public sealed record UserProfileResponse(string DisplayName, string AuthProvider);

public sealed record ProviderStatusResponse(bool MicrosoftEnabled, bool GoogleEnabled, string DefaultProvider);
