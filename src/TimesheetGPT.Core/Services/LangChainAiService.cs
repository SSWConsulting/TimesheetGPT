using TimesheetGPT.Core.Interfaces;

namespace TimesheetGPT.Core.Services;

public class LangChainAiService : IAiService
{
    public async Task<string> GetSummary(string text, string extraPrompts)
    {
        throw new NotImplementedException();
    }
}
