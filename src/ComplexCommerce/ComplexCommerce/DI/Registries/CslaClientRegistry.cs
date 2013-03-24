using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using ComplexCommerce.Business;
using ComplexCommerce.Business.Context;

namespace ComplexCommerce.DI.Registries
{
    // Register Dependencies for Business layer (client side only)
    internal class CslaClientRegistry : Registry
    {
        public CslaClientRegistry()
        {
            //this.For<ITenant>()
            //    .Use(x => ApplicationContext.CurrentTenant);
        }
    }
}