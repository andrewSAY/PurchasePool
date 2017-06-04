
namespace PurchasePool.Common.Interfaces.DI
{
    public interface IExportResolver
    {
        ICommonDependecyResolver Resolver { get; set; }
        void Resolve();
    }
}
