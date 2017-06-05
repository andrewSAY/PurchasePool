using System;
using System.Collections.Generic;
using PurchasePool.Common.Models;

namespace PurchasePool.Common.Interfaces.Providers
{
    public interface IPoductProvider: IDataProvider<Product>
    {
        IEnumerable<Product> GetByName(string name);
        Product GetById(Guid Id);
        IEnumerable<Product> GetInCategories(IEnumerable<Category> categories);
    }
}
