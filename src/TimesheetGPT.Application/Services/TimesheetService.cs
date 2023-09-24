using Microsoft.Graph;
using TimesheetGPT.Application.Classes;
using TimesheetGPT.Application.Interfaces;

namespace TimesheetGPT.Application.Services;

public class TimesheetService
{
    private readonly IAiService _aiService;

    public TimesheetService(IAiService aiService)
    {
        _aiService = aiService;
    }

    public async Task<SummaryWithRaw> GenerateSummary(DateTime date, GraphServiceClient client, string extraPrompts = "")
    {
        IGraphService graphService = new GraphService(client);
        
        var emailSubjects = await graphService.GetEmailSubjects(date);
        var meetings = await graphService.GetMeetings(date);
        
        var summary = await _aiService.GetSummary(StringifyData(emailSubjects, meetings), extraPrompts);
        
        return new SummaryWithRaw
        {
            Emails = emailSubjects,
            Meetings = meetings,
            Summary = summary,
            ModelUsed = "GPT-4" //TODO: get this from somewhere
        };
    }
    
    private string StringifyData(IList<string> emails, IList<Meeting> meetings)
    {
        var result = "Sent emails (subject) \n";
        foreach (var email in emails)
        {
            result += email + "\n";
        }
        result += "\n Calendar Events (name - length) \n";
        foreach (var meeting in meetings)
        {
            result += $"{meeting.Name} - {meeting.Length} \n";
        }
        return result;
    }
}
