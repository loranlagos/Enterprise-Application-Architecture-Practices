using Pacogroup.Ecommerce.Infrastructure.Interfaces;

namespace Pacogroup.Ecommerce.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICostumersRepository Customers { get; }
        public IUsersRepository Users { get; }

        public UnitOfWork(ICostumersRepository costumers, IUsersRepository users)
        {
            Customers = costumers;
            Users = users;
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}