using System;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using Csla.Web.Mvc;
using ComplexCommerce.Web;
using ComplexCommerce.Web.Mvc.DI;
using ComplexCommerce.Web.Mvc.Routing;
using ComplexCommerce.DI.Conventions;
using ComplexCommerce.Web.Mvc.Globalization;
using ComplexCommerce.Web.Mvc.Globalization.ValidationMessages;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Presentation (UI) layer
    internal class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<IContextUtilities>();
                scan.AssemblyContainingType<IRouteUtilities>();
                scan.WithDefaultConventions();
            });

            //this.For<IModelBinder>()
            //    .Use<CslaModelBinder>();


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


            // Localization
            this.Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssemblyContainingType<IValidationMessageProvider>();
                scan.AddAllTypesOf<IValidationMessageProvider>();
            });

            this.For<IValidationMessageProvider>().Use<CompositeValidationMessageProvider>()
                .EnumerableOf<IValidationMessageProvider>().Contains(x =>
                {
                    x.Type<ValidationMessageProvider>();
                    x.Type<MvcValidationMessageProvider>();
                    x.Type<DataAnnotationDefaultStrings>();
                });
        }
    }
}