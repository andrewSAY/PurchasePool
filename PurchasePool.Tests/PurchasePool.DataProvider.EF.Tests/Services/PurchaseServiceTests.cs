using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PurchasePool.DataProvider.EF.Services;
using PurchasePool.Common.Models;

namespace PurchasePool.Web.Tests.PurchasePool.DataProvider.EF.Tests.Services
{
    [TestFixture]
    public class PurchaseServiceTests
    {        
        [Test]
        public void Add_WhenCalled_AffectedRowsCountGrows()
        {
            var mock = new FakeDataContext();
            var serviceUnderTest = new PurchaseService(mock);
            var affectedRowsBeforeCall = mock.changesCount;

            serviceUnderTest.Add(new Purchase());

            var affectedRowsAfterCall = mock.changesCount;

            Assert.Greater(affectedRowsAfterCall, affectedRowsBeforeCall);
        }
    }
}
