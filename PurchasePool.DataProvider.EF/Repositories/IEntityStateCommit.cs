using System.Collections.Generic;

namespace PurchasePool.DataProvider.EF.Repositories
{
{
    public interface IEntityStateCommit
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        int CommitState();
    }
}
