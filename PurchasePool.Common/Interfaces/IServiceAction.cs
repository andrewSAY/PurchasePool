using System.Threading.Tasks;

namespace PurchasePool.Common.Interfaces
{
    public interface IServiceActionBase<T>
    {               
    }

    public interface IServiceAction<T>: IServiceActionBase<T>
    {
        T ExecuteAction();
    }

    public interface IServiceActionAsync<T>: IServiceActionBase<T>
    {
        Task<T> ExecuteActionAsync();
    }
}
