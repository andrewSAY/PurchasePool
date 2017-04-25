using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PurchasePool.DataProvider.EF.Repositories
{
    public class CommonRepository<TEntity> : IRepository<TEntity> where TEntity: class
    {        
        protected readonly IQueryable<TEntity> _set;

        public CommonRepository(IDataContext context, TrackingBehavior tracking = TrackingBehavior.NoTracking)
        {     
            _set = tracking == TrackingBehavior.NoTracking ? context.NotTrackedSet<TEntity>() :
                context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return _set.ToList();
        }


        public TEntity FirstBy(Expression<Func<TEntity, bool>> condition)
        {
            return FirstBy<TEntity>(condition, x => x, new List<string>());
        }

        public TResult FirstBy<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames)
        {
            var query = AttachProperties(includedePropertyNames);
            return query.Where(condition).Select(filter).FirstOrDefault();
        }
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> condition)
        {
            return FindBy(condition, x => x, new List<string>());
        }
        public IEnumerable<TResult> FindBy<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames)
        {
            var query = AttachProperties(includedePropertyNames);
            return query.Where(condition).Select(filter).ToList();
        }
        
        protected IQueryable<TEntity> AttachProperties(IEnumerable<string> properties)
        {            
            return properties.Aggregate(_set, (current, property) => current.Include(property));
        }        
    }
}
