using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Moq;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.Tests.TestsSupport;

namespace PurchasePool.Tests.Fakes
{
    class FakeDataContext : IDataContext
    {
        public bool AreSavingSuccess = true;
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry Entry(object entity)
        {
            throw new NotImplementedException();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            if (AreSavingSuccess)
            {
                return 1;
            }
            return 0;
        }

        public async Task<int> SaveChangesAsync()
        {
            await Task.Delay(1);
            if (AreSavingSuccess)
            {
                return 1;
            }
            return 0;
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            DbSet<TEntity> targetField = null;
            foreach (var property in GetType().GetProperties().ToList())
            {
                if (property.GetType() == typeof(DbSet<TEntity>))
                {
                    targetField = property.GetValue(this) as DbSet<TEntity>;
                    break;
                }
            }

            return targetField;
        }

        public void SetCollectionAsDbSet<T>(List<T> value) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IDbAsyncEnumerable<T>>()
            .Setup(m => m.GetAsyncEnumerator())
            .Returns(new TestDbAsyncEnumerator<T>(value.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(() => new TestDbAsyncQueryProvider<T>(value.AsQueryable().Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(() => value.AsQueryable().Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(() => value.AsQueryable().ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => value.GetEnumerator());

            mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(s => value.Add(s));
            mockSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(s => value.Remove(s));
            mockSet.Setup(d => d.RemoveRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>(s => {
                s.ToList().ForEach(s_ => value.Remove(s_));
            });
            var propertyList = GetType().GetProperties().ToList();
            foreach (var property in propertyList)
            {
                if (property.PropertyType == typeof(DbSet<T>))
                {
                    property.SetValue(this, mockSet.Object);
                    return;
                }
            }
        }
    }
}
