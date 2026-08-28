namespace Pacogroup.Ecommerce.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICostumersRepository Customers { get; }
        IUsersRepository Users { get; }
    }
}