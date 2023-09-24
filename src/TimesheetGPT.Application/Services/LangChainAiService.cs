using TimesheetGPT.Application.Interfaces;

namespace TimesheetGPT.Application.Services;

public class LangChainAiService : IAiService
{
    public async Task<string> GetSummary(string text, string extraPrompts)
    {
        throw new NotImplementedException();
    }
}
