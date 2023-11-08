using TimesheetGPT.Core.Models;

namespace TimesheetGPT.WebUI.Models;

public class ResultVm
{
    public bool Loading { get; set; }
    public IList<Email>? Emails { get; set; }
    public IList<Meeting>? Meetings { get; set; }
    public string? SummaryText { get; set; }
}