namespace TimesheetGPT.Application.Interfaces;

public interface IAiService
{
    public Task<string> GetSummary(string text, string extraPrompts);
}
