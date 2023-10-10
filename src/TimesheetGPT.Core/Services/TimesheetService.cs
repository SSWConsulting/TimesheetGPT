using System.Collections;
using Microsoft.Graph;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Services;

public class TimesheetService(IAiService aiService, GraphServiceClient graphServiceClient)
{
    public async Task<SummaryWithRaw> GenerateSummary(DateTime date, string extraPrompts = "", string additionalNotes = "")
    {
        var graphService = new GraphService(graphServiceClient);
        
        var emailSubjects = await graphService.GetEmailSubjects(date);
        var meetings = await graphService.GetMeetings(date);
        // var calls = await graphService.GetTeamsCalls(date);
        // TODO: SSW needs to allow the CallRecords.Read.All scope for this to work
        
        var summary = await aiService.GetSummary(StringifyData(emailSubjects, meetings), extraPrompts, additionalNotes);
        
        return new SummaryWithRaw
        {
            Emails = emailSubjects,
            Meetings = meetings,
            Summary = summary,
            ModelUsed = "GPT-4" //TODO: get this from somewhere
        };
    }
    
    private string StringifyData(IEnumerable<string> emails, IList<Meeting> meetings)
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
