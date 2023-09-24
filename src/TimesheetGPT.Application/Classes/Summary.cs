namespace TimesheetGPT.Application.Classes;

public class SummaryWithRaw
{
    public List<string> Emails { get; set; }
    public List<Meeting> Meetings { get; set; }
    public string Summary { get; set; }
    public string ModelUsed { get; set; }
}
