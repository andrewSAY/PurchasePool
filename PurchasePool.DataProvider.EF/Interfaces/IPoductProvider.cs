using System;
using System.Collections.Generic;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;

namespace PurchasePool.DataProvider.EF.Interfaces
{
    public interface IPoductProvider: IDataProvider<Product>
    {
        IEnumerable<Product> GetByName(string name);
        Product GetById(Guid Id);
        IEnumerable<Product> GetInCategories(IEnumerable<Category> categories);
    }
}
