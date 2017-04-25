using System.Collections.Generic;

namespace PurchasePool.Common.ServiceInterfaces
{
    public interface IDataServiceProvider<T>: IService where T: class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);        
    }
}
