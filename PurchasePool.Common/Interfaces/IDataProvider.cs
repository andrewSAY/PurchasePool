using System;
using System.Collections.Generic;

namespace PurchasePool.Common.Interfaces
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> Get(Func<T, bool> condition);
        IEnumerable<T> Get();
        IEnumerable<T> Set(IEnumerable<T> collection);
        IEnumerable<T> Set(T singleton);
        void Commit();
    }
}
