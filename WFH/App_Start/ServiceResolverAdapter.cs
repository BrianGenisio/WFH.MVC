using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WFH.App_Start
{
    public class ServiceResolverAdapter : IDependencyResolver, IDependencyScope
    {
        private readonly System.Web.Mvc.IDependencyResolver dependencyResolver;

        public ServiceResolverAdapter(System.Web.Mvc.IDependencyResolver dependencyResolver)
        {
            if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
            this.dependencyResolver = dependencyResolver;
        }

        public object GetService(Type serviceType)
        {
            return dependencyResolver.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return dependencyResolver.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
        }
    }

    public static class ServiceResolverExtensions
    {
        public static IDependencyResolver ToServiceResolver(this System.Web.Mvc.IDependencyResolver dependencyResolver)
        {
            return new ServiceResolverAdapter(dependencyResolver);
        }
    }
}