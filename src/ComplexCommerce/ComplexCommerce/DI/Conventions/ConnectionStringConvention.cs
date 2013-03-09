using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Graph;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace ComplexCommerce.DI.Conventions
{
    internal class ConnectionStringConvention : IRegistrationConvention
    {
        private readonly string connectionString;

        public ConnectionStringConvention(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            this.connectionString = connectionString;
        }

        #region IRegistrationConvention Members

        public void Process(Type type, Registry registry)
        {
            //if (!typeof(IPersistenceDetails).IsAssignableFrom(type))
            //    return;
            if (!type.Name.Equals("PersistenceDetails"))
                return;

            IConfiguredInstance ctor = new ConstructorInstance(type);
            ctor.SetValue("location", this.connectionString);
            registry.For(type.BaseType).HttpContextScoped().Add(ctor);
        }

        #endregion
    }
}