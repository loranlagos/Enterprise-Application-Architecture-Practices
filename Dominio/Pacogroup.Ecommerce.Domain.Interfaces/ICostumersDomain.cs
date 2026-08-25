using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Domain.Interfaces
{
    public interface ICostumersDomain
    {
        Task<bool> InsertAsync(Costumer customer);
        Task<bool> UpdateAsync(Costumer customer);
        Task<bool> DeleteAsync(string customerId);
        Task<bool> GetAsync(string customerId);
        Task<IEnumerable<Costumer>> GetAllAsync();
    }
}