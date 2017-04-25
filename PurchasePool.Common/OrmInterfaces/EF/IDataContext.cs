using System.Linq;
using System.Collections.Generic;

namespace PurchasePool.Common.OrmInterfaces.EF
{
    public interface IDataContext
    {
        int SaveChanges();
        IQueryable<TEntity> Set<TEntity>() where TEntity : class;
        IQueryable<TEntity> NotTrackedSet<TEntity>() where TEntity : class;
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
