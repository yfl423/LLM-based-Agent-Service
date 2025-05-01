using AgentApiService.Models;
using AgentApiService.Prompts;

namespace AgentApiService.Prompts;

public static class PromptContextBuilder
{
    public static Message[] BuildPrompt(List<Message> history)
    {
        var systemContent =
            PromptTemplates.SystemIntro + "\n\n" +
            "Schema:\n" + PromptTemplates.TableSchema + "\n\n" +
            PromptTemplates.ResponseFormatHint + "\n\n" +
            PromptTemplates.AdditionalRules;

        var messages = new List<Message>();

        
        messages.Add(new Message
        {
            Role = "system",
            Content = systemContent
        });

        messages.AddRange(history);

        return messages.ToArray();
    }
}
