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
    class ConvertCategories<TReturned> : IServiceAction<List<TReturned>> where TReturned: class
    {
        private readonly List<ModelCategory> _modelCategories;
        private readonly List<EntityCategory> _entityCategories;
        public ConvertCategories(List<ModelCategory> categories)
        {            
            _modelCategories = categories;
        }
        public ConvertCategories(List<EntityCategory> categories)
        {
            _entityCategories = categories;
        }

        public List<TReturned> ExecuteAction()
        {
            var categories = new List<TReturned>();
            if(typeof(EntityCategory) == typeof(TReturned))
            {
                _modelCategories.ForEach(category => {
                categories.Add(ConvertCategory(category) as TReturned);
                });
            }
            if (typeof(ModelCategory) == typeof(TReturned))
            {
                _entityCategories.ForEach(category => {
                    categories.Add(ConvertCategory(category) as TReturned);
                });
            }

            return categories;
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

        private EntityCategory ConvertCategory(ModelCategory category)
        {
            return new EntityCategory
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}
