using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PurchasePool.Common.Interfaces
{
    public interface IRepository
    {
        IEntityStateCommit CommiteInterface { get; }
        IEnumerable<TEntity> All<TEntity>() where TEntity : class;
        TEntity FirstBy<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        TResult FirstBy<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class;
        TEntity FirstBy<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class;
        IEnumerable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        IEnumerable<TResult> FindBy<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class;
        IEnumerable<TEntity> FindBy<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class;
        Task<IEnumerable<TEntity>> AllAsync<TEntity>() where TEntity : class;
        Task<IEnumerable<TEntity>> FindByAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
        Task<IEnumerable<TResult>> FindByAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class;
        Task<IEnumerable<TEntity>> FindByAsync<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class;
        Task<TResult> FirstByAsync<TEntity, TResult>(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, TResult>> filter, IEnumerable<string> includedePropertyNames) where TEntity : class;
        Task<TEntity> FirstByAsync<TEntity>(Expression<Func<TEntity, bool>> condition, IEnumerable<string> includedePropertyNames) where TEntity : class;
        Task<TEntity> FirstByAsync<TEntity>(Expression<Func<TEntity, bool>> condition) where TEntity : class;
    }

}
