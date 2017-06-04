using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasePool.Common.Interfaces.DI;
using PurchasePool.Common.Interfaces;
using PurchasePool.DataProvider.EF.Providers;
using PurchasePool.DataProvider.EF.Interfaces;

namespace PurchasePool.Web.Dependicies
{
    public class Export : IExportResolver
    {
        public ICommonDependecyResolver Resolver { get; set; }        

        public void Resolve()
        {
            Resolver.BindTo<IPoductProvider, ProductProvider>();
            Resolver.BindTo<ICategoryProvider, CategoryProvider>();
        }
    }
}
