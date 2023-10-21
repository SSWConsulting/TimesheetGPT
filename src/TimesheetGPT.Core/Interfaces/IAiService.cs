using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Interfaces;

public interface IAiService
{
    public Task<string?> ChatWithGraphApi(string ask);
    public Task<string> GetSummaryBoring(IList<Email> emails, IEnumerable<Meeting> meetings, string extraPrompts, CancellationToken cancellationToken, string additionalNotes = "");
}
