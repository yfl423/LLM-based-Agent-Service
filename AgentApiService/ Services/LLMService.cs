using System.Text;
using System.Net.Http;
using System.Text.Json;

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

    private readonly HttpClient _httpClient;

    public LLMService(IMemoryCache cache, ILogger<LLMService> logger, IOptions<OpenAIOptions> options)
    {
        _cache = cache;
        _logger = logger;
        _apiKey = options.Value.ApiKey;
        _httpClient = new HttpClient();
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

        Message[] prompt = PromptContextBuilder.BuildPrompt(history);

       _logger.LogInformation("SessionId: {SessionId}, Prompt:\n{Prompt}", sessionId, string.Join("\n", prompt.Select(m => m.ToString())));

        // OpenAI call
        var llmResult = await CallOpenAIApiAsync(prompt);

        // Add assistant response to history
        history.Add(new Message { Role = "assistant", Content = llmResult.Message });

        // Re-cache updated session
        _cache.Set(sessionKey, history, TimeSpan.FromMinutes(10));

        return llmResult;
    }

    private async Task<LLMResponse> CallOpenAIApiAsync(Message[] prompt)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = prompt,
            temperature = 0.2
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

        var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        _logger.LogInformation("Raw LLM response: {0}", responseBody);

        using var doc = JsonDocument.Parse(responseBody);
        var contentText = doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString();

        // Deserialize content to LLMResponse
        var llmResponse = JsonSerializer.Deserialize<LLMResponse>(contentText!, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return llmResponse!;
    }


    // private async Task<LLMResponse> FakeOpenAICallAsync(string prompt)
    // {
    //     _logger.LogInformation("Sending prompt to LLM:\n{Prompt}", prompt);

    //     await Task.Delay(100); // Simulate latency

    //     return new LLMResponse
    //     {
    //         IsExecutable = true,
    //         Sql = "SELECT * FROM plan_table WHERE id = 1;",
    //         Message = "Your current plan is 'Unlimited Everything'."
    //     };
    // }
}
