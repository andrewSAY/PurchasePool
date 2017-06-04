using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;


namespace PurchasePool.Common.Interfaces.DI
{
    public interface ICommonDependecyResolver
    {
        void BindTo<T, TResolve>() where TResolve: T;
        void Bind<T>();
        void BindToInScopeRequest<T, TResolve>() where TResolve: T;
    }
}
