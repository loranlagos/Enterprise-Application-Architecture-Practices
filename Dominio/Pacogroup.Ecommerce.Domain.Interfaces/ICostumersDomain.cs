using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Domain.Interfaces
{
    public interface ICostumersDomain
    {
        /// <summary>
        /// Permite la insercion de un Costumer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> InsertAsync(Costumer customer);

        /// <summary>
        /// Permite actulizar un Costumer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Costumer customer);

        /// <summary>
        /// Permite eliminar un Costumer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string customerId);

        /// <summary>
        /// Permite obtener un Costumer por su identificador unico
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Costumer> GetAsync(string customerId);

        /// <summary>
        /// Permite obtener una lista de Costumers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Costumer>> GetAllAsync();

        /// <summary>
        /// Permite obtener una lista de Costumers con paginacion
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<Costumer>> GetAllWithPaginationAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Permite contar todos los Costumers en la tabla
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
    }
}