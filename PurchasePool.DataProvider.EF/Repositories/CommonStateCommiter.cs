﻿using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using PurchasePool.Data.EF.Interfaces;
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
            _context.Set<TEntity>().Attach(entity);
            _context.Set<TEntity>().Remove(entity);
            var dbContext = _context as DbContext;
            if (dbContext != null)
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                dbContext.ChangeTracker.DetectChanges();                
            }
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
            var dbContext = _context as DbContext;
            if (dbContext != null)
            {
                dbContext.ChangeTracker.DetectChanges();
                dbContext.Entry(entity).State = EntityState.Added;
            }
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
            var dbContext = _context as DbContext;            
            _context.Set<TEntity>().Attach(entity);
            if(dbContext != null)
            {
                dbContext.ChangeTracker.DetectChanges();
                dbContext.Entry(entity).State = EntityState.Modified;
            }            
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
