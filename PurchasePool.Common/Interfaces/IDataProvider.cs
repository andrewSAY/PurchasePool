using System;
using System.Collections.Generic;

namespace PurchasePool.Common.Interfaces
{
    public interface IDataProvider<T>
    {       
        IEnumerable<T> Get();
        void Set(IEnumerable<T> collection);
        void Set(T singleton);        
        void Commit();
    }
}
