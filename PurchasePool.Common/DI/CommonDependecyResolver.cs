using PurchasePool.Common.Interfaces.DI;
using Ninject;
using Ninject.Web.Common;

namespace PurchasePool.Common.DI
{
    public class CommonDependecyResolver : ICommonDependecyResolver
    {
        private readonly IKernel _kernel;
        public CommonDependecyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }
        public void Bind<T>()
        {
            _kernel.Bind<T>();
        }

        public void BindTo<T, TResolve>() where TResolve : T
        {
            _kernel.Bind<T>().To<TResolve>();
        }

        public void BindToInScopeRequest<T, TResolve>() where TResolve : T
        {
            _kernel.Bind<T>().To<TResolve>().InRequestScope();
        }
    }
}
