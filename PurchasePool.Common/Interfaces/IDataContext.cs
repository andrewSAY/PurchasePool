using System.Threading.Tasks;

namespace PurchasePool.Common.Interfaces
{
    public interface IDataContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
