using System.Data;
using Dapper;
using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Infrastructure.Data;
using Pacogroup.Ecommerce.Infrastructure.Interfaces;

namespace Pacogroup.Ecommerce.Infrastructure.Repository;

public class CostumersRepository : ICostumersRepository
{
    private readonly DapperContext _context;

    public CostumersRepository(DapperContext dapperContext)
    {
        _context = dapperContext;
    }

    public async Task<bool> DeleteAsync(string costumerId)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersDelete";

        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", costumerId);

        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<IEnumerable<Costumer>> GetAllAsync()
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersList";

        var customers = await connection.QueryAsync<Costumer>(query, commandType: CommandType.StoredProcedure);
        return customers;
    }

    public async Task<Costumer?> GetByIdAsync(string costumerId)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersGetById";

        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", costumerId);

        var customer = await connection.QuerySingleOrDefaultAsync<Costumer>(query, parameters, commandType: CommandType.StoredProcedure);
        return customer;
    }

    public async Task<bool> InsertAsync(Costumer customer)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersInsert";

        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);

        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Costumer customer)
    {
        using var connection = _context.CreateConnection();
        var query = "CustomersUpdate";

        var parameters = new DynamicParameters();
        parameters.Add("CustomerID", customer.CustomerId);
        parameters.Add("CompanyName", customer.CompanyName);
        parameters.Add("ContactName", customer.ContactName);
        parameters.Add("ContactTitle", customer.ContactTitle);
        parameters.Add("Address", customer.Address);
        parameters.Add("City", customer.City);
        parameters.Add("Region", customer.Region);
        parameters.Add("PostalCode", customer.PostalCode);
        parameters.Add("Country", customer.Country);
        parameters.Add("Phone", customer.Phone);
        parameters.Add("Fax", customer.Fax);

        var result = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
        return result > 0;
    }
}
