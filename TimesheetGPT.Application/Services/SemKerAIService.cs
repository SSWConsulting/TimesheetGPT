using Microsoft.SemanticKernel;
using TimesheetGPT.Application.Interfaces;

namespace TimesheetGPT.Application.Services;

public class SemKerAIService : IAIService
{
    public async Task<string> GetSummary(string text)
    {
        var builder = new KernelBuilder();

        // builder.WithAzureChatCompletionService(
        //     "gpt-4-turbo", // Azure OpenAI Deployment Name
        //     "https://contoso.openai.azure.com/", // Azure OpenAI Endpoint
        //     "...your Azure OpenAI Key..."); // Azure OpenAI Key

        builder.WithOpenAIChatCompletionService(
            "gpt-4",
            "😉");

        var kernel = builder.Build();
        
        var summarizeFunction = kernel.CreateSemanticFunction(Prompts.SummarizeEmailsAndCalendar, maxTokens: 400, temperature: 0, topP: 0.5);

        var summary = await summarizeFunction.InvokeAsync(text);

        Console.WriteLine(summary);

        return summary.Result;
    }
}
