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
        T Resolve<T>();
    }
}
