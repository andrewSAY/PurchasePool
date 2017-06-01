using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PurchasePool.DataProvider.EF.Services;
using PurchasePool.Common.Models;
using PurchasePool.Tests.Fakes;

namespace PurchasePool.Web.Tests.PurchasePool.DataProvider.EF.Tests.Services
{    
    [TestFixture]
    public class PurchaseServiceTests
    {        
        private FakeDataContext PrepareFakeDataContext()
        {
            return new FakeDataContext();
        }

        [Test]
        public void Add_WhenCalled_AffectedRowsCountGrows()
        {
            var mock = new FakeDataContext();
            var serviceUnderTest = new PurchaseService(mock);            

            serviceUnderTest.Add(new Product());

            var affectedRowsAfterCall = mock.ChangesCount;

            Assert.Greater(affectedRowsAfterCall, 0);
        }

        [Test]
        public void Add_Remove_AffectedRowsCountGrows()
        {
            var mock = new FakeDataContext();
            var purchase = new Product();
            mock.Purchases.Add(purchase);

            var serviceUnderTest = new PurchaseService(mock);            

            serviceUnderTest.Remove(purchase);

            var affectedRowsAfterCall = mock.ChangesCount;

            Assert.Greater(affectedRowsAfterCall, 0);
        }

        [Test]
        public void Update_WhenCalled_AffectedRowsCountGrows()
        {
            var mock = new FakeDataContext();
            var purchase = new Product();
            mock.Purchases.Add(purchase);

            var serviceUnderTest = new PurchaseService(mock);            

            serviceUnderTest.Add(purchase);

            var affectedRowsAfterCall = mock.ChangesCount;

            Assert.Greater(affectedRowsAfterCall, 0);
        }

        [Test]
        public void GetAll_ByDefault_ReturnedCountMustBeEqualsTwo()
        {
            var mock = new FakeDataContext();            
            mock.Purchases.Add(new Product());
            mock.Purchases.Add(new Product());

            var serviceUnderTest = new PurchaseService(mock);            

            var returnedCount = serviceUnderTest.GetAll().Count();

            Assert.Equals(returnedCount, 2);
        }

        [Test]        
        public void GetByOperator_WhenCalledWithAllParameters_ReturnedCountMustBeEqualsTwo()
        {
            var mock = new FakeDataContext();
            var operatorFirst = new Category() { Name = "OperatorOne" };
            var operatorTwo = new Category() { Name = "OperatorTwo" };
                        

            var serviceUnderTest = new PurchaseService(mock);

            var returnedCount = serviceUnderTest.GetAll().Count();

            Assert.Equals(returnedCount, 2);
        }
    }
}
