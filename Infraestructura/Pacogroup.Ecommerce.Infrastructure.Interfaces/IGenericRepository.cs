namespace Pacogroup.Ecommerce.Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);

        Task<int> CountAsync();

        Task<T?> GetByIdAsync(string id);

        Task<bool> InsertAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(string id);
    }
}