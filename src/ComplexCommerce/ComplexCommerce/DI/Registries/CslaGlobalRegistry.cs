using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies required by both Csla Client and Server

    // Note that dependencies to CSLA objects should be injected as
    // properties (setter injection) rather than injecting through a
    // constructor.
    public class CslaGlobalRegistry : Registry
    {
        public CslaGlobalRegistry()
        {
            this.Scan(scan =>
            {
                scan.AssemblyContainingType<ApplicationContext>();
                scan.WithDefaultConventions();
            });

        }
    }
}