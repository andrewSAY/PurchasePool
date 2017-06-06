using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasePool.Common.Interfaces.DI;
using PurchasePool.Data.EF;
using PurchasePool.DataProvider.EF;
using PurchasePool.DataProvider.EF.Repositories;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.DataProvider.EF.Dependicies
{
    public class Export : IExportResolver
    {
        public ICommonDependecyResolver Resolver { get; set; }        

        public void Resolve()
        {
            Resolver.BindTo<IRepository, CommonRepository>();
            Resolver.BindToInScopeRequest<IDataContext, MainDataContext>();
        }
    }
}
