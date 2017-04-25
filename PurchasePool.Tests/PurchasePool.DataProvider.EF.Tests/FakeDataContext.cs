using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasePool.Common.OrmInterfaces.EF;
using PurchasePool.Common.Models;

namespace PurchasePool.Web.Tests.PurchasePool.DataProvider.EF.Tests
{
    class FakeDataContext : IDataContext
    {
        public List<Purchase> Purchases { get; set; }
        public int changesCount = 0;
        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().ToList().AddRange(entities);
            changesCount += entities.Count();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().ToList().Add(entity);
            changesCount ++;
        }

        public IQueryable<TEntity> NotTrackedSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();            
        }

        public void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            var list = Set<TEntity>().ToList();
            entities.ToList().ForEach(entity => {
                list.Remove(entity);
                changesCount ++;
            });
            changesCount += entities.Count();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().ToList().Remove(entity);
            changesCount ++;
        }

        public int SaveChanges()
        {
            return changesCount;
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            var property = typeof(FakeDataContext).GetProperties()
                .FirstOrDefault(p => p.GetType() == typeof(TEntity));
            return property as IQueryable<TEntity>;
        }

        public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {            
            changesCount += entities.Count();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {            
            changesCount++;
        }
    }
}
