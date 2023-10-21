using TimesheetGPT.Core.Models;
using TimesheetGPT.Core.Services;

namespace TimesheetGPT.Core.Interfaces;

public interface IGraphService
{
    public Task<List<Email>> GetSentEmails(DateTime date);
    public Task<List<Meeting>> GetMeetings(DateTime date);
    public Task<List<TeamsCall>> GetTeamsCalls(DateTime date);
    Task<Email> GetEmailBody(string subject);
}
