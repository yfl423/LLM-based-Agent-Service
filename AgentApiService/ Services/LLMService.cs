using System.Text;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using AgentApiService.Models;
using AgentApiService.Prompts;

namespace AgentApiService.Services;

public class LLMService
{
    private readonly string _apiKey;
    private readonly IMemoryCache _cache; // Maintain session history for multi-round conversation
    private readonly ILogger<LLMService> _logger;

    public LLMService(IMemoryCache cache, ILogger<LLMService> logger, IOptions<OpenAIOptions> options)
    {
        _cache = cache;
        _logger = logger;
        _apiKey = options.Value.ApiKey;
    }

    public async Task<LLMResponse> ProcessMessageAsync(string sessionId, string message)
    {
        var sessionKey = $"session:{sessionId}";

        // Get or initialize session context
        var history = _cache.GetOrCreate(sessionKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            
            List<Message> list = new List<Message>();
            return list;
        });

        history.Add(new Message { Role = "user", Content = message });

        string prompt = PromptContextBuilder.BuildPrompt(history);

        // Simulate OpenAI call
        var llmResult = await FakeOpenAICallAsync(prompt);

        // Add assistant response to history
        history.Add(new Message { Role = "assistant", Content = llmResult.Message });

        // Re-cache updated session
        _cache.Set(sessionKey, history, TimeSpan.FromMinutes(10));

        return llmResult;
    }


    private async Task<LLMResponse> FakeOpenAICallAsync(string prompt)
    {
        _logger.LogInformation("Sending prompt to LLM:\n{Prompt}", prompt);

        await Task.Delay(100); // Simulate latency

        return new LLMResponse
        {
            IsExecutable = true,
            Sql = "SELECT * FROM plan_table WHERE id = 1;",
            Message = "Your current plan is 'Unlimited Everything'."
        };
    }
}
