using System;
using System.Data.Entity.Migrations;

namespace PurchasePool.Data.EF.Migrations
{
    class MainConfiguration: DbMigrationsConfiguration<MainDataContext>
    {
        public MainConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
