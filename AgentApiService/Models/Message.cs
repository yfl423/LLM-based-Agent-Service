namespace AgentApiService.Models;

public class Message
{
    public string Role { get; set; } = "";     // "user" or "assistant or "system" "
    public string Content { get; set; } = "";  // Message content
}