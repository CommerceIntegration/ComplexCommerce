using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Context;

namespace ComplexCommerce.Data.SqlServer.Context
{
    public interface IEntityFrameworkObjectContext : IPersistenceContext
    {
        IObjectContextManager ContextManager { get; }
    }
}
