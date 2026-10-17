using Microsoft.Data.SqlClient;
using SimplyUmbraco.Models;

namespace SimplyUmbraco.Services;

public class DistrictRepository
{
    private readonly IConfiguration _configuration;

    public DistrictRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<DistrictListItem>> GetDistrictsAsync()
    {
        var districts = new List<DistrictListItem>();

        var connectionString =
            _configuration.GetConnectionString("ShopDataModel");

        await using var connection =
            new SqlConnection(connectionString);

        await connection.OpenAsync();

        var sql = """
            SELECT
                DistrictID,
                DistrictName,
                IsActive,
                ActiveFrom,
                ActiveTo,
                StatusText
            FROM DistrictListView
            ORDER BY DistrictName
            """;

        await using var command =
            new SqlCommand(sql, connection);

        await using var reader =
            await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            districts.Add(new DistrictListItem
            {
                DistrictID = reader.GetGuid(0),
                DistrictName = reader.GetString(1),
                IsActive = reader.GetBoolean(2),
                ActiveFrom = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
                ActiveTo = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                StatusText = reader.IsDBNull(5) ? "" : reader.GetString(5)
            });
        }
        return districts;
        return districts;
    }

    public async Task<int> GetDistrictCountAsync()
    {
        var connectionString =
            _configuration.GetConnectionString("ShopDataModel");

        await using var connection =
            new SqlConnection(connectionString);

        await connection.OpenAsync();

        var sql = "SELECT COUNT(*) FROM DistrictListView";

        await using var command =
            new SqlCommand(sql, connection);

        return (int)await command.ExecuteScalarAsync();
    }

}
    
