using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using StructureMap;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Data;
using ComplexCommerce.Data.Context;
using ComplexCommerce.DI.Conventions;

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
            //string dalInitializerType = ConfigurationManager.AppSettings["DalInitializerType"];
            string connectionString = ConfigurationManager.ConnectionStrings["MainlandCommerce"].ConnectionString;


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

            //this.For<Mainland.Commerce.Dal.IPersistenceDetails>()
            //    .Use<Mainland.Commerce.DalEf.PersistenceDetails>();

            //// Setup the persistanceDetails injection for the repositories.
            //this.Scan(scan =>
            //{
            //    //scan.AssemblyContainingType<Mainland.Commerce.Dal.IPersistenceDetails>();
            //    scan.AssemblyContainingType(persistenceContextType);
            //    scan.Include(t => typeof(Mainland.Commerce.Dal.IPersistenceDetails).IsAssignableFrom(t));
            //    scan.WithDefaultConventions();
            //    scan.SingleImplementationsOfInterface();
            //    //scan.Convention<SingletonConvention>();
            //    //scan.Convention<HttpContextScopedConvention>(); // NOTE: we won't want singleton because we need to inject multiple data layers
            //});

            //this.For<Mainland.Commerce.Dal.IPersistenceContext>()
            //    .Use<Mainland.Commerce.DalEf.PersistenceContext>();


            // Setup the connection string injection
            this.Scan(scan =>
            {
                scan.AssemblyContainingType(persistenceContextType);
                scan.With(new ConnectionStringConvention(connectionString));
            });

            //// Setup the persistanceLocation injection for the repositories.
            //this.Scan(scan =>
            //{
            //    scan.Assembly("Mainland.Commerce.DalEf");
            //    scan.Include(t => t.Name.EndsWith("Repository"));
            //    scan.With(new PersistanceConvention(persistanceLocation));
            //});

            // Setup the persistence context factory for BOs
            //this.Scan(scan =>
            //{
            //    scan.AssemblyContainingType(persistanceDetailsType);
            //    scan.Include(t => typeof(Mainland.Commerce.Dal.IPersistenceContextFactory).IsAssignableFrom(t));
            //    scan.WithDefaultConventions();
            //    scan.SingleImplementationsOfInterface();
            //    scan.With(new PersistenceContextFactoryConvention(
            //});

            //var contextFactory = new PersistenceContextFactory(persistenceContextTypeName, container);

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


            // We create a new Setter Injection Policy that
            // forces StructureMap to inject all public properties
            // where the Property Type name equals 'IPersistenceContextFactory'
            this.SetAllProperties(p =>
            {
                p.TypeMatches(t => t.Name == "IPersistenceContextFactory");
            });

        }

    }
}