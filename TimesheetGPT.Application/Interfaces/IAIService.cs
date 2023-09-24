namespace TimesheetGPT.Application.Interfaces;

public interface IAIService
{
    public Task<string> GetSummary(string text);
}
