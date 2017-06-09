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
using PurchasePool.DataProvider.EF.Providers;
using PurchasePool.Common.Interfaces.Providers;

namespace PurchasePool.DataProvider.EF.Dependicies
{
    public class Export : IExportResolver
    {
        public ICommonDependecyResolver Resolver { get; set; }        

        public void Resolve()
        {
            Resolver.BindTo<IRepository, CommonRepository>();
            Resolver.BindToInScopeRequest<IDataContext, MainDataContext>();
            Resolver.BindTo<IPoductProvider, ProductProvider>();
            Resolver.BindTo<ICategoryProvider, CategoryProvider>();
        }
    }
}
