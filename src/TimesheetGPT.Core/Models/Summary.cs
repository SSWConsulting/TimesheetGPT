namespace TimesheetGPT.Core.Models;

public class SummaryWithRaw
{
    public List<Email> Emails { get; set; }
    public List<Meeting> Meetings { get; set; }
    public string Summary { get; set; }
    public string ModelUsed { get; set; }
}
