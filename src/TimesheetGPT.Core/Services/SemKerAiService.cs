using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Planners;
using TimesheetGPT.Core.Interfaces;
using TimesheetGPT.Core.Models;

namespace TimesheetGPT.Core.Services;

public class SemKerAiService : IAiService
{
    private readonly string _apiKey;
    private readonly IGraphService _graphService;
    public SemKerAiService(IConfiguration configuration, IGraphService graphService)
    {
        _graphService = graphService;
        ArgumentNullException.ThrowIfNull(configuration);

        _apiKey = configuration["OpenAI:ApiKey"] ?? "";
    }

    public async Task<string?> ChatWithGraphApi(string ask)
    {
        var builder = new KernelBuilder();

        builder.WithOpenAIChatCompletionService(
            // "gpt-3.5-turbo", // Cheap mode
            "gpt-4", // 💸
            _apiKey);

        var kernel = builder.Build();

        kernel.ImportFunctions(new GraphPlugins(_graphService));

        var planner = new StepwisePlanner(kernel);

        var plan = planner.CreatePlan(ask);

        var context = kernel.CreateNewContext();
        var res = await plan.InvokeAsync(context);

        return res.GetValue<string>();
    }
    
    public async Task<string> GetSummaryBoring(IList<Email> emails, IEnumerable<Meeting> meetings, string extraPrompts, CancellationToken cancellationToken, string additionalNotes = "")
    {
        var builder = new KernelBuilder();

        builder.WithOpenAIChatCompletionService(
            // "gpt-3.5-turbo", // Cheap mode
            "gpt-4", // 💸
            _apiKey);

        var kernel = builder.Build();

        var generateTimesheetFunction = kernel.CreateSemanticFunction(PromptTemplates.SummarizeEmailsAndCalendar, PromptConfigs.SummarizeEmailsAndCalendar);

        var context = kernel.CreateNewContext();

        context.Variables.TryAdd(PromptVariables.Emails, StringifyEmails(emails));
        context.Variables.TryAdd(PromptVariables.Meetings, StringifyMeetings(meetings));
        context.Variables.TryAdd(PromptVariables.AdditionalNotes, additionalNotes);
        context.Variables.TryAdd(PromptVariables.ExtraPrompts, extraPrompts);

        await generateTimesheetFunction.InvokeAsync(context, cancellationToken: cancellationToken);

        return context.Result;
    }

    private static string StringifyMeetings(IEnumerable<Meeting> meetings)
    {
        var result = "Calendar Events (name - length) \n";
        foreach (var meeting in meetings)
        {
            result += $"{meeting.Name} - {meeting.Length} \n";
        }

        return result;
    }
    private static string StringifyEmails(IEnumerable<Email> emails)
    {
        var result = "Sent emails (recipients - subject - bodyPreview) \n";
        foreach (var email in emails)
        {
            result += $"{email.To} - {email.Subject} \n";
        }

        return result;
    }
}
