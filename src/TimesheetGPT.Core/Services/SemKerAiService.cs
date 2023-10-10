using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using TimesheetGPT.Core.Interfaces;

namespace TimesheetGPT.Core.Services;

public class SemKerAiService : IAiService
{
    private readonly string _apiKey;
    public SemKerAiService(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        _apiKey = configuration["OpenAI:ApiKey"] ?? "";
    }

    public async Task<string> GetSummary(string text, string extraPrompts, string additionalNotes = "")
    {
        var builder = new KernelBuilder();

        // builder.WithAzureChatCompletionService(
        //     "gpt-4-turbo", // Azure OpenAI Deployment Name
        //     "https://contoso.openai.azure.com/", // Azure OpenAI Endpoint
        //     "...your Azure OpenAI Key..."); // Azure OpenAI Key

        builder.WithOpenAIChatCompletionService(
            // "gpt-3.5-turbo", // Cheap mode
            "gpt-4", // 💸
            _apiKey);

        var kernel = builder.Build();
        
        // Note: Token limit hurts things like additional notes. If you don't have enough, the prompt will suck
        var summarizeFunction = kernel.CreateSemanticFunction(Prompts.SummarizeEmailsAndCalendar, maxTokens: 400, temperature: 0, topP: 0.5);
        
        var context = kernel.CreateNewContext();

        context.Variables.TryAdd(PromptVariables.Input, text);
        context.Variables.TryAdd(PromptVariables.AdditionalNotes, additionalNotes);
        context.Variables.TryAdd(PromptVariables.ExtraPrompts, extraPrompts);

        var summary = await summarizeFunction.InvokeAsync(context);

        return summary.Result;
    }
}
