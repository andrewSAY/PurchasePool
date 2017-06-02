using System.Data.Entity;
using PurchasePool.Data.EF.Migrations;

namespace PurchasePool.Data.EF.Initializers
{
    class NormalInitializer: MigrateDatabaseToLatestVersion<MainDataContext, MainConfiguration>
    {
    }
}
