using System;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;

namespace ComplexCommerce.DI.Conventions
{
    internal class HttpContextScopedConvention
        : IRegistrationConvention
    {
        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            registry.For(type).HttpContextScoped();
        }

        #endregion
    }
}