using System;
using System.Collections.Generic;
using System.Linq;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.Data.EF.Entities;
using PurchasePool.Common.Interfaces.Providers;
using System.Linq.Expressions;
using PurchasePool.DataProvider.EF.Actions;
using EntityCategory = PurchasePool.Data.EF.Entities.Category;
using ModelCategory = PurchasePool.Common.Models.Category;

namespace PurchasePool.DataProvider.EF.Providers
{
    public class ProductProvider : IPoductProvider
    {
        private readonly IRepository _repository;

        public ProductProvider(IRepository repository)
        {
            _repository = repository;
        }
        public void Commit()
        {
            _repository.CommiteInterface.CommitState();
        }       

        public IEnumerable<Product> Get()
        {
            return GetByCondition(p => 1 == 1);
        }

        public Product GetById(Guid Id)
        {
            return GetByCondition(p => p.Id == Id)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetByName(string name)
        {
            return GetByCondition(p => p.Name == name);
        }
        public IEnumerable<Product> GetInCategories(IEnumerable<ModelCategory> categories)
        {
            var categoryIds = categories.Select(c => c.Id);
            var references = _repository.FindBy<CategoryGoodReference>(r => categoryIds.Contains(r.Category.Id));

            return new ConvertGoodsToProducts(references.ToList()).ExecuteAction();
        }

        public void Set(IEnumerable<Product> collection)
        {
            var pairs = new ConvertProductsToGoods(collection.ToList()).ExecuteAction();
            SaveGoods(pairs);
            SaveRefernces(pairs);
        }

        public void Set(Product singleton)
        {
            Set(new List<Product> { singleton });
        }        

        private IEnumerable<Product> GetByCondition(Expression<Func<Good, bool>> condition)
        {
            var goods = _repository.FindBy(condition);
            var goodIds = goods.Select(g => g.Id);
            var references = _repository.FindBy<CategoryGoodReference>(r => goodIds.Contains(r.Good.Id))
                .Select(r => new KeyValuePair<Guid, EntityCategory>(r.Good.Id, r.Category));
            return new ConvertGoodsToProducts(goods.ToList(), references.ToList()).ExecuteAction();
        }
        private void SaveGoods(List<KeyValuePair<Good, List<EntityCategory>>> pairs)
        {
            var goodIdCollection = pairs.Select(i => i.Key.Id);
            var existingGoodsCollection = _repository.FindBy<Good>(g => goodIdCollection.Contains(g.Id))
                .Select(g => g.Id)
                .ToList();
            pairs.ForEach(pair => {
                if(existingGoodsCollection.Contains(pair.Key.Id))
                {
                    _repository.CommiteInterface.Update(pair.Key);
                }
                else
                {
                    _repository.CommiteInterface.Add(pair.Key);
                }
            });
        }      
        private void SaveRefernces(List<KeyValuePair<Good, List<EntityCategory>>> pairs)
        {            
            var goodIdCollection = pairs.Select(i => i.Key.Id);            
            var existingReferencesCollection = _repository
                .FindBy<CategoryGoodReference>(r => goodIdCollection.Contains(r.Good.Id))
                .ToList();            
            pairs.ForEach(pair => {
                pair.Value.ForEach(category => {
                    _repository.CommiteInterface.Update(category);
                    var reference = existingReferencesCollection
                    .FirstOrDefault(r => r.Good.Id == pair.Key.Id && r.Category.Id == category.Id);
                    if (reference == null)
                    {
                        reference = new CategoryGoodReference
                        {
                            Category = category,
                            Good = pair.Key
                        };
                        _repository.CommiteInterface.Add(reference);
                    }
                });
                RemoveOldReferences(existingReferencesCollection, pair);
            });
        }

        private void RemoveOldReferences(List<CategoryGoodReference> existingReferencesCollection, KeyValuePair<Good, List<EntityCategory>> pair)
        {            
            existingReferencesCollection.Where(r => r.Good.Id == pair.Key.Id)
                .ToList()
                .ForEach(reference =>{
                    var category = pair.Value.FirstOrDefault(c => c.Id == reference.Category.Id);
                    if (category == null)
                    {
                        reference.Good = pair.Key;
                        reference.Category = category;
                        _repository.CommiteInterface.Remove(reference);
                    }
            });
        }
    }
}
