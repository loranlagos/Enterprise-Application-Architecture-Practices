using Pacogroup.Ecommerce.Application.DTO;
using Pacogroup.Ecommerce.Transversal.Common;

namespace Pacogroup.Ecommerce.Application.Interfaces
{
    public interface ICostumersApplication
    {
        Task<Response<bool>> InsertAsync(CustomerDTO customerDTO);
        Task<Response<bool>> UpdateAsync(CustomerDTO customerDTO);
        Task<Response<bool>> DeleteAsync(string customerId);
        Task<Response<CustomerDTO>> GetAsync(string customerId);
        Task<Response<IEnumerable<CustomerDTO>>> GetAllAsync();
    }
}