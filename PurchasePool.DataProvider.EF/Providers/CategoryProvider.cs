using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PurchasePool.Common.Interfaces;
using PurchasePool.DataProvider.EF.Actions;
using PurchasePool.Common.Interfaces.Providers;
using EntityCategory = PurchasePool.Data.EF.Entities.Category;
using ModelCategory = PurchasePool.Common.Models.Category;

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
            _repository.CommiteInterface.CommitState();
        }

        public IEnumerable<ModelCategory> GetByName(string name)
        {
            return GetByCondition(c => c.Name == name);
        }
        public ModelCategory GetById(Guid Id)
        {
            return GetByCondition(c => c.Id == Id).FirstOrDefault();
        }

        public IEnumerable<ModelCategory> Get()
        {
            return GetByCondition(c => c == c);
        }

        public void Set(IEnumerable<ModelCategory> collection)
        {
            var identityCollection = collection.Select(c => c.Id);
            var existingCategoryIdsCollection = _repository.FindBy<EntityCategory, Guid>(c => identityCollection.Contains(c.Id), c => c.Id, new List<string>());
            var entityCollection = new ConvertCategories<EntityCategory>(collection.ToList()).ExecuteAction();
            entityCollection.ForEach(category => {
                if(existingCategoryIdsCollection.FirstOrDefault(c => c == category.Id) != Guid.Empty)
                {
                    _repository.CommiteInterface.Update(category);
                }
                else
                {
                    _repository.CommiteInterface.Add(category);
                }
            });
        }

        public void Set(ModelCategory singleton)
        {
            Set(new List<ModelCategory> { singleton });
        }

        private IEnumerable<ModelCategory> GetByCondition(Expression<Func<EntityCategory, bool>> condition)
        {
            var categories = _repository.FindBy(condition).ToList();
            return new ConvertCategories<ModelCategory>(categories).ExecuteAction();
        }
    }
}
