using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.DataProvider.EF.Interfaces;

namespace PurchasePool.DataProvider.EF.Providers
{
    public class CategoryProvider : ICategoryProvider
    {

        private readonly IRepository _repository;

        public CategoryProvider(IRepository repository)
        {
            _repository = repository;
        }
        public void Commit()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Get(Func<Category, bool> condition)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Set(IEnumerable<Category> collection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Set(Category singleton)
        {
            throw new NotImplementedException();
        }
    }
}
