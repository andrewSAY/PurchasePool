using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PurchasePool.Data.EF.Entities;
using EntityCategory = PurchasePool.Data.EF.Entities.Category;
using ModelCategory = PurchasePool.Common.Models.Category;
using PurchasePool.DataProvider.EF.Providers;
using PurchasePool.Common.Models;
using PurchasePool.Tests.Fakes;
using PurchasePool.DataProvider.EF.Repositories;

namespace PurchasePool.Web.Tests.PurchasePool.DataProvider.EF.Tests.Providers
{
    public class CategoryProviderTests
    {
        #region TestInfo       
        private Guid category_1 = Guid.NewGuid();
        private Guid category_2 = Guid.NewGuid();
        #endregion
        #region preparing methods
        private FakeDataContext CreateDataContext()
        {
            var context = new FakeDataContext();           

            var category1 = new EntityCategory
            {
                Id = category_1,
                Name = "Category1",
                Description = "Category one"
            };
            var category2 = new EntityCategory
            {
                Id = category_2,
                Name = "Category2",
                Description = "Category two"
            };
            
            context.SetCollectionAsDbSet(new List<EntityCategory> { category1, category2});            
            return context;
        }
        private CategoryProvider CreateProvider(FakeDataContext context = null)
        {
            context = context ?? CreateDataContext();
            var repository = new CommonRepository(context);
            return new CategoryProvider(repository);
        }       
        public ModelCategory CreateNewCategory(string name)
        {
            return new ModelCategory
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = $"{name} descripion"
            };
        }
        #endregion  

        [Test]
        public void Get_WithoutCondition_CountReturnedRecordsEqualsTwo()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 2;
            var returnedRecordsCount = provider.Get().Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void GetByName_PassParameterEqualsCategory1_CountReturnedRecordsEqualsOne()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 1;
            var returnedRecordsCount = provider.GetByName("Category1").Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void GetByName_PassEmptyStringAsParameter_CountReturnedRecordsEqualsZero()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 0;
            var returnedRecordsCount = provider.GetByName(string.Empty).Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void GetById_PassExistingId_ReturnedRecordNameEqualsProduct1()
        {
            var provider = CreateProvider();
            var returnedRecord = provider.GetById(category_1);

            Assert.AreEqual("Category1", returnedRecord.Name);
        }
        [Test]
        public void GetById_PassNotExistingId_ReturnedRecordsIsNull()
        {
            var provider = CreateProvider();
            var newGuid = Guid.NewGuid();
            var returnedRecord = provider.GetById(newGuid);

            Assert.IsNull(returnedRecord);
        }
        [Test]
        public void Set_PassedCollectionWithTwoItems_CountGoodRecordsInContextEqualsFour()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newCategoriesCollection = new List<ModelCategory> { CreateNewCategory("NewCategory1"), CreateNewCategory("NewCategory2") };
            var expectedRecordsCount = 4;

            provider.Set(newCategoriesCollection);

            var recordsCountInContext = context.Categories.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedExistingProductWithOtherName_CountGoodRecordsInContextEqualsTwo()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var category = new ModelCategory { Id = category_1, Name = "ChangedName"};
            var expectedRecordsCount = 2;

            provider.Set(category);

            var recordsCountInContext = context.Categories.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
    }
}
