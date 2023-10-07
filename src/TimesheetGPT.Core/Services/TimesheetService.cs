using System.Collections;
using Microsoft.Graph;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Services;

public class TimesheetService
{
    private readonly IAiService _aiService;

    public TimesheetService(IAiService aiService)
    {
        _aiService = aiService;
    }

    public async Task<SummaryWithRaw> GenerateSummary(DateTime date, GraphServiceClient client, string extraPrompts = "", string additionalNotes = "")
    {
        IGraphService graphService = new GraphService(client);
        
        var emailSubjects = await graphService.GetEmailSubjects(date);
        var meetings = await graphService.GetMeetings(date);
        
        var summary = await _aiService.GetSummary(StringifyData(emailSubjects, meetings, additionalNotes), extraPrompts);
        
        return new SummaryWithRaw
        {
            Emails = emailSubjects,
            Meetings = meetings,
            Summary = summary,
            ModelUsed = "GPT-4" //TODO: get this from somewhere
        };
    }
    
    private string StringifyData(List<string> emails, List<Meeting> meetings, string additionalNotes = "")
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
        
        if (string.IsNullOrWhiteSpace(additionalNotes))
            return result;
        
        result += "\n Additional notes (freeform) \n";
        result += $"{additionalNotes}\n";
        
        return result;
    }
}
