using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PurchasePool.DataProvider.EF.Interfaces;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.DataProvider.EF.Repositories
{
    public class CommonStateCommiter: IEntityStateCommitEF
    {
        protected readonly IDataContextEF _context;

        public CommonStateCommiter(IDataContext context)
        {
            _context = context as IDataContextEF;
        }

        public void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Affected<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Affected<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            var set = _context.Set<TEntity>();
            entities.ToList().ForEach(entity => set.Attach(entity));
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            (_context as DbContext).Entry(entity).State = EntityState.Added;
        }

        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            entities.ToList().ForEach(entity => Add(entity));
        }

        public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            entities.ToList().ForEach(entity => Update(entity));
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
            (_context as DbContext).Entry(entity).State = EntityState.Modified;
        }

        public int CommitState()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitStateAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
