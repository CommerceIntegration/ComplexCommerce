using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using ComplexCommerce.DI;
using ComplexCommerce.DI.Registries;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Web.Mvc.DI;
using ComplexCommerce.Data;

namespace ComplexCommerce
{
    public class DIConfig
    {
        public static IDependencyInjectionContainer RegisterDependencyInjectionContainer()
        {
            // Create the DI container
            var container = new Container();
            var resolver = new StructureMapResolver(container);
            var diContainer = new StructureMapContainer(container);

            // Setup the static member so CSLA has access to resolve its dependencies using
            // the rules specified here
            ComplexCommerce.Csla.DI.IoC.Container = resolver;

            //http://codebetter.com/jeremymiller/2010/02/10/nested-containers-in-structuremap-2-6-1/
            //var disposableContainer = container.GetNestedContainer();

            // Setup configuration of DI
            container.Configure(r => r.AddRegistry<PresentationRegistry>());
            container.Configure(r => r.AddRegistry<CslaClientRegistry>());
            container.Configure(r => r.AddRegistry(new CslaDataPortalRegistry(diContainer)));
            container.Configure(r => r.AddRegistry<CslaGlobalRegistry>());

               // Verify the configuration
            // TODO: Move this into a test
            //container.AssertConfigurationIsValid();

            // Reconfigure MVC to use StructureMap for DI
            ControllerBuilder.Current.SetControllerFactory(
                new InjectableControllerFactory(diContainer)
            );

            // Configure AutoMapper - all work done in constructor
            container.GetInstance<IDataInitializer>();

            // Return our DI container instance
            return diContainer;
        }
    }
}