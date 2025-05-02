using System.Data;
using Microsoft.Data.SqlClient;


namespace AgentApiService.Repository;

public class ClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("SqlServer");
    }

    public async Task<DataTable> ExecuteSqlAsync(string sql)
    {
        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand(sql, conn);
        var dt = new DataTable();

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();
        dt.Load(reader);
        return dt;
    }
}