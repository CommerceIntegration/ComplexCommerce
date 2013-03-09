using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
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

    internal class SingletonConvention<TPluginFamily> : IRegistrationConvention
    {
        public void Process(Type type, Registry registry)
        {
            if (!typeof(TPluginFamily).IsAssignableFrom(type))
                return;

            registry.For(typeof(TPluginFamily)).Singleton().Use(type);
        }
    }
}