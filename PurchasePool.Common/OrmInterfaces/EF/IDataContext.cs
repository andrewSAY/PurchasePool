using System.Linq;
using System.Collections.Generic;

namespace PurchasePool.Common.OrmInterfaces.EF
{
    public interface IDataContext
    {
        int SaveChanges();
        IQueryable<T> Set<T>() where T : class;
        IQueryable<T> NotTrackedSet<T>() where T : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
