using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PurchasePool.Common;
using PurchasePool.Common.OrmInterfaces.EF;
using PurchasePool.Common.Models;
using PurchasePool.Common.ServiceInterfaces;


namespace PurchasePool.DataProvider.EF.Services
{
    public class PurchaseService : IPurchaseDataProvider
    {
        private IDataContext _context;
        public PurchaseService(IDataContext context)
        {
            _context = context;
        }
        public void Add(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetByOperator(Operator operator_)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetByOperator(Operator operator_, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetByOperator(Operator operator_, int pageNumber, int pageSize, string sortFieldName, SortDirection direction)
        {
            throw new NotImplementedException();
        }

        public void Remove(Purchase entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Purchase entity)
        {
            throw new NotImplementedException();
        }
    }
}
