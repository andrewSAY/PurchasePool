using System.Collections.Generic;
using PurchasePool.Common.Models;

namespace PurchasePool.Common.ServiceInterfaces
{
    public interface IPurchaseDataProvider:IDataServiceProvider<Purchase>
    {
        IEnumerable<Purchase> GetByOperator(Operator operator_);
        IEnumerable<Purchase> GetByOperator(Operator operator_, int pageNumber, int pageSize);
        IEnumerable<Purchase> GetByOperator(Operator operator_, int pageNumber, int pageSize, string sortFieldName, SortDirection direction);
    }
}
