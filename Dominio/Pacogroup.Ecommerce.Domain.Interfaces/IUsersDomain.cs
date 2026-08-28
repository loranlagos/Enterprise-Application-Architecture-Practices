using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Domain.Interfaces
{
    public interface IUsersDomain
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}