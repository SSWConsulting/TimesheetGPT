using Microsoft.SemanticKernel;
using System.ComponentModel;
using System.Globalization;
using TimesheetGPT.Core.Interfaces;
using System.Text.Json;


namespace TimesheetGPT.Core;

public class GraphPlugins(IGraphService graphService)
{
    [SKFunction, Description("Get email body from Id")]
    public async Task<string?> GetEmailBody(string id)
    {
        return (await graphService.GetEmailBody(id)).Body;
    }
    
    [SKFunction, Description("Get sent emails (subject, to, Id) for a date)")]
    public async Task<string> GetSentEmails(DateTime dateTime)
    {
        var emails = await graphService.GetSentEmails(dateTime, new CancellationToken());
        return JsonSerializer.Serialize(emails);
    }
    
    [SKFunction, Description("Get meetings for a date")]
    public async Task<string> GetMeetings(DateTime dateTime)
    {
        var meetings = await graphService.GetMeetings(dateTime, new CancellationToken());
        return JsonSerializer.Serialize(meetings);
    }
    
    [SKFunction, Description("Get todays date")]
    public string GetTodaysDate(DateTime dateTime)
    {
        return DateTime.Today.ToString(CultureInfo.InvariantCulture);
        
    }
}
