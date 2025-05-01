using System.Text.Json.Serialization;

namespace AgentApiService.Models;

public class Message
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = "";     // "user" or "assistant or "system" "

    [JsonPropertyName("content")]
    public string Content { get; set; } = "";  // Message content
    
    public override string ToString()
    {
        return $"{{Role: \"{Role}\", Content: \"{Content}\"}}";
    }
}