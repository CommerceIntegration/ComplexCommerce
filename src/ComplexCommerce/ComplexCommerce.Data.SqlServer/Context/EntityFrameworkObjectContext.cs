using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Data.Context;
using ComplexCommerce.Data.SqlServer.Model;

namespace ComplexCommerce.Data.SqlServer.Context
{
    public class EntityFrameworkObjectContext : IEntityFrameworkObjectContext
    {
        public IObjectContextManager ContextManager { get; private set; }

        public EntityFrameworkObjectContext(IPersistenceDetails persistenceDetails)
        {
            ContextManager = ObjectContextManager.GetManager(persistenceDetails.Location);
        }

        public void Dispose()
        {
            ContextManager.Dispose();
            ContextManager = null;
        }
    }
}
