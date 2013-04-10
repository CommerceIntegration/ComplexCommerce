using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComplexCommerce.Shared.DI;
using StructureMap;

namespace ComplexCommerce.DI
{
    public class SMDependencyResolver
        : IDependencyResolver
    {
        public SMDependencyResolver(
            IDependencyInjectionContainer container
            )
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
        }

        private readonly IDependencyInjectionContainer container;

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetInstances(serviceType) as IEnumerable<object>;
        }

        #endregion
    }
}