using System;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;

namespace ComplexCommerce.DI.Conventions
{
    internal class SingletonConvention : IRegistrationConvention
    {
        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            registry.For(type).Singleton();
        }

        #endregion
    }
}