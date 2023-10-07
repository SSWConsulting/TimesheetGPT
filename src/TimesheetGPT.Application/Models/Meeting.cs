namespace TimesheetGPT.Application.Models;

public class Meeting
{
    public TimeSpan Length { get; set; }
    public string Name { get; set; }
    public bool Repeating { get; set; }
    public string Sender { get; set; }
}
