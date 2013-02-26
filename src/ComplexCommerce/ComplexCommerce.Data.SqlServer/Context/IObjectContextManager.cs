using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.SqlServer.Model;

namespace ComplexCommerce.Data.SqlServer.Context
{
    public interface IObjectContextManager : IDisposable
    {
        IComplexCommerce ObjectContext { get; }
    }
}
