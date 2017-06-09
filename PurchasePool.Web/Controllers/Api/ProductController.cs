using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PurchasePool.Common.Models;
using PurchasePool.Common.Interfaces.Providers;
using PurchasePool.DataProvider.EF.Providers;

namespace PurchasePool.Web.Controllers.Api
{
    public class ProductController : ApiController
    {
        IPoductProvider _productProvider;
        public ProductController(IPoductProvider productProvider)
        {
            _productProvider = productProvider;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productProvider.Get();
        }
    }
}
