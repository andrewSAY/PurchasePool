using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PurchasePool.DataProvider.EF.Repositories
{
    public interface IRepository<TEntity> where TEntity: class
    {
        IEnumerable<TEntity> All();
        TEntity FirstBy(Expression<Func<TEntity, bool>> condition);
        TResult FirstBy<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> condition);
        IEnumerable<TResult>FindBy<TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames);
        Task<IEnumerable<TEntity>> AllAsync();       
    }
}
