using Microsoft.SemanticKernel;
using TimesheetGPT.Application.Interfaces;

namespace TimesheetGPT.Application.Services;
using Microsoft.Extensions.Configuration;


public class SemKerAIService : IAIService
{
    private readonly string _apiKey;
    public SemKerAIService(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));
        
        _apiKey = configuration["OpenAI:ApiKey"] ?? "";
    }

    public async Task<string> GetSummary(string text)
    {
        var builder = new KernelBuilder();

        // builder.WithAzureChatCompletionService(
        //     "gpt-4-turbo", // Azure OpenAI Deployment Name
        //     "https://contoso.openai.azure.com/", // Azure OpenAI Endpoint
        //     "...your Azure OpenAI Key..."); // Azure OpenAI Key

        builder.WithOpenAIChatCompletionService(
            "gpt-4",
            _apiKey);

        var kernel = builder.Build();
        
        var summarizeFunction = kernel.CreateSemanticFunction(Prompts.SummarizeEmailsAndCalendar, maxTokens: 400, temperature: 0, topP: 0.5);

        var summary = await summarizeFunction.InvokeAsync(text);

        Console.WriteLine(summary);

        return summary.Result;
    }
}
