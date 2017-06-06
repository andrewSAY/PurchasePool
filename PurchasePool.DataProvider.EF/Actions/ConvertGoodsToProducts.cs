using System;
using System.Collections.Generic;
using System.Linq;
using EntityCategory = PurchasePool.Data.EF.Entities.Category;
using ModelCategory = PurchasePool.Common.Models.Category;
using PurchasePool.Data.EF.Entities;
using PurchasePool.Common.Models;
using PurchasePool.Common.Interfaces;

namespace PurchasePool.DataProvider.EF.Actions
{
    class ConvertGoodsToProducts : IServiceAction<List<Product>>
    {
        private readonly List<Good> _goods;
        private readonly List<KeyValuePair<Guid, EntityCategory>> _refernces;
        public ConvertGoodsToProducts(List<Good> goods, List<KeyValuePair<Guid, EntityCategory>> references)
        {
            _goods = goods;
            _refernces = references;
        }

        public ConvertGoodsToProducts(List<CategoryGoodReference> references)
        {
            _goods = references.Select(r => r.Good).ToList();
            _refernces = references.Select(r => new KeyValuePair<Guid, EntityCategory>(r.Good.Id, r.Category)).ToList();
        }

        public List<Product> ExecuteAction()
        {
            return _goods.Select(g => {
                var product = ConvertProduct(g);
                AttachCategories(product);
                return product;
            })
            .ToList();
        }

        private ModelCategory ConvertCategory(EntityCategory category)
        {
            return new ModelCategory
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        private Product ConvertProduct(Good good)
        {
            return new Product
            {
                Id = good.Id,
                Name = good.Name,
                Description = good.Description,
                DateCreate = good.DateCreate,
                WebLink = good.WebLink,
                Categories = new List<ModelCategory>()
            };
        }

        private void AttachCategories(Product product)
        {            
            _refernces.Where(r => r.Key == product.Id)                
                .ToList()
                .ForEach(r => {                   
                    var category = ConvertCategory(r.Value);                                        
                    product.Categories.Add(category);
                });
        }
    }
}
