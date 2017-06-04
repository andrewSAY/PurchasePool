using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PurchasePool.DataProvider.EF;
using PurchasePool.Common.Models;
using PurchasePool.Tests.Fakes;
using PurchasePool.DataProvider.EF.Repositories;

namespace PurchasePool.Web.Tests.PurchasePool.DataProvider.EF.Tests
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
            var product1 = new Product
            {
                Id = product_1,
                Name = "Product1",
                Description = "Product one",
                Categories = new List<Category>()
            };
            var product2 = new Product
            {
                Id = product_2,
                Name = "Product2",
                Description = "Product two",
                Categories = new List<Category>()
            };

            var category1 = new Category
            {
                Id = category_1,
                Name = "Category1",
                Description = "Category one",
                Products = new List<Product>()
            };
            category1.Products.ToList().Add(product1);
            product1.Categories.ToList().Add(category1);

            context.SetCollectionAsDbSet(new List<Product> { product1, product2 });
            context.SetCollectionAsDbSet(new List<Category> { category1});
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
                Categories = new List<Category>()
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
        public void Get_WithConditionNameEqualsProduct1_CountReturnedRecordsEqualsOne()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 1;
            var condition = new Func<Product, bool>(p => p.Name == "Product1");
            var returnedRecordsCount = provider.Get(condition).Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void Get_WithConditionNameEqualsProduct1_ReturnedRecordHasNameProduct1()
        {
            var provider = CreateProvider();            
            var condition = new Func<Product, bool>(p => p.Name == "Product1");
            var returnedRecord = provider.Get(condition).FirstOrDefault();

            Assert.AreEqual("Product1", returnedRecord.Name);
        }       
        [Test]
        public void Get_WithConditionProductHasCategoryWitnNameCategory1_CountReturnedRecordsEqualsOne()
        {
            var provider = CreateProvider();
            var expectedRecordsCount = 1;
            var condition = new Func<Product, bool>(p => p.Categories.Count(c => c.Name == "Category1") == 1);
            var returnedRecordsCount = provider.Get(condition).Count();

            Assert.AreEqual(expectedRecordsCount, returnedRecordsCount);
        }
        [Test]
        public void Get_WithConditionProductHasCategoryWitnNameCategory1_ReturnedRecordHasHasOnlyOneCategoryWithNameCategory1()
        {
            var provider = CreateProvider();
            var condition = new Func<Product, bool>(p => p.Categories.Count(c => c.Name == "Category1") == 1);
            var returnedRecord = provider.Get(condition).FirstOrDefault();

            Assert.AreEqual("Category1", returnedRecord.Categories.FirstOrDefault().Name);
        }
        [Test]
        public void Set_PassedCollectionWitTwoItems_CountGoodRecordsInContextEqualsFour()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newProductsCollection = new List<Product> { CreateNewProduct("NewProdict1"), CreateNewProduct("NewProdict2") };
            var expectedRecordsCount = 4; 

            provider.Set(newProductsCollection);

            var recordsCountInContext = context.Goods.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
        [Test]
        public void Set_PassedOneItem_CountGoodRecordsInContextEqualsThree()
        {
            var context = CreateDataContext();
            var provider = CreateProvider(context);
            var newProductsCollection = CreateNewProduct("NewProdict1");
            var expectedRecordsCount = 3;

            provider.Set(newProductsCollection);

            var recordsCountInContext = context.Goods.Count();

            Assert.AreEqual(expectedRecordsCount, recordsCountInContext);
        }
    }
}
