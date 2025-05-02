using System.Data;
using System.Text.Json;
using AgentApiService.Repository;

namespace AgentApiService.Services;

public class ClientService
{
    private readonly ClientRepository _clientRepository;
    private readonly ILogger<ClientService> _logger;

    public ClientService(ClientRepository clientRepository, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
    }

    public async Task<string> ExecuteSQL(string sql)
    {
        var table = await _clientRepository.ExecuteSqlAsync(sql);

        _logger.LogInformation("Rows fetched from DB: " + table.Rows.Count);

        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }

        return JsonSerializer.Serialize(list, new JsonSerializerOptions
        {
            WriteIndented = false
        });
    }
}
