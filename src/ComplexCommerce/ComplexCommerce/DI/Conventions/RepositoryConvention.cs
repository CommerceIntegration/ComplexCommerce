using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace ComplexCommerce.DI.Conventions
{
    internal class RepositoryConvention : IRegistrationConvention
    {

        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            // only interested in non abstract concrete types that have a matching named interface and ending in Repository           
            if (type.IsAbstract || !type.IsClass || type.GetInterface("I" + type.Name) == null || !type.Name.EndsWith("Repository"))
                return;

            // Get interface and register (can use AddType overload method to create named types
            Type interfaceType = type.GetInterface("I" + type.Name);
            registry.AddType(interfaceType, type);

            // We create a new Setter Injection Policy that
            // forces StructureMap to inject all public properties
            // where the Property Type name ends with 'Repository'
            registry.SetAllProperties(p =>
            {
                p.TypeMatches(t => t.Name.EndsWith("Repository"));
            });
        }

        #endregion
    }
}