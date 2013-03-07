using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Runtime.Caching;
using StructureMap;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Data;
using ComplexCommerce.Data.Context;
using ComplexCommerce.DI.Conventions;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;
using ComplexCommerce.Business.Caching;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Data layer (server side only)

    // Note that dependencies to CSLA objects should be injected as
    // properties (setter injection) rather than injecting through a
    // constructor.
    public class CslaDataPortalRegistry : Registry
    {
        public CslaDataPortalRegistry(IDependencyInjectionContainer container)
        {
            
            // Get Configuration Settings to Inject
            string persistenceDetailsTypeName = ConfigurationManager.AppSettings["PersistenceDetailsType"];
            string persistenceContextTypeName = ConfigurationManager.AppSettings["PersistenceContextType"];
            string connectionString = ConfigurationManager.ConnectionStrings["ComplexCommerce"].ConnectionString;


            Type persistenceDetailsType = Type.GetType(persistenceDetailsTypeName);
            var persistenceDetails = (IPersistenceDetails)Activator.CreateInstance(
                    persistenceDetailsType,
                    new object[] { connectionString }
                    );

            Type persistenceContextType = Type.GetType(persistenceContextTypeName);

            // Setup the convention for injecting repositories
            this.Scan(scan =>
            {
                scan.AssemblyContainingType<IPersistenceDetails>();
                scan.AssemblyContainingType(persistenceDetailsType);
                scan.Convention<RepositoryConvention>();
            });

            // Setup the persistanceLocation injection for the repositories.
            this.For<IPersistenceDetails>()
                .Singleton()
                .Use(persistenceDetails);

            // Setup the connection string injection
            this.Scan(scan =>
            {
                scan.AssemblyContainingType(persistenceContextType);
                scan.With(new ConnectionStringConvention(connectionString));
            });

            // Setup the persistanceLocation injection for the repositories.
            this.For<IPersistenceContextFactory>()
                .Singleton()
                .Use(new PersistenceContextFactory(persistenceContextTypeName, container));


            // Setup the initializer for the repositories.
            this.Scan(scan =>
            {
                scan.AssemblyContainingType(persistenceContextType);
                scan.Include(t => typeof(IDataInitializer).IsAssignableFrom(t));
                scan.WithDefaultConventions();
                scan.SingleImplementationsOfInterface();
                scan.Convention<SingletonConvention>();
            });

            // Setup the object cache
            this.For<ObjectCache>()
                .Use(x => MemoryCache.Default);

            this.For<ICachePolicy>()
                .Use(new CachePolicy { AbsoluteExpiration = TimeSpan.FromMinutes(5) });

            // Setup the Micro Cache
            this.For(typeof(IMicroObjectCache<>))
                .Use(typeof(MicroObjectCache<>));


            this.SetAllProperties(p =>
            {
                p.TypeMatches(t => t == typeof(IMicroObjectCache<RouteUrlPageList>));
            });

            this.SetAllProperties(p =>
            {
                p.TypeMatches(t => t == typeof(IMicroObjectCache<RouteUrlProductList>));
            });



            // We create a new Setter Injection Policy that
            // forces StructureMap to inject all public properties
            // where the Property Type equals 'IPersistenceContextFactory'
            this.SetAllProperties(p =>
            {
                p.TypeMatches(t => t == typeof(IPersistenceContextFactory));
            });

        }

    }
}