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
        private readonly IDependencyInjectionContainer container;
        private readonly string contextTypeName;
        private Type contextType = null;

        // NOTE: The container injected here should be a nested container so dispose gets called
        public PersistenceContextFactory(string contextTypeName, IDependencyInjectionContainer container)
        {
            if (string.IsNullOrEmpty(contextTypeName))
                throw new ArgumentNullException("contextTypeName");
            if (container == null)
                throw new ArgumentNullException("container");
            this.contextTypeName = contextTypeName;
            this.container = container;
        }

        #region IPersistenceContextFactory Members

        public IPersistenceContext GetContext()
        {
            if (contextType == null)
            {
                contextType = Type.GetType(contextTypeName);

                if (contextType == null)
                    throw new ArgumentException(string.Format("Type {0} could not be found", contextTypeName));
            }
            return container.Resolve(contextType) as IPersistenceContext;
            //return (IPersistenceContext)Activator.CreateInstance(contextType);
        }

        #endregion
    }
}
