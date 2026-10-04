using Pacogroup.Ecommerce.Domain.Entity;
using Pacogroup.Ecommerce.Domain.Interfaces;
using Pacogroup.Ecommerce.Infrastructure.Interfaces;

namespace Pacogroup.Ecommerce.Domain.Core;

public class CustomersDomain : ICostumersDomain
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Inyeccion de las interfaces necesarias, es este caso 
    /// - IUnitOfWork
    /// </summary>
    /// <param name="unitOfWork"></param>
    public CustomersDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CountAsync()
    {
        return await _unitOfWork.Customers.CountAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string customerId)
    {
        return await _unitOfWork.Customers.DeleteAsync(customerId);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Costumer>> GetAllAsync()
    {
        return await _unitOfWork.Customers.GetAllAsync();
    }

    public async Task<IEnumerable<Costumer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        return await _unitOfWork.Customers.GetAllWithPaginationAsync(pageNumber, pageSize);
    }

    /// <inheritdoc/>
    public async Task<Costumer> GetAsync(string customerId)
    {
        return await _unitOfWork.Customers.GetByIdAsync(customerId);
    }

    /// <inheritdoc/>
    public async Task<bool> InsertAsync(Costumer customer)
    {
        return await _unitOfWork.Customers.InsertAsync(customer);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(Costumer customer)
    {
        return await _unitOfWork.Customers.UpdateAsync(customer);
    }
}
