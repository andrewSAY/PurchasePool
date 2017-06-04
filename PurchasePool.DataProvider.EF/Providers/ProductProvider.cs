﻿using System;
using System.Collections.Generic;
using System.Linq;
using PurchasePool.Common.Interfaces;
using PurchasePool.Common.Models;
using PurchasePool.Data.EF.Entities;
using PurchasePool.DataProvider.EF.Interfaces;
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
            return GetByCondition(p => true);
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

        public IEnumerable<Product> Set(IEnumerable<Product> collection)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Set(Product singleton)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Product> GetByCondition(Expression<Func<Good, bool>> condition)
        {
            var goods = _repository.FindBy<Good>(p => true);
            var goodIds = goods.Select(g => g.Id);
            var references = _repository.FindBy<CategoryGoodReference>(r => goodIds.Contains(r.Good.Id))
                .Select(r => new KeyValuePair<Guid, EntityCategory>(r.Good.Id, r.Category));
            return new ConvertGoodsToProducts(goods.ToList(), references.ToList()).ExecuteAction();
        }
    }
}