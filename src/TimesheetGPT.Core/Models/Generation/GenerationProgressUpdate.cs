namespace TimesheetGPT.Core.Models.Generation;

public sealed record GenerationProgressUpdate(
    DateTime Date,
    string Stage,
    string Message,
    int CompletedDays,
    int TotalDays)
{
    public double PercentComplete => TotalDays == 0 ? 0 : (double)CompletedDays / TotalDays * 100;
}
