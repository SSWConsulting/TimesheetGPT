using Microsoft.Graph;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Services;

public class TimesheetService(IAiService aiService, GraphServiceClient graphServiceClient)
{
    public async Task<Summary> GenerateSummary(DateTime date, string extraPrompts = "", string additionalNotes = "", CancellationToken ct = default)
    {
        var graphService = new GraphService(graphServiceClient);
        
        var emails = await graphService.GetSentEmails(date, ct);
        var meetings = await graphService.GetMeetings(date, ct);
        // var calls = await graphService.GetTeamsCalls(date);
        // TODO: SSW needs to allow the CallRecords.Read.All scope for this to work
        
        var summary = await aiService.GetSummaryBoring(emails, meetings, extraPrompts, ct, additionalNotes);
        
        return new Summary
        {
            Emails = emails,
            Meetings = meetings,
            Text = summary,
            ModelUsed = "GPT-4" //TODO: get this from somewhere
        };
    }

    public async Task<string?> ChatWithGraph(string ask)
    {
        return await aiService.ChatWithGraphApi(ask);
    }
}
