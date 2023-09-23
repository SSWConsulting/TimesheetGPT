using TimesheetGPT.Application.Classes;

namespace TimesheetGPT.Application;

public interface IGraphService
{
    public Task<List<string>> GetEmailSubjects(DateTime date);
    public Task<List<Meeting>> GetMeetings(DateTime date);
    public Task<List<string>> GetTeamsCalls(DateTime date);
}
