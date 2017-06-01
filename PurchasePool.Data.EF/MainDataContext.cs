using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchasePool.Data.EF.Entities;
using PurchasePool.Data.EF.Interfaces;

namespace PurchasePool.Data.EF
{
    public class MainDataContext : DbContext, IMainContainer
    {
        public MainDataContext(): base("MainData")
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<CategoryGoodReference> CategoryGoodReferences { get; set; }
        

    }
}
