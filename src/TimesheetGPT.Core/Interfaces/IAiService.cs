namespace TimesheetGPT.Core.Interfaces;

public interface IAiService
{
    public Task<string> GetSummary(string text, string extraPrompts);
}
