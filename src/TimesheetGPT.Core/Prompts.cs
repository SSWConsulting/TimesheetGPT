namespace TimesheetGPT.Core;

public static class Prompts
{

    
    // Doesn't hot reload
    public static readonly string SummarizeEmailsAndCalendar = $$$"""
                                              These are all my meetings attended and emails sent for the day, turn them into a succinct summary for a timesheet.
                                              Meetings are in the format of 'Meeting Name - Meeting Length', use the length to determine relevancy and importance but dont include it in the output
                                              Ignore meetings that are trivial e.g. Daily Scrums, these happen every day and are not important
                                              all day (or 9 hour) meetings are bookings e.g. which Client I worked for or if it contains 'SSW' this means its an internal day and what comes after 'SSW' was my focus
                                              Use the emails to determine tasks complete.
                                              Note if the email subject starts with RE:, this means its a reply not the original
                                              If you see an email with 'Sprint X Review', this means I ran the Sprint review/retro meeting

                                              Combine the emails and meetings into a summary for my timesheet.
                                              Sometimes I run a sprint review/retro meeting and send an email at the end. This means the email and meeting are the same thing, so only include it once

                                              if there is an email with 'Sick Today', this means I was sick and didnt work so ignore the meetings in my calendar for that day

                                              keep response lighthearted and use a few emojis, but dont overdo it

                                              Only output a Markdown unordered list.
                                              If there is no meetings or emails, respond with a joke about the user not doing any work on this day :)

                                              {{${{{PromptVariables.ExtraPrompts}}}}}
                                              
                                              {{${{{PromptVariables.AdditionalNotes}}}}}
                                               
                                              {{${{{PromptVariables.Input}}}}}
                                           """;

}


public static class PromptVariables
{
    public const string ExtraPrompts = "extraPrompts";
    public const string AdditionalNotes = "additionalNotes";
    public const string Input = "inputContent";
}