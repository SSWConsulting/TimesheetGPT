namespace TimesheetGPT.Application.Classes;

public abstract class Meeting
{
    public DateTimeOffset Length { get; set; }
    public string Name { get; set; }
    public bool Repeating { get; set; }
}
