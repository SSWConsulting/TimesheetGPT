namespace TimesheetGPT.Core.Models;

public class Summary
{
    public List<Email> Emails { get; set; } = [];
    public List<Meeting> Meetings { get; set; } = [];
    public string? Text { get; set; }
    public string? ModelUsed { get; set; }
}
