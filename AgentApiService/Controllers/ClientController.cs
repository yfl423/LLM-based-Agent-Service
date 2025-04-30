using Microsoft.AspNetCore.Mvc;
using AgentApiService.DTO;
using AgentApiService.Services;
using AgentApiService.Models;

namespace AgentApiService.Controllers;

[ApiController]
[Route("api/client")]
public class ClientController : ControllerBase
{
    private readonly ILogger<ClientController> _logger;
    private readonly LLMService _llmService;

    public ClientController(ILogger<ClientController> logger, LLMService llmService)
    {
        _logger = logger;
        _llmService = llmService;
    }

    [HttpGet("infoPass")]
    public async Task<string> Get([FromQuery] ClientRequest req)
    {
        _logger.LogInformation("ClientController Get called: SessionId={SessionId}, Message={Message}", req.SessionID, req.Message);

        var response = await _llmService.ProcessMessageAsync(req.SessionID, req.Message);

        _logger.LogInformation("Get Response from LLM: " + response);

        // TODO: Apply LLM Response to do biz logic or db execution
        return "OK";
    }
}
