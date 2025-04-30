using System.Text;
using AgentApiService.Models;

namespace AgentApiService.Prompts;

public static class PromptContextBuilder
{
    public static string BuildPrompt(List<Message> history)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"System: {PromptTemplates.SystemIntro}");
        sb.AppendLine($"Schema Info: {PromptTemplates.TableSchema}");
        sb.AppendLine();

        foreach (var message in history)
        {
            sb.AppendLine($"{message.Role}: {message.Content}");
        }

        return sb.ToString();
    }
}
