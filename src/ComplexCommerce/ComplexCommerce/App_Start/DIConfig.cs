using System;
using System.Web.Mvc;
using StructureMap;
using ComplexCommerce.DI;
using ComplexCommerce.DI.Registries;
using ComplexCommerce.Csla.DI;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Web.Mvc.DI;
using ComplexCommerce.Data;

namespace ComplexCommerce
{
    public class DIConfig
    {
        public static IDependencyInjectionContainer Register()
        {
            // Create the DI container
            var container = new Container();
            
            // Setup configuration of DI
            container.Configure(r => r.AddRegistry(new StructureMapRegistry(container)));
            container.Configure(r => r.AddRegistry<MvcSiteMapProviderRegistry>());
            container.Configure(r => r.AddRegistry<PresentationRegistry>());
            container.Configure(r => r.AddRegistry<CslaClientRegistry>());
            container.Configure(r => r.AddRegistry<CslaDataPortalRegistry>());
            container.Configure(r => r.AddRegistry<CslaGlobalRegistry>());

               // Verify the configuration
            // TODO: Move this into a test
            //container.AssertConfigurationIsValid();

            // Reconfigure MVC to use StructureMap for DI
            ControllerBuilder.Current.SetControllerFactory(container.GetInstance<IControllerFactory>());

            // Setup the static member so CSLA has access to resolve its dependencies using
            // the rules specified here
            ComplexCommerce.Csla.DI.IoC.Container = container.GetInstance<IResolver>();

            // Return our DI container instance
            return container.GetInstance<IDependencyInjectionContainer>();
        }
    }
}