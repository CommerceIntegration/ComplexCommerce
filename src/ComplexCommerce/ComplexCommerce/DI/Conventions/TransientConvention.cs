using System;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;

namespace ComplexCommerce.DI.Conventions
{
    internal class TransientConvention
         : IRegistrationConvention
    {
        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            registry.For(type).LifecycleIs(StructureMap.InstanceScope.Transient);
        }

        #endregion
    }
}