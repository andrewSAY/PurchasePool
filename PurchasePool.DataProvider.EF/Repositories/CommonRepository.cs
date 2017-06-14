using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PurchasePool.Common.Interfaces;
using PurchasePool.Data.EF.Interfaces;

namespace PurchasePool.DataProvider.EF.Repositories
{
    public class CommonRepository : IRepository
    {
        protected readonly IDataContextEF _context;
        protected readonly bool _noTracking;
        public IEntityStateCommit CommiteInterface { get; private set; }

        public CommonRepository(IDataContext context)
        {
            _context = context as IDataContextEF;
            CommiteInterface = new CommonStateCommiter(context);
            _noTracking = (_context as DbContext) != null;
        }

        private IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            return _noTracking ? _context.Set<TEntity>().AsNoTracking() : _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All<TEntity>() where TEntity : class
        {
            return Set<TEntity>().ToList();
        }


        public TEntity FirstBy<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return FirstBy(condition, x => x, new List<string>());
        }

        public TResult FirstBy<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            var query = AttachProperties<TEntity>(includedePropertyNames);
            return query.Where(condition).Select(filter).FirstOrDefault();
        }
        public TEntity FirstBy<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            return FirstBy(condition, x => x, includedePropertyNames);
        }
        public IEnumerable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return FindBy(condition, x => x, new List<string>());
        }
        public IEnumerable<TResult> FindBy<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            var query = AttachProperties<TEntity>(includedePropertyNames);
            return query.Where(condition).Select(filter).ToList();
        }
        public IEnumerable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            return FindBy(condition, x => x, includedePropertyNames);
        }

        public async Task<IEnumerable<TEntity>> AllAsync<TEntity>() where TEntity : class
        {
            return await Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TResult>> FindByAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            var query = AttachProperties<TEntity>(includedePropertyNames);
            return await query.Where(condition).Select(filter).ToListAsync();
        }

        public async Task<TEntity> FirstByAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return await FirstByAsync(condition, x => x, new List<string>());
        }

        public async Task<IEnumerable<TEntity>> FindByAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class
        {
            return await FindByAsync(condition, x => x, new List<string>());
        }
        public Task<TEntity> FirstByAsync<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            return FirstByAsync(condition, x => x, includedePropertyNames);
        }
        public async Task<TResult> FirstByAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            var query = AttachProperties<TEntity>(includedePropertyNames);
            return await query.Where(condition).Select(filter).FirstOrDefaultAsync();
        }
        public Task<IEnumerable<TEntity>> FindByAsync<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class
        {
            return FindByAsync(condition, x => x, includedePropertyNames);
        }

        protected IQueryable<TEntity> AttachProperties<TEntity>(IEnumerable<string> properties) where TEntity : class
        {
            return properties.Aggregate(Set<TEntity>(), (current, property) => current.Include(property));
        }
    }
}
