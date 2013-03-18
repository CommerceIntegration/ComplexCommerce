using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using ComplexCommerce.Csla.DI;

namespace ComplexCommerce.DI
{
    // Container to pass the instance of the DI container to CSLA for resolving
    // CSLA dependencies
    public class StructureMapResolver :
        IResolver
    {
        private readonly StructureMap.IContainer container;

        public StructureMapResolver(StructureMap.IContainer container)
        {
            this.container = container;
        }

        #region IResolver Members

        public void Inject(object instance)
        {
            container.BuildUp(instance);
        }

        #endregion
    }
}