using AgentApiService.Models;
using AgentApiService.Prompts;

namespace AgentApiService.Prompts;

public static class PromptContextBuilder
{
    public static List<Message> InitializePrompt()
    {
        var systemContent =
            PromptTemplates.SystemIntro + "\n\n" +
            "Schema:\n" + PromptTemplates.TableSchema + "\n\n" +
            PromptTemplates.SampleQuery + "\n\n" +
            PromptTemplates.ResponseFormatHint + "\n\n" +
            PromptTemplates.AdditionalRules;

        var messages = new List<Message>();

        
        messages.Add(new Message
        {
            Role = "system",
            Content = systemContent
        });

        return messages;
    }

    public static List<Message> AppendPrompt(List<Message> history, string message, bool isSQLResult)
    {
        history.Add(new Message{
            Role = "system",
            Content = isSQLResult ? PromptTemplates.SQLResultHint + message : message,
        });
        return history;
    }
}
