using Pacogroup.Ecommerce.Domain.Entity;

namespace Pacogroup.Ecommerce.Transversal.Common
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}