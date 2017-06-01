using System.Collections.Generic;
using PurchasePool.Common.Models;

namespace PurchasePool.Common.ServiceInterfaces
{
    public interface IPurchaseDataProvider:IDataServiceProvider<Product>
    {
        IEnumerable<Product> GetByOperator(Category operator_);
        IEnumerable<Product> GetByOperator(Category operator_, int pageNumber, int pageSize);
        IEnumerable<Product> GetByOperator(Category operator_, int pageNumber, int pageSize, string sortFieldName, SortDirection direction);
    }
}
