using System.Collections.Generic;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.Data.EF.Interfaces
{
    public interface IEntityStateCommitEF: IEntityStateCommit
    {
        void Affected<TEntity>(TEntity entity) where TEntity : class;
        void Affected<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
