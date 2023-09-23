using Microsoft.Graph;
using TimesheetGPT.Application.Classes;

namespace TimesheetGPT.Application;

public class GraphService : IGraphService
{
    private GraphServiceClient _client;

    public GraphService(GraphServiceClient client)
    {
        if (client == null)
            throw new ArgumentNullException(nameof(client));

        _client = client ?? throw new ArgumentNullException(nameof(client));
    }
    
    
    public async Task<List<string>> GetEmailSubjects(DateTime date)
    {
        if (_client == null)
            throw new ArgumentNullException("HOW???");
        
        // 1. Connect to Graph API
        // GET https://graph.microsoft.com/v1.0/me
        var user = await _client.Me.GetAsync();

        Console.WriteLine($"User: {user.DisplayName}");

        // 2. Get emails from date from sent folder
        // GET https://graph.microsoft.com/v1.0/me/mailFolders('sentitems')/messages
        var messages = await _client.Me.MailFolders["sentitems"].Messages
            // .Filter($"sentDateTime ge {date.ToString("yyyy-MM-dd")}")
            // .Select("subject")
            .GetAsync();


        if (messages is { Value.Count: > 1 })
        {
            return messages.Value.Select(m => m.Subject).ToList();
        }

        return new List<string>(); //slack
    }
    
    
    public List<Meeting> GetMeetings(DateTime date) => throw new NotImplementedException();
    public List<string> GetTeamsCalls(DateTime date) => throw new NotImplementedException();
}
