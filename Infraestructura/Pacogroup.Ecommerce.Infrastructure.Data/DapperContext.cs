namespace Pacogroup.Ecommerce.Infrastructure.Data;

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

public class DapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("NorthwindConnection") ?? throw new InvalidOperationException("La cadena de conexion 'NorthwindConnection' no fue encontrada");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
