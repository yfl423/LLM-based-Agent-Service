using System.Text.Json;

namespace AgentApiService.Models;

public class LLMResponse
{
    public bool IsExecutable { get; set; } // LLM decides if client's request is in our biz scope, and can produce executable SQL
    public string? Sql { get; set; } // Return applicable SQL
    public string? Message { get; set; } // Generate extra repsonse info to client

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = false
        });
    }
}