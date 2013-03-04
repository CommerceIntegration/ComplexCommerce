using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Business layer (client side only)
    public class CslaClientRegistry : Registry
    {
        public CslaClientRegistry()
        {
            //this.For<ITenant>()
            //    .Use(ApplicationContext.CurrentTenant);
        }
    }
}