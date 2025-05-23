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
    private readonly ClientService _clientService;

    public ClientController(ILogger<ClientController> logger, LLMService llmService, ClientService clientService)
    {
        _logger = logger;
        _llmService = llmService;
        _clientService = clientService;
    }

    /**
    * User → [LLM generates SQL] → [Executes SQL] → [LLM encapsulates resposne to client]
    **/
    [HttpGet("infoPass")]
    public async Task<string> Get([FromQuery] ClientRequest req)
    {
        _logger.LogInformation("ClientController Get called: SessionId={SessionId}, Message={Message}", req.SessionID, req.Message);

        var response = await _llmService.ProcessMessageAsync(req.SessionID, req.Message, false);

        _logger.LogInformation("Get Response from LLM: " + response);

        
        if(response.IsExecutable){
            // TODO: Apply LLM Response to do  db execution

            //_clientService.ExecuteSQL(response.Sql);
            //_logger.LogInformation("SQL execution result: " + _clientService.ExecuteSQL(response.Sql));
            // TODO: Call LLM to encapsulate response
            // try
            //     {
            //         var sqlResult = _clientService.ExecuteSQL(response.Sql);
            //         _logger.LogInformation("SQL execution result: " + sqlResult);
            //     }
            //     catch (Exception ex)
            //     {
            //         _logger.LogError(ex, "SQL execution failed.");
            //         return "Error executing SQL: " + ex.Message;
            //     }
            

            // _clientService.ExecuteSQL(response.Sql);

            // Apply LLM Response to do  db execution
            string res = await _clientService.ExecuteSQL(response.Sql);
            
            _logger.LogInformation("SQL execution result: " + res);
            
            // Call LLM to encapsulate response
            response = await _llmService.ProcessMessageAsync(req.SessionID, res, true);
        }

        return response.Message;
    }
}
