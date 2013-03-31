using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Web;
using ComplexCommerce.Web.Mvc.DI;
using ComplexCommerce.Web.Mvc.Routing;
using ComplexCommerce.DI.Conventions;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Presentation (UI) layer
    internal class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
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


            // Fix for controllers - need to ensure they are httpContext scoped or
            // there will be problems.
            //http://code-inside.de/blog/2011/01/18/fix-a-single-instance-of-controller-foocontroller-cannot-be-used-to-handle-multiple-requests-mvc3/
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<ComplexCommerce.Web.Mvc.Controllers.SystemController>();
                scan.AddAllTypesOf<IController>();
                scan.Include(t => typeof(IController).IsAssignableFrom(t));
                scan.Convention<TransientConvention>();
            });

            this.For<IControllerFactory>()
                .Singleton()
                .Use<InjectableControllerFactory>();
        }
    }
}