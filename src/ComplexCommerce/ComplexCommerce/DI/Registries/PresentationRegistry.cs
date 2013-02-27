using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Shared.DI;
using ComplexCommerce.Csla.DI;
using ComplexCommerce.Web;

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
        }
    }
}