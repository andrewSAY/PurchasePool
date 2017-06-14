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
    [TestFixture]
    public class ProductProviderTests
    {
        #region TestInfo
        private Guid product_1 = Guid.NewGuid();
        private Guid product_2 = Guid.NewGuid();
        private Guid category_1 = Guid.NewGuid();
        private Guid category_2 = Guid.NewGuid();
        #endregion
        #region preparing methods
        private FakeDataContext CreateDataContext()
        {
            var context = new FakeDataContext();
            var product1 = new Good
            {
                Id = product_1,
                Name = "Product1",
                Description = "Product one"                
            };
            var product2 = new Good
            {
                Id = product_2,
                Name = "Product2",
                Description = "Product two",                
            };

            var category1 = new EntityCategory
            {
                Id = category_1,
                Name = "Category1",
                Description = "Category one"                
            };
            var reference = new CategoryGoodReference
            {
                Id = Guid.NewGuid(),
                Category = category1, 
                Good = product1
            };

            context.SetCollectionAsDbSet(new List<Good> { product1, product2 });
            context.SetCollectionAsDbSet(new List<EntityCategory> { category1});
            context.SetCollectionAsDbSet(new List<CategoryGoodReference> { reference });
            return context;
        }
        private ProductProvider CreateProvider(FakeDataContext context = null)
        {
            context = context ?? CreateDataContext();
            var repository = new CommonRepository(context);
            return new ProductProvider(repository);
        }
        public Product CreateNewProduct(string name)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = $"{name} descripion",
                Categories = new List<ModelCategory>()
            };
        }
        public ModelCategory CreateCategory(Guid Id)
        {
            return new ModelCategory
            {
                Id = Id,
                Name = "Category1",
                Description = "Category one"
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
        public void GetByName_PassParameterEqualsProduct1_CountReturnedRecordsEqualsOne()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 1;
            var returnedRecordsCount = provider.GetByName("Product1").Count();

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
            var returnedRecord = provider.GetById(product_1);

            Assert.AreEqual("Product1", returnedRecord.Name);
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
        public void GetInCategories_PassExistingCategory_CountReturnedRecordsEqualsOne()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 1;
            var category = CreateCategory(category_1);
            var returnedRecordsCount = provider.GetInCategories(new List<ModelCategory> { category }).Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void GetInCategories_PassExistingCategory_ReturnedRecordNameEqualsProduct1()
        {
            var provider = CreateProvider();           
            var category = CreateCategory(category_1);
            var returnedRecord = provider.GetInCategories(new List<ModelCategory> { category }).FirstOrDefault();

            Assert.AreEqual("Product1", returnedRecord.Name);
        }
        [Test]
        public void GetInCategories_PassNotExistingCategory_CountReturnedRecordsEqualsZero()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 0;
            var newId = Guid.NewGuid();
            var category = CreateCategory(newId);
            var returnedRecordsCount = provider.GetInCategories(new List<ModelCategory> { category }).Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void GetInCategories_PassExistingCategory_ReturnedRecordHasCategoryWithNameCategory1()
        {
            var provider = CreateProvider();
            var newId = Guid.NewGuid();
            var category = CreateCategory(category_1);
            var returnedRecord = provider.GetInCategories(new List<ModelCategory> { category }).FirstOrDefault();

            Assert.AreEqual("Category1", returnedRecord.Categories.FirstOrDefault().Name);
        }
        [Test]
        public void Set_PassedCollectionWithTwoItems_CountGoodRecordsInContextEqualsFour()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newProductsCollection = new List<Product> { CreateNewProduct("NewProduct1"), CreateNewProduct("NewProduct2") };
            var expectedRecordsCount = 4; 

            provider.Set(newProductsCollection);

            var recordsCountInContext = context.Goods.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedCollectionWithOneItemAndOneCategory_CountGoodCatgoryReferenceRecordsInContextEqualsTwo()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newProduct = CreateNewProduct("NewProduct1");
            var category = new ModelCategory { Id = category_1 };
            newProduct.Categories.Add(category);
            var expectedRecordsCount = 2;

            provider.Set(newProduct);

            var recordsCountInContext = context.CategoryGoodReferences.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedCollectionWithOneItemAndOneCategory_CountCategoriesInContextEqualsOne()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newProduct = CreateNewProduct("NewProduct1");
            var category = new ModelCategory { Id = category_1 };
            newProduct.Categories.Add(category);
            var expectedRecordsCount = 1;

            provider.Set(newProduct);

            var recordsCountInContext = context.Categories.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedExistingProductWithOtherName_CountGoodRecordsInContextEqualsTwo()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var product = new Product { Id = product_1, Name = "ChangedName", Categories = new List<ModelCategory>() };
            var expectedRecordsCount = 2;

            provider.Set(product);

            var recordsCountInContext = context.Goods.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedExistingProductWithoutCategories_CountReferenceRecordsInContextEqualsZer()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var product = new Product { Id = product_1, Categories = new List<ModelCategory>() };
            var expectedRecordsCount = 0;

            provider.Set(product);

            var recordsCountInContext = context.CategoryGoodReferences.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
    }
}
