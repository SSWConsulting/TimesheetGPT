namespace TimesheetGPT.Core;

public static class PromptTemplates
{
    // Doesn't hot reload
    public static readonly string SummarizeEmailsAndCalendar = $"""
                                                                Generate a concise timesheet summary in chronological order from my meetings and emails.
                                                                
                                                                For meetings, follow the format 'Meeting Name - Meeting Length'
                                                                Skip non-essential meetings like Daily Scrums.
                                                                Treat all-day (or 9-hour) meetings as bookings. They indicate the client I worked for or, if tagged with 'SSW,' an internal focus day.
                                                                Use email subjects to figure out what tasks were completed.
                                                                Note that emails starting with 'RE:' are replies, not new tasks.
                                                                An email titled 'Sprint X Review' means I led that Sprint review/retro.
                                                                Merge meetings and emails into one summary. If an item appears in both, mention it just once.
                                                                Ignore the day's meetings if an email is marked 'Sick Today.'
                                                                Appointments labeled 'Leave' should be omitted.
                                                                Only output the timesheet summary so i can copy it directly. Use a Markdown unordered list, keeping it lighthearted with a few emojis. 🌟

                                                                {PromptVarFormatter(PromptVariables.ExtraPrompts)}
                                                                
                                                                Here is the data:
                                                                
                                                                {PromptVarFormatter(PromptVariables.Emails)}
                                                                
                                                                {PromptVarFormatter(PromptVariables.Meetings)}
                                                                
                                                                Additional notes:
                                                                
                                                                {PromptVarFormatter(PromptVariables.AdditionalNotes)}
                                                                """;
    
    public static readonly string SummarizeEmailBody = $"""
                                                        Summarise this email body in 1-2 sentences. This summary will later be used to generate a timesheet summary.
                                                        Respond in this format: Recipients - Subject - Summary of body
                                                        
                                                        Here is the data:
                                                        Recipients: {PromptVarFormatter(PromptVariables.Recipients)}
                                                        Subject: {PromptVarFormatter(PromptVariables.Subject)}
                                                        Body: {PromptVarFormatter(PromptVariables.Subject)}
                                                        """;
    
    private static string PromptVarFormatter(string promptVar) => "{{$" + promptVar + "}}";
}
