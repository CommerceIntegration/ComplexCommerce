using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Data.Context
{
    public interface IPersistenceContextFactory
    {
        IPersistenceContext GetContext();
    }
}
