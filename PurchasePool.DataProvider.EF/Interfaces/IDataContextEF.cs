using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.DataProvider.EF.Interfaces
{
    public interface IDataContextEF : IDataContext
    {
        DbSet<T> Set<T>() where T : class;
        DbEntityEntry<T> Entry<T>(T entity) where T : class;
    }
}
