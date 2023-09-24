namespace TimesheetGPT.Application;

public static class Prompts
{
    // Doesn't hot reload
    public static readonly string SummarizeEmailsAndCalendar = """
                                                               These are all my meetings attended and emails sent for the day, turn them into a succinct summary for a timesheet.
                                                               Meetings are in the format of 'Meeting Name - Meeting Length', use the length to determine relevancy and importance but dont include it in the output
                                                               Ignore meetings that are trivial e.g. Daily Scrums, these happen every day and are not important
                                                               all day (or 9 hour) meetings are bookings e.g. which Client I worked for or if it contains "SSW" this means its an internal day and what comes after "SSW" was my focus    
                                                               Use the emails to determine tasks complete.
                                                               Note if the email subject starts with RE:, this means its a reply not the original

                                                               keep it lighthearted and use a few emojis

                                                               Output a Markdown unordered list.

                                                               {{$input}}
                                                               """;
}
