using System.Data.Entity;
using PurchasePool.Data.EF.Entities;

namespace PurchasePool.Data.EF.Interfaces
{
    public interface IMainContainer: IDataContextEF
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Good> Goods { get; set; }
        DbSet<CategoryGoodReference> CategoryGoodReferences { get; set; }
    }
}
