using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Domain.Interfaces;

namespace Pacogroup.Ecommerce.Domain.Core;

public class CustomersDomain : ICostumersDomain
{
    public Task<bool> DeleteAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Costumer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetAsync(string customerId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertAsync(Costumer customer)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Costumer customer)
    {
        throw new NotImplementedException();
    }
}
