using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.SqlServer.Context
{
    public interface IObjectContextManager : IDisposable
    {
        IMainlandCommerce ObjectContext { get; }
    }
}
