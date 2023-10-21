using Microsoft.Graph;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Services;

public class TimesheetService(IAiService aiService, GraphServiceClient graphServiceClient)
{
    public async Task<SummaryWithRaw> GenerateSummary(DateTime date, string extraPrompts = "", string additionalNotes = "")
    {
        var graphService = new GraphService(graphServiceClient);
        
        var emails = await graphService.GetSentEmails(date);
        var meetings = await graphService.GetMeetings(date);
        // var calls = await graphService.GetTeamsCalls(date);
        // TODO: SSW needs to allow the CallRecords.Read.All scope for this to work
        
        var summary = await aiService.GetSummaryBoring(emails, meetings, extraPrompts, additionalNotes);
        
        return new SummaryWithRaw
        {
            Emails = emails,
            Meetings = meetings,
            Summary = summary,
            ModelUsed = "GPT-4" //TODO: get this from somewhere
        };
    }

    public async Task<string?> ChatWithGraph(string ask)
    {
        return await aiService.ChatWithGraphApi(ask);
    }
}
