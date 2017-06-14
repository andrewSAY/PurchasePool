using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PurchasePool.Common.Models;
using PurchasePool.Common.Interfaces.Providers;
using PurchasePool.Web.BusinessLogic.Validators;

namespace PurchasePool.Web.Controllers.Api
{
    public class ProductController : ApiController
    {
        IPoductProvider _productProvider;
        ICategoryProvider _categoryProvider;
        public ProductController(IPoductProvider productProvider, ICategoryProvider categoryProvider)
        {
            _productProvider = productProvider;
            _categoryProvider = categoryProvider;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productProvider.Get();
        }

        public IEnumerable<Category> GetCategories()
        {
            return _categoryProvider.Get();
        }

        public void PostProduct(Product product)
        {
            var validator = new ProductValidator(product);
            validator.Valid();
            _productProvider.Set(product);
            _productProvider.Commit();
        } 
        
        public Product GetProduct(Guid id)
        {
            return _productProvider.GetById(id);
        }
    }
}
