using System.Collections.Generic;
using System.Linq;
using EntityCategory = PurchasePool.Data.EF.Entities.Category;
using ModelCategory = PurchasePool.Common.Models.Category;
using PurchasePool.Data.EF.Entities;
using PurchasePool.Common.Models;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.DataProvider.EF.Actions
{
    class ConvertProductsToGoods : IServiceAction<List<KeyValuePair<Good, List<EntityCategory>>>>
    {
        private readonly List<Product> _products;
        public ConvertProductsToGoods(List<Product> products)
        {
            _products = products;        
        }

        public List<KeyValuePair<Good, List<EntityCategory>>> ExecuteAction()
        {
            var returningList = _products.Select(p => GetProductAsPairGoodAndCategory(p));            
            return returningList.ToList();
        }

        private List<EntityCategory> ConvertCategories(List<ModelCategory> categories)
        {
            var collection = new List<EntityCategory>();
            categories.ForEach(category => {
                collection.Add(new EntityCategory
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                });
            });

            return collection;
        }

        private Good ConvertProduct(Product product)
        {
            return new Good
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                DateCreate = product.DateCreate,
                WebLink = product.WebLink                
            };
        }

        private KeyValuePair<Good, List<EntityCategory>> GetProductAsPairGoodAndCategory(Product product)
        {
            var newPair = new KeyValuePair<Good, List<EntityCategory>>(ConvertProduct(product)
                ,ConvertCategories(product.Categories.ToList()));            
            return newPair;
        }
    }
}
