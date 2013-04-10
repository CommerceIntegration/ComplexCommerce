using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Shared.DI
{
    public interface IDependencyInjectionContainer
    {
        object Resolve(Type type);

        // TODO: Remove when taking out IDependencyResolver
        IEnumerable<object> GetInstances(Type type);
    }

    public static class DependencyInjectionContainerExtensions
    {
        public static T Resolve<T>(this IDependencyInjectionContainer container)
        {
            return (T)container.Resolve(typeof(T));
        }
    }
}
