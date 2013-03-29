using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Entity.Model;

namespace ComplexCommerce.Data.Entity.Context
{
    public interface IObjectContextManager 
        : IDisposable
    {
        IComplexCommerce ObjectContext { get; }
    }
}
