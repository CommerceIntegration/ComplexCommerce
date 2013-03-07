using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Csla.DI;
using ComplexCommerce.Web;
using ComplexCommerce.Web.Mvc;
using ComplexCommerce.Web.Mvc.Routing;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Presentation (UI) layer
    public class PresentationRegistry : Registry
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

        }
    }
}