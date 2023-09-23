using Microsoft.Graph;
using Microsoft.Graph.Models;
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
        var nextDay = date.AddDays(1);

        // GET https://graph.microsoft.com/v1.0/me/mailFolders('sentitems')/messages
        var messages = await _client.Me.MailFolders["sentitems"].Messages
            .GetAsync(rc =>
            {
                rc.QueryParameters.Top = 999;
                rc.QueryParameters.Select =
                    new[] { "subject" };
                rc.QueryParameters.Filter =
                    $"sentDateTime ge {date:yyyy-MM-dd} and sentDateTime lt {nextDay:yyyy-MM-dd}";
                rc.QueryParameters.Orderby = new[] { "sentDateTime asc" };

            });


        if (messages is { Value.Count: > 1 })
        {
            return messages.Value.Select(m => m.Subject).ToList();
        }

        return new List<string>(); //slack
    }


    public async Task<List<Meeting>> GetMeetings(DateTime date)
    {
        var nextDay = date.AddDays(1);
        var meetings = await _client.Me.CalendarView.GetAsync(rc =>
        {
            rc.QueryParameters.Top = 999;
            rc.QueryParameters.StartDateTime = date.ToString("o");
            rc.QueryParameters.EndDateTime = nextDay.ToString("o");
            rc.QueryParameters.Orderby = new[] { "start/dateTime" };
            rc.QueryParameters.Select = new[] { "subject", "start", "end", "occurrenceId" };
        });
        
        if (meetings is { Value.Count: > 1 })
        {
            return meetings.Value.Select(m => new Meeting
            {
                Name = m.Subject,
                Length = DateTime.Parse(m.End.DateTime) - DateTime.Parse(m.Start.DateTime),
                Repeating = m.Type == EventType.Occurrence,
                // Sender = m.EmailAddress.Address TODO: Why is Organizer and attendees null? permissions?
            }).ToList();
        }
        
        return new List<Meeting>(); //slack
    }
    public Task<List<string>> GetTeamsCalls(DateTime date) => throw new NotImplementedException();
}
