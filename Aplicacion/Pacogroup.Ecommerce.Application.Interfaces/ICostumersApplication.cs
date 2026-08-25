using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Transversal.Common;

namespace Pacogroup.Ecommerce.Application.Interfaces
{
    public interface ICostumersApplication
    {
        Task<Reponse<bool>> InsertAsync(CustomerDTO customerDTO);
        Task<Reponse<bool>> UpdateAsync(CustomerDTO customerDTO);
        Task<Reponse<bool>> DeleteAsync(string customerId);
        Task<Reponse<CustomerDTO>> GetAsync(string customerId);
        Task<Reponse<IEnumerable<CustomerDTO>>> GetAllAsync();
    }
}