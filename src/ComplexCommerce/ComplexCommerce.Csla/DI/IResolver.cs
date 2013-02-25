using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Csla.DI
{
    public interface IResolver
    {
        void Inject(object instance);
    }
}
