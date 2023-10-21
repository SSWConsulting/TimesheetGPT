using Microsoft.SemanticKernel.AI;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI;
using Microsoft.SemanticKernel.TemplateEngine;

namespace TimesheetGPT.Core;

// TODO: Refactor into a JSON file
// https://learn.microsoft.com/en-us/semantic-kernel/ai-orchestration/plugins/semantic-functions/serializing-semantic-functions
public static class PromptConfigs
{
    public static readonly PromptTemplateConfig SummarizeEmailsAndCalendar = new()
    {
        Schema = 1,
        Description = "Summarises users emails and meetings.",
        ModelSettings = new List<AIRequestSettings>
        {
            // Note: Token limit hurts things like additional notes. If you don't have enough, the prompt will suck
            new OpenAIRequestSettings { MaxTokens = 1000, Temperature = 0, TopP = 0.5 }
        },
        Input =
        {
            Parameters = new List<PromptTemplateConfig.InputParameter>
            {
                new()
                {
                    Name = PromptVariables.Meetings,
                    Description = "meetings",
                    DefaultValue = ""
                },
                new()
                {
                    Name = PromptVariables.Emails,
                    Description = "emails",
                    DefaultValue = ""
                },
                new()
                {
                    Name = PromptVariables.AdditionalNotes,
                    Description = "Additional Notes",
                    DefaultValue = ""
                },
                new()
                {
                    Name = PromptVariables.ExtraPrompts,
                    Description = "extraPrompts",
                    DefaultValue = ""
                }
            }
        }
    };
    
    public static readonly PromptTemplateConfig SummarizeEmailBody = new()
    {
        Schema = 1,
        Description = "Summarizes body of an email",
        ModelSettings = new List<AIRequestSettings>
        {
            // Note: Token limit hurts things like additional notes. If you don't have enough, the prompt will suck
            new OpenAIRequestSettings { MaxTokens = 1000, Temperature = 0, TopP = 0.5 }
        },
        Input =
        {
            Parameters = new List<PromptTemplateConfig.InputParameter>
            {
                new()
                {
                    Name = PromptVariables.Recipients,
                    Description = nameof(PromptVariables.Recipients),
                    DefaultValue = ""
                },
                new()
                {
                    Name = PromptVariables.Subject,
                    Description = nameof(PromptVariables.Subject),
                    DefaultValue = ""
                },
                new()
                {
                    Name = PromptVariables.EmailBody,
                    Description = nameof(PromptVariables.EmailBody),
                    DefaultValue = ""
                }
            }
        }
    };
}
