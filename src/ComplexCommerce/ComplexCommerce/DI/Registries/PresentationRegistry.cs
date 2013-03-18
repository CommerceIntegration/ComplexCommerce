using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Csla.DI;
using ComplexCommerce.Web;
using ComplexCommerce.Web.Mvc;
using ComplexCommerce.Web.Mvc.Routing;
using ComplexCommerce.DI.Conventions;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Presentation (UI) layer
    internal class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            //this.For<IDependencyInjectionContainer>()
            //    .Use<StructureMapContainer>();

            //this.For<IResolver>()
            //    .Use<StructureMapResolver>();

            this.Scan(scan =>
            {
                scan.AssemblyContainingType<IContextUtilities>();
                scan.WithDefaultConventions();
            });

            this.Scan(scan =>
            {
                scan.AssemblyContainingType<IRouteUtilities>();
                scan.WithDefaultConventions();
            });

            //this.For<IModelBinder>()
            //    .Use<LocalizedModelBinder>();


            //this.Scan(scan =>
            //{
            //    scan.TheCallingAssembly();
            //    scan.
            //});

            //this.For<IController>().Transient();


            // Fix for controllers - need to ensure they are httpContext scoped or
            // there will be problems.
            //http://code-inside.de/blog/2011/01/18/fix-a-single-instance-of-controller-foocontroller-cannot-be-used-to-handle-multiple-requests-mvc3/
            //var controllerTypes =
            //    from t in Assembly.GetExecutingAssembly().GetTypes()
            //    where typeof(IController).IsAssignableFrom(t)
            //    select t;

            //foreach (Type t in controllerTypes)
            //    this.For(t).LifecycleIs(StructureMap.InstanceScope.Transient);

            //this.Scan(scan =>
            //{
            //    scan.TheCallingAssembly();
            //    scan.AddAllTypesOf<IController>();
            //    scan.Include(t => typeof(IController).IsAssignableFrom(t));
            //    scan.Convention<TransientConvention>();
            //});


            // Fix for controllers - need to ensure they are httpContext scoped or
            // there will be problems.
            //http://code-inside.de/blog/2011/01/18/fix-a-single-instance-of-controller-foocontroller-cannot-be-used-to-handle-multiple-requests-mvc3/
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AddAllTypesOf<IController>();
                scan.Include(t => typeof(IController).IsAssignableFrom(t));
                scan.Convention<TransientConvention>();
            });
        }
    }
}