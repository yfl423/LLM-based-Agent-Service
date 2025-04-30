using System.Text.Json;

namespace AgentApiService.Models;

public class LLMResponse
{
    public string Intent { get; set; } // LLM tells if it's malicious
    public string Sql { get; set; } // Return applicable SQL
    public object? Data { get; set; } // 
    public string Message { get; set; } // Generate repsonse info to client

    public override string ToString()
    {
        return System.Text.Json.JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            WriteIndented = false
        });
    }
}