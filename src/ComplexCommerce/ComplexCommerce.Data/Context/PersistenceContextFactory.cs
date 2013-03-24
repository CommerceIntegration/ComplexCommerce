using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplexCommerce.Shared.DI;

namespace ComplexCommerce.Data.Context
{
    public class PersistenceContextFactory : IPersistenceContextFactory
    {
        public PersistenceContextFactory(
            Type contextType, 
            IDependencyInjectionContainer container
            )
        {
            if (contextType == null)
                throw new ArgumentNullException("contextType");
            if (container == null)
                throw new ArgumentNullException("container");
            this.contextType = contextType;
            this.container = container;
        }

        private readonly Type contextType = null;
        private readonly IDependencyInjectionContainer container;

        #region IPersistenceContextFactory Members

        public IPersistenceContext GetContext()
        {
            return container.Resolve(contextType) as IPersistenceContext;
        }

        #endregion
    }
}
