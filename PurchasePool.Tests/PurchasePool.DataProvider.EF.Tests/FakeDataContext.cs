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
        private int _changesCount = 0;
        private int _changesCountAfterSave = 0;
        public int ChangesCount { get { return _changesCountAfterSave; } set { _changesCountAfterSave = value; } }
        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().ToList().AddRange(entities);
            _changesCount += entities.Count();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().ToList().Add(entity);
            _changesCount ++;
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
                _changesCount ++;
            });
            _changesCount += entities.Count();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().ToList().Remove(entity);
            _changesCount ++;
        }

        public int SaveChanges()
        {
            ChangesCount = _changesCount;
            return ChangesCount();
        }

        public IQueryable<TEntity> Set<TEntity>() where TEntity : class
        {
            var property = typeof(FakeDataContext).GetProperties()
                .FirstOrDefault(p => p.GetType() == typeof(TEntity));
            return property as IQueryable<TEntity>;
        }

        public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {            
            _changesCount += entities.Count();
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {            
            _changesCount++;
        }
    }
}
