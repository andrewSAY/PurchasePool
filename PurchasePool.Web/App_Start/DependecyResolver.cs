using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ninject;
using PurchasePool.Common.DI;
using PurchasePool.Common.Interfaces.DI;

namespace PurchasePool.Web.App_Start
{
    public class DependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public DependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            var commonResolver = new CommonDependecyResolver(kernel);
            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name.StartsWith("PurchasePool"))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsAssignableFrom(typeof(IExportResolver)) && !t.IsInterface)
                .ToList()
                .ForEach(typeResolve => {
                    var resolver = Activator.CreateInstance(typeResolve) as IExportResolver;
                    resolver.Resolve();
                });
        }
    }
}