using System;
using System.Collections.Generic;
using System.Linq;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.DataProvider.EF.Interfaces;

namespace PurchasePool.DataProvider.EF
{
    public class ProductProvider : IPoductProvider
    {
        private readonly IRepository _repository;

        public ProductProvider(IRepository repository)
        {
            _repository = repository;
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get(Func<Product, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Set(IEnumerable<Product> collection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Set(Product singleton)
        {
            throw new NotImplementedException();
        }
    }
}
